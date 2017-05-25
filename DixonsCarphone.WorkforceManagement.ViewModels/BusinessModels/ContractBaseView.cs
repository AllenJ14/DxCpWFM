using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ContractBaseView
    {
        public short Branch { get; set; }
        public Nullable<short> Contract_Base { get; set; }
        public Nullable<short> Highest_Week { get; set; }
        public Nullable<short> Lowest_Week { get; set; }
        public Nullable<decimal> P1 { get; set; }
        public Nullable<decimal> P2 { get; set; }
        public Nullable<decimal> P3 { get; set; }
        public Nullable<decimal> P4 { get; set; }
        public Nullable<decimal> P5 { get; set; }
        public Nullable<decimal> P6 { get; set; }
        public Nullable<decimal> P7 { get; set; }
        public Nullable<decimal> P8 { get; set; }
        public Nullable<decimal> P9 { get; set; }
        public Nullable<decimal> P10 { get; set; }
        public Nullable<decimal> P11 { get; set; }
        public Nullable<decimal> P12 { get; set; }
        public int TotalHoliday { get; set; }
    }
}
