//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DixonsCarphone.WorkforceManagement.Business.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public long Activityid { get; set; }
        public string ActivityName { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public Nullable<System.DateTime> ActivityDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Nullable<int> ActivityTypeId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<int> StoreNumber { get; set; }
        public Nullable<short> PriorityTypeId { get; set; }
    
        public virtual ActivityType ActivityType { get; set; }
        public virtual PriorityType PriorityType { get; set; }
    }
}