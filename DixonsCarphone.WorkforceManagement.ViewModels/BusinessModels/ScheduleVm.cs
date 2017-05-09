using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ScheduleVm : BaseViewModel
    {
        public List<ScheduleDetail> ScheduleCollection { get; set; }
        public string SelectedDate { get; set; }
        public DateTime WeekStart { get; set; }
    }
}
