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
    public class SqlClient
    {
        public DbHelper db { get { return new DbHelper(); } set { } }
        public MapHelper map { get { return new MapHelper(); } set { } }
        public enum Sql
        {
            select,
            update,
            delete
        }

        /// <summary>
        /// 使用正则表达式，解析sql语句
        /// </summary>
        /// <param name="sqlStr">sql语句字符串</param>
        /// <returns>返回解析好的类型字符串</returns>
        public string ParsesSql(string sqlStr)
        {
            try
            {
                string pattern = "^(select|update|delete)";
                RegexOptions option = RegexOptions.Compiled | RegexOptions.IgnoreCase;
                MatchCollection coll = Regex.Matches(sqlStr, pattern, option);
                 return coll[0].Value;  
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Execute(string sql, CommandType cmdType, DbParameter[] param, out object obj)
        {
            try
            {
                string value = ParsesSql(sql);
                if (value.Equals(Sql.select.ToString()))
                {
                    obj = db.ExecuteScalar(sql, cmdType, param);
                }
                else if (value.Equals(Sql.update.ToString()))
                {
                    obj = db.ExecuteNonQuery(sql, cmdType, param);
                }
                else
                {
                    obj = db.ExecuteNonQuery(sql, cmdType, param);
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }  
        }
        //public void ExecuteToDataTable(string sql, CommandType cmdType, DbParameter[] param, out object obj)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}
    }
}
