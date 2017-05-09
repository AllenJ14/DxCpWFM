using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System.Collections.Generic;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class AdminOpeningTimes
    {
        public List<StoreHoursForApproval> PendingApproval { get; set; }

        public StoreOpeningTimeView QueriedRecord { get; set; }
    }
}
