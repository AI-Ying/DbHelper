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
    public class OperateTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Operate oper = new Operate();
            int result = oper.Add(new Student() { ID = 1, Age = 18, Name = "Ying" });
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Operate oper = new Operate();
            int result = oper.Delete(new Student() { ID = 1 });
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void QueryTest()
        {
            Operate oper = new Operate();
            List<Student> list = oper.Query(new Student() { Name = "Park" });
            Assert.AreEqual(list[0].ID, 0);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Operate opre = new Operate();
            int result = opre.Update(new Student { Name = "jimi"}, new Student { Name = "Park"});
            Assert.AreEqual(result, 1);
        }
    }
}