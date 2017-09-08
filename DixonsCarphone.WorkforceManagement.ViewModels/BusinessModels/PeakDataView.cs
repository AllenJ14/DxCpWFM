using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeakDataView
    {
        public int BranchNumber { get; set; }
        public int WeekNumber { get; set; }
        public Nullable<double> Budget { get; set; }
        public Nullable<double> Contract { get; set; }
        public Nullable<double> Holiday { get; set; }
        public Nullable<byte> Temps { get; set; }
        public Nullable<byte> TempsOption { get; set; }

        public double? Flex { get
            {
                return Budget - Contract - Holiday;
            } }
        
        public bool tempflag { get
            {
                return Temps > 0 || TempsOption > 0 ? true : false;
            } }
    }
}
