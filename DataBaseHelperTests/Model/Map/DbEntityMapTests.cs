using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using DataBaseHelperTests.Model;

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