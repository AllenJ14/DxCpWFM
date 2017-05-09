using DixonsCarphone.WorkforceManagement.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [UserFilter(AccessLevel = "Admin, OHAdmin")]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }
    }
}