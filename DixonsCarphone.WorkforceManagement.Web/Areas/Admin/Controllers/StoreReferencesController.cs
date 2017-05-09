using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using System.Net;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    [UserFilter(AccessLevel = "Admin")]
    public class StoreReferencesController : BaseController
    {
        private readonly IDashMaintenance _dashManager;

        public StoreReferencesController()
        {
            _dashManager = new DashMaintenance();
        }

        // GET: Admin/StoreReferences
        public async Task<ActionResult> Index(int? page, string searchString)
        {
            var result = await _dashManager.StoreReferenceList();

            if (!string.IsNullOrEmpty(searchString))
            {
                int numVal = Int32.Parse(searchString);
                result = result.Where(x => x.Br_ == numVal).ToList();
            }

            int pageSize = 50;
            int pageNumber = page ?? 1;

            return View(result.ToPagedList(pageNumber,pageSize));
        }

        //display details of single entry
        public async Task<ActionResult> Details (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoreReferenceView storeReference = mapper.Map<StoreReference, StoreReferenceView> (await _dashManager.StoreReferenceSingle(id));
            if(storeReference == null)
            {
                return HttpNotFound();
            }

            return View(storeReference);
        }

        //add new storereference record
        public ActionResult Create(string SeedNumber)
        {
            ViewBag.SeedNumber = SeedNumber;
            return View();
        }

        //post new storereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoreReferenceView model)
        {
            await _dashManager.SubmitNewStoreReference(mapper.Map<StoreReferenceView, StoreReference>(model));
            return RedirectToAction("Index");
        }

        //get storereference record for edit
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoreReferenceView storeReference = mapper.Map<StoreReference, StoreReferenceView>(await _dashManager.StoreReferenceSingle(id));
            if (storeReference == null)
            {
                return HttpNotFound();
            }

            return View(storeReference);
        }

        //post changes to storereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StoreReferenceView model)
        {
            await _dashManager.SubmitStoreReferenceChange(mapper.Map<StoreReferenceView, StoreReference>(model));

            return RedirectToAction("Index");
        }

        //get storereference record for confirm delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoreReferenceView storeReference = mapper.Map<StoreReference, StoreReferenceView>(await _dashManager.StoreReferenceSingle(id));
            if (storeReference == null)
            {
                return HttpNotFound();
            }

            return View(storeReference);
        }

        //post delete storereference record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _dashManager.DeleteStoreReferenceRecord(id);

            return RedirectToAction("Index");
        }
    }
}