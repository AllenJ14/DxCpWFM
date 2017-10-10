using System;
using System.Collections.Generic;

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
        public bool KronosScheduled { get; set; }
        public bool KronosPunched { get; set; }

        public short RAG
        {
            get
            {
                if(SignedOff < Headcount)
                {
                    if(!KronosScheduled && !KronosPunched)
                    {
                        return 2;
                    }
                    else if(!KronosScheduled || !KronosPunched)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
