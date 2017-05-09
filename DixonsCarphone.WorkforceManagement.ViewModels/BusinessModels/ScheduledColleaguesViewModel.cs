using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ScheduledColleaguesViewModel : BaseViewModel
    {
        public List<ScheduledCollegues> ScheduledStaff { get; set; }

        public string SelectedDate { get; set; }
    }
}
