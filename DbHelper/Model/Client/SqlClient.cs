using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataBaseHelper;

namespace DataBaseHelper
{
    public class SqlClient : IDbHelper 
    {
        public DbHelper db { get { return new DbHelper(); } set { } }
        public DbEntityMap dbMap { get { return new DbEntityMap(); } set { } }

        private static List<DbParameter> ParamList = new List<DbParameter>();
        public DbParameter[] Param { get { return ParamList.ToArray(); } }
        public enum Sql
        {
            insert,
            delete,
            update,
            select
        }

        /// <summary>
        /// 设置sql语句的参数
        /// </summary>
        /// <param name="parString">参数名</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string parString, object value)
        {
            try
            {
                DbParameter par = db.Factory.CreateParameter();
                par.ParameterName = $"{parString}";
                par.Value = value;
                ParamList.Add(par);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 使用正则表达式，解析sql语句
        /// </summary>
        /// <param name="sqlStr">sql语句字符串</param>
        /// <returns>返回解析好的类型字符串</returns>
        private string ParsesSql(string sqlStr)
        {
            try
            {
                // 聚合函数和数学函数暂不详论。先讨论select的结果为datatable或list情况
                string pattern = "^(insert|delete|update|select)";
                RegexOptions option = RegexOptions.Compiled | RegexOptions.IgnoreCase;
                MatchCollection coll = Regex.Matches(sqlStr, pattern, option);
                 return coll[0].Value;  
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 根据sql语句查询数据库转成实体集合
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">sql语句类型默认为文本类型</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回一个实体集合</returns>
        public List<T> QueryToList<T>(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                return dbMap.GetReaderList<T>(sql, cmdType, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据sql语句查询数据库转成实体集合
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回一个实体集合</returns>
        public List<T> QueryToList<T>(string sql, DbParameter[] param)
        {
            try
            {
                return QueryToList<T>(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据Sql语句查询数据库转换成对应的DataTable
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">sql语句类型默认为文本类型</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回一个实体对应的DataTable</returns>
        public DataTable QueryToDataTable<T>(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                return dbMap.GetDataTable<T>(sql, cmdType, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据Sql语句查询数据库转换成对应的DataTable
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回一个实体对应的DataTable</returns>
        public DataTable QueryToDataTable<T>(string sql, DbParameter[] param)
        {
            try
            {
                return QueryToDataTable<T>(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据sql语句增、删、改、查数据库
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">sql语句类型默认为文本类型</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回一个对象，可以为DataTable(注意：这里的DataTable没有进行实体映射),操作数据库影响的行数等</returns>
        public object Execute(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                string value = ParsesSql(sql).ToLower();
                if (value.Equals(Sql.insert.ToString()) || value.Equals(Sql.delete.ToString()) || value.Equals(Sql.update.ToString()))
                {
                    return db.ExecuteNonQuery(sql, cmdType, param);
                }
                else if(value.Equals(Sql.select.ToString()))
                {
                    return db.GetDataTable(sql, cmdType, param);
                }
                else
                {
                    return db.ExecuteScalar(sql, cmdType, param);
                }
            }
            catch (Exception e)
            {
                throw e;
            }  
        }
        /// <summary>
        /// 根据sql语句增、删、改、查数据库
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql语句参数</param>
        /// <returns>返回一个对象，可以为DataTable,操作数据库影响的行数等</returns>
        public object Execute(string sql, DbParameter[] param)
        {
            try
            {
                return Execute(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
