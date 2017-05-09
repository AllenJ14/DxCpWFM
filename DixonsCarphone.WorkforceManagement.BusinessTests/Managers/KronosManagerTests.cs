using Microsoft.VisualStudio.TestTools.UnitTesting;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DixonsCarphone.WorkforceManagement.Business.Managers.Tests
{
    [TestClass()]
    public class KronosManagerTests
    {
        [TestMethod()]
        public void GetStoreScheduleForWeekTest()
        {
            //var kronos = new KronosManager();
            //var t = kronos.GetStoreScheduleForWeek("UK 1877 Guildford High St", DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture), DateTime.Now.AddDays(7).ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
            //t.Wait();
            //var result = t.Result;
            //Assert.IsTrue(result.ScheduleShift.Count > 0);
        }
    }
}