using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    [UserFilter(AccessLevel ="Admin, PLAdmin")]
    public class PLController : BaseController
    {
        private readonly IDashMaintenance _dashManager;

        public PLController()
        {
            _dashManager = new DashMaintenance();
        }

        // GET: Admin/PL
        public async Task<ActionResult> Index()
        {
            var result = await _dashManager.GetFileRecords("P&L");

            FileUploadView vm = new FileUploadView
            {
                _recordCollection = mapper.Map<List<FileUploadRecord>>(result)
            };
            
            return View(vm);
        }

        public ActionResult UploadFile(string fileName)
        {
            var result = _dashManager.ImportPLSheet(fileName);

            return RedirectToAction("Index");
        }
    }
}