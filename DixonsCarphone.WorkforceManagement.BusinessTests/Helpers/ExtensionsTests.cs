using Microsoft.VisualStudio.TestTools.UnitTesting;
using DixonsCarphone.WorkforceManagement.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using DixonsCarphone.WorkforceManagement.Business.Kronos;

namespace DixonsCarphone.WorkforceManagement.Business.Helpers.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void DeserializeToObjectTest()
        {
            var xml = File.ReadAllText(@"C:\Dev\KronosApiTest\HyperfindResult.xml"); //change as appropriate
            var t = xml.DeserializeToObject<HyperFindResult>();
            t.Wait();
            var test = t.Result;

            Assert.IsNotNull(test);

            xml = File.ReadAllText(@"C:\Dev\KronosApiTest\ScheduleResult.xml"); //change as appropriate
            var t1 = xml.DeserializeToObject<ScheduleShift>();
            t1.Wait();
            var test1 = t1.Result;
            Assert.IsTrue(test1.Count > 0);
        }

        [TestMethod()]
        public void GetFirstDayOfWeekTest()
        {
           // var result = DateTime.Now.GetFirstDayOfWeek();
           // Assert.IsTrue(result.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}