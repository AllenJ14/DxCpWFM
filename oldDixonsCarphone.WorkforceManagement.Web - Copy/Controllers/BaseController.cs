using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IStoreManager _storeManager;
        protected Store _store = System.Web.HttpContext.Current.GetSessionObject<Store>("_StoreDetails") ?? GetStoreInfo();
        protected BaseViewModel baseVm = new BaseViewModel();

        protected IMapper mapper;

        private MapperConfiguration config = AutoMapperWebConfiguration.MapperConfig;

        protected int StoreNumber
        {
            get
            {
                return _store != null ? _store.CST_CNTR_ID : 0;
            }
        }

        public BaseController()
        {
            _storeManager = new StoreManager();
            mapper = config.CreateMapper();
            GetFooter();
        }

        private void GetFooter()
        {
            var serObjPath = System.Web.HttpContext.Current.Server.MapPath("~/Footer/FootContent.xml");
            baseVm.Footer = Helpers.DeSerializeObject<List<FooterContent>>(serObjPath) ?? new List<FooterContent>();
        }

        private static Store GetStoreInfo()
        {
            var ip = MvcHelper.GetIPHelper();
            var _storeManager = new StoreManager();
            var t = Task.Run(() => _storeManager.GetStoreDetails(ip));
            var store = t.Result ?? new Store();
            System.Web.HttpContext.Current.Session.Add("_StoreDetails", store);

            return store;
        }
    }
} 