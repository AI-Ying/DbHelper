//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using DataBaseHelper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Common;
//using DbHelperTests.EntityTest;
//using System.Data;

//namespace DataBaseHelper.Tests
//{
//    [TestClass()]
//    public class AdoTests
//    {
//        [TestMethod()]
//        public void GetReaderListTest()
//        {
//            DBHelper db = new DBHelper();
//            DbParameter param1 = db.Factory.CreateParameter();
//            List<DbParameter> paramList = new List<DbParameter>();
//            param1.ParameterName = "@StuID";
//            param1.Value = 1;
//            paramList.Add(param1);

//            string sql = "select * From Student where StuID = @StuID;";
//            Ado ado = new Ado();
//            List<Student> list = ado.GetReaderList<Student>(sql, System.Data.CommandType.Text, paramList.ToArray());
//            Assert.AreEqual(list[0].ID, 1);
//            Assert.AreEqual(list[0].Age, 20);
//            Assert.AreEqual(list[0].Name, "Ying");
//        }

//        [TestMethod()]
//        public void GetTableListTest()
//        {
//            DBHelper db = new DBHelper();
//            DbParameter param1 = db.Factory.CreateParameter();
//            List<DbParameter> paramList = new List<DbParameter>();
//            param1.ParameterName = "@StuID";
//            param1.Value = 1;
//            paramList.Add(param1);

//            string sql = "select * From Student where StuID = @StuID;";
//            Ado ado = new Ado();
//            List<Student> list = ado.GetTableList<Student>(sql, System.Data.CommandType.Text, paramList.ToArray());
//            Assert.AreEqual(list[0].ID, 1);
//            Assert.AreEqual(list[0].Age, 20);
//            Assert.AreEqual(list[0].Name, "Ying");
//        }

//        [TestMethod()]
//        public void GetParametersTest()
//        {
//            Ado ado = new Ado();
//            DbParameter[] param = ado.GetParameters<Student>(new Student { ID = 1 });
//            Assert.AreEqual(param[0].ToString(), "@StuID");
//        }

//        [TestMethod()]
//        public void GetListDataTableTest()
//        {
//            Ado ado = new Ado();
//            List<Student> list = new List<Student>() { new Student { ID = 1, Age = 20, Name = "Ying"} };
//            DataTable dt = ado.GetListDataTable(list);
//            Assert.AreEqual(dt.Rows[0][2], "Ying");
//        }
//    }
//}