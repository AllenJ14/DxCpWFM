using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class HomeController : BaseController
    {
        IActivityManager _activityManager;
        IDashBoardDataManager _dashBoardManager;

        public HomeController()
        {
            _activityManager = new ActivityManager();
            _dashBoardManager = new DashBoardDataManager();
        }

        public async Task<ActionResult> Index()
        {
            var weekOfYear = DateTime.Now.GetWeekOfYear();
            var result = await _activityManager.GetActivities();
            var dashData = await _dashBoardManager.GetStoreDashBoardData(StoreNumber);
            var current = dashData.FirstOrDefault(x => x.WeekNumber == weekOfYear || x.WeekNumber == 1); //TODO: Take out 1
            var lastWeek = dashData.FirstOrDefault(x => x.WeekNumber == weekOfYear - 1) ?? new DashBoardData { SOHUtilization = 4, ComplianceScore = 6 }; // test data
            var nextWeek = dashData.FirstOrDefault(x => x.WeekNumber == weekOfYear + 1) ?? new DashBoardData { SOHUtilization = 5, ComplianceScore = 6 }; // test data
            var weekAfter = dashData.FirstOrDefault(x => x.WeekNumber == weekOfYear + 2) ?? new DashBoardData { SOHUtilization = 7, ComplianceScore = 6 }; // test data


            var dict = new Dictionary<string, List<ScoresAndUtilizationViewModel>>();

            var currentWeek = new List<ScoresAndUtilizationViewModel>
            {
                new ScoresAndUtilizationViewModel { Score = current.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                new ScoresAndUtilizationViewModel { Score = 8, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
            };

            dict.Add("Current Week", currentWeek);

            if (lastWeek != null)
            {
                dict.Add("Last Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = lastWeek.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = lastWeek.ComplianceScore.GetValueOrDefault(), Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance }, //TO DO - DB Column
                    new ScoresAndUtilizationViewModel { Score = 8, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                });
            }

            if (nextWeek != null)
            {
                dict.Add("Next Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = nextWeek.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = 8, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                });
            }

            if (nextWeek != null)
            {
                dict.Add("Week After", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = weekAfter.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = 8, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                });
            }

            result = result.OrderByDescending(x => x.ActivityDate).ThenByDescending(x => x.StartTime).ToList();

            var vm = new HomeViewModel {  Scores = dict };

            return View(vm);
        }
    }
}
