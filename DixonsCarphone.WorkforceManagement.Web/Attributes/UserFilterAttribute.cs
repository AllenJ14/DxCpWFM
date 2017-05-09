using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DixonsCarphone.WorkforceManagement.Web.Attributes
{
    public class UserFilterAttribute : ActionFilterAttribute
    {
        public string AccessLevel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Index",
                        returnUrl = filterContext.HttpContext.Request.Url.PathAndQuery,
                        area = ""
                    })
                );

                return;
            }

            var isAuthorised = false;
            var accessLevels = AccessLevel.Split(',').ToList();

            if(HttpContext.Current.Session["_AccessLevel"].ToString() != "")
            {
                string _accessLevel = HttpContext.Current.Session["_AccessLevel"].ToString();
                foreach (var accessLevel in accessLevels)
                {
                    if (accessLevel.Contains(_accessLevel))
                    {
                        isAuthorised = true;
                        break;
                    }
                }
            }
            else if(accessLevels.Contains("BM"))
            {
                var groups = HttpContext.Current.GetSessionObject<List<GroupPrincipal>>("_UserGroups");
                var branchManagerGrp = ConfigurationManager.AppSettings["BranchManagerGroup"];

                if(groups != null && branchManagerGrp != null && groups.Any(x => x.Name != null && x.Name.Contains(branchManagerGrp)))
                {
                    isAuthorised = true;
                }
            }

            if (!isAuthorised)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised",
                        area = ""
                    })
                );
            }
        }
    }
}