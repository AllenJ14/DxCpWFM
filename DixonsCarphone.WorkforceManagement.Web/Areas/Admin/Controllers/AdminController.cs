using DixonsCarphone.WorkforceManagement.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        [UserFilter(AccessLevel ="Admin, OHAdmin, PLAdmin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}