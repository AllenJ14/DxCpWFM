using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ScheduleBMView
    {
        public int EntryId { get; set; }
        public string FullBranch { get; set; }
        public Nullable<short> HomeBranch { get; set; }
        public Nullable<int> PersonNumber { get; set; }
        public Nullable<decimal> ContractHours { get; set; }
        public string FullName { get; set; }
        public string ShiftType { get; set; }
        public string TransferBranch { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<byte> DayNum { get; set; }
        public Nullable<decimal> PersonWeekTotal { get; set; }
        public Nullable<decimal> DayWeekTotal { get; set; }
        public Nullable<System.DateTime> DateTimeUpdated { get; set; }
        public Nullable<short> JobCode { get; set; }
    }
}
