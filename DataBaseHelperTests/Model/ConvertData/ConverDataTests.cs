using DataBaseHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataBaseHelperTests.Model;
using System.Data;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class ConverDataTests
    {
        [TestMethod()]
        public void JsonSerializerTestEntity()
        {
            Operate oper = new Operate();
            List<Student> list = oper.Query(new Student() { ID = 1 });
            ConverData cv = new ConverData();
            // string json = cv.JsonSerializerNonLib(list[0]);
            // string json = cv.JsonSerializerSys(list[0]);
            string json = cv.JsonSerializerNewtonsoft(list[0]);
            Assert.AreEqual(json, "{\"ID\":1,\"Age\":18,\"Name\":\"AI\"}");
        }

        [TestMethod()]
        public void JsonSerializerTestList()
        {
            Operate oper = new Operate();
            List<Student> list = oper.Query(new Student() { ID = 1 });
            ConverData cv = new ConverData();
            // string json = cv.JsonSerializerNonLib(list);
            // string json = cv.JsonSerializerSys(list);
            string json = cv.JsonSerializerNewtonsoft(list);
            Assert.AreEqual(json, "[{\"ID\":1,\"Age\":18,\"Name\":\"AI\"},{\"ID\":1,\"Age\":19,\"Name\":\"Ying\"}]");
        }

        [TestMethod()]
        public void JsonSerializerTestTable()
        {
            Operate oper = new Operate();
            List<Student> list = oper.Query(new Student() { ID = 1 });
            ConverData cv = new ConverData();
            DataTable dt = cv.GetListDataTable(list);
            // string json = cv.JsonSerializerNonLib(dt);
            string json = cv.JsonSerializerNewtonsoft(dt);
            Assert.AreEqual(json, "[{\"ID\":1,\"Age\":18,\"Name\":\"AI\"},{\"ID\":1,\"Age\":19,\"Name\":\"Ying\"}]");
        }

        [TestMethod()]
        public void JsonDeserializerTestList()
        {
            string json = "[{\"ID\":1,\"Age\":18,\"Name\":\"AI\"},{\"ID\":1,\"Age\":19,\"Name\":\"Ying\"}]";
            ConverData cv = new ConverData();
            List<Student> list = cv.JsonDeserializerNewtonsoft<List<Student>>(json);
            Assert.AreEqual(list[0].Name, "AI");
        }

        [TestMethod()]
        public void JsonDeserializerTestEntity()
        {
            string json = "{\"ID\":1,\"Age\":18,\"Name\":\"AI\"}";
            ConverData cv = new ConverData();
            Student stu = cv.JsonDeserializerNewtonsoft<Student>(json);
            Assert.AreEqual(stu.Name, "AI");
        }

        [TestMethod()]
        public void JsonDeserializerTestDataTable()
        {
            string json = "[{\"ID\":1,\"Age\":18,\"Name\":\"AI\"},{\"ID\":1,\"Age\":19,\"Name\":\"Ying\"}]";
            ConverData cv = new ConverData();
            DataTable dt = cv.JsonDeserializerNewtonsoft<DataTable>(json);
            Assert.AreEqual(dt.Rows[1]["Name"], "Ying");
        }
    }
}