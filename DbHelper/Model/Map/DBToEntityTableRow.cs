using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace DataBaseHelper
{
    public partial class DbEntityMap : IMapHelper
    {

        /// <summary>
        /// 获取与实体相关的DataTable
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回DataTable类型对象</returns>
        public DataTable GetDataTable<T>(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                Type type = Activator.CreateInstance<T>().GetType();
                DataTable dt = db.GetDataTable(sql, cmdType, param);
                PropertyInfo[] proInfo = type.GetProperties();
                foreach (var p in proInfo)
                {
                    var fieldName = map.GetFieldAttribute(p).FieldName;
                    if (dt.Columns.Contains(fieldName))
                    {
                        dt.Columns[fieldName].ColumnName = p.Name;
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取与实体相关的DataTable
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回DataTable类型对象</returns>
        public DataTable GetDataTable<T>(string sql, DbParameter[] param)
        {
            try
            {
                DataTable dt = GetDataTable<T>(sql, CommandType.Text, param);
                return dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取实体相关的DataRow
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="index">DataTable中的索引行值</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回实体对象类型的DataRow</returns>
        public DataRow GetDataRow<T>(string sql, int index, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                DataTable dt = GetDataTable<T>(sql, cmdType, param);
                return dt.Rows[index];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取实体相关的DataRow：默认索引为第零行
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回实体对象类型的DataRow</returns>
        public DataRow GetDataRow<T>(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                return GetDataRow<T>(sql, 0, cmdType, param);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取实体相关的DataRow：默认索引为第一行，ComandType类型为Text
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回实体对象类型的DataRow</returns>
        public DataRow GetDataRow<T>(string sql, DbParameter[] param)
        {
            try
            {
                return GetDataRow<T>(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
