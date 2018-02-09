using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataBaseHelper.Helper;
using DataBaseHelper.Map;

namespace DataBaseHelper.EntityOperate
{
    public class PageQuyeyMaxTop : IPageQuery
    {
        public DbEntityMap ado { get { return new DbEntityMap(); } set { } }
        public DbHelper db { get { return new DbHelper(); } set { } }
        /// <summary>
        /// 分页查询所需的主键
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 分页查询，每页数据大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 分页查询定位的页码
        /// </summary>
        public int PageIndex { get; set; }

        #region 带有条件的分页查询

        /// <summary>
        /// 获取分页所需的总数据大小
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="where">sql查询条件</param>
        /// <returns>返回一个整型值：数据量</returns>
        public int GetDataCount(string tableName, DbParameter[] param, string where)
        {
            try
            {
                string sql = $"select count(*) from {tableName} where {where};";
                int count = Convert.ToInt32(db.ExecuteScalar(sql, param));
                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把实体条件转换成sql语句的查询条件
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns>返回一个字符串：分页查询的sql查询条件</returns>
        public string QueryPrerequisite<T>(T entity) where T : new()
        {
            StringBuilder fieldString = new StringBuilder();
            Type entityType = entity.GetType();
            PropertyInfo[] proInfo = entityType.GetProperties();
            List<DbParameter> paramList = new List<DbParameter>();
            foreach (var p in proInfo)
            {
                var properAttribute = ado.GetFieldAttribute(p);
                if (p.GetValue(entity, null) != null)
                {
                    fieldString.Append($"{properAttribute.FieldName}=@{properAttribute.FieldName} and ");
                }
            }
            string where = fieldString.ToString().TrimEnd("and ".ToCharArray());
            return where;
        }
        /// <summary>
        /// 把分页查询的结果转换成DataTable
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetPageData<T>(T entity) where T : new()
        {
            try
            {
                string tableName = ado.GetTableName(entity.GetType());
                string sql = string.Empty;
                DbParameter[] param = ado.GetParameters(entity);
                string where = QueryPrerequisite(entity);
                int dataCount = GetDataCount(tableName, param, where);
                if (dataCount > PageSize)
                {
                    sql = $"select top {PageSize} * from {tableName} where {where} and {PrimaryKey} > (select max({PrimaryKey}) from ( select top {PageIndex} {PrimaryKey} from  {tableName} order by {PrimaryKey} ) as t ) order by {PrimaryKey};";
                }
                else if (0 < dataCount && dataCount <= PageSize)
                {
                    sql = $"select top {PageSize} * from {tableName} where {where} order by {PrimaryKey};";
                }
                else
                {
                    return null;
                }
                db.BeginTransaction();
                DataTable dt = db.GetDataTable(sql, param);
                db.CommitTransaction();
                return dt;
            }
            catch (Exception e)
            {
                db.RollBackTransaction();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取分页查询结果
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable QueryWhere<T>(T entity) where T : new()
        {
            try
            {
                DataTable dt = GetPageData(entity);
                return dt == null ? null : dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region 不带条件的分页查询

        /// <summary>
        /// 获取数据表表名的参数转化成sql语句类型
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns>返回一个字符串</returns>
        public string GetTableNameParameter<T>() where T : new()
        {
            T entity = new T();
            StringBuilder tableNameString = new StringBuilder();
            List<DbParameter> paramList = new List<DbParameter>();
            var properAttribute = ado.GetTableAttribute(entity.GetType());
            tableNameString.Append($"{properAttribute.TableName}");
            string tableName = tableNameString.ToString();
            return tableName;
        }
        /// <summary>
        /// 获取分页所需的总数据大小
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="param">sql查询参数</param>
        /// <returns>返回一个整型值：数据量</returns>
        public int GetDataCount(string tableName, DbParameter[] param)
        {
            try
            {
                string sql = $"select count(*) from {tableName};";
                int count = Convert.ToInt32(db.ExecuteScalar(sql, param));
                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把分页查询的结果转换成DataTable
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetPageData<T>() where T : new()
        {
            try
            {
                T entity = new T();
                string tableName = GetTableNameParameter<T>();
                string sql = string.Empty;
                DbParameter[] param = ado.GetTableParameters<T>();
                int dataCount = GetDataCount(tableName, param);
                if (dataCount > PageSize)
                {
                    sql = $"select top {PageSize} * from {tableName} where {PrimaryKey} > (select max({PrimaryKey}) from ( select top {PageIndex} {PrimaryKey} from  {tableName} order by {PrimaryKey} ) as t ) order by {PrimaryKey};";
                }
                else if (0 < dataCount && dataCount <= PageSize)
                {
                    sql = $"select top {PageSize} * from {tableName} order by {PrimaryKey};";
                }
                else
                {
                    return null;
                }
                db.BeginTransaction();
                DataTable dt = db.GetDataTable(sql, param);
                db.CommitTransaction();
                return dt;
            }
            catch (Exception e)
            {
                db.RollBackTransaction();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取分页查询结果
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <returns>返回一个DataTable</returns>
        public DataTable QueryNonWhere<T>() where T : new()
        {
            try
            {
                DataTable dt = GetPageData<T>();
                return dt == null ? null : dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion
    }
}
