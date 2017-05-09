using Microsoft.VisualStudio.TestTools.UnitTesting;
using DixonsCarphone.WorkforceManagement.Business.Kronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Kronos.Tests
{
    [TestClass()]
    public class KronosApiTests
    {
        [TestMethod()]
        public void LogonTest()
        {
            var t = KronosApi.Logon();
            t.Wait();
            var result = t.Result;
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void LogoffTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HyperfindResultTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RequestScheduleDetailTest()
        {
            Assert.Fail();
        }
    }
}