using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataBaseHelper;

namespace DataBaseHelper
{
    public partial class Ado
    {
        public DBHelper db { get { return new DBHelper(); } set { } }
        public List<DbParameter> Param { get; set; }

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
        /// 获取Sql语句查询中的表名参数
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <returns></returns>
        public DbParameter[] GetTableParameters<T>() where T : new()
        {
            T entity = new T();
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
        public DbParameter[] GetParameters<T>(T entity) where T : new()
        {
            Type type = entity.GetType();
            PropertyInfo[] proInfo = type.GetProperties();
            List<DbParameter> paramList = new List<DbParameter>();
            foreach (var p in proInfo)
            {
                var properAttribute = GetFieldAttribute(p);
                if (p.GetValue(entity, null) != null)
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
                if (p.GetValue(entity, null) != null)
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
                Param.Add(par);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
