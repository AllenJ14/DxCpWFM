using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DixonsCarphone.WorkforceManagement.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebConfiguration.Configure();
        }

        void Session_Start(object sender, EventArgs e)
        {
            var ip = MvcHelper.GetIPHelper();
            var storeManager = new StoreManager();
            var t = Task.Run(() => storeManager.GetStoreDetails(ip));
            var store = t.Result;
            HttpContext.Current.Session.Add("_StoreDetails", store);
        }
    }
}
