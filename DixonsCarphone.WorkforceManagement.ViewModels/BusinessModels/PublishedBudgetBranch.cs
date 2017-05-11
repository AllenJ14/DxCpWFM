using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PublishedBudgetBranch
    {
        public string Branch { get; set; }
        public Nullable<byte> Week { get; set; }
        public Nullable<double> SOHTarget { get; set; }
    }
}
