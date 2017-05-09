using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class NonScheduledColleguesViewModel : BaseViewModel
    {
        public List<NonScheduledColleagues> NonScheduledColleagues { get; set; }
        public string SelectedDate { get; set; }

    }

    public class NonScheduledColleagues
    {
        public string FullName { get; set; }
        public string PersonNumber { get; set; }

        public double TotalHoursForWeek { get; set; }

        public double ContractedHours { get; set; }

        public string WeekNumber { get; set; }
    }
}
