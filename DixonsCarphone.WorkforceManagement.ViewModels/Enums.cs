using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels
{
    public enum ActivityType
    {
        Task = 1,
        Event = 2,
        Notification = 3,
        Announcement = 4,
        News = 5,
        Actions = 6
    }

    public enum PriorityType
    {
        High = 1, 
        Medium = 2,
        Low = 3
    }

    public enum AccountEntryType
    {
        Asset = 1,
        Liability = 2
    }

    public enum RegionOrder
    {
        SAS = 0, SIS = 1, ROI = 2
    }
}
