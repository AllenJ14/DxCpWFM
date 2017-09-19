using System.Collections.Generic;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ColleaguePayDataVm
    {
        public List<PayCalendarDateView> payDates { get; set; }
        public List<ColleaguePayDataView> payData { get; set; }
        public List<TimesheetView> tSheet { get; set; }

        public bool errorPayroll { get; set; }
    }
}
