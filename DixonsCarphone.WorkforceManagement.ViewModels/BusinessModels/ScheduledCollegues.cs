using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ScheduledCollegues
    {
        public string FullName { get; set; }
        public string PersonNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string BranchAssigned { get; set; }

        public bool IsAbsent { get; set; }
        public string AbscenceAmountInTime { get; set; }

    }
}
