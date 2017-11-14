using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeriodDashView
    {
        public string StoreFlag { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public string Year { get; set; }
        public Nullable<byte> Period { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<double> SOH { get; set; }
        public Nullable<double> FinalTarget { get; set; }
        public Nullable<double> VarToTar { get; set; }
        public Nullable<double> TCSignOff { get; set; }
        public Nullable<int> CountNonComp { get; set; }
        public Nullable<int> AllStores { get; set; }

        public int lvl { get
            {
                return Region != null ? 1 : (Division != null ? 2 : 3);
            } }
    }
}
