using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHelperTests.EntityTest;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class MapHelperTests
    {
        [TestMethod()]
        public void DBNullTest()
        {
            MapHelper map = new MapHelper();
            Student stu = new Student() { ID = 1 };
            string name = stu.Name.GetType().Name;
            //bool isNull = map.DBNull(stu.Name.GetType()) == null;
            Assert.AreEqual(name, true);
        }
    }
}