using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class KpiViewModel : BaseViewModel
    {
        public string Kpi { get; set; }
        public double? Current { get; set; }
        public double? Target { get; set; }
        public int WeekNumber { get; set; }
        public string Year { get; set; }

        public int Score
        {
            get
            {
                return Current.HasValue && Target.HasValue ? (int)(Current / Target) * 100 : 0;
            }
        }
    }
}
