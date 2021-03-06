﻿using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
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
        IKronosManager _KronosManager;
        IDashMaintenance _DashMaintenance;
        IPeopleManager _PeopleManager;

        public APITestController()
        {
            _KronosManager = new KronosManager();
            _DashMaintenance = new DashMaintenance();
            _PeopleManager = new PeopleManager();
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

        [HttpGet]
        [Authorize]
        public PartialViewResult _empList()
        {
            var data = _PeopleManager.GetStoreStaffWait(StoreNumber);
            return PartialView(mapper.Map<List<HrFeedView>>(data));
        }

        [HttpGet]
        public PartialViewResult _timeList()
        {
            IEnumerable<SelectListItem> Times = Helpers.GetTimes();
            return PartialView(Times.Where(x => x.Text != "Closed"));
        }

        [HttpGet]
        public PartialViewResult _branchValidate(string branchNum)
        { 
            var data = _DashMaintenance.StoreReferenceSearch(branchNum);

            return PartialView(mapper.Map<StoreReferenceView>(data));
        }

        [HttpGet]
        public string _ticketsPending()
        {
            return "";
        }
        
        [HttpGet]
        public async Task<PartialViewResult> _PayDates(string period)
        {
            ColleaguePayDataVm vm = new ColleaguePayDataVm();

            string payroll = System.Web.HttpContext.Current.Session["_EmpNum"].ToString();
            string sessionID = System.Web.HttpContext.Current.Session.SessionID;

            var dates = _storeManager.GetPayCalendarDates((_store.Channel == "ROI" ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString(), period);

            if(payroll != "e")
            {
                //if(await Task.Run(() => _KronosManager.LogOn(sessionID)))
                //{
                    vm.tSheet = mapper.Map<List<TimesheetView>>(_KronosManager.GetTimesheet(dates.Select(x => x.WCDate).ToArray(), payroll, System.Web.HttpContext.Current.Session.SessionID));
                    vm.punch = mapper.Map<List<PunchCompView>>(_storeManager.GetEmployeePunch(payroll, dates.Min(x => x.Week), dates.Max(x => x.Week)));
                    //await Task.Run(() => _KronosManager.LogOff(sessionID));
                //}                
            }
            else
            {
                vm.errorPayroll = true;
            }        
            
            vm.payDates = mapper.Map<List<PayCalendarDateView>>(dates);
            
            return PartialView(vm);
        }
    }
}