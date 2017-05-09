using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class DayOpeningTimes : BaseViewModel
    {
        public DayOfWeek Day { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }

    }
}
