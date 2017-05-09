using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.Model
{
    public class HomeViewModel : BaseViewModel
    {
        public Dictionary<string, List<ScoresAndUtilizationViewModel>> Scores { get; set; }

        public List<ActivityView> Activities { get; set; }
    }
}
