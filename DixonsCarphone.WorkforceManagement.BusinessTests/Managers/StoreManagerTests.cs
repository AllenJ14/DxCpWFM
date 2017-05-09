using Microsoft.VisualStudio.TestTools.UnitTesting;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers.Tests
{
    [TestClass()]
    public class StoreManagerTests
    {
        IStoreManager _storeManager;

        public StoreManagerTests()
        {
            _storeManager = new StoreManager();
        }

        [TestMethod()]
        public void GetStorePandLTest()
        {
            //var t = _storeManager.GetStorePandL(2, "2016", "10");
            //t.Wait();
            //var result = t.Result;

            //Assert.IsNotNull(result);
        }
    }
}