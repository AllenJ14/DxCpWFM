using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace DixonsCarphone.WorkforceManagement.Web.Attributes
{
    public class RoleFilterAttribute : ActionFilterAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var returnUrl = filterContext.HttpContext.Request.Url;

                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Index",
                        returnUrl = returnUrl
                    })
                );
                return;
            }

            var isAuthorised = false;
            var userGroups = HttpContext.Current.GetSessionObject<List<GroupPrincipal>>("_UserGroups");

            if (userGroups != null && !string.IsNullOrEmpty(AccessLevel))
            {
                var accessLevels = AccessLevel.Split(',').ToList();
                foreach (var accessLvl in accessLevels)
                {
                    var grpName = ConfigurationManager.AppSettings[accessLvl]?.ToString() ?? accessLvl;
                    if (!string.IsNullOrEmpty(grpName))
                    {
                        isAuthorised = userGroups.Any(x => x.Name != null && x.Name.Contains(grpName));
                        if (isAuthorised)
                            break;
                    }
                }
            }

                //isAuthorised = userGroups != null && !string.IsNullOrEmpty(AccessLevel) &&
                //(userGroups.Any(x => AccessLevel.Split(',').Contains(x.Name)));

            if (!isAuthorised)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised"
                    })
                );
            }
        }
    }
}