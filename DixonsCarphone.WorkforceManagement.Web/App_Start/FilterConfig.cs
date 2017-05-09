using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
