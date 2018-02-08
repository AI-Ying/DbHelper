// <copyright file="DBHelperTest.cs" company="Microsoft">Copyright © Microsoft 2018</copyright>
using System;
using DataBaseHelper;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseHelper.Tests
{
    /// <summary>此类包含 DBHelper 的参数化单元测试</summary>
    [PexClass(typeof(DBHelper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class DBHelperTest
    {
        /// <summary>测试 Getcommand(String) 的存根</summary>
        [PexMethod]
        public int GetcommandTest([PexAssumeUnderTest]DBHelper target, string sql)
        {
            int result = target.Getcommand(sql);
            return result;
            // TODO: 将断言添加到 方法 DBHelperTest.GetcommandTest(DBHelper, String)
        }
    }
}
