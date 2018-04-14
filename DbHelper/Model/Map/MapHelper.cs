using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using DataBaseHelper;

namespace DataBaseHelper
{
    public class MapHelper : IDbHelper
    {
        public DbHelper db { get { return new DbHelper(); } set { } }


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

        #region 设置参数化查询实体映射

        /// <summary>
        /// 设置类型的空值。
        /// </summary>
        /// <param name="type">System.Data类型</param>
        /// <returns>返回类型空值对象</returns>
        public object DBNull(Type type)
        {
            try
            {
                if (type == typeof(int))
                {
                    return default(int);
                }
                else if (type == typeof(float))
                {
                    return default(float);
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
        /// 获取Sql语句查询中的表名参数
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <returns></returns>
        public DbParameter[] GetTableNameParameters<T>()
        {
            try
            {
                T entity = Activator.CreateInstance<T>();
                List<DbParameter> paramList = new List<DbParameter>();
                var properAttribute = GetTableAttribute(entity.GetType());
                DbParameter param = db.Factory.CreateParameter();
                param.ParameterName = $"{properAttribute.TableName}";
                param.Value = properAttribute.TableName;
                paramList.Add(param);
                return paramList.ToArray();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取sql语句查询中的字段参数:用于更新数据中的参数
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns></returns>
        public DbParameter[] GetParameters<T>(T entity, string str)
        {
            try
            {
                PropertyInfo[] proInfo = entity.GetType().GetProperties();
                List<DbParameter> paramList = new List<DbParameter>();
                foreach (var p in proInfo)
                {
                    var properAttribute = GetFieldAttribute(p);
                    bool isNull = object.Equals(p.GetValue(entity, null), DBNull(p.PropertyType));
                    if (!isNull)
                    {
                        DbParameter param = db.Factory.CreateParameter();
                        param.ParameterName = $"@{str}{properAttribute.FieldName}";
                        param.Value = p.GetValue(entity);
                        paramList.Add(param);
                    }
                }
                return paramList.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取Sql语句查询中的字段参数
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <returns></returns>
        public DbParameter[] GetParameters<T>(T entity)
        {
            try
            {
                return GetParameters<T>(entity, string.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }      
        }
        /// <summary>
        /// 获取实体映射成数据库字段的字符串
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式化好的字符串</returns>
        public string GetFieldNameStr<T>(T entity)
        {
            try
            {
                StringBuilder fieldNameStr = new StringBuilder();
                PropertyInfo[] proInfo = entity.GetType().GetProperties();
                foreach (var p in proInfo)
                {
                    var properAttribute = GetFieldAttribute(p);
                    bool isNull = object.Equals(p.GetValue(entity, null), DBNull(p.PropertyType));
                    if (!isNull)
                    {
                        fieldNameStr.Append($"{properAttribute.FieldName},");
                    }
                }
                return fieldNameStr.ToString().Remove(fieldNameStr.ToString().Length - 1, 1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取实体映射成数据库字段参数的字符串
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式化好的字符串</returns>
        public string GetFieldValueStr<T>(T entity)
        {
            try
            {
                StringBuilder fieldValueStr = new StringBuilder();
                PropertyInfo[] proInfo = entity.GetType().GetProperties();
                foreach (var p in proInfo)
                {
                    var properAttribute = GetFieldAttribute(p);
                    bool isNull = object.Equals(p.GetValue(entity, null), DBNull(p.PropertyType));
                    if (!isNull)
                    {
                        fieldValueStr.Append($"@{properAttribute.FieldName},");
                    }
                }
                return fieldValueStr.ToString().Remove(fieldValueStr.ToString().Length - 1, 1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取实体映射查询、更新、删除时的数据库字段参数的字符串
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式好的字符串</returns>
        public string GetSetWhereStr<T>(T entity, string str)
        {
            try
            {
                StringBuilder whereStr = new StringBuilder();
                PropertyInfo[] proInfo = entity.GetType().GetProperties();
                foreach (var p in proInfo)
                {
                    var properAttribute = GetFieldAttribute(p);
                    bool isNull = object.Equals(p.GetValue(entity, null), DBNull(p.PropertyType));
                    if (!isNull)
                    {
                        whereStr.Append($"{properAttribute.FieldName}=@{str}{properAttribute.FieldName} and ");
                    }
                }
                return whereStr.ToString().Remove(whereStr.ToString().Length - 4, 4);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取实体映射查询、更新、删除时的数据库字段参数的字符串（str默认为空）
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式好的字符串</returns>
        public string GetSetWhereStr<T>(T entity)
        {
            try
            {
                return GetSetWhereStr<T>(entity, string.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

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
