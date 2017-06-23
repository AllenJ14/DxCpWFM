using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PandLSummaryDetail
    {
        public int Heirarchy { get; set; }
        public string Year { get; set; }
        public Nullable<short> Month { get; set; }
        public string DetailName { get; set; }
        public Nullable<decimal> PeriodActual { get; set; }
        public Nullable<decimal> PeriodBudget { get; set; }
        public Nullable<decimal> QuarterActual { get; set; }
        public Nullable<decimal> QuarterBudget { get; set; }
        public Nullable<decimal> AnnualActual { get; set; }
        public Nullable<decimal> AnnualBudget { get; set; }

        public decimal? PercToBudgetTotal => Helpers.CalcPerc(PeriodActual, PeriodBudget);
        public decimal? QTDPercToBudgetTotal => Helpers.CalcPerc(QuarterActual, QuarterBudget);
        public decimal? YTDPercToBudgetTotal => Helpers.CalcPerc(AnnualActual, AnnualBudget);
    }
}
