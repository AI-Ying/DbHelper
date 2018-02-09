using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using DataBaseHelper;

namespace DataBaseHelper
{
    public class MapHelper : IDbHelper
    {
        public DbHelper db { get { return new DbHelper(); } set { } }
        public List<DbParameter> ParamList { get; set; }
        public DbParameter[] Param { get { return ParamList.ToArray(); } }

        /// <summary>
        /// 获取数据库中表的名称
        /// </summary>
        /// <param name="type">实体类类型</param>
        /// <returns></returns>
        public string GetTableName(Type type)
        {
            var tableAttribute = (DbTableAttribute)type.GetCustomAttributes(typeof(DbTableAttribute), true).FirstOrDefault();
            return tableAttribute == null ? type.Name : tableAttribute.TableName;
        }
        /// <summary>
        /// 根据反射，获取实体类的特性
        /// </summary>
        /// <param name="p">属性信息</param>
        /// <returns></returns>
        public DbTableAttribute GetTableAttribute(Type type)
        {
            var tableAttribute = (DbTableAttribute)type.GetCustomAttributes(typeof(DbTableAttribute), true).FirstOrDefault();
            return tableAttribute;
        }
        /// <summary>
        /// 根据反射，获取实体类的属性的特性
        /// </summary>
        /// <param name="p">属性信息</param>
        /// <returns></returns>
        public DbDataFieldAttribute GetFieldAttribute(PropertyInfo p)
        {
            return (DbDataFieldAttribute)p.GetCustomAttributes(typeof(DbDataFieldAttribute), true).FirstOrDefault();
        }
        /// <summary>
        /// 设置类型的空值。
        /// </summary>
        /// <param name="type">System.Data类型</param>
        /// <returns>返回类型空值对象</returns>
        private object DBNull(Type type)
        {
            try
            {
                if (type == typeof(int))
                {
                    return default(int);
                }
                else if (type == typeof(double))
                {
                    return default(double);
                }
                else if (type == typeof(string))
                {
                    return default(string);
                }
                else if (type == typeof(decimal))
                {
                    return default(decimal);
                }
                else if (type == typeof(DateTime))
                {
                    return default(DateTime);
                }
                else if (type == typeof(bool))
                {
                    return default(bool);
                }
                else
                {
                    return default(Guid);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取Sql语句查询中的表名参数
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <returns></returns>
        public DbParameter[] GetTableParameters<T>()
        {
            T entity = default(T);
            List<DbParameter> paramList = new List<DbParameter>();
            var properAttribute = GetTableAttribute(entity.GetType());
            DbParameter param = db.Factory.CreateParameter();
            param.ParameterName = properAttribute.TableName;
            param.Value = properAttribute.TableName;
            paramList.Add(param);
            return paramList.ToArray();
        }
        /// <summary>
        /// 获取Sql语句查询中的字段参数
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <returns></returns>
        public DbParameter[] GetParameters<T>(T entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] proInfo = type.GetProperties();
            List<DbParameter> paramList = new List<DbParameter>();
            foreach (var p in proInfo)
            {
                var properAttribute = GetFieldAttribute(p);
                bool isNull = p.GetValue(entity, null).Equals(DBNull(p.PropertyType));
                if (!isNull)
                {
                    DbParameter param = db.Factory.CreateParameter();
                    param.ParameterName = "@" + properAttribute.FieldName;
                    param.Value = p.GetValue(entity);
                    paramList.Add(param);
                }
            }
            return paramList.ToArray();
        }
        /// <summary>
        /// 获取sql语句查询中的字段参数:用于更新数据中的参数
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns></returns>
        public DbParameter[] GetNewParameters<T>(T entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] proInfo = type.GetProperties();
            List<DbParameter> paramList = new List<DbParameter>();
            foreach (var p in proInfo)
            {
                var properAttribute = GetFieldAttribute(p);
                bool isNull = p.GetValue(entity, null).Equals(DBNull(p.PropertyType));
                if (!isNull)
                {
                    DbParameter param = db.Factory.CreateParameter();
                    param.ParameterName = "@New" + properAttribute.FieldName;
                    param.Value = p.GetValue(entity);
                    paramList.Add(param);
                }
            }
            return paramList.ToArray();
        }
        /// <summary>
        /// 设置sql语句的参数
        /// </summary>
        /// <param name="parString">参数名</param>
        /// <param name="value">参数值</param>
        public void SetParameter(string parString, object value)
        {
            try
            {
                DbParameter par = db.Factory.CreateParameter();
                par.ParameterName = $"@{parString}";
                par.Value = value;
                ParamList.Add(par);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 根据实体，映射DataTable架构
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="type">实体类型</param>
        /// <returns>返回一个DataTable类型</returns>
        public DataTable CreateDataTable<T>(Type type)
        {
            try
            {
                string tableName = GetTableName(type);
                DataTable dt = new DataTable(tableName);
                PropertyInfo[] proInfo = type.GetProperties();
                foreach (var p in proInfo)
                {
                    dt.Columns.Add(p.Name, p.PropertyType);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
