using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using DixonsCarphone.WorkforceManagement.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class PlanningController : BaseController
    {
        IActivityManager _activityManager;

        public PlanningController()
        {
            _activityManager = new ActivityManager();
        }

        // GET: Store OpeningTimes
        public async Task<ActionResult> Index()
        {
            var storeVm = await GetStoreOpeningTimes();

            if(TempData["ErrorMessage"] != null)
            {
                storeVm.MessageType = MessageType.Error;
                storeVm.Message = TempData["ErrorMessage"].ToString();
            }

            return View("Store", storeVm);
        }

        public async Task<ActionResult> GetActivities(int activityTypeId)
        {
            var result = await _activityManager.GetActivitiesForActivityType(activityTypeId, StoreNumber);

            result = result.OrderByDescending(x => x.ActivityDate).ThenByDescending(x => x.StartTime).ToList();

            var vm = mapper.Map<List<ActivityView>>(result);

            return View("NewsEventsNotifications", vm);
        }

        public async Task<ActionResult> MyBudgets()
        {
            PublishedBudgetsVM vm = new PublishedBudgetsVM();
            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var data = await _storeManager.GetBudgetsRegion(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString());
                vm.BudgetCollection = mapper.Map<List<PublishedBudgetBranch>>(data);
            }
            else
            {
                var data = await _storeManager.GetBudgetsBranch(_store.CST_CNTR_ID);
                vm.BudgetCollection = mapper.Map<List<PublishedBudgetBranch>>(data);
            }

            return View("MyBudgets", vm);
        }

        //Retrieve active store opening time records
        private async Task<StoreDetailViewModel> GetStoreOpeningTimes()
        {
            StoreDetailViewModel storeVm = new StoreDetailViewModel();

            if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null || System.Web.HttpContext.Current.Session["_DivisionName"] != null || System.Web.HttpContext.Current.Session["_ChannelName"] !=null)
            {
                storeVm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                storeVm.MessageType = MessageType.Error;
                return storeVm;
            }

            var openingTimes = await _storeManager.GetStoreOpeningTimes(_store.CST_CNTR_ID);         

            foreach (StoreOpeningTime item in openingTimes)
            {
                if (item.Status == "Live")
                {
                    storeVm.CurrentTime = mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(item);
                }
                else if (item.Status.StartsWith("Pending"))
                {
                    storeVm.PendingTimes.Add(mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(item));
                }
                else if (item.Status.Contains("Peak"))
                {
                    storeVm.PeakTimes.Add(mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(item));
                }
            }

            // Return message if no records were found
            if (openingTimes == null)
            {
                storeVm.Message = "No Opening times found for your store, please raise this through Medic Feedback.";
                storeVm.MessageType = MessageType.Warning;
            }
            return storeVm;
        }

        // Load view for new opening time request
        [Authorize]
        public ActionResult NewStoreTimeRequest()
        {
            var vm = new ChangeStoreOpeningTimeViewModel();
            return View("ChangeStoreOpeningTimes", vm);
        }

        // Load confirmation for removing opening time change request
        [Authorize]
        public async Task<ActionResult> DeleteRequest(int EntryId)
        {
            var vm = new SingleOpeningTimeViewModel();
            vm.TimeToEdit = mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(await _storeManager.GetSingleTime(EntryId, StoreNumber, "Pending"));

            if (vm.TimeToEdit == null)
            {
                TempData["ErrorMessage"] = "The record you are trying to access could not be found.";
                return RedirectToAction("Index", "Planning");
            }

            return View(vm);
        }

        [HttpPost, ActionName("DeleteRequest")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int entryId)
        {
            int result = await _storeManager.CancelRequest(entryId, System.Web.HttpContext.Current.Session["_UserName"].ToString(), StoreNumber);

            if (result == 0)
            {
                TempData["ErrorMessage"] = "The update attempt failed, please try again.";
            }

            return RedirectToAction("Index", "Planning");
        }

        public ActionResult PayrollCalender()
        {
            return View();
        }

        public ActionResult Budgets()
        {
            return View();
        }

        //Post new opening time submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeTimes(ChangeStoreOpeningTimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ReasonForChange = Server.HtmlEncode(model.ReasonForChange);
                await _storeManager.ProposeNewStoreOpeningTime(model, StoreNumber, System.Web.HttpContext.Current.Session["_UserName"].ToString());
            }

            return RedirectToAction("Index", "Planning");
        }

        //Edit single opening time
        [Authorize]
        public async Task<ActionResult> EditSingleTime(int EntryId)
        {
            var vm = new SingleOpeningTimeViewModel();
            vm.TimeToEdit = mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(await _storeManager.GetSingleTime(EntryId, StoreNumber, "Peak"));

            if(vm.TimeToEdit == null)
            {
                TempData["ErrorMessage"] = "The record you are trying to access could not be found.";
                return RedirectToAction("Index", "Planning");
            }

            return View(vm);
        }

        //Submit change for single opening time
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSingleTime(SingleOpeningTimeViewModel vm)
        {
            int result = 0;

            if (ModelState.IsValid)
            {
                result = await _storeManager.EditSingleTime(vm.TimeToEdit, System.Web.HttpContext.Current.Session["_UserName"].ToString(), StoreNumber);
            }

            if(result == 0)
            {
                TempData["ErrorMessage"] = "The update attempt failed, please try again.";
            }
            return RedirectToAction("Index", "Planning");
        }

        //Confirm peak opening time entry
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AcceptPeak(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int result = await _storeManager.ConfirmPeakEntry(id, System.Web.HttpContext.Current.Session["_UserName"].ToString(), StoreNumber);

            if (result == 0)
            {
                TempData["ErrorMessage"] = "The update attempt failed, please try again.";
            }

            return RedirectToAction("Index", "Planning");
        }

        public async Task<ActionResult> GetSchedule(string selectedDate = "Current Week")
        {
            var weekOfYr = GetWeekNumber(selectedDate);
            ScheduleVm vm = new ScheduleVm();

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else
            {
                var data = await _storeManager.GetBranchSchedule(StoreNumber, weekOfYr);
                vm.ScheduleCollection = mapper.Map<List<ScheduleDetail>>(data);
            }

            var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek(), DateTime.Now.GetFirstDayOfWeek().AddDays(28));
            vm.GetWeeksOfYear(DateTime.Now.GetFirstDayOfWeek().AddDays(28), weekNumbers);
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekOfYr.ToString());
            vm.SelectedDate = vm.WeeksOfYear.Where(x => x.Selected == true).Single().Text.ToString().Substring(11, 10); ;

            ViewBag.store = string.Format("{0} {1}",_store.CST_CNTR_ID, _store.StoreName);
            return View("Schedule", vm);
        }

        //public async Task<ActionResult> Footfall(string selectedDay = null, string selectedDate = null)
        //{
        //    int wkOfYr;
        //    DateTime dt;
        //    var day1 = (int)DateTime.Now.GetFirstDayOfWeek().DayOfWeek;
        //    int dayNum = !string.IsNullOrEmpty(selectedDay) && int.TryParse(selectedDay, out dayNum) ? dayNum : day1;
        //    var finYr = ConfigurationManager.AppSettings["FinancialYearStart"];

        //    int fullYrWeekNum = !string.IsNullOrEmpty(selectedDate) && int.TryParse(selectedDate, out wkOfYr) ? wkOfYr : DateTime.Now.GetWeekOfYear(finYr);

        //    var wkNum = int.Parse(fullYrWeekNum.ToString().Substring(4));
        //    var shortYr = int.Parse(fullYrWeekNum.ToString().Substring(2, 2));

        //    var invGrpYr = string.Format("{0}/{1}", shortYr, shortYr + 1);

        //    var footFallVm = new FootFallsViewModel
        //    {
        //        DailyFootFalls = mapper.Map<List<DailyFootfall>, List<DailyFootfallView>>(await _storeManager.GetDailyFootFall(_store.CST_CNTR_ID, dayNum, fullYrWeekNum)),
        //        WeeklyFootFalls = mapper.Map<List<WeeklyFootfall>, List<WeeklyFootfallView>>(await _storeManager.GetWeeklyFootFall(_store.CST_CNTR_ID, wkNum, invGrpYr))
        //    };

        //    footFallVm.PageBlurb = ConfigurationManager.AppSettings["FootFallBlurb"];
        //    footFallVm.SelectedDate = selectedDate;
        //    footFallVm.SelectedDay = !string.IsNullOrEmpty(selectedDay) ? selectedDay : ((DayOfWeek)day1).ToString();

        //    return View(footFallVm);
        //}

        // Display full text of a single activity
        public async Task<ActionResult> GetActivity(long activityId)
        {
            var activities = new List<ActivityView>();
            if (activityId == -1)
            {
                var data = await _activityManager.GetActivities(StoreNumber);
                activities.AddRange(data.OrderByDescending(x => x.ActivityDate).ThenByDescending(x => x.StartTime).Select(x => mapper.Map<ActivityView>(x)));
            }
            else
            {
                var act = await _activityManager.GetActivity(activityId);
                var vm = mapper.Map<ActivityView>(act);
                activities.Add(vm);
            }

            return View("NewsEventsNotifications", activities);
        }

        //public async Task<ActionResult> Calendar()
        //{
        //    var result = await _activityManager.GetActivities();

        //    result = result.OrderByDescending(x => x.ActivityDate).ThenByDescending(x => x.StartTime).ToList();

        //    var vm = new CalendarViewModel { Activities = mapper.Map<List<Activity>, List<ActivityView>>(result) };
        //    return View(vm);

        //}

        private int GetWeekNumber(string selectedDate)
        {
            int weekOfYr;
            if (!int.TryParse(selectedDate, out weekOfYr))
            {
                var dt = GetScheduledWeekDate(selectedDate);
                weekOfYr = (int)_storeManager.GetSingleWeek(dt);
            }

            return weekOfYr;
        }

        private static DateTime GetScheduledWeekDate(string scheduledWeek)
        {
            DateTime dt = DateTime.Now;
            if (string.IsNullOrEmpty(scheduledWeek)) return dt;
            switch (scheduledWeek.ToLower())
            {
                case "this week":
                    dt = DateTime.Now;
                    break;
                case "last week":
                    dt = DateTime.Now.AddDays(-7);
                    break;
                case "next week":
                    dt = DateTime.Now.AddDays(7);
                    break;
                case "week after":
                    dt = DateTime.Now.AddDays(14);
                    break;
                default:
                    dt = DateTime.TryParse(scheduledWeek, out dt) ? dt : dt;
                    break;
            }

            return dt;
        }
    }
}