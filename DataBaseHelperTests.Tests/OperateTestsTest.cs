// <copyright file="OperateTestsTest.cs" company="Microsoft">Copyright © Microsoft 2018</copyright>

using System;
using DataBaseHelper.Tests;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseHelper.Tests.Tests
{
    [TestClass]
    [PexClass(typeof(OperateTests))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class OperateTestsTest
    {

        [PexMethod(MaxConditions = 1000)]
        public void AddTest([PexAssumeUnderTest]OperateTests target)
        {
            target.AddTest();
            // TODO: 将断言添加到 方法 OperateTestsTest.AddTest(OperateTests)
        }
    }
}
