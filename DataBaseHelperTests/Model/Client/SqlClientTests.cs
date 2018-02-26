using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using DataBaseHelperTests.Model;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class SqlClientTests
    {

        [TestMethod()]
        public void QueryToListTest()
        {
            SqlClient sc = new SqlClient();
            string sql = @"select * from Student where StuID < @StuID;";
            sc.AddParameter("@StuID", 3);
            List<Student> list = sc.QueryToList<Student>(sql, sc.Param);
            Assert.AreEqual(list[0].Name, "AI");
        }

        [TestMethod()]
        public void QueryToDataTableTest()
        {
            SqlClient sc = new SqlClient();
            string sql = @"select * from Student where StuID < @StuID;";
            sc.AddParameter("@StuID", 3);
            DataTable dt = sc.QueryToDataTable<Student>(sql, sc.Param);
            Assert.AreEqual(dt.Rows[0]["Name"], "AI");
        }

        [TestMethod()]
        public void ExecuteTestSelect()
        {
            SqlClient sc = new SqlClient();
            string sql = @"select count(*) as num, StuID from Student where StuID not in (select StuID from Student where StuID > @StuID) group by StuID;";
            sc.AddParameter("@StuID", 3);
            DataTable dt = sc.Execute(sql, sc.Param) as DataTable;
            Assert.AreEqual(dt.Rows[0]["num"], 2);
        }

        [TestMethod()]
        public void ExecuteTestAdd()
        {
            SqlClient sc = new SqlClient();
            string sql = @"insert into Student(StuID, StuAge, StuName)values(@StuID, @StuAge, @StuName)";
            sc.AddParameter("@StuID", 200);
            sc.AddParameter("@StuAge", 23);
            sc.AddParameter("@StuName", "Mono");
            object result = sc.Execute(sql, sc.Param);
            Assert.AreEqual(result, 1);
        }
    }
}