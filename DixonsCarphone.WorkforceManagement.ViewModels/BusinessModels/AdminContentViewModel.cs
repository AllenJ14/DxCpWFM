using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class AdminContentViewModel : BaseViewModel
    {
        public ActivityAdminViewModel Activities = new ActivityAdminViewModel();

        public List<PendingOpeningTimeView> PendingOpeningTimes { get; set; }
    }
}
