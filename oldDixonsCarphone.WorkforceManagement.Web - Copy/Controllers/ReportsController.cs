using DixonsCarphone.WorkforceManagement.Business.Entities;
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
    public class ReportsController : BaseController
    {
        IDashBoardDataManager _dashBoardManager;
        private List<DashBoardData> _dashBoardData;

        public ReportsController()
        {
            _dashBoardManager = new DashBoardDataManager();
            _store = System.Web.HttpContext.Current.GetSessionObject<Store>("_StoreDetails");
        }

        public ActionResult Index()
        {
            return View("Wfm");
        }

        public async Task<ActionResult> Wfm(string report)
        {
            var model = new StandardReportViewModel { ReportName = report };
            var data = await GetDashData();
            var current = data.FirstOrDefault(x => x.WeekNumber == DateTime.Now.GetWeekOfYear() || x.WeekNumber == 1); //todo

            switch (report.ToLower())
            {
                case "deployment":
                    model.Scores = GetDeploymentScores(current);
                    break;
                case "compliance":
                    model.Scores = GetComplianceData(current);
                    break;
                default:
                    model.Scores = new Dictionary<string, int>();
                    break;
            }

            return View("Wfm", model);
        }

        public ActionResult Footfall()
        {
            return View("Footfall");
        }

        public async Task<ActionResult> DashboardData()
        {
            var dashData = await GetDashData();
            var current = dashData.FirstOrDefault(x => x.WeekNumber == DateTime.Now.GetWeekOfYear() || x.WeekNumber == 1); //todo
            var model = new List<KpiViewModel>
            {
                new KpiViewModel { Kpi = "Contract Hours", Current = current.FinalTarget, Target = current.OriginalTarget, WeekNumber = (int)current.WeekNumber, Year = current.Year },
                new KpiViewModel { Kpi = "Weekly Spend", Current = 45, Target = 100, WeekNumber = (int)current.WeekNumber, Year = current.Year },
                new KpiViewModel { Kpi = "Compliance Score", Current = current.ComplianceScore, Target = current.SOHUtilization, WeekNumber = (int)current.WeekNumber, Year = current.Year },
                new KpiViewModel { Kpi = "Deployment Score", Current = 55, Target = 100, WeekNumber = (int)current.WeekNumber, Year = current.Year }

            };

            return View("DashBoardData", model);
        }

        public ActionResult KeyPerformace()
        {
            return View("Kpis");
        }

        public ActionResult Schedules()
        {
            return View("Schedules");
        }

        public ActionResult StorePandL()
        {
            return View("StorePandL");
        }

        public ActionResult Cgm()
        {
            return View("Cgm");
        }

        private async Task<List<DashBoardData>> GetDashData()
        {
            return await _dashBoardManager.GetStoreDashBoardData(StoreNumber);
        }

        private Dictionary<string, int> GetDeploymentScores(DashBoardData data)
        {
            var dict = new Dictionary<string, int>()
            {
                {"Current Contract Hours", (int)data.ContractHours.GetValueOrDefault() },
                {"Contract Base", (int)data.ContractBaseTarget.GetValueOrDefault() },
                {"SOH Target", (int)data.FinalTarget.GetValueOrDefault() },
                {"SOH Spend", (int)data.SOH.GetValueOrDefault() },
                {"Deployment Score", (int)data.SOHUtilization.GetValueOrDefault() },
            };

            return dict;
        }

        private Dictionary<string, int> GetComplianceData(DashBoardData data)
        {
            var dict = new Dictionary<string, int>()
            {
                {"SOH Spend Over Target", (int)(data.SOH.GetValueOrDefault() - data.FinalTarget.GetValueOrDefault())  },
                {"Current Contract Over Base", (int)(data.ContractHours.GetValueOrDefault() - data.ContractBaseTarget.GetValueOrDefault()) },
                {"Paid for Hrs Not Worked", (int)data.LeakageCount.GetValueOrDefault() },
                {"Pay Corrections", (int)data.SOH.GetValueOrDefault() },
                {"Deployment Score", (int)data.PayrollCorrections.GetValueOrDefault() },
                {"Missing & Zero Hour Time Cards", 5 }, // (int)(data.ZeroHour.GetValueOrDefault() - data.ContractBaseTarget.GetValueOrDefault()) },// to do
                {"Overall Compliance", (int)data.ComplianceScore.GetValueOrDefault() },
            };

            return dict;
        }
    }
}