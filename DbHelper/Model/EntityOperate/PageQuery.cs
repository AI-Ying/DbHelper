using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DataBaseHelper
{
    public class PageQuyeyMaxTop : IPageQuery
    {
        public MapHelper map { get { return new MapHelper(); } set { } }
        public DbHelper db { get { return new DbHelper(); } set { } }
        public DbEntityMap tableMap { get { return new DbEntityMap(); } set { } }
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
        private int GetDataCount(string tableName, DbParameter[] param, string where)
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
        /// 把分页查询的结果转换成DataTable
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns>返回一个DataTable</returns>
        private DataTable GetPageData<T>(T entity)
        {
            try
            {
                string tableName = map.GetTableName(entity.GetType());
                string where = map.GetSetWhereStr(entity);
                List<DbParameter> paramTable = map.GetTableNameParameters<T>().ToList();
                List<DbParameter> paramWhere = map.GetParameters(entity).ToList();
                paramTable.AddRange(paramWhere);
                DbParameter[] param = paramTable.ToArray();               
                string sql = string.Empty;
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
                DataTable dt = tableMap.GetDataTable<T>(sql, param);
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
        public DataTable QueryWhere<T>(T entity)
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
        /// 获取分页所需的总数据大小
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="param">sql查询参数</param>
        /// <returns>返回一个整型值：数据量</returns>
        private int GetDataCount(string tableName, DbParameter[] param)
        {
            try
            {
                string sql = $"Select count(*) from {tableName};";
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
        private DataTable GetPageData<T>()
        {
            try
            {
                T entity = Activator.CreateInstance<T>();
                string tableName = map.GetTableName(entity.GetType());
                DbParameter[] param = map.GetTableNameParameters<T>();
                string sql = string.Empty;
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
                DataTable dt = tableMap.GetDataTable<T>(sql, param);
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
        public DataTable QueryNonWhere<T>()
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
