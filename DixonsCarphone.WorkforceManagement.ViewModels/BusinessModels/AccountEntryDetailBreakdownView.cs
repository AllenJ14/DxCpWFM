using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class AccountEntryDetailBreakdownView
    {
        public long AccountEntryDetailBreakDownId { get; set; }
        public long AccountEntryDetailId { get; set; }
        public string BreakdownTitle { get; set; }
        public string BreakdownText { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
    }
}
