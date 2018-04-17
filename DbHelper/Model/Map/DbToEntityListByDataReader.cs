using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace DataBaseHelper
{
    public partial class DbEntityMap : IDbHelper, IDisposable
    {
        public MapHelper map { get { return new MapHelper(); } set { } }
        public DbHelper db { get { return new DbHelper(); } set { } }

        /// <summary>
        /// 根据DataReader的返回结果转化成一个实体类集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <returns>返回一个实体集合</returns>
        public List<T> GetReaderList<T>(DbDataReader reader) 
        {
            try
            {
                List<T> list = new List<T>();               
                while(reader.Read())
                {
                    // 这个实例要放在while里面，这是个引用实例，再循环外修改会更改原来的值。
                    T entity = Activator.CreateInstance<T>();
                    PropertyInfo[] proInfo = entity.GetType().GetProperties();
                    foreach (var p in proInfo)
                    {
                        var fieldAttribute = map.GetFieldAttribute(p);
                        p.SetValue(entity, reader[fieldAttribute.FieldName], null);
                    }
                    list.Add(entity);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 根据DataReader的返回结果转化成一个实体类集合
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个实体类集合List</returns>
        public List<T> GetReaderList<T>(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                DbDataReader reader = db.ExecuteReader(sql, cmdType, param);
                return GetReaderList<T>(reader);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 根据DataReader的返回结果转化成一个实体类集合
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个实体类集合List</returns>
        public List<T> GetReaderList<T>(string sql, DbParameter[] param)
        {
            try
            {
                DbDataReader reader = db.ExecuteReader(sql, CommandType.Text, param);
                return GetReaderList<T>(reader);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Dispose()
        {
            if (db != null & map != null)
            {
                db.Dispose();
                map.Dispose();
            }
        }
    }
}
