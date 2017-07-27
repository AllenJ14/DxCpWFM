using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FootfallView
    {
        public string Key { get; set; }
        public string Invoice_Group_Year_Name { get; set; }
        public Nullable<short> Invoice_Group_Week_Number { get; set; }
        public string Invoice_Day_Name { get; set; }
        public Nullable<short> Branch_Number { get; set; }
        public Nullable<short> Hour_In_Day_24 { get; set; }
        public Nullable<short> Footfall_Volume { get; set; }
    }
}