using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TimecardSignOffVm
    {
        public List<TimesheetView> ts { get; set; }
        public List<HyperFindResultView> hf { get; set; }

        public DateTime weekStart { get; set; }
    }
}
