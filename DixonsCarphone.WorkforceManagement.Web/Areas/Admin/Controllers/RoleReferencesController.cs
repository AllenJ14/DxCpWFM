using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Business.Entities;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    [UserFilter(AccessLevel = "Admin")]
    public class RoleReferencesController : BaseController
    {
        private readonly IDashMaintenance _dashManager;

        public RoleReferencesController()
        {
            _dashManager = new DashMaintenance();
        }

        // GET: Admin/RoleReferences
        public async Task<ActionResult> Index()
        {
            return View(await _dashManager.RoleReferenceList());
        }

        //display details of single entry
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleReferenceView roleReference = mapper.Map<RoleReference, RoleReferenceView>(await _dashManager.RoleReferenceSingle(id));
            if (roleReference == null)
            {
                return HttpNotFound();
            }

            return View(roleReference);
        }

        //add new rolereference record
        public ActionResult Create(string SeedRole)
        {
            ViewBag.SeedRole = SeedRole;
            return View();
        }

        //post new rolereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleReferenceView model)
        {
            await _dashManager.SubmitNewRoleReference(mapper.Map<RoleReferenceView, RoleReference>(model));
            return RedirectToAction("Index");
        }

        //get rolereference record for edit
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleReferenceView roleReference = mapper.Map<RoleReference, RoleReferenceView>(await _dashManager.RoleReferenceSingle(id));
            if (roleReference == null)
            {
                return HttpNotFound();
            }

            return View(roleReference);
        }

        //post changes to rolereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleReferenceView model)
        {
            await _dashManager.SubmitRoleReferenceChange(mapper.Map<RoleReferenceView, RoleReference>(model));

            return RedirectToAction("Index");
        }

        //get rolereference record for confirm delete
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleReferenceView roleReference = mapper.Map<RoleReference, RoleReferenceView>(await _dashManager.RoleReferenceSingle(id));
            if (roleReference == null)
            {
                return HttpNotFound();
            }

            return View(roleReference);
        }

        //post delete rolereference record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await _dashManager.DeleteRoleReferenceRecord(id);

            return RedirectToAction("Index");
        }
    }
}