using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataBaseHelperTests.Model;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class PageQuyeyMaxTopTests
    {
        [TestMethod()]
        public void QueryWhereTest()
        {
            PageQuyeyMaxTop pq = new PageQuyeyMaxTop();
            pq.PageSize = 5;
            pq.PageIndex = 5;
            pq.PrimaryKey = "id";
            DataTable dt = pq.QueryWhere(new Student() { ID = 3 });
            ConverData cv = new ConverData();
            List<Student> list = cv.GetTableList<Student>(dt);
            Assert.AreEqual(list[0].Name, "Tom");       
        }

        [TestMethod()]
        public void QueryNonWhereTest()
        {
            PageQuyeyMaxTop pq = new PageQuyeyMaxTop();
            pq.PageSize = 5;
            pq.PageIndex = 5;
            pq.PrimaryKey = "id";
            DataTable dt = pq.QueryNonWhere<Student>();
            ConverData cv = new ConverData();
            List<Student> list = cv.GetTableList<Student>(dt);
            Assert.AreEqual(list[0].Name, "park");
        }
    }
}