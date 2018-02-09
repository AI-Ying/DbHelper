using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHelperTests.EntityTest;
using System.Data.Common;

namespace DataBaseHelper.Map.Tests
{
    [TestClass()]
    public class DbEntityMapTests
    {
        [TestMethod()]
        public void GetParametersTest()
        {
            Student stu = new Student() { Name = "Ying" };
            MapHelper map = new MapHelper();
            DbParameter[] param = map.GetParameters(stu);
            Assert.AreEqual(param[0].Value , "Ying");
        }
    }
}