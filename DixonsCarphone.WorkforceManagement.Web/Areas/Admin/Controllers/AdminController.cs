using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IDashMaintenance _dashManager;

        public AdminController()
        {
            _dashManager = new DashMaintenance();
        }

        // GET: Admin/Admin
        [UserFilter(AccessLevel ="Admin, OHAdmin, PLAdmin")]
        public ActionResult Index()
        {
            return View();
        }

        [UserFilter(AccessLevel = "Admin, OHAdmin, PLAdmin")]
        public ActionResult Test()
        {
            return View();
        }

        public PartialViewResult _SearchBranch(string keyword)
        {
            var data = _dashManager.StoreReferenceSearch(keyword).Result;
            return PartialView(mapper.Map<StoreReferenceView>(data));
        }
    }
}