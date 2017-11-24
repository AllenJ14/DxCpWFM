using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeakHCView
    {
        public string Channel { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public Nullable<short> Branch { get; set; }
        public string Store_Name { get; set; }
        public System.DateTime Date { get; set; }
        public int MaxHC { get; set; }
        public int DeployedHC { get; set; }
        public int Closed { get; set; }

        public string FullStore { get
            {
                return Branch.ToString() + " - " + Store_Name;
            } }
    }
}
