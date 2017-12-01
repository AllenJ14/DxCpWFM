using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeriodCompView
    {
        public string StoreFlag { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public string Year { get; set; }
        public Nullable<byte> Period { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<int> BranchCount { get; set; }
        public Nullable<int> Compliant { get; set; }
        public Nullable<int> TCComp { get; set; }
        public Nullable<int> TimecardsMissing { get; set; }
        public Nullable<int> TotalHC { get; set; }
        public Nullable<int> ZeroHour { get; set; }
        public Nullable<int> PunchCompliance { get; set; }
        public Nullable<int> ShortShiftCompliance { get; set; }
        public Nullable<int> ShortShifts { get; set; }

        public int lvl
        {
            get
            {
                return Region != null ? 1 : (Division != null ? 2 : 3);
            }
        }

        public string fullName { get
            {
                return string.Format("{0} - {1}", BranchCount, StoreFlag);
            } }

        public int CompPerc { get
            {
                return (int)(Math.Round((double)Compliant / (double)BranchCount * 100,0));
            } }

        public int ClockPerc
        {
            get
            {
                return (int)(Math.Round((double)PunchCompliance / (double)BranchCount * 100, 0));
            }
        }

        public int TCPerc
        {
            get
            {
                return (int)(Math.Round((double)TCComp / (double)BranchCount * 100, 0));
            }
        }

        public int SSPerc
        {
            get
            {
                return (int)(Math.Round((double)ShortShiftCompliance / (double)BranchCount * 100, 0));
            }
        }

        public int wk { get
            {
                return WeekNumber ?? 999999;
            } }
    }
}
