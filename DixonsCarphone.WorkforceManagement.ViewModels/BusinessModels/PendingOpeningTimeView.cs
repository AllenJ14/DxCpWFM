using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PendingOpeningTimeView
    {
        public long PendingOpenTimeId { get; set; }
        public int StoreOpenTimeId { get; set; }
        public int? Week_Commence { get; set; }
        public int? SUN_OPEN { get; set; }
        public int? SUN_CLOSE { get; set; }
        public int? MON_OPEN { get; set; }
        public int? MON_CLOSE { get; set; }
        public int? TUE_OPEN { get; set; }
        public int? TUE_CLOSE { get; set; }
        public int? WED_OPEN { get; set; }
        public int? WED_CLOSE { get; set; }
        public int? THU_OPEN { get; set; }
        public int? THU_CLOSE { get; set; }
        public int? FRI_OPEN { get; set; }
        public int? FRI_CLOSE { get; set; }
        public int? SAT_OPEN { get; set; }
        public int? SAT_CLOSE { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        public string ReasonRejected { get; set; }
        public int? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public DateTime? DateSubmitted { get; set; }
    }
}
