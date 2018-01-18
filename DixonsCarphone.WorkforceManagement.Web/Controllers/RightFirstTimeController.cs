using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class RightFirstTimeController : BaseController
    {
        IKronosManager _KronosManager;

        public RightFirstTimeController()
        {
            _KronosManager = new KronosManager();
        }

        public ActionResult Guide()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> ColleaguePayPortal()
        {
            ColleaguePortalVm vm = new ColleaguePortalVm();

            if(System.Web.HttpContext.Current.Session["_PTFlag"] == null)
            {
                if (!(bool)System.Web.HttpContext.Current.Session["_ROIFlag"])
                {
                    System.Web.HttpContext.Current.Session["_PTFlag"] = _storeManager.FTPTCheck(System.Web.HttpContext.Current.Session["_EmpNum"].ToString()) ? "PT" : "FT";
                }
                else
                {
                    System.Web.HttpContext.Current.Session["_PTFlag"] = "";
                }
            }

            vm.rawMenu = mapper.Map<List<PayCalendarRefView>>(await _storeManager.GetPayCalendarRef((_store.Channel == "ROI" ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString()));
            if(System.Web.HttpContext.Current.Session["_AccessLevel"].ToString() == "BM")
            {
                vm.rawMenu = new List<PayCalendarRefView>();
            }

            return View(vm);
        }
        
        public async Task<ActionResult> TimecardSignOff(string selectedDate = "Last Week")
        {
            TimecardSignOffVm vm = new TimecardSignOffVm();
            vm.weekStart = selectedDate == "Last Week" ? DateTime.Now.AddDays(-7).GetFirstDayOfWeek().Date : DateTime.Parse(selectedDate);
            var weekOfYr = (int)_storeManager.GetSingleWeek(vm.weekStart);

            //if (true)
            //{
            //    vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind("UK - Division " + _store.Division, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString()));
            //    var employeeList = await _storeManager.GetActiveColleaguesDivision(_store.Division);
            //    var combined = vm.hf.Join(employeeList, kronos => kronos.PersonNumber, db => db.PersonNumber, (kronos, db) => new { PersonNumber = kronos.PersonNumber, Region = db.Region, signedOff = kronos.PersonData.Person.ManagerSignoffThruDateTime });

            //    vm.rso = new List<RegionSignOff>();
            //    foreach (var item in employeeList.GroupBy(x => x.Region).Select(c => c.Key).OrderBy(x => x).ToList())
            //    {
            //        var data = combined.Where(x => x.Region == item);

            //        vm.rso.Add(new RegionSignOff { BranchNumber = data.First().Region, Headcount = data.Count(), SignedOff = data.Where(x => x.signedOff.Date >= vm.weekStart).Count() });
            //    }
            //}
            if(System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if(System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                //if (await _KronosManager.LogOn(System.Web.HttpContext.Current.Session.SessionID))
                //{
                    string hfQuery = _store.Channel == "ROI" ? "IE Region " : "UK - Region CPW";
                    vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind(hfQuery + System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID));
                    var employeeList = await _storeManager.GetActiveColleagues(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString());
                    var combined = vm.hf.Where(x => x.PersonData.Person.ManagerSignoffThruDateTime.Year != 1901).Join(employeeList, kronos => kronos.PersonNumber, db => db.PersonNumber, (kronos, db) => new { PersonNumber = kronos.PersonNumber, BranchNumber = db.HomeBranch, BranchName = db.BranchName, signedOff = kronos.PersonData.Person.ManagerSignoffThruDateTime, PersonName = db.PersonName });
                    var punched = await _KronosManager.GetPunchStatus(employeeList.Where(x => x.KronosUser).Select(x => x.PersonNumber).ToList(), System.Web.HttpContext.Current.Session.SessionID);
                    var punchCombined = employeeList.Where(x => x.KronosUser).Join(punched, db => db.PersonNumber, kronos => kronos.Employee.PersonIdentity.PersonNumber, (db, kronos) => new { PersonNumer = db.PersonNumber, BranchNumber = db.HomeBranch, punched = kronos.Status });

                    vm.rso = new List<RegionSignOff>();
                    foreach (var item in employeeList.GroupBy(x => x.HomeBranch).Select(c => c.Key).OrderBy(x => x).ToList())
                    {
                        var data = combined.Where(x => x.BranchNumber == item);
                        if (data != null)
                        {
                            bool userScheduled = employeeList.Where(x => x.HomeBranch == item && x.Scheduled && x.KronosUser).Count() > 0;
                            bool userPunched = punchCombined.Where(x => x.BranchNumber == item && x.punched == "In").Count() > 0;

                            vm.rso.Add(new RegionSignOff { BranchNumber = item, BranchName = data.First().BranchName, Headcount = data.Count(), SignedOff = data.Where(x => x.signedOff.Date >= vm.weekStart).Count(), KronosScheduled = userScheduled, KronosPunched = userPunched });
                        }
                    }

                    //await _KronosManager.LogOff(System.Web.HttpContext.Current.Session.SessionID);
                //}                
            }
            else
            {
                //if(await _KronosManager.LogOn(System.Web.HttpContext.Current.Session.SessionID))
                //{
                    vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind(_store.KronosStoreName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID));
                    vm.ss = mapper.Map<List<ShortShiftView>>(await _storeManager.GetShortShiftsBranch(StoreNumber, weekOfYr));
                    vm.HelpTcks = mapper.Map<List<HelpTckSummaryView>>(await _storeManager.GetHelpTickets(StoreNumber));

                    short a = 1;
                    while ((vm.hf == null || vm.hf.Count() == 0) && a < 3)
                    {
                        vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind(_store.KronosStoreName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID));
                        a++;
                    }
                    //vm.ts = mapper.Map<List<TimesheetView>>(await _KronosManager.GetTimesheetForStore(vm.weekStart, vm.hf.Select(x => x.PersonNumber).ToArray()));

                //    await _KronosManager.LogOff(System.Web.HttpContext.Current.Session.SessionID);
                //}
            }
            //var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek().AddDays(-56), DateTime.Now.GetFirstDayOfWeek().AddDays(-7));

            //vm.GetDatesOfYear(DateTime.Now.GetFirstDayOfWeek().AddDays(-7), weekNumbers);
            //vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == vm.weekStart.ToShortDateString());
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult RaiseTicket(string empId, string formType)
        {
            TempData["empId"] = int.Parse(empId.Replace("UK", ""));

            return RedirectToAction("NewSubmission","Form", new {area="Workflow", FormTypeId=formType });
        }

        public async Task<ActionResult> PeriodSummary()
        {
            RFTPPeriodSummaryVm vm = new RFTPPeriodSummaryVm();

            vm.Cases = mapper.Map<List<RFTPCaseStubView>>(await _storeManager.GetRFTPCases("101", "17/18", 9));
            vm.Actions = mapper.Map<List<RFTPCaseActionView>>(await _storeManager.GetRFTPActions());
            vm.RegionManagers = mapper.Map<List<KronosEmpSummaryView>>(await _storeManager.GetActiveManagers("101"));
            
            return View(vm);
        }

        [HttpGet]
        public async Task<PartialViewResult> _employeeSearch(string crit)
        {
            var searchResult = new List<KronosEmpSummaryView>();
            if(crit.Length != 0)
            {
                searchResult = mapper.Map<List<KronosEmpSummaryView>>(await _storeManager.EmployeeSearch(crit));
            }            
            return PartialView(searchResult);
        }

        [HttpPost]
        public async Task<ActionResult> caseConfirm(int caseID)
        {
            var result = await _storeManager.ConfirmCase(caseID, System.Web.HttpContext.Current.Session["_UserName"].ToString());

            return RedirectToAction("PeriodSummary");
        }

        [HttpPost]
        public async Task<ActionResult> caseOverride(int caseID, string reason, string comment)
        {
            var result = await _storeManager.OverrideCase(caseID, System.Web.HttpContext.Current.Session["_UserName"].ToString(), reason, comment);

            return RedirectToAction("PeriodSummary");
        }

        [HttpPost]
        public async Task<ActionResult> caseReassign(int caseID, string empNumber, string comment)
        {
            var result = await _storeManager.ReassignCase(caseID, empNumber, System.Web.HttpContext.Current.Session["_UserName"].ToString(), comment);

            return RedirectToAction("PeriodSummary");
        }
    }
}