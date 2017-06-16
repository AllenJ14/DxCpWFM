using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class HolidayPlanningEmpBM
    {
        public int EntryId { get; set; }
        public int BranchNumber { get; set; }
        public string PersonNum { get; set; }
        public string EmployeeName { get; set; }
        public int Balance { get; set; }
        public int Taken { get; set; }
        public int Scheduled { get; set; }
    }
}
