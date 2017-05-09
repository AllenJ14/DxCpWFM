using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    class StoreOpeningTimesViewModel : BaseViewModel
    {
        public StoreOpeningTimeView CurrentTime { get; set; }
        public List<StoreOpeningTimeView> PendingChange { get; set; }
        public List<StoreOpeningTimeView> PeakTimes { get; set; }
    }
}
