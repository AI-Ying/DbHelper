using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class SqlClientTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            SqlClient sc = new SqlClient();
            DbParameter param1 = sc.db.Factory.CreateParameter();
            List<DbParameter> paramList = new List<DbParameter>();
            param1.ParameterName = "@StuID";
            param1.Value = 1;
            paramList.Add(param1);
            string sql = "select Count(*) From Student where StuID = @StuID;";
            object obj = null;
            sc.Execute(sql, System.Data.CommandType.Text, paramList.ToArray(), out obj);
            Assert.AreEqual(obj, 1);
        }
    }
}