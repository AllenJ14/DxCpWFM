using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Kronos;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using DixonsCarphone.WorkforceManagement.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web.Controllers
{
    public class ReportsController : BaseController
    {
        IDashBoardDataManager _dashBoardManager;
        IPeopleManager _peopleManager;
        //IKronosManager _kronosManager;

        public ReportsController()
        {
            _dashBoardManager = new DashBoardDataManager();
            _peopleManager = new PeopleManager();
            //_kronosManager = new KronosManager(isOffice);
        }

        //public ActionResult Index()
        //{
        //    return View("Wfm");
        //}

        //public async Task<ActionResult> Wfm(string report, string scheduledWeek)
        //{
        //    var dt = GetScheduledWeekDate(scheduledWeek);
        //    var weekOfYear = dt.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);

        //    var data = await _dashBoardManager.GetStoreDashBoardData(storeNumber ,weekOfYear);
        //    var dashData = data.FirstOrDefault() ?? new DashBoardData(); //todo

        //    var model = GetReportData(report, dashData);

        //    return View("Wfm", model);
        //}

        //Load compliance summary page for specified date and selected branch
        public async Task<ActionResult> GetCompliance(string selectedDate = "Last Week")
        {
            ComplianceViewModel vm = new ComplianceViewModel();

            var weekOfYr = GetWeekNumber(selectedDate);

            if(System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                var data = await _dashBoardManager.GetAllChannelDashboardData(System.Web.HttpContext.Current.Session["_ChannelName"].ToString(), weekOfYr);
                vm._dashboardViewCollectionChannel = mapper.Map<List<DashboardViewChannel>>(data);
                if (data.Sum(x => x.FinalTarget) == 0)
                {
                    vm.MessageType = MessageType.Warning;
                }
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                var data = await _dashBoardManager.GetAllDivisionDashboardData(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), weekOfYr);
                vm._dashboardViewCollectionChannel = mapper.Map<List<DashboardViewChannel>>(data);
                if (data.Sum(x => x.FinalTarget) == 0)
                {
                    vm.MessageType = MessageType.Warning;
                }
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var data = await _dashBoardManager.GetAllRegionDashBoardData((int)System.Web.HttpContext.Current.Session["_RegionNumber"], weekOfYr, weekOfYr);
                vm._dashboardViewCollection = mapper.Map<List<DashBoardView>>(data);
                if (data.Sum(x => x.FinalTarget) == 0)
                {
                    vm.MessageType = MessageType.Warning;
                }
            }
            else
            {
                var data = mapper.Map<DashBoardView>(await _dashBoardManager.GetStoreDashBoardData(StoreNumber, weekOfYr));
                var detailData = mapper.Map<List<EmpComplianceDetailView>>(await _storeManager.GetComplianceDetail(StoreNumber, weekOfYr));
                if (data.FinalTarget != null)
                {
                    vm.populateClass(detailData, data);
                }
                else
                {
                    vm.MessageType = MessageType.Warning;
                }
            }

            var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek().AddDays(-56), DateTime.Now.GetFirstDayOfWeek().AddDays(28));

            vm.GetWeeksOfYear(DateTime.Now.GetFirstDayOfWeek().AddDays(28), weekNumbers);
            vm.PageBlurb = ConfigurationManager.AppSettings["ComplianceBlurb"];

            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekOfYr.ToString());
            return View("Compliance", vm);
        }

        //Load deployment summary page for specified date and selected branch
        public async Task<ActionResult> GetDeployment(string selectedDate = "Last Week")
        {
            DeploymentViewModel vm = new DeploymentViewModel();

            var weekOfYr = GetWeekNumber(selectedDate);
            var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek().AddDays(-56), DateTime.Now.GetFirstDayOfWeek().AddDays(28));

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                var data = await _dashBoardManager.GetAllChannelDashboardData(System.Web.HttpContext.Current.Session["_ChannelName"].ToString(), weekOfYr);
                vm._dashboardViewCollection = mapper.Map<List<DashBoardView>>(data);
                if (data.Sum(x => x.FinalTarget) == 0)
                {
                    vm.MessageType = MessageType.Warning;
                }
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                var data = await _dashBoardManager.GetAllDivisionDashboardData(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), weekOfYr);
                vm._dashboardViewCollection = mapper.Map<List<DashBoardView>>(data);
                if (data.Sum(x => x.FinalTarget) == 0)
                {
                    vm.MessageType = MessageType.Warning;
                }
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var data = await _dashBoardManager.GetAllRegionDashBoardData((int)System.Web.HttpContext.Current.Session["_RegionNumber"], weekOfYr, weekOfYr);
                vm._dashboardViewCollection = mapper.Map<List<DashBoardView>>(data);
                if (data.Sum(x => x.FinalTarget) == 0)
                {
                    vm.MessageType = MessageType.Warning;
                }
            }
            else
            {
                var data = await _dashBoardManager.GetStoreDashBoardData(StoreNumber, weekOfYr);
                var dailydata = await _storeManager.GetDailyDeployment(StoreNumber, weekOfYr);
                vm._dashboardView = mapper.Map<DashBoardView>(data);
                vm.DailyDetails = Helpers.TransformDeployment(mapper.Map<DailyDeploymentView>(dailydata));
                vm.DailyDetailAdvisory = Helpers.GenerateDeploymentCommentary(mapper.Map<DailyDeploymentView>(dailydata));
                vm.RequiredGraphArray = Helpers.BuildGraphArray(vm.DailyDetails, "required");
                vm.DeployedGraphArray = Helpers.BuildGraphArray(vm.DailyDetails, "deployed");
            }

            vm.GetWeeksOfYear(DateTime.Now.GetFirstDayOfWeek().AddDays(28), weekNumbers);
            vm.PageBlurb = ConfigurationManager.AppSettings["DeploymentBlurb"];

            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekOfYr.ToString());
            return View("Deployment", vm);
        }

        public async Task<ActionResult> Footfall(string selectedYear = null, string selectedWeek = null)
        {
            FootfallVm vm = new FootfallVm();

            if(selectedWeek == null)
            {
                var weekDetail =  _storeManager.GetSingleWeek(DateTime.Now.AddDays(-7));
                selectedYear = weekDetail.ToString().Substring(2, 2);
                selectedYear =  (int.Parse(selectedYear) - 1).ToString() + "/" + selectedYear;
                selectedWeek = weekDetail.ToString().Substring(4, 2);
            }

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                var data = mapper.Map<List<FootfallView>>(await _storeManager.GetDivisionFootfall(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), selectedYear, int.Parse(selectedWeek)));
                if (data.Count() > 0)
                {
                    vm.Populate(data);
                    vm.test = data.GroupBy(x => x.Invoice_Day_Name).Select(x => new { x.Key, Footfall = x.Sum(y => y.Footfall_Volume) }).Select(x => x.Footfall).ToArray();
                }
                else
                {
                    vm.Message = "No data found for the selected period";
                    vm.MessageType = MessageType.Warning;
                }
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var data = mapper.Map<List<FootfallView>>(await _storeManager.GetRegionFootfall(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), selectedYear, int.Parse(selectedWeek)));
                if (data.Count() > 0)
                {
                    vm.Populate(data);
                    vm.test = data.GroupBy(x => x.Invoice_Day_Name).Select(x => new { x.Key, Footfall = x.Sum(y => y.Footfall_Volume) }).Select(x => x.Footfall).ToArray();
                }
                else
                {
                    vm.Message = "No data found for the selected period";
                    vm.MessageType = MessageType.Warning;
                }
            }
            else
            {
                var data = mapper.Map<List<FootfallView>>(await _storeManager.GetBranchFootfall(StoreNumber, selectedYear, int.Parse(selectedWeek)));
                if (data.Count() > 0)
                {
                    vm.Populate(data);
                    vm.test = data.GroupBy(x => x.Invoice_Day_Name).Select(x => new { x.Key, Footfall = x.Sum(y => y.Footfall_Volume) }).Select(x => x.Footfall).ToArray();
                }
                else
                {
                    vm.Message = "No data found for the selected period";
                    vm.MessageType = MessageType.Warning;
                }                
            }

            return View(vm);
        }

        [UserFilter(AccessLevel = "Admin,TPC,RM,DD,RD,BM")]
        public async Task<ActionResult> MyTeam()
        {
            MyTeamViewModel vm = new MyTeamViewModel();
            
            if(System.Web.HttpContext.Current.Session["_ChannelName"] != null)
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
                vm.RegionContractBases = mapper.Map<List<RegionContractBase>>(await _peopleManager.GetRegionContractBase(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString()));
            }
            else
            {
                vm.MyTeam = mapper.Map<List<HrFeedView>>(await _peopleManager.GetStoreStaff(StoreNumber));
                vm.ContractBases = mapper.Map<ContractBaseView>(await _peopleManager.GetContractBase(StoreNumber));
            }

            vm.PageBlurb = ConfigurationManager.AppSettings["MyTeamBlurb"];
            return View(vm);
        }
        
        public async Task<ActionResult> HolidayPlanning(int year = 201800)
        {
            HolidayPlanningVM vm = new HolidayPlanningVM();

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                var storeData = mapper.Map<List<HolidayPlanningStoreBM>>(await _storeManager.GetDivisionHoliday(System.Web.HttpContext.Current.Session["_DivisionName"].ToString()));
                var empData = mapper.Map<List<HolidayPlanningEmpBM>>(await _storeManager.GetDivisionHolidayAll(System.Web.HttpContext.Current.Session["_DivisionName"].ToString()));
                var depData = mapper.Map<List<DashBoardView>>(await _dashBoardManager.GetDivisionDashboardData(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), year + 1, year + 52));

                vm.populate(storeData, empData, 2, GetWeekNumber("Last Week"), depData);
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var storeData = mapper.Map<List<HolidayPlanningStoreBM>>(await _storeManager.GetRegionHoliday(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString()));
                var empData = mapper.Map<List<HolidayPlanningEmpBM>>(await _storeManager.GetRegionHolidayAll(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString()));
                var depData = mapper.Map<List<DashBoardView>>(await _dashBoardManager.GetRegionDashboardData(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), year + 1, year + 52));

                vm.populate(storeData, empData, 1, GetWeekNumber("Last Week"), depData);
            }
            else
            {
                var storeData = mapper.Map<List<HolidayPlanningStoreBM>>(await _storeManager.GetStoreHoliday(_store.CST_CNTR_ID.ToString()));
                var empData = mapper.Map<List<HolidayPlanningEmpBM>>(await _storeManager.GetEmpHoliday(_store.CST_CNTR_ID.ToString()));
                var depData = mapper.Map<List<DashBoardView>>(await _dashBoardManager.GetStoreDashBoardData(_store.CST_CNTR_ID, year + 1, year + 52));

                vm.populate(storeData, empData, 0, GetWeekNumber("Last Week"), depData);                
            }

            return View(vm);
        }

        //Get P&L data for selected period
        [UserFilter(AccessLevel = "Admin,TPC,RM,DD,RD,BM")]
        public async Task<ActionResult> StorePandL(string selectedYear = null, string selectedMonth = null, bool a = false)
        {
            if (!a && (System.Web.HttpContext.Current.Session["_ChannelName"] != null || System.Web.HttpContext.Current.Session["_DivisionName"] != null || System.Web.HttpContext.Current.Session["_RegionNumber"] != null))
            {
                return RedirectToAction("PandLSummary", new { selectedYear = selectedYear, selectedMonth = selectedMonth, a = true });
            }

            StorePandLViewModel vm = new StorePandLViewModel();
            
            //Retrieve most recent published sheet
            if(selectedMonth == null)
            {
                if(System.Web.HttpContext.Current.Session["_ChannelName"] != null)
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetChannelPandL(System.Web.HttpContext.Current.Session["_ChannelName"].ToString()));
                }
                else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetDivisionPandL(System.Web.HttpContext.Current.Session["_DivisionName"].ToString()));
                }
                else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetRegionPandL(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString()));
                }
                else
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetStorePandL(StoreNumber));
                }
            }
            //Retrieve selected sheet
            else
            {
                int month = int.Parse(selectedMonth);
                if(System.Web.HttpContext.Current.Session["_ChannelName"] != null)
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetChannelPandL(System.Web.HttpContext.Current.Session["_ChannelName"].ToString(), selectedYear, month));
                }
                else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetDivisionPandL(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), selectedYear, month));
                }
                else if(System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetRegionPandL(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), selectedYear, month));
                }
                else
                {
                    vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetStorePandL(StoreNumber, selectedYear, month));
                }
            }

            vm.PageBlurb = ConfigurationManager.AppSettings["StorePandLBlurb"];
            vm.SelectedMonth = selectedMonth == null && vm.PandLDetails.Count() >0  ? vm.PandLDetails[0].PeriodMonth.ToString() : selectedMonth;
            vm.SelectedYear = selectedYear == null && vm.PandLDetails.Count() >0 ? vm.PandLDetails[0].PeriodYear : selectedYear;
            
            return View("StorePandL", vm);
        }

        //Get P&L data summary for selected period
        [UserFilter(AccessLevel = "Admin,TPC,RM,DD,RD")]
        public async Task<ActionResult> PandLSummary(string selectedYear = null, string selectedMonth = null, bool a = false)
        {
            if (!a)
            {
                return RedirectToAction("StorePandL", new { selectedYear = selectedYear, selectedMonth = selectedMonth });
            }
            PandLSummaryvm vm = new PandLSummaryvm();
            //Retrieve most recent published sheet
            if (selectedMonth == null)
            {
                if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
                {
                    vm.DetailCollection = mapper.Map<List<PandLSummaryDetail>>(await _storeManager.GetChannelPLSummary(System.Web.HttpContext.Current.Session["_ChannelName"].ToString()));
                    ViewBag.current = "All";
                }
                else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
                {
                    vm.DetailCollection = mapper.Map<List<PandLSummaryDetail>>(await _storeManager.GetDivisionPLSummary(System.Web.HttpContext.Current.Session["_DivisionName"].ToString()));
                    ViewBag.current = System.Web.HttpContext.Current.Session["_DivisionName"].ToString();
                }
                else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
                {
                    vm.DetailCollection = mapper.Map<List<PandLSummaryDetail>>(await _storeManager.GetRegionPLSummary(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString()));
                    ViewBag.current = System.Web.HttpContext.Current.Session["_RegionNumber"].ToString();
                }
            }
            //Retrieve selected sheet
            else
            {
                int month = int.Parse(selectedMonth);
                if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
                {
                    vm.DetailCollection = mapper.Map<List<PandLSummaryDetail>>(await _storeManager.GetChannelPLSummary(System.Web.HttpContext.Current.Session["_ChannelName"].ToString(), selectedYear, month));
                    ViewBag.current = "All";
                }
                else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
                {
                    vm.DetailCollection = mapper.Map<List<PandLSummaryDetail>>(await _storeManager.GetDivisionPLSummary(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), selectedYear, month));
                    ViewBag.current = System.Web.HttpContext.Current.Session["_DivisionName"].ToString();
                }
                else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
                {
                    vm.DetailCollection = mapper.Map<List<PandLSummaryDetail>>(await _storeManager.GetRegionPLSummary(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), selectedYear, month));
                    ViewBag.current = System.Web.HttpContext.Current.Session["_RegionNumber"].ToString();
                }
            }

            vm.SelectedMonth = selectedMonth == null && vm.DetailCollection.Count() > 0 ? vm.DetailCollection[0].Month.ToString() : selectedMonth;
            vm.SelectedYear = selectedYear == null && vm.DetailCollection.Count() > 0 ? vm.DetailCollection[0].Year : selectedYear;
            return View("PandLSummary", vm);
        }

        //Get P&L data for selected period
        [UserFilter(AccessLevel = "Admin,TPC,RM,DD,RD,BM")]
        public async Task<ActionResult> StorePandLselection(int heirarchy, string selection, string selectedYear = null, string selectedMonth = null)
        {
            if(selection == null)
            {
                return RedirectToAction("PandLSummary", new { selectedYear = selectedYear, selectedMonth = selectedMonth, a = true });
            }
            StorePandLViewModel vm = new StorePandLViewModel();

            //Retrieve most recent published sheet
            if (selectedMonth == null)
            {
                switch (heirarchy)
                {
                    case 1:
                        var brStr = selection.Substring(0, selection.IndexOf(' ', 0) - 1);
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetStorePandL(int.Parse(brStr)));
                        break;
                    case 2:
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetRegionPandL(selection));
                        break;
                    case 3:
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetDivisionPandL(selection));
                        break;
                    case 4:
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetChannelPandL(selection));
                        break;
                    default:
                        vm.PandLDetails = new List<PandLView>();
                        break;
                }
            }
            //Retrieve selected sheet
            else
            {
                int month = int.Parse(selectedMonth);
                switch (heirarchy)
                {
                    case 1:
                        var brStr = selection.Substring(0, selection.IndexOf(' ', 0));
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetStorePandL(int.Parse(brStr), selectedYear, month));
                        break;
                    case 2:
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetRegionPandL(selection, selectedYear, month));
                        break;
                    case 3:
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetDivisionPandL(selection, selectedYear, month));
                        break;
                    case 4:
                        vm.PandLDetails = mapper.Map<List<PandLView>>(await _storeManager.GetChannelPandL(selection, selectedYear, month));
                        break;
                    default:
                        vm.PandLDetails = new List<PandLView>();
                        break;
                }
            } 

            vm.PageBlurb = ConfigurationManager.AppSettings["StorePandLBlurb"];
            vm.SelectedMonth = selectedMonth == null && vm.PandLDetails.Count() > 0 ? vm.PandLDetails[0].PeriodMonth.ToString() : selectedMonth;
            vm.SelectedYear = selectedYear == null && vm.PandLDetails.Count() > 0 ? vm.PandLDetails[0].PeriodYear : selectedYear;
            
            if(heirarchy >= 3)
            {
                ViewBag.abbr = 1;
            }

            ViewBag.back = 1;

            switch (heirarchy)
            {
                case 1:
                    ViewBag.header = selection;
                    break;
                case 2:
                    ViewBag.header = "Region " + selection;
                    break;
                case 3:
                    ViewBag.header = "Division " + selection;
                    break;
            }

            return View("StorePandL", vm);
        }

        public FileResult TrainingDoc()
        {
            return File("/Uploads/ProfitLossBriefing.pdf", "application/pdf");
        }

        public FileResult TeamTrainingDoc()
        {
            return File("/Uploads/ContractBases.pdf", "application/pdf");
        }

        public async Task<ActionResult> PunchCompliance(string selectedDate = "Last Week")
        {
            PunchCompVM vm = new PunchCompVM();
            var weekOfYr = GetWeekNumber(selectedDate);

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                var data = await _storeManager.GetChannelPunch(System.Web.HttpContext.Current.Session["_ChannelName"].ToString(), weekOfYr);
                vm.RegionDetail = mapper.Map<List<RegionPunchComplianceItem>>(data);
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                var data = await _storeManager.GetDivisionPunch(System.Web.HttpContext.Current.Session["_DivisionName"].ToString(), weekOfYr);
                vm.RegionDetail = mapper.Map<List<RegionPunchComplianceItem>>(data);
            }
            else if (System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                var data = await _storeManager.GetRegionPunch(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), weekOfYr);
                var bmdata = await _storeManager.GetRegionBMPunch(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString(), weekOfYr);
                vm.RegionDetail = mapper.Map<List<RegionPunchComplianceItem>>(data);
                vm.PunchDetail = mapper.Map<List<PunchCompView>>(bmdata);
            }
            else
            {
                var data = await _storeManager.GetStorePunch(StoreNumber, weekOfYr);
                vm.PunchDetail = mapper.Map<List<PunchCompView>>(data);
            }

            var weekNumbers = await _storeManager.GetWeekNumbers(DateTime.Now.GetFirstDayOfWeek().AddDays(-56), DateTime.Now.GetFirstDayOfWeek().AddDays(-7));

            vm.GetWeeksOfYear(DateTime.Now.GetFirstDayOfWeek().AddDays(-7), weekNumbers);
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekOfYr.ToString());

            return View("PunchCompliance", vm);
        }

        [UserFilter(AccessLevel = "Admin,TPC,RM,DD,RD")]
        public async Task<ActionResult> WeekendWorking()
        {
            WeWorkingVm vm = new WeWorkingVm();

            if (System.Web.HttpContext.Current.Session["_ChannelName"] != null)
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (System.Web.HttpContext.Current.Session["_DivisionName"] != null)
            {
                vm.PopulateHeader(mapper.Map<List<WeWorkingView>>(await _storeManager.GetDivisionBmWeWorking(System.Web.HttpContext.Current.Session["_DivisionName"].ToString())));
                vm.PopulateSecHeader(mapper.Map<List<WeWorkingView>>(await _storeManager.GetChannelBmWeWorking(_store.Channel)));
                vm.DisplayType = 3;
            }
            else if(System.Web.HttpContext.Current.Session["_RegionNumber"] != null)
            {
                vm.PopulateHeader(mapper.Map<List<WeWorkingView>>(await _storeManager.GetBmWeWorking(System.Web.HttpContext.Current.Session["_RegionNumber"].ToString())));
                vm.DisplayType = 2;
            }
            else
            {
                vm.Message = "This page is not available in the currently selected view, please select a region from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
                vm.DisplayType = 1;
            }

            return View(vm);
        }

        private int GetWeekNumber(string selectedDate)
        {
            int weekOfYr;
            if (!int.TryParse(selectedDate, out weekOfYr))
            {
                var dt = GetScheduledWeekDate(selectedDate);
                weekOfYr = (int) _storeManager.GetSingleWeek(dt);
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