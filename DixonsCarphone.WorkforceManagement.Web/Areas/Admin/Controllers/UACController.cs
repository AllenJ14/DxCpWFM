using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using DixonsCarphone.WorkforceManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Areas.Admin.Controllers
{
    [UserFilter(AccessLevel = "Admin")]
    public class UACController : BaseController
    {
        private readonly IPeopleManager _peopleManager;

        public UACController()
        {
            _peopleManager = new PeopleManager();
        }

        // GET: Admin/UAC
        public async Task<ActionResult> Index()
        {
            return View(await _peopleManager.GetUACList());
        }

        //display details of single entry
        public async Task<ActionResult> Details(int id)
        {
            UserAccessDetail uacReference = mapper.Map<UserAccessDetail>(await _peopleManager.GetUACSingle(id));
            if (uacReference == null)
            {
                return HttpNotFound();
            }

            return View(uacReference);
        }

        //add new UAC record
        public ActionResult Create()
        {
            return View();
        }

        //post new UAC record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserAccessDetail model)
        {
            await _peopleManager.SubmitNewUAC(mapper.Map<UserAccess>(model));
            return RedirectToAction("Index");
        }

        //get UAC record for edit
        public async Task<ActionResult> Edit(int id)
        {
            UserAccessDetail uacReference = mapper.Map<UserAccessDetail>(await _peopleManager.GetUACSingle(id));
            if (uacReference == null)
            {
                return HttpNotFound();
            }

            return View(uacReference);
        }

        //post changes to UAC record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserAccessDetail model)
        {
            await _peopleManager.SubmitUACChange(mapper.Map<UserAccess>(model));

            return RedirectToAction("Index");
        }

        //get UAC record for confirm delete
        public async Task<ActionResult> Delete(int id)
        {
            UserAccessDetail uacReference = mapper.Map<UserAccessDetail>(await _peopleManager.GetUACSingle(id));
            if (uacReference == null)
            {
                return HttpNotFound();
            }

            return View(uacReference);
        }

        //post delete UAC record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _peopleManager.DeleteUACRecord(id);

            return RedirectToAction("Index");
        }
    }
}