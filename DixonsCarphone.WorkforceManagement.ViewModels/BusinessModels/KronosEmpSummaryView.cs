using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class KronosEmpSummaryView
    {
        public Nullable<short> HomeBranch { get; set; }
        public Nullable<short> Region { get; set; }
        public string Division { get; set; }
        public string Channel { get; set; }
        public string PersonNumber { get; set; }
        public Nullable<decimal> EmployeeStandardHours { get; set; }
        public Nullable<System.DateTime> SignedOffThrough { get; set; }
        public string JobRole { get; set; }
        public string PersonName { get; set; }
        public bool KronosUser { get; set; }
        public string BranchName { get; set; }
    }
}
