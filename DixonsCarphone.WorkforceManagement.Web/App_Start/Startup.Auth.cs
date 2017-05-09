using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace DixonsCarphone.WorkforceManagement.Web.App_Start
{
    public static class CpwWfmAuthentication
    {
        public const String ApplicationCookie = "CpwWfmAuthenticationType";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // need to add UserManager into owin, because this is used in cookie invalidation
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CpwWfmAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "CpwWfmAuthCookie",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(20), // adjust to your needs
            });
        }
    }
}