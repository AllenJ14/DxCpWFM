using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ComplianceViewModel : BaseViewModel
    {
        public DashBoardView _dashboardView { get; set; }
        public List<DashBoardView> _dashboardViewCollection { get; set; }
        public List<DashboardViewChannel> _dashboardViewCollectionChannel { get; set; }
        public CompOverTargetDetail OverTarget { get; set; }
        public CompOverContractDetail OverContract { get; set; }
        public CompFTLeakageDetail FTLeakage { get; set; }
        public CompTimecardDetail Timecard { get; set; }

        public string SelectedDate { get; set; }

        public void populateClass(List<EmpComplianceDetailView> inputData, DashBoardView dashboardData)
        {
            _dashboardView = dashboardData;

            OverTarget = new CompOverTargetDetail
            {
                SOHSpend = (decimal)dashboardData.SOH,
                SOHTarget = (decimal)dashboardData.FinalTarget
            };

            OverContract = new CompOverContractDetail
            {
                ContractBase = (decimal)dashboardData.ContractBaseTarget,
                CurrentContract = (decimal)dashboardData.ContractHours
            };

            var tempData = inputData.Where(x => x.FTLeakageFlag);

            FTLeakage = new CompFTLeakageDetail();
            FTLeakage.HeadlineFigure = tempData.Count().ToString();
            foreach(var item in tempData)
            {
                FTLeakage.FTLeakageDetail.Add(new FTLeakage
                {
                    empName = item.PersonName,
                    empNumber = item.PersonNumber,
                    ContractHours = item.EmployeeStandardHours,
                    WorkedHours = item.WorkedHours
                });
            }

            tempData = inputData.Where(x => !x.TimecardSignedOff || x.ZeroHourFlag);

            Timecard = new CompTimecardDetail();
            Timecard.HeadlineFigure = tempData.Count().ToString();
            foreach(var item in tempData)
            {
                Timecard.TimecardDetail.Add(new TimecardEntry
                {
                    empName = item.PersonName,
                    empNumber = item.PersonNumber,
                    type = !item.TimecardSignedOff ? "Not submitted" : "Zero hours"
                });
            }
        }
        
    }

}
