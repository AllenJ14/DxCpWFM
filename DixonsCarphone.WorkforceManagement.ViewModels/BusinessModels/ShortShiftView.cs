using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ShortShiftView
    {
        public int WeekNumber { get; set; }
        public int HomeBranch { get; set; }
        public string PersonNum { get; set; }
        public string PersonName { get; set; }
        public System.DateTime PunchDate { get; set; }
    }
}
