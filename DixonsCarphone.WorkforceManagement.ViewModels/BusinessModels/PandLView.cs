using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PandLView
    {
        public long AccountEntryHeaderId { get; set; }
        public string AccountEntryHeaderText { get; set; }
        public string PeriodYear { get; set; }
        public short? PeriodMonth { get; set; }
        public int? StoreNumber { get; set; }
        public string ManagerName { get; set; }
        public long AccountEntryDetailId { get; set; }
        public short AccountEntryTypeId { get; set; }
        public int? AccountEntrySubTypeId { get; set; }
        public string AccountEntrySubTypeText { get; set; }
        public string DetailTitle { get; set; }
        public string DetailText { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? QtdActualAmount { get; set; }
        public decimal? QtdBudgetAmount { get; set; }
        public decimal? YtdActualAmount { get; set; }
        public decimal? YtdBudgetAmount { get; set; }
        public long? AccountEntryDetailBreakDownId { get; set; }
        public string BreakdownTitle { get; set; }
        public string BreakdownText { get; set; }
        public decimal? BreakdownActualAmount { get; set; }
        public decimal? BreakdownBudgetAmount { get; set; }

        public decimal? PercToBudget => Helpers.CalcPerc(ActualAmount, BudgetAmount);
        public decimal? QtdPercToBudget => Helpers.CalcPerc(QtdActualAmount, QtdBudgetAmount);
        public decimal? YtdPercToBudget => Helpers.CalcPerc(YtdActualAmount, YtdBudgetAmount);


    }
}
