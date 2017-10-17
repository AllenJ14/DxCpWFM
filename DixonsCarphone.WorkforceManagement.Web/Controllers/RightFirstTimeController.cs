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
            if (!(bool)System.Web.HttpContext.Current.Session["_ROIFlag"])
            {
                System.Web.HttpContext.Current.Session["_PTFlag"] = _storeManager.FTPTCheck(System.Web.HttpContext.Current.Session["_EmpNum"].ToString()) ? "PT" : "FT";
            }
            else
            {
                System.Web.HttpContext.Current.Session["_PTFlag"] = "";
            }
        }

        [Authorize]
        public async Task<ActionResult> ColleaguePayPortal()
        {
            ColleaguePortalVm vm = new ColleaguePortalVm();

            vm.rawMenu = mapper.Map<List<PayCalendarRefView>>(await _storeManager.GetPayCalendarRef((_store.Channel == "ROI" ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString()));
            
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
            if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind("UK - Region CPW" + _store.RegionNo, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString()));
                var employeeList = await _storeManager.GetActiveColleagues(_store.RegionNo);
                var combined = vm.hf.Where(x => x.PersonData.Person.ManagerSignoffThruDateTime.Year != 1901).Join(employeeList, kronos => kronos.PersonNumber, db => db.PersonNumber, (kronos, db) => new { PersonNumber = kronos.PersonNumber, BranchNumber = db.HomeBranch, BranchName = db.BranchName, signedOff = kronos.PersonData.Person.ManagerSignoffThruDateTime });
                var punched = await _KronosManager.GetPunchStatus(employeeList.Where(x => x.KronosUser).Select(x => x.PersonNumber).ToList());
                var punchCombined = employeeList.Where(x => x.KronosUser).Join(punched, db => db.PersonNumber, kronos => kronos.Employee.PersonIdentity.PersonNumber, (db, kronos) => new { PersonNumer = db.PersonNumber, BranchNumber = db.HomeBranch, punched = kronos.Status });

                vm.rso = new List<RegionSignOff>();
                foreach(var item in employeeList.GroupBy(x => x.HomeBranch).Select(c => c.Key).OrderBy(x => x).ToList())
                {
                    var data = combined.Where(x => x.BranchNumber == item);
                    bool userScheduled = employeeList.Where(x => x.HomeBranch == item && x.Scheduled && x.KronosUser).Count() > 0;
                    bool userPunched = punchCombined.Where(x => x.BranchNumber == item && x.punched == "In").Count() > 0;

                    vm.rso.Add(new RegionSignOff { BranchNumber = item, BranchName = data.First().BranchName, Headcount = data.Count(), SignedOff = data.Where(x => x.signedOff.Date >= vm.weekStart).Count() , KronosScheduled = userScheduled, KronosPunched = userPunched});
                }
            }
            else
            {
                vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind(_store.KronosStoreName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString()));
                vm.ss = mapper.Map<List<ShortShiftView>>(await _storeManager.GetShortShiftsBranch(StoreNumber, weekOfYr));

                short a = 1;
                while(vm.hf == null && a<3)
                {
                    vm.hf = mapper.Map<List<HyperFindResultView>>(await _KronosManager.GetKronosHyperFind(_store.KronosStoreName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString()));
                    a++;
                }
                //vm.ts = mapper.Map<List<TimesheetView>>(await _KronosManager.GetTimesheetForStore(vm.weekStart, vm.hf.Select(x => x.PersonNumber).ToArray()));
            }
            //var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek().AddDays(-56), DateTime.Now.GetFirstDayOfWeek().AddDays(-7));

            //vm.GetDatesOfYear(DateTime.Now.GetFirstDayOfWeek().AddDays(-7), weekNumbers);
            //vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == vm.weekStart.ToShortDateString());

            await _KronosManager.LogOff();
            return View(vm);
        }
    }
}