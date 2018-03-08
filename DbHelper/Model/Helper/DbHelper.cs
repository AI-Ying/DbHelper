using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataBaseHelper
{
    public sealed partial class DbHelper : IDisposable
    {
        #region Properties

        /// <summary>
        /// 从连接数据库字符串集合
        /// </summary>
        List<string> slaveConnectionStrings = new List<string>();
        /// <summary>
        /// 从连接数据库连接
        /// </summary>
        List<DbConnection> SlaveConnection = new List<DbConnection>();
        public string ConnectionString { get; set; }       
        public string ProviderName { get; set; }
        public DbProviderFactory Factory { get; set; }
        public DbConnection Connection { get; set; }
        public bool IsBeginTransaction { get; set; }
        public DbTransaction Transaction { get; set; }
        public DbCommand Command { get; set; }

        #endregion

        #region Constructor
        
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        public DbHelper()
        {
            try
            {
                List<string> conStr = DbConfig.ConnectionStrings;
                ConnectionString = conStr[0];
                ProviderName = DbConfig.ProviderNames[0];
                if (DbConfig.ConnectionStrings.Count > 1)
                {
                    conStr.RemoveAt(0);
                    slaveConnectionStrings.AddRange(conStr);
                }
                else
                {
                    slaveConnectionStrings.AddRange(conStr);
                }
                CreateFactory();
                Log.Info("打开数据库连接");
            }
            catch (Exception e)
            {
                Log.Error("数据库打开失败", e);
                throw e;
            }
            
        }

        #endregion

        #region Helper

        /// <summary>
        /// 创建一个数据提供程序工厂
        /// </summary>
        public void CreateFactory()
        {
            if (ProviderName == null)
            {
                throw new ArgumentException("Please Checkr your ConnectionString of App.config!");
            }
            Factory = DbProviderFactories.GetFactory(ProviderName);
        }

        /// <summary>
        /// 获取一个已打开的连接
        /// </summary>
        /// <param name="isMaster">是否开启主从模式</param>
        /// <returns></returns>
        public DbConnection GetConnection(bool isMaster)
        {
            try
            {
                if (isMaster)
                {
                    Connection = Factory.CreateConnection();
                    Connection.ConnectionString = ConnectionString;
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Connection.Open();
                    }
                    return Connection;
                }
                else
                {
                    foreach (var item in slaveConnectionStrings)
                    {
                        DbConnection conn = Factory.CreateConnection();
                        conn.ConnectionString = item;
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SlaveConnection.Add(conn);
                    }
                    var count = SlaveConnection.Count;
                    Connection = SlaveConnection[new Random().Next(0, count - 1)];
                    return Connection;
                }
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 参数化查询的参数是以参数数组形式传递，如果多次引用不及时释放会造成错误
        /// </summary>
        public void ClearParameters()
        {
            try
            {
                if (Command.Parameters.Count > 0)
                {
                    Command.Parameters.Clear();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 开始事物
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                if (Connection == null)
                {
                    Connection = GetConnection(true);
                }
                Transaction = Connection.BeginTransaction();
                IsBeginTransaction = true;
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 提交事物
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (IsBeginTransaction && Transaction != null)
                {
                    Transaction.Commit();
                    IsBeginTransaction = false;
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTransaction()
        {
            try
            {
                if (IsBeginTransaction && Transaction != null)
                {
                    Transaction.Rollback();
                }
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取SQL执行命令
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>DbCommand类型的命令</returns>
        public DbCommand GetCommand(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                if (IsBeginTransaction)
                {
                    Command = Connection.CreateCommand();
                    Command.Transaction = Transaction;
                    Command.CommandText = sql;
                    Command.CommandType = cmdType;
                    Command.Parameters.AddRange(param);
                    return Command;
                }
                else
                {
                    Connection = GetConnection(false);
                    Command = Connection.CreateCommand();
                    Command.CommandText = sql;
                    Command.CommandType = cmdType;
                    Command.Parameters.AddRange(param);
                    return Command;
                }
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 返回执行命令后受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回Int类型行数</returns>
        public int ExecuteNonQuery(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                BeginTransaction();
                using (DbCommand command = GetCommand(sql, cmdType, param))
                {
                    int result = command.ExecuteNonQuery();
                    CommitTransaction();
                    ClearParameters();
                    return result;
                }
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 返回执行命令后受影响的行数（ComandType默认为text）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回Int类型行数</returns>
        public int ExecuteNonQuery(string sql, DbParameter[] param)
        {
            try
            {
                return ExecuteNonQuery(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取执行命令结果中的第一行第一列（select count(*) from student）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回Object类型的结果值</returns>
        public object ExecuteScalar(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                using (DbCommand command = GetCommand(sql, cmdType, param))
                {
                    Object obj = command.ExecuteScalar();
                    ClearParameters();
                    return obj;
                }
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取执行命令结果中的第一行第一列：ComandType默认为text
        /// （select count(*) from student）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回Object类型的结果值</returns>
        public object ExecuteScalar(string sql, DbParameter[] param)
        {
            try
            {
                return ExecuteScalar(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行命令获取DataReader对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个DataReader对象</returns>
        public DbDataReader ExecuteReader(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                using (DbCommand command = GetCommand(sql, cmdType, param))
                {
                    DbDataReader reader = command.ExecuteReader();
                    ClearParameters();
                    return reader;
                }
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行命令获取DataReader对象，ComandType默认为text。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个DataReader对象</returns>
        public DbDataReader ExecuteReader(string sql, DbParameter[] param)
        {
            try
            {
                return ExecuteReader(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行命令，获取一个DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个DataSet</returns>
        public DataSet GetDataSet(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                using (DbCommand command = GetCommand(sql, cmdType, param))
                {
                    
                    using (DbDataAdapter da = Factory.CreateDataAdapter())
                    {
                        DataSet ds = new DataSet();
                        da.SelectCommand = command;
                        da.Fill(ds);
                        ClearParameters();
                        return ds == null ? null : ds;
                        
                    }
                }
            }
            catch(Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行命令，获取一个DataSet(ComandType默认为text)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个DataSet</returns>
        public DataSet GetDataSet(string sql, DbParameter[] param)
        {
            try
            {
                return GetDataSet(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
                throw;
            }
        }
        /// <summary>
        /// 执行查询命令，获取一个DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetDataTable(string sql, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                DataSet ds = GetDataSet(sql, cmdType, param);
                DataTable dt = dt = ds.Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行查询命令，获取一个DataTable（ComandType默认为text）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetDataTable(string sql, DbParameter[] param)
        {
            try
            {
                return GetDataTable(sql, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行查询命令，获取索引为index的一行DataRow
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="index"></param>
        /// <param name="cmdType">命令类型：（sql文本 or 存储过程）</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回为一行DataRow</returns>
        public DataRow GetDataRow(string sql, int index, CommandType cmdType, DbParameter[] param)
        {
            try
            {
                DataTable dt = GetDataTable(sql, cmdType, param);
                DataRow dr = dt.Rows[index];
                return dr;
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行查询命令，获取索引为index的一行DataRow(ComandType默认为Text)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="index">DataRow的索引值</param>
        /// <param name="param">sql参数</param>
        /// <returns>返回为一行DataRow</returns>
        public DataRow GetDataRow(string sql, int index, DbParameter[] param)
        {
            try
            {
                return GetDataRow(sql, index, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行查询命令，获取索引为index的一行DataRow(ComandType默认为Text且
        /// index默认为第零行)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="index"></param>
        /// <param name="param">sql参数</param>
        /// <returns>返回为一行DataRow</returns>
        public DataRow GetDataRow(string sql, DbParameter[] param)
        {
            try
            {
                return GetDataRow(sql, 0, CommandType.Text, param);
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception(e.Message);
            }
        }

        #endregion

        public void Dispose()
        {
            if (Connection != null)
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    if (Transaction != null)
                    {
                        IsBeginTransaction = false;
                        Transaction.Rollback();
                        Transaction.Dispose();
                    }
                    Connection.Close();
                }
                Connection = null;
            }
        }
    }
}
