using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class StoreReferenceView
    {
        public short Br_ { get; set; }
        public string UK_BR { get; set; }
        public string Store_Name { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public string Region_name { get; set; }
        public string Channel { get; set; }
    }
}