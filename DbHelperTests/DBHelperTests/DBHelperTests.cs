//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using DataBaseHelper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Common;
//using System.Data;

//namespace DataBaseHelper.Tests
//{
//    [TestClass()]
//    public class DBHelperTests
//    {

//        [TestMethod()]
//        public void ExecuteNonQueryTest()
//        {
//            DBHelper db = new DBHelper();
//            DbParameter param1 = db.Factory.CreateParameter();
//            DbParameter param2 = db.Factory.CreateParameter();
//            DbParameter param3 = db.Factory.CreateParameter();
//            List<DbParameter> paramList = new List<DbParameter>();
//            param1.ParameterName = "@StuID";
//            param1.Value = 3;
//            paramList.Add(param1);
//            param2.ParameterName = "@StuName";
//            param2.Value = "AI";
//            paramList.Add(param2);
//            param3.ParameterName = "@StuAge";
//            param3.Value = 22;
//            paramList.Add(param3);

//            string sql = "insert Student(StuID, StuName, StuAge) values(@StuID, @StuName, @StuAge)";
//            int result = 0;
//            db.BeginTransaction();
//            try
//            {
//                for (int i = 0; i < 100; i++)
//                {
//                    result = db.ExecuteNonQuery(sql, paramList.ToArray());
//                    db.ClearParameters();
//                    if (0 == i % 10)
//                    {
//                        db.CommitTransaction();
//                        db.BeginTransaction();
//                    }
//                }
//            }
//            catch
//            {
//                db.RollBackTransaction();
//            }


//            Assert.AreEqual(result, 1);
//        }

//        [TestMethod()]
//        public void ExecuteScalarTest()
//        {
//            DBHelper db = new DBHelper();
//            DbParameter param1 = db.Factory.CreateParameter();
//            List<DbParameter> paramList = new List<DbParameter>();
//            param1.ParameterName = "@StuID";
//            param1.Value = 1;
//            paramList.Add(param1);

//            string sql = "select Count(*) From Student where StuID = @StuID;";
//            Object obj = db.ExecuteScalar(sql, paramList.ToArray());
//            db.ClearParameters();
//            Assert.AreEqual(obj, 1);
//        }

//        [TestMethod()]
//        public void GetDataRowTest()
//        {
//            DBHelper db = new DBHelper();
//            DbParameter param1 = db.Factory.CreateParameter();
//            List<DbParameter> paramList = new List<DbParameter>();
//            param1.ParameterName = "@StuID";
//            param1.Value = 1;
//            paramList.Add(param1);

//            string sql = "select * From Student where StuID = @StuID;";
//            DataRow row = db.GetDataRow(sql, paramList.ToArray()) ;
//            Assert.AreEqual(row["StuID"], 1);
//            Assert.AreEqual(row["StuAge"], 20);
//            Assert.AreEqual(row["StuName"], "Ying");
//        }
//    }
//}