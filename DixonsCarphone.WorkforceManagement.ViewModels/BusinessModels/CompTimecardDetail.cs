using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class CompTimecardDetail 
    {
        public string HeadlineFigure { get; set; }

        public List<TimecardEntry> TimecardDetail { get; set; } = new List<TimecardEntry>();
        
    }

    public class TimecardEntry
    {
        public string empName { get; set; }
        public string empNumber { get; set; }
        public string type { get; set; }
    }
}
