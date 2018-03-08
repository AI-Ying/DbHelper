using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Tests
{
    [TestClass()]
    public class DbHelperTests
    {
        [TestMethod()]
        public void DbHelperTest()
        {
            DbHelper db = new DbHelper();
        }
    }
}