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
        public CompTimecardDetail Timecard { get; set; }
        public List<PunchCompDetail> Punch { get; set; }
        public List<ShortShiftDetail> SS { get; set; }

        public string SelectedDate { get; set; }

        public void populateClass(List<EmpComplianceDetailView> inputData, DashBoardView dashboardData)
        {
            _dashboardView = dashboardData;

            var tempData = inputData.Where(x => !x.TimecardSignedOff || x.ZeroHourFlag);

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
        
        public void populatePunch(List<PunchCompView> a)
        {
            Punch = new List<PunchCompDetail>();
            var temp = a.GroupBy(x => new { x.FORENAME, x.EMP_NUM }).Select(s => new { EmpNum = s.Key.EMP_NUM, FullName = s.Key.FORENAME, ShiftCount = s.Count(), MissedIn = s.Sum(x => x.Count_In_Missing), MissedOut = s.Sum(x => x.Count_Out_Missing), LateIn = s.Sum(x => x.Clock_In_Not_Acceptable) }).OrderBy(x => x.FullName).ToList();
            foreach(var item in temp)
            {
                Punch.Add(new PunchCompDetail { PersonName = item.FullName, PunchComp = 100-(((decimal)item.MissedIn + item.MissedOut) / item.ShiftCount )*50});
            }
        }

        public void populateSS(List<ShortShiftView> a)
        {
            SS = new List<ShortShiftDetail>();
            var temp = a.GroupBy(x => new { x.PersonName }).Select(s => new { PersonName = s.Key.PersonName, ShortShifts = s.Count() }).OrderBy(x => x.PersonName).ToList();
            foreach(var item in temp)
            {
                SS.Add(new ShortShiftDetail { PersonName = item.PersonName, ShortShifts = item.ShortShifts });
            }
        }
    }

    public class ShortShiftDetail
    {
        public string PersonName { get; set; }
        public int ShortShifts { get; set; }
    }

    public class PunchCompDetail
    {
        public string PersonName { get; set; }
        public decimal PunchComp { get; set; }
    }
}
