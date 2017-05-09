using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DixonsCarphone.WorkforceManagement.Web.App_Start.Startup))]
namespace DixonsCarphone.WorkforceManagement.Web.App_Start
{
     public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}