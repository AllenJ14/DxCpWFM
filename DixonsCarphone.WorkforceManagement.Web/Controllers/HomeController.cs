using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class HomeController : BaseController
    {
        IActivityManager _activityManager;
        IDashBoardDataManager _dashBoardManager;
        IKronosManager _kronosManager;
        IPeopleManager _peopleManager;

        public HomeController()
        {
            _activityManager = new ActivityManager(); //Methods for retrieving activities
            _dashBoardManager = new DashBoardDataManager(); //Methods for retrieving dashboard data
            _peopleManager = new PeopleManager(); //Method for retrieving HrFeed data
            _kronosManager = new KronosManager(isOffice);
        }

        // Index Action
        public async Task<ActionResult> Index()
        {
            if(_store.IpRange == null)
            {
                return RedirectToAction("UnknownStore", "Profiles");
            }

            // Loads all activities, including store specific. Orders by ActivityDate>Priority>Time
            var result = await _activityManager.GetActivities(StoreNumber);
            result = result.OrderByDescending(x => x.ActivityDate).ThenBy(x => x.PriorityTypeId).ThenByDescending(x => x.StartTime).ToList();

            // Load dashboard data records
            var dict = await GetDashBoardData();                
            
            // Map Activity and Dashboard objects into viewmodel
            var vm = new HomeViewModel { Scores = dict, Activities = mapper.Map<List<ActivityView>>(result), PageBlurb = ConfigurationManager.AppSettings["HomePageBlurb"] };

            //if (isOffice)
            //    await _kronosManager.LogOff();

            if(TempData["errorMessage"] != null)
            {
                vm.Message = TempData["errorMessage"].ToString();
                vm.MessageType = MessageType.Error;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string feedbackText)
        {
            await _activityManager.LogFeedback(feedbackText, (short)StoreNumber);
            return RedirectToAction("Index");
        }

        //public async Task<List<string>> GetKronosScheduledStaff(DateTime theDate)
        //{
        //    if (!isOffice)
        //        return new List<string> { "xxxx", "yyyy" };

        //    var startDate = theDate.GetFirstDayOfWeek();
        //    var result = await _kronosManager.GetKronosHyperFind(_store.KronosStoreName, startDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
        //        startDate.AddDays(6).ToString("dd/M/yyyy", CultureInfo.InvariantCulture));

        //    return result != null ? result.Select(x => x.PersonNumber).ToList() : new List<string>();
        //}

        // Load 4 week of dashboard data for single branch
        private async Task<Dictionary<string, List<ScoresAndUtilizationViewModel>>> GetDashBoardData()
        {
            var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek().AddDays(-7), DateTime.Now.GetFirstDayOfWeek().AddDays(14));

            // retrieve dashboard records for relevant weeks
            List<DashBoardData> current;
            List<DashBoardData> lastWeek;
            List<DashBoardData> nextWeek;
            List<DashBoardData> weekAfter;

            // retrieve dashboard records for relevant weeks
            List<DashBoardData> allData;

            if(System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                var result = await _dashBoardManager.GetChannelDashboardData(System.Web.HttpContext.Current.Session["_ChannelName"].ToString(), (int)weekNumbers[3], (int)weekNumbers[0]);
                allData = mapper.Map<List<DashBoardData>>(result);
            }
            else if(System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                var result = await _dashBoardManager.GetDivisionDashboardData(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), (int)weekNumbers[3], (int)weekNumbers[0]);
                allData = mapper.Map<List<DashBoardData>>(result);
            }
            else if(System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var result = await _dashBoardManager.GetRegionDashboardData(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), (int)weekNumbers[3], (int)weekNumbers[0]);
                allData =  mapper.Map<List<DashBoardData>>(result);
            }
            else
            {
                allData = await _dashBoardManager.GetStoreDashBoardData(StoreNumber, (int)weekNumbers[3], (int)weekNumbers[0]);
            }

            lastWeek = allData.Where(x => x.WeekNumber == weekNumbers[3]).ToList();
            current = allData.Where(x => x.WeekNumber == weekNumbers[2]).ToList();
            nextWeek = allData.Where(x => x.WeekNumber == weekNumbers[1]).ToList();
            weekAfter = allData.Where(x => x.WeekNumber == weekNumbers[0]).ToList();
            
            // start date of weeks
            var lastWeekDate = DateTime.Now.AddDays(-7).GetFirstDayOfWeek();
            var nextWeekDate = DateTime.Now.AddDays(7).GetFirstDayOfWeek();
            var wkAfterDate = DateTime.Now.AddDays(14).GetFirstDayOfWeek();

            var dict = new Dictionary<string, List<ScoresAndUtilizationViewModel>>();

            // Below block used for calculating non-scheduled colleagues
            //var staff = await _peopleManager.GetStoreStaff(StoreNumber);

            //var kronosHyperCurrent = await GetKronosData(DateTime.Now.GetFirstDayOfWeek(), DateTime.Now.AddDays(6));
            //var kronosHyperLastWeek = await GetKronosData(lastWeekDate, lastWeekDate.AddDays(6));
            //var kronosHyperNextWeek = await GetKronosData(nextWeekDate, nextWeekDate.AddDays(6));
            //var kronosHyperWeekAfter = await GetKronosData(wkAfterDate, wkAfterDate.AddDays(6));

            //var lastWkNonScheduled = GetNonScheduledCollegues(kronosHyperLastWeek, staff)?.NonScheduledColleagues.Where(x => x.WeekNumber == (currWkNum - 1).ToString()).ToList();
            //var currentWkNonScheduled = GetNonScheduledCollegues(kronosHyperCurrent, staff)?.NonScheduledColleagues.Where(x => x.WeekNumber == (currWkNum).ToString()).ToList();
            //var nextWkNonScheduled = GetNonScheduledCollegues(kronosHyperNextWeek, staff)?.NonScheduledColleagues.Where(x => x.WeekNumber == (currWkNum + 1).ToString()).ToList();
            //var wkAfterNonScheduled = GetNonScheduledCollegues(kronosHyperWeekAfter, staff)?.NonScheduledColleagues.Where(x => x.WeekNumber == (currWkNum + 2).ToString()).ToList();


            if (lastWeek.Count() > 0)
            {
                dict.Add("Last Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = lastWeek[0]?.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = lastWeek[0]?.ComplianceScore.GetValueOrDefault(-1), Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperLastWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = lastWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }
            else
            {
                dict.Add("Last Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperLastWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = lastWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }

            if (current.Count() > 0)
            {
                dict.Add("Current Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = current[0]?.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = current[0]?.ComplianceScore.GetValueOrDefault(), Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperLastWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = lastWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }
            else
            {
                dict.Add("Current Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperLastWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = lastWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }

            if (nextWeek.Count() > 0)
            {
                dict.Add("Next Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = nextWeek[0]?.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = nextWeek[0]?.ComplianceScore.GetValueOrDefault(), Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperNextWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = nextWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }
            else
            {
                dict.Add("Next Week", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperLastWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = lastWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }

            if (weekAfter.Count() > 0)
            {
                dict.Add("Week After", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = weekAfter[0]?.SOHUtilization.GetValueOrDefault(), Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = weekAfter[0]?.ComplianceScore.GetValueOrDefault(), Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperWeekAfter.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = wkAfterNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }
            else
            {
                dict.Add("Week After", new List<ScoresAndUtilizationViewModel>
                {
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Deployment", Total = 10, ScoreType = ScoreType.Deployment },
                    new ScoresAndUtilizationViewModel { Score = -1, Title = "Compliance", Total = 10, ScoreType = ScoreType.Compliance },
                    //new ScoresAndUtilizationViewModel { Score = kronosHyperLastWeek.GroupBy(x => x.PersonNumber).Select(x => x.FirstOrDefault()).ToList().Count, Title = "Scheduled Colleagues", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, //TO DO - DB Column
                    //new ScoresAndUtilizationViewModel { Score = lastWkNonScheduled?.Count,
                    //    Title = "Non-Scheduled", Total = 10, ScoreType = ScoreType.ColleaguesUnderContract }, 
                });
            }

            return dict;
        }

        private NonScheduledColleguesViewModel GetNonScheduledCollegues(List<ScheduledCollegues> kronosData, List<HrFeed> staff)
        {
            return Utils.GetNonScheduledCollegues(kronosData, staff);
        }

        private async Task<List<ScheduledCollegues>> GetKronosData(DateTime startDate, DateTime endDate)
        {
            var data = await Utils.GetKronosData(startDate, endDate, _store.KronosStoreName, isOffice);

            return data;
        }

    }
}
