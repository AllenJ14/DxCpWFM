using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PunchCompVM : BaseViewModel
    {
        public List<PunchCompView> PunchDetail { get; set; }
        public List<RegionPunchComplianceItem> RegionDetail { get; set; }
        public string SelectedDate { get; set; }
    }
}
