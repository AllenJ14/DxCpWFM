using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PunchExceptionsView
    {
        public string RegionNo { get; set; }
        public int CST_CNTR_ID { get; set; }
        public string StoreName { get; set; }
        public string FORENAME { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public decimal Compliance { get; set; }
    }
}
