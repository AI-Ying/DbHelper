using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseHelperTests.Model;
using log4net;
using log4net.Config;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class OperateTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Log.Info("添加信息测试");
            Operate oper = new Operate();
            int result = oper.Add(new Student{ ID = 100, Age = 18, Name = "Park" });
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Operate oper = new Operate();
            int result = oper.Delete(new Student() { ID = 100 });
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void QueryTest()
        {
            Operate oper = new Operate();
            Log.Info("查询测试");  
            List<Student> list = oper.Query(new Student() { ID = 100 });
            Assert.AreEqual(list[0].Name, "Park");
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Operate opre = new Operate();
            int result = opre.Update(new Student { Name = "AI" }, new Student { Name = "Park" });
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { ID = 100, Age = 20, Name = "Tim"});
            list.Add(new Student() { ID = 101, Age = 21, Name = "WeChat" });
            Operate opre = new Operate();
            int result = opre.AddRange(list);
            Assert.AreEqual(2, result);
        }
    }
}