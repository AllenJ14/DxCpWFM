using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class ROIController : BaseController
    {
        // GET: ROI
        public async Task<ActionResult> Index()
        {
            if (TempData["storeSelectError"] != null)
            {
                ViewBag.storeSelectError = true;
            }

            var result = await _storeManager.GetAllROIStores();
            var vm = new StoreListViewModel
            {
                storeList = mapper.Map<List<StoreViewModel>>(result)
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Index(int storeSelection)
        {
            await _storeManager.SubmitROIStoreRecord(storeSelection, MvcHelper.GetIPHelper());
            System.Web.HttpContext.Current.Session.Remove("_StoreDetails");
            return RedirectToAction("Confirmed");
        }

        public ActionResult Confirmed()
        {
            return View();
        }
    }
}