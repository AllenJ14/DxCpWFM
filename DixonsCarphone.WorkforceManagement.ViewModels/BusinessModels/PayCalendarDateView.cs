using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PayCalendarDateView
    {
        public string Chain { get; set; }
        public string Year { get; set; }
        public string Period { get; set; }
        public int Week { get; set; }
        public System.DateTime WCDate { get; set; }
    }
}
