using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RegionPunchComplianceItem
    {
        public string Name { get; set; }
        public Nullable<int> ShiftCount { get; set; }
        public Nullable<short> PunchCompliance { get; set; }
        public Nullable<int> MissedIn { get; set; }
        public Nullable<int> MissedOut { get; set; }
        public Nullable<int> LateIn { get; set; }
        public Nullable<short> Lateness { get; set; }
    }
}
