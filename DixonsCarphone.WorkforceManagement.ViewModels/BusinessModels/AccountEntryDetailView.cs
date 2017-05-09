using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class AccountEntryDetailView
    {
        public long AccountEntryDetailId { get; set; }
        public long AccountEntryHeaderId { get; set; }
        public int? AccountEntryTypeId { get; set; }

        public int? AccountEntrySubTypeId { get; set; }
        public string AccountEntrySubTypeText { get; set; }
        public string DetailTitle { get; set; }
        public string DetailText { get; set; }

        public decimal? ActualAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? PercToBudget => Helpers.CalcPerc(ActualAmount, BudgetAmount);

        public decimal? QtdActualAmount { get; set; }
        public decimal? QtdBudgetAmount { get; set; }
        public decimal? QtdPercToBudget => Helpers.CalcPerc(QtdActualAmount, QtdBudgetAmount);


        public decimal? YtdActualAmount { get; set; }
        public decimal? YtdBudgetAmount { get; set; }
        public decimal? YtdPercToBudget => Helpers.CalcPerc(YtdActualAmount, YtdBudgetAmount);


        public List<AccountEntryDetailBreakdownView> AccountEntryDetailBreakdowns { get; set; }
    }
}
