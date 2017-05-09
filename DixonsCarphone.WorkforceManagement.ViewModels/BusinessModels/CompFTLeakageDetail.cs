using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class CompFTLeakageDetail
    {
        public string HeadlineFigure { get; set; }
        public List<FTLeakage> FTLeakageDetail { get; set; } = new List<FTLeakage>();
            
    }

    public class FTLeakage
    {
        public string empName { get; set; }
        public string empNumber { get; set; }
        public decimal ContractHours { get; set; }
        public decimal WorkedHours { get; set; }
    }
}
