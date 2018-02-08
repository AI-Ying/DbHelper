using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHelperTests.EntityTest;

namespace DataBaseHelper.Operation.Tests
{
    [TestClass()]
    public class OperateTests
    {
        [TestMethod()]
        public void QueryTest()
        {
            Operates q = new Operates();
            var list = q.Query(new Student { ID = 1, Age = 20 });
            Assert.AreEqual(list[0].Name, "Ying");
        }

        [TestMethod()]
        public void AddTest()
        {
            Operates insert = new Operates();
            int result = insert.Add(new Student { ID = 3, Age = 20, Name = "Park" });
            Assert.AreEqual(result, 1);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Operates q = new Operates();
            int result = q.Delete(new Student { ID = 3, Age = 20 });
            Assert.AreEqual(result, 1);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Operates update = new Operates();
            int result = update.Update(new Student { Name = "Park" }, new Student { ID = 0, Age = 0});
            Assert.AreEqual(2, result);
        }

    }
}