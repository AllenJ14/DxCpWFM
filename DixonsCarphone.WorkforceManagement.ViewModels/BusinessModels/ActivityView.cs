using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ActivityView
    {
        public long Activityid { get; set; }
        public string ActivityName { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? ActivityTypeId { get; set; }

        public virtual ActivityTypeView ActivityType { get; set; }
    }

    public class ActivityTypeView
    {
        public int ActivityTypeId { get; set; }
        public string ActivityType1 { get; set; }
    }
}