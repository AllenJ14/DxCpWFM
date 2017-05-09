using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class DailyDeploymentArrayForm
    {
        public string DayName { get; set; }
        public decimal Required { get; set; }
        public decimal Deployed { get; set; }
    }
}
