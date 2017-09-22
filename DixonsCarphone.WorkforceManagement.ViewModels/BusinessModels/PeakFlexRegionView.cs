using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeakFlexRegionView
    {
        public string RegionNo { get; set; }
        public string Branch { get; set; }
        public Nullable<double> ContractHrs { get; set; }
        public Nullable<int> Headcount { get; set; }
        public Nullable<double> HighestPeak { get; set; }
        public Nullable<double> AvgFlex { get; set; }
        public Nullable<int> Temps { get; set; }
        public Nullable<int> TempsOption { get; set; }
        public int TempVacancies { get; set; }

        public double AvgContract { get
            {
                double contract = ContractHrs.HasValue ? (double)ContractHrs : 0.0;
                return Math.Round(contract / (int)Headcount, 1);
            } }
    }
}
