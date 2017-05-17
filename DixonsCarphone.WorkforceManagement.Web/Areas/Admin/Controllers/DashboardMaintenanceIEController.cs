using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    [UserFilter(AccessLevel = "Admin")]
    public class DashboardMaintenanceIEController : BaseController
    {
        private readonly IDashMaintenance _dashManager;

        public DashboardMaintenanceIEController()
        {
            _dashManager = new DashMaintenanceIE();
        }

        // GET: Admin/DashboardMaintenance
        public ActionResult Index()
        {
            if (TempData["PackageRunning"] != null)
            {
                ViewBag.PackageRunning = true;
            }

            return View(_dashManager.GetErrors());
        }

        //Run sp to build dashboard with existing timecard data file
        public ActionResult RunBuild()
        {
            TempData["PackageRunning"] = _dashManager.RunBuild();

            return RedirectToAction("Index");
        }

        //Run sp to build dashboard with new timecard data file
        public ActionResult RunBuildNewData()
        {
            TempData["PackageRunning"] = _dashManager.RunBuildNewData();
            return RedirectToAction("Index");
        }

        //Run sp to import and update budgets from file
        public ActionResult RunBudgets()
        {
            TempData["PackageRunning"] = _dashManager.RunBudgets();
            return RedirectToAction("Index");
        }

        //Run sp to import and update budgets from file
        public ActionResult PushData()
        {
            TempData["PackageRunning"] = _dashManager.PushData();
            return RedirectToAction("Index");
        }

    }
}