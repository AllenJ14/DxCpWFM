using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using DixonsCarphone.WorkforceManagement.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        // GET: Planning
        public async Task<ActionResult> Index()
        {
            var openingTimes = await _storeManager.GetStoreOpeningTimes((int)_store.CST_CNTR_ID);

            var storeVm = new StoreDetailViewModel { OpeningTimes = mapper.Map<StoreOpeningTime, StoreOpeningTimeView>(openingTimes), StoreName = _store.StoreName, StoreAddress = string.Format("{0} {1}", _store.Address1, _store.PostCode) };

            return View("Store", storeVm);
            //return View("FootFall");
        }

        public async Task<ActionResult> NewStoreTime()
        {
            var model = new ChangeStoreOpeningTimeViewModel();

            return View("ChangeStoreOpeningTimes", model);
        }

        public async Task<ActionResult> Footfall()
        {
            var footFallVm = new FootFallsViewModel
            {
                DailyFootFalls = mapper.Map<List<DailyFootfall>, List<DailyFootfallView>>(await _storeManager.GetDailyFootFall(_store.CST_CNTR_ID)),
                WeeklyFootFalls = mapper.Map<List<WeeklyFootfall>, List<WeeklyFootfallView>>(await _storeManager.GetWeeklyFootFall(_store.CST_CNTR_ID))
            };

            return View(footFallVm);
        }

        public async Task<ActionResult> Calendar()
        {
            var result = await _activityManager.GetActivities();

            result = result.OrderByDescending(x => x.ActivityDate).ThenByDescending(x => x.StartTime).ToList();

            var vm = new CalendarViewModel { Activities = mapper.Map<List<Activity>, List<ActivityView>>(result) };
            return View(vm);

        }

        [HttpPost]
        public async Task<ActionResult> ChangeTimes(ChangeStoreOpeningTimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _storeManager.ProposeNewStoreOpeningTime(model, StoreNumber, ConfigurationManager.AppSettings["NotificationsEmail"]);
            }

            return Redirect("/");
        }
    }
}