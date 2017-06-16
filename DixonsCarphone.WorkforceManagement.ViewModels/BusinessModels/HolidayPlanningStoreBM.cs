using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class HolidayPlanningStoreBM
    {
        public int EntryId { get; set; }
        public int WeekNumber { get; set; }
        public string BranchNumber { get; set; }
        public int Taken { get; set; }
        public int Scheduled { get; set; }
        public int Guideline { get; set; }
    }
}
