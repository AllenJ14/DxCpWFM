using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PunchTrendView
    {
        public string RegionNo { get; set; }
        public int CST_CNTR_ID { get; set; }
        public string StoreName { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<decimal> Compliance { get; set; }
    }
}
