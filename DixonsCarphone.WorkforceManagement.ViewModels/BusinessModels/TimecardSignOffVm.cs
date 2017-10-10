using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TimecardSignOffVm : BaseViewModel
    {
        //public List<TimesheetView> ts { get; set; }
        public List<HyperFindResultView> hf { get; set; }
        public List<ShortShiftView> ss { get; set; }

        public List<RegionSignOff> rso { get; set; }

        public DateTime weekStart { get; set; }
        public string SelectedDate { get; set; }
    }

    public class RegionSignOff
    {
        public short? BranchNumber { get; set; }
        public string BranchName { get; set; }
        public int SignedOff { get; set; }
        public int Headcount { get; set; }
    }
}
