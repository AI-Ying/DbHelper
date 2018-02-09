//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using DataBaseHelper.ConvertData;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DbHelperTests.EntityTest;
//using DataBaseHelper.Operation;
//using System.Data;

//namespace DataBaseHelper.ConvertData.Tests
//{
//    [TestClass()]
//    public class ConvertToJsonTests
//    {
//        [TestMethod()]
//        public void EntityToJsonTest()
//        {
//            ConvertToJson cd = new ConvertToJson();
//            string result = cd.JsonSerializer(new Student { ID = 4, Age = 22, Name = "Park" });
//            Assert.AreEqual("{\"Age\":22,\"ID\":4,\"Name\":\"Park\"}", result);
//        }

//        [TestMethod()]
//        public void ListToJsonTest()
//        {
//            ConvertToJson cd = new ConvertToJson();
//            Operates opt = new Operates();
//            List<Student> st = opt.Query(new Student { ID = 0, Age = 0 });
//            string json = cd.JsonSerializer(st);
//            Assert.AreEqual(json, "[{\"Age\":0,\"ID\":0,\"Name\":\"Park\"},{\"Age\":0,\"ID\":0,\"Name\":\"Park\"}]");
//        }

//        [TestMethod()]
//        public void DataTableToJsonTest()
//        {
//            PageQuery.QuyeyMaxTop pq = new PageQuery.QuyeyMaxTop();
//            pq.PageIndex = 0;
//            pq.PageSize = 10;
//            pq.PrimaryKey = "id";
//            DataTable dt = pq.QueryWhere(new Student { ID = 0, Age = 0 });
//            ConvertToJson cd = new ConvertToJson();
//            Ado ado = new Ado();
//            List<Student> list = ado.GetTableList<Student>(dt); 
//            string json = cd.JsonSerializer(list);
//            Assert.AreEqual("[{\"Age\":0,\"ID\":0,\"Name\":\"Park\"},{\"Age\":0,\"ID\":0,\"Name\":\"Park\"}]", json);
//        }

//        [TestMethod()]
//        public void JsonDeserializerTest()
//        {
//            string json = "[{\"Age\":0,\"ID\":0,\"Name\":\"Park\"},{\"Age\":0,\"ID\":0,\"Name\":\"Park\"}]";
//            ConvertToJson cd = new ConvertToJson();
//            List<Student> list = cd.JsonDeserializer<List<Student>>(json);
//            Assert.AreEqual(list[0].Name, "Park");
//        }

//    }
//}