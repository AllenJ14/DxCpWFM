using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeakFlexDivisionView
    {
        public string Division { get; set; }
        public string RegionNo { get; set; }
        public Nullable<int> HighMix { get; set; }
        public Nullable<int> HealthyMix { get; set; }
        public Nullable<int> LowMix { get; set; }
        public Nullable<int> Temps { get; set; }
        public Nullable<int> TempsOption { get; set; }
        public Nullable<int> TempVacancies { get; set; }

        public int TempTotal { get
            {
                return (int)TempsOption + (int)Temps;
            } }
    }
}
