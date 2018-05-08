using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataBaseHelper
{
    public class Operate : IOperate
    {
        public MapHelper map { get { return new MapHelper(); } set { } }
        //public DbHelper db { get { return new DbHelper(); } set { } }

        /// <summary>
        /// 把实体类转换成相对应的sql插入语句字符串
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式化好的sql语句</returns>
        private string InsertSqlString<T>(T entity)
        {
            try
            {
                //Insert into Table (StuID, StuAge, StuName) values(@StuID, @StuAge, @StuName);
                string tableName = map.GetTableName(entity.GetType());
                string fieldNameStr = map.GetFieldNameStr(entity);
                string fieldValueStr = map.GetFieldValueStr(entity);
                string sql = $"Insert into {tableName} ({fieldNameStr}) values ({fieldValueStr});";
                return sql;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 添加实体到数据库
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回插入数据影响的行数</returns>
        public int Add<T>(T entity)
        {
            try
            {
                using (DbHelper db = new DbHelper())
                {
                    string sql = InsertSqlString(entity);
                    List<DbParameter> paramTable = map.GetTableNameParameters<T>().ToList();
                    List<DbParameter> paramValue = map.GetParameters(entity).ToList();
                    paramTable.AddRange(paramValue);
                    DbParameter[] param = paramTable.ToArray();
                    return db.ExecuteNonQuery(sql, param);
                }       
            }
            catch (Exception e)
            {
                Log.Error("添加实体信息失败", e);
                throw e;
            }
        }
        /// <summary>
        /// 批量插入相应的实体到数据库中
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>返回插入数据成功的行数</returns>
        public int AddRange<T>(List<T> list) where T : class
        {
            try
            {
                int sum = 0;
                foreach (var item in list)
                {
                    T entity = item as T;
                    int result = Add(entity);
                    sum += result;
                }
                return sum;
            }
            catch (Exception e)
            {
                Log.Error("批量添加实体信息失败", e);
                throw e;
            }
        }
        /// <summary>
        /// 把实体转换成相对应的sql删除语句字符串
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式化好的sql删除语句</returns>
        private string DeleteSqlString<T>(T entity)
        {
            try
            {
                // Delete from Student Where StuID = @StuID;
                string tableName = map.GetTableName(entity.GetType());
                string whereStr = map.GetSetWhereStr(entity);
                string sql = $"Delete from {tableName} where {whereStr};";
                return sql;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 从数据库中删除一个实体数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回删除数据影响的行数</returns>
        public int Delete<T>(T entity)
        {
            try
            {
                using (DbHelper db = new DbHelper())
                {
                    string sql = DeleteSqlString(entity);
                    List<DbParameter> paramTable = map.GetTableNameParameters<T>().ToList();
                    List<DbParameter> paramWhere = map.GetParameters(entity).ToList();
                    paramTable.AddRange(paramWhere);
                    DbParameter[] param = paramTable.ToArray();
                    return db.ExecuteNonQuery(sql, param);
                }        
            }
            catch (Exception e)
            {
                Log.Error("删除实体信息失败", e);
                throw e;
            }
        }
        /// <summary>
        /// 把实体转换成相对应的sql查询语句字符串
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式化好的sql查询语句</returns>
        private string QuerySqlString<T>(T entity)
        {
            try
            {
                // Select * from Student Where StuID = @StuID
                string tableName = map.GetTableName(entity.GetType());
                string whereStr = map.GetSetWhereStr(entity);
                string sql = $"Select * from {tableName} where {whereStr};";
                return sql;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据实体条件查询数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回一个实体类型的结果集</returns>
        public List<T> Query<T>(T entity)
        {
            try
            {
                string sql = QuerySqlString(entity);
                List<DbParameter> paramTable = map.GetTableNameParameters<T>().ToList();
                List<DbParameter> paramWhere = map.GetParameters(entity).ToList();
                paramTable.AddRange(paramWhere);
                DbParameter[] param = paramTable.ToArray();
                DbEntityMap entityMap = new DbEntityMap();
                return entityMap.GetReaderList<T>(sql, param);       
            }
            catch (Exception e)
            {
                Log.Error("查询实体信息失败", e);
                throw e;
            }
        }
        /// <summary>
        /// 把实体转换成相对应的sql查询语句字符串
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回格式化好的sql查询语句</returns>
        private string QuerySqlStringNoWhere<T>(T entity)
        {
            try
            {
                // Select * from Student Where StuID = @StuID
                string tableName = map.GetTableName(entity.GetType());
                string sql = $"Select * from {tableName};";
                return sql;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 查询某一个实体的全部数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns>返回一个实体类型的结果集</returns>
        public List<T> Query<T>()
        {
            try
            {
                T entity = Activator.CreateInstance<T>();
                string sql = QuerySqlStringNoWhere(entity);
                DbParameter[] param = map.GetTableNameParameters<T>();
                DbEntityMap entityMap = new DbEntityMap();
                return entityMap.GetReaderList<T>(sql, param);         
            }
            catch (Exception e)
            {
                Log.Error("查询实体信息失败", e);
                throw e;
            }
        }
        /// <summary>
        /// 把实体转换成相对应的sql更新语句字符串
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">需要更改的实体信息</param>
        /// <param name="prerequisite">实体条件</param>
        /// <returns>返回格式化好的sql更新语句</returns>
        private string UpdateSqlString<T>(T entity, T prerequisite)
        {
            try
            {
                // Update Student Set StuName = @NewStuName where StuName = @StuName;
                string tableName = map.GetTableName(entity.GetType());
                string setStr = map.GetSetWhereStr(entity, "New");
                string whereStr = map.GetSetWhereStr(prerequisite);
                string sql = $"Update {tableName} Set {setStr} where {whereStr};";
                return sql;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 根据实体数据更新数据库数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity"></param>
        /// <param name="prerequisite"></param>
        /// <returns>返回更新数据库后影响的行数</returns>
        public int Update<T>(T entity, T prerequisite)
        {
            try
            {
                using (DbHelper db = new DbHelper())
                {
                    string sql = UpdateSqlString(entity, prerequisite);
                    List<DbParameter> paramTable = map.GetTableNameParameters<T>().ToList();
                    List<DbParameter> paramSet = map.GetParameters(entity, "New").ToList();
                    List<DbParameter> paramWhere = map.GetParameters(prerequisite).ToList();
                    paramTable.AddRange(paramSet);
                    paramTable.AddRange(paramWhere);
                    DbParameter[] param = paramTable.ToArray();
                    return db.ExecuteNonQuery(sql, param);
                }          
            }
            catch (Exception e)
            {
                Log.Error("更新实体信息失败", e);
                throw e;
            }
        }
    }
}
