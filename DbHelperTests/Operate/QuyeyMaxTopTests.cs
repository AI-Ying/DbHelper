//using DataBaseHelper.PageQuery;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using DbHelperTests.EntityTest;

//namespace DataBaseHelper.PageQuery.Tests
//{
//    [TestClass()]
//    public class QuyeyMaxTopTests
//    {
//        [TestMethod()]
//        public void QueryNonWhereTest()
//        {
//            QuyeyMaxTop pq = new QuyeyMaxTop();
//            Ado ado = new Ado();
//            pq.PageSize = 10;
//            pq.PageIndex = 20;
//            pq.PrimaryKey = "id";
//            DataTable dt = pq.QueryNonWhere<Student>();
//            var list = ado.GetTableList<Student>(dt);
//            Assert.AreEqual(list[0].Name, "AI");
//        }

//        [TestMethod()]
//        public void QueryWhereTest()
//        {
//            QuyeyMaxTop pq = new QuyeyMaxTop();
//            Ado ado = new Ado();
//            pq.PageSize = 10;
//            pq.PageIndex = 20;
//            pq.PrimaryKey = "id";
//            DataTable dt = pq.QueryWhere(new Student { ID = 1, Age = 20});
//            var list = ado.GetTableList<Student>(dt);
//            Assert.AreEqual(list[0].Name, "Ying");
//        }
//    }
//}
