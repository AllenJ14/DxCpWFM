using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class APITestController : BaseController
    {
        //IKronosManager _KronosManager;
        IDashMaintenance _DashMaintenance;

        public APITestController()
        {
            //_KronosManager = new KronosManager();
            _DashMaintenance = new DashMaintenance();
        }

        // GET: APITest
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [UserFilter(AccessLevel = "Admin")]
        public PartialViewResult _SearchBranch(string keyword)
        {
            var data = _DashMaintenance.StoreReferenceSearch(keyword);
            return PartialView(mapper.Map<StoreReferenceView>(data));
        }
    }
}