using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;

namespace DixonsCarphone.WorkforceManagement.ViewModels.Model
{
    public class StoreDetailViewModel : BaseViewModel
    {
        public StoreOpeningTimeView CurrentTime { get; set; }
        public List<StoreOpeningTimeView> PendingTimes { get; set; } = new List<StoreOpeningTimeView>();
        public List<StoreOpeningTimeView> PeakTimes { get; set; } = new List<StoreOpeningTimeView>();
    }
}
