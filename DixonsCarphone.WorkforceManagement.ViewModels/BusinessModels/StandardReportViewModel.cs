using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class StandardReportViewModel : BaseViewModel
    {
        public string ReportName { get; set; }
        public Dictionary<string, int> Scores { get; set; }

        public string SelectedDate { get; set; }
    }
}
