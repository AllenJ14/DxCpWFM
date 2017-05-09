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
        IKronosManager _kronosManager;

        public ReportsController()
        {
            _dashBoardManager = new DashBoardDataManager();
            _peopleManager = new PeopleManager();
            _kronosManager = new KronosManager(isOffice);
        }

        public ActionResult Index()
        {
            return View("Wfm");
        }

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

        //public async Task<ActionResult> WfmForWeek(StandardReportViewModel vm)
        //{
        //    DateTime dt;
        //    if (DateTime.TryParse(vm.SelectedDate, out dt))
        //    {
        //        var weekOfYear = dt.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);

        //        var data = await GetDashData(weekOfYear);
        //        var dashData = data.FirstOrDefault(x => x.WeekNumber == weekOfYear) ?? new DashBoardData(); //todo

        //        var model = GetReportData(vm.ReportName, dashData);
        //        return View("Wfm", model);
        //    }
        //    else
        //        return View("Wfm", new StandardReportViewModel { ReportName = vm.ReportName });
        //}

        //public async Task<ActionResult> Schedules(string selectedDate)
        //{
        //    var ls = new List<ScheduledCollegues>();
        //    DateTime dt = DateTime.MinValue;

        //    if (string.IsNullOrEmpty(selectedDate))
        //        dt = DateTime.Now.GetFirstDayOfWeek();
        //    else
        //    {
        //        var dtString = Helpers.GetFinancialWeeks().FirstOrDefault(x => x.Value == selectedDate)?.Text;
        //        if (!string.IsNullOrEmpty(dtString))
        //        {
        //            dtString = dtString.Split(' ').LastOrDefault()?.Replace(")", "")?.Trim(); // dtString.Substring(dtString.IndexOf("wc") + 1, dtString.Length - 1)?.Trim();
        //            DateTime.TryParse(dtString, out dt);
        //        }

        //    }

        //    var firstDate = dt.GetFirstDayOfWeek();
        //    ls = await GetKronosData(firstDate, firstDate.AddDays(6));
        //    var vm = new ScheduledColleaguesViewModel { ScheduledStaff = ls, PageBlurb = ConfigurationManager.AppSettings["MyScheduledBlurb"] };

        //    return View(vm);
        //}

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
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else
            {
                vm.MyTeam = mapper.Map<List<HrFeedView>>(await _peopleManager.GetStoreStaff(StoreNumber));
            }

            vm.PageBlurb = ConfigurationManager.AppSettings["MyTeamBlurb"];
            return View(vm);
        }

        //Get P&L data for selected period
        [UserFilter(AccessLevel = "Admin,RM,DD,RD,BM")]
        public async Task<ActionResult> StorePandL(string selectedYear = null, string selectedMonth = null)
        {
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
                vm.RegionDetail = mapper.Map<List<RegionPunchComplianceItem>>(data);
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

        //public async Task<ActionResult> MyTargets(string selectedMonth = null)
        //{
        //    var selectedYear =  DateTime.Now.Year.ToString();
        //    selectedMonth = !string.IsNullOrEmpty(selectedMonth) ? selectedMonth : DateTime.Now.Month.ToString();

        //    int month = int.Parse(selectedMonth);

        //    var result = await _storeManager.GetStorePandL(StoreNumber, selectedYear, month);
        //    var vm = new MyTargetsViewModel { PageBlurb = ConfigurationManager.AppSettings["MyTargetsBlurb"], SelectedMonth = selectedMonth, PandLBudgetTargets = new Dictionary<string, decimal>(), SohBudgets = new Dictionary<int, double?>() };

        //    var startWeek = vm.WeeksInMonth.OrderBy(x => x).FirstOrDefault().GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);
        //    var endWeek = vm.WeeksInMonth.OrderByDescending(x => x).FirstOrDefault().GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);

        //    var dashData = await _dashBoardManager.GetStoreDashBoardData(StoreNumber, startWeek, endWeek);

        //    for (var i = 0; i < vm.WeeksInMonth.Count; i++)
        //    {
        //        vm.SohBudgets.Add(i, dashData.FirstOrDefault(x => x.WeekNumber == startWeek + i)?.FinalTarget.GetValueOrDefault());
        //    }

        //    if (result != null)
        //    {
        //        foreach (var pAndl in result?.GroupBy(x => x.AccountEntrySubTypeId).Select(x => x.FirstOrDefault()))
        //        {
        //            var total = result.Where(x => x.AccountEntrySubTypeId == pAndl.AccountEntrySubTypeId).Sum(x => x.BudgetAmount).GetValueOrDefault();
        //            vm.PandLBudgetTargets.Add(pAndl.AccountEntrySubTypeText, total);
        //        }
        //    }

        //    return View("MyTargets", vm);
        //}

        //public async Task<ActionResult> SohSpend()
        //{
        //    var wk = DateTime.Now.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);
        //    var startWk = wk - 8;
        //    var endWk = wk + 5;
        //    var data = await _dashBoardManager.GetStoreDashBoardData(StoreNumber, startWk, endWk);
        //    var hr = await _peopleManager.GetStoreStaff(StoreNumber);

        //    var hrManagement = hr.Where(x => x.ROLE.ToLower().Contains("manager")).ToList();
        //    var hrSales = hr.Where(x => x.ROLE.ToLower().Contains("customer consultant") || x.ROLE.ToLower().Contains("greeter")).ToList();
        //    var hrGeekSquad = hr.Where(x => x.ROLE.ToLower().Contains("specialist")).ToList();
        //    var hrEngineer = hr.Where(x => x.ROLE.ToLower().Contains("engineer")).ToList();
        //    var hrSat = hr.Where(x => x.ROLE.ToLower().Contains("trainer")).ToList();

        //    var vm = new SohSpendViewModel { PageBlurb = ConfigurationManager.AppSettings["SohSpendBlurb"] };
        //    var currWk = wk.ToString().Substring(4);

        //    var currentWkStart = DateTime.Now.GetFirstDayOfWeek();
        //    var kronos = await GetKronosData(currentWkStart, currentWkStart.AddDays(28));

        //    double lvSoh;
        //    double lv2Soh;
        //    double satSoh;

        //    for (var i = 0; i < 13; i++)
        //    {

        //        double? spend = null;
        //        double? geekSquad = null;
        //        double? l1 = null;
        //        double? l2 = null;
        //        double? sales = null;
        //        double? salesFt = null;
        //        double? salesPt = null;
        //        double? management = null;
        //        double? sat = null;

        //        var wkNum = startWk + i;
        //        var dashBoard = data.FirstOrDefault(x => x.WeekNumber.GetValueOrDefault() == wkNum);

        //        vm.SohBudgets.Add(wkNum, dashBoard == null ? null :  (double?)dashBoard.SOH.GetValueOrDefault());

        //        if (wkNum < wk)
        //        {
        //            spend = dashBoard == null ? null : (double?)dashBoard.SOHUtilization.GetValueOrDefault();
        //            geekSquad = dashBoard == null ? null : (double?)dashBoard.GSSOHSpend.GetValueOrDefault();
        //            l1 = dashBoard == null ? null : (double?)dashBoard.ServiceLV1SOH.GetValueOrDefault();
        //            l2 = dashBoard == null ? null : (double?)dashBoard.ServiceLV2SOH.GetValueOrDefault();
        //            sales = dashBoard == null ? null : (double?)dashBoard.SOH.GetValueOrDefault();
        //            salesFt = dashBoard == null ? null : (double?)dashBoard.SOH.GetValueOrDefault();
        //            salesPt = dashBoard == null ? null : (double?)dashBoard.SOH.GetValueOrDefault();
        //            management = dashBoard == null ? null : (double?)dashBoard.SOH.GetValueOrDefault();
        //            sat = dashBoard == null ? null : (double?)dashBoard.SATSOH.GetValueOrDefault();
        //        }
        //        else // kronos
        //        {
        //            var salesList = kronos.Where(k => hrSales.Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).ToList();

        //            geekSquad = kronos.Where(k => hrGeekSquad.Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).Sum((x => Utils.CalculateHours(x.EndTime, x.StartTime)));
        //            l1 = kronos.Where(k => hrEngineer.Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).Sum((x => Utils.CalculateHours(x.EndTime, x.StartTime)));
        //            management = kronos.Where(k => hrManagement.Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).Sum((x => Utils.CalculateHours(x.EndTime, x.StartTime)));
        //            sat = kronos.Where(k => hrSat.Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).Sum((x => Utils.CalculateHours(x.EndTime, x.StartTime)));

        //            sales = salesList.Sum((x => Utils.CalculateHours(x.EndTime, x.StartTime)));

        //            salesFt = kronos.Where(k => hrSales.Where(x => x.CONTRACT_HOURS >= 45).Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).Sum(x => Utils.CalculateHours(x.EndTime, x.StartTime));
        //            salesPt = kronos.Where(k => hrSales.Where(x => x.CONTRACT_HOURS < 45).Select(x => string.Format("UK{0}", x.EMP_NUM.GetValueOrDefault().ToString().PadLeft(6, '0'))).Contains(k.PersonNumber)).Sum(x => Utils.CalculateHours(x.EndTime, x.StartTime));
        //            l2 = l1;

        //            var tot = kronos.Where(x => x.ScheduledDate.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]) == wkNum)
        //                .Sum(x => Utils.CalculateHours(x.EndTime, x.StartTime));

        //            spend = (double?)tot;
        //        }

        //        vm.SohSpends.Add(wkNum, spend);
        //        vm.GeekSquad.Add(wkNum, geekSquad);
        //        vm.L1Engineer.Add(wkNum, l1);
        //        vm.L2Engineer.Add(wkNum, l2);
        //        vm.Sales.Add(wkNum, sales);
        //        vm.SalesFt.Add(wkNum, salesFt);
        //        vm.SalesPt.Add(wkNum, salesPt);
        //        vm.Management.Add(wkNum, dashBoard == null ? null : (double?)dashBoard.SOH.GetValueOrDefault());
        //        vm.SmartAcademy.Add(wkNum, sat);
        //    }

        //    return View("SohSpend", vm);
        //}

        //public ActionResult Footfall()
        //{
        //    return View("Footfall");
        //}


        //public async Task<ActionResult> DashboardData()
        //{
        //    var weekOfYear = DateTime.Now.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);
        //    var dashData = await GetDashData(weekOfYear);
        //    var data = dashData.FirstOrDefault(); //todo
        //    var model = data != null ? new List<KpiViewModel>
        //    {
        //        new KpiViewModel { Kpi = "Contract Hours", Current = data.FinalTarget, Target = data.OriginalTarget, WeekNumber = (int)data.WeekNumber, Year = data.Year },
        //        new KpiViewModel { Kpi = "Weekly Spend", Current = 45, Target = 100, WeekNumber = (int)data.WeekNumber, Year = data.Year },
        //        new KpiViewModel { Kpi = "Compliance Score", Current = data.ComplianceScore, Target = data.SOHUtilization, WeekNumber = (int)data.WeekNumber, Year = data.Year },
        //        new KpiViewModel { Kpi = "Deployment Score", Current = 55, Target = 100, WeekNumber = (int)data.WeekNumber, Year = data.Year }

        //    } : new List<KpiViewModel>();

        //    return View("DashBoardData", model);
        //}

        //public async Task<ActionResult> GetNonScheduled()
        //{
        //    var dt = DateTime.Now.GetFirstDayOfWeek();
        //    var staff = await _peopleManager.GetStoreStaff(StoreNumber);

        //    var kronos = await GetKronosData(dt, dt.AddDays(13));

        //    var nonScheduled = GetNonScheduledCollegues(kronos, staff);

        //    var vm = nonScheduled;
        //    //vm.SelectedDate = selectedDate;
        //    vm.PageBlurb = ConfigurationManager.AppSettings["NonScheduledBlurb"];

        //    return View("NonScheduledColleagues", vm);

        //}

        //public ActionResult KeyPerformace()
        //{
        //    return View("Kpis");
        //}

        //[RoleFilter(AccessLevel = "BranchManagerGroup, RegionalManagerGroup")]
        //public ActionResult Cgm()
        //{
        //    return View("Cgm");
        //}

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

        //private List<DeploymentDetail> CreateWeeklyDetails(WeeklyDeployment weeklyData)
        //{
        //    var ls = new List<DeploymentDetail>();
        //    if (weeklyData == null) return ls;

        //    ls.Add(new DeploymentDetail { Spend = weeklyData.SundaySpend.GetValueOrDefault() * 100, Target = weeklyData.SundayReq.GetValueOrDefault() * 100, WeekNumber = "Sunday" });
        //    ls.Add(new DeploymentDetail { Spend = weeklyData.MondaySpend.GetValueOrDefault() * 100, Target = weeklyData.MondayReq.GetValueOrDefault() * 100, WeekNumber = "Monday" });
        //    ls.Add(new DeploymentDetail { Spend = weeklyData.TuesdaySpend.GetValueOrDefault() * 100, Target = weeklyData.TuesdayReq.GetValueOrDefault() * 100, WeekNumber = "Tuesday" });
        //    ls.Add(new DeploymentDetail { Spend = weeklyData.WednesdaySpend.GetValueOrDefault() * 100, Target = weeklyData.WednesdayReq.GetValueOrDefault() * 100, WeekNumber = "Wednesday" });
        //    ls.Add(new DeploymentDetail { Spend = weeklyData.ThursdaySpend.GetValueOrDefault() * 100, Target = weeklyData.ThursdayReq.GetValueOrDefault() * 100, WeekNumber = "Thursday" });
        //    ls.Add(new DeploymentDetail { Spend = weeklyData.FridaySpend.GetValueOrDefault() * 100, Target = weeklyData.FridayReq.GetValueOrDefault() * 100, WeekNumber = "Friday" });
        //    ls.Add(new DeploymentDetail { Spend = weeklyData.SaturdaySpend.GetValueOrDefault() * 100, Target = weeklyData.SaturdayReq.GetValueOrDefault() * 100, WeekNumber = "Saturday" });

        //    return ls;
        //}

        private static DateTime GetScheduledWeekDate(string scheduledWeek)
        {
            DateTime dt = DateTime.Now;
            if (string.IsNullOrEmpty(scheduledWeek)) return dt;
            switch (scheduledWeek.ToLower())
            {
                case "current week":
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

        //private NonScheduledColleguesViewModel GetNonScheduledCollegues(List<ScheduledCollegues> kronosData, List<HrFeed> staff)
        //{
        //    return Utils.GetNonScheduledCollegues(kronosData, staff);
        //}

        //private StandardReportViewModel GetReportData(string report, DashBoardData data)
        //{
        //    var model = new StandardReportViewModel { ReportName = report };

        //    switch (report.ToLower())
        //    {
        //        case "deployment":
        //            model.Scores = GetDeploymentScores(data);
        //            model.PageBlurb = ConfigurationManager.AppSettings["DeploymentBlurb"];
        //            break;
        //        case "compliance":
        //            model.Scores = GetComplianceData(data);
        //            model.PageBlurb = ConfigurationManager.AppSettings["ComplianceBlurb"];
        //            break;
        //        default:
        //            model.Scores = new Dictionary<string, int>();
        //            break;
        //    }

        //    return model;
        //}


        //private async Task<List<ScheduledCollegues>> GetKronosData(DateTime startDate, DateTime endDate)
        //{
        //    var data =  await Utils.GetKronosData(startDate, endDate, _store.KronosStoreName, isOffice);

        //    return data;
        //}

        //private Dictionary<string, int> GetDeploymentScores(DashBoardData data)
        //{
        //    var dict = new Dictionary<string, int>()
        //    {
        //        {"Current Contract Hours", (int)data?.ContractHours.GetValueOrDefault() },
        //        {"Contract Base", (int)data?.ContractBaseTarget.GetValueOrDefault() },
        //        {"SOH Target", (int)data?.SOH.GetValueOrDefault() },
        //        {"SOH Spend", (int)data?.FinalTarget.GetValueOrDefault() },
        //        {"Deployment Score", (int)data?.SOHUtilization.GetValueOrDefault() },
        //    };

        //    return dict;
        //}

        //private Dictionary<string, int> GetComplianceData(DashBoardData data)
        //{
        //    var dict = new Dictionary<string, int>()
        //    {
        //        {"SOH Spend Over Target", (int)(data?.SOH.GetValueOrDefault() - data.FinalTarget.GetValueOrDefault())  },
        //        {"Current Contract Over Base", (int)(data?.ContractHours.GetValueOrDefault() - data.ContractBaseTarget.GetValueOrDefault()) },
        //        {"Paid for Hrs Not Worked", (int)data?.LeakageCount.GetValueOrDefault() },
        //        {"Pay Corrections", (int)data?.SOH.GetValueOrDefault() },
        //        {"Deployment Score", (int)data?.PayrollCorrections.GetValueOrDefault() },
        //        {"Missing & Zero Hour Time Cards", 5 }, // (int)(data.ZeroHour.GetValueOrDefault() - data.ContractBaseTarget.GetValueOrDefault()) },// to do
        //        {"Overall Compliance", (int)data?.ComplianceScore.GetValueOrDefault() },
        //    };

        //    return dict;
        //}
    }
}