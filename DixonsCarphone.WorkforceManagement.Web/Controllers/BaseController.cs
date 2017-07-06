using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Mapping;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    // Base class for all controllers.
    // variables:
    // _storeManager - methods for querying store level data
    // _store - store profile variables, populated based on IP address query
    // baseVm - viewmodel elements common to all pages (message, blurb, footer, etc)
    // isOffice - flag used for debugging [DEPRECATED]
    // mapper - object>object mapping profile
    // StoreNumber - protected integer containing store number
    //

    public class BaseController : Controller
    {
        protected IStoreManager _storeManager;
        protected Store _store = System.Web.HttpContext.Current.GetSessionObject<Store>("_StoreDetails") ?? GetStoreInfo(); //If session object is null calls method to identify store
        protected BaseViewModel baseVm = new BaseViewModel();
        protected bool isOffice = ConfigurationManager.AppSettings["IsOffice"] != null && bool.Parse(ConfigurationManager.AppSettings["IsOffice"]); //Office flag for debugging

        protected IMapper mapper;
        private MapperConfiguration config = AutoMapperWebConfiguration.MapperConfig;

        protected int StoreNumber
        {
            get
            {
                return _store != null ? _store.CST_CNTR_ID : 0;
            }
        }

        protected string RegionNumber
        {
            get
            {
                return _store != null ? _store.RegionNo : "";
            }
        }

        // constructor
        public BaseController()
        {
            _storeManager = new StoreManager();
            mapper = config.CreateMapper();
        }

        // Retrieves store detail based on IP address, returns to the call and loads into session object
        private static Store GetStoreInfo()
        {
            var ip = MvcHelper.GetIPHelper();
            var _storeManager = new StoreManager();
            var t = Task.Run(() => _storeManager.GetStoreDetails(ip));
            var store = t.Result ?? new Store();
            System.Web.HttpContext.Current.Session.Add("_StoreDetails", store);
            System.Web.HttpContext.Current.Session.Add("_ROIFlag", store.Channel == "ROI");
            
            return store;
        }
    }
} 