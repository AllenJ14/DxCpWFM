using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class StorePandLViewModel : BaseViewModel
    {
        public List<PandLView> PandLDetails { get; set; }

        //Margin cost totals
        public SectionTotals MarginCostSectionTotals => new SectionTotals
        {
            ActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 1)?.Sum(x => x.ActualTotal),
            BudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 1)?.Sum(x => x.BudgetTotal),
            QtdActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 1)?.Sum(x => x.QtdActualTotal),
            QtdBudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 1)?.Sum(x => x.QtdBudgetTotal),
            YtdActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 1)?.Sum(x => x.YtdActualTotal),
            YtdBudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 1)?.Sum(x => x.YtdBudgetTotal)
        };


        //Controllable cost totals
        public SectionTotals ControllableCostSectionTotals => new SectionTotals
        {
            ActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 2)?.Sum(x => x.ActualTotal),
            BudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 2)?.Sum(x => x.BudgetTotal),
            QtdActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 2)?.Sum(x => x.QtdActualTotal),
            QtdBudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 2)?.Sum(x => x.QtdBudgetTotal),
            YtdActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 2)?.Sum(x => x.YtdActualTotal),
            YtdBudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 2)?.Sum(x => x.YtdBudgetTotal)
        };

        //NonControllable cost totals
        public SectionTotals NonControllableCostSectionTotals => new SectionTotals
        {
            ActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 3)?.Sum(x => x.ActualTotal),
            BudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 3)?.Sum(x => x.BudgetTotal),
            QtdActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 3)?.Sum(x => x.QtdActualTotal),
            QtdBudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 3)?.Sum(x => x.QtdBudgetTotal),
            YtdActualTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 3)?.Sum(x => x.YtdActualTotal),
            YtdBudgetTotal = SubTypeSectionTotals.Where(x => x.AccountEntryTypeId == 3)?.Sum(x => x.YtdBudgetTotal)
        };

        //Sub total for each AccountEntrySubTypeId
        public List<SectionTotals> SubTypeSectionTotals
        {
            get
            {
                var ls = new List<SectionTotals>();

                if (PandLDetails != null)
                {
                    foreach (var detail in PandLDetails.GroupBy(x => x.AccountEntrySubTypeId).Select(x => x.FirstOrDefault()))
                    {
                        var pAndL = PandLDetails.Where(x => x.AccountEntrySubTypeId == detail.AccountEntrySubTypeId).GroupBy(x => x.AccountEntryDetailId).Select(x => x.FirstOrDefault()).ToList();
                        AddToSectionTotals(ls, detail, pAndL);
                    }
                }

                return ls;
            }
        }

        //List of all breakdown items
        public List<BreakDownsDetails> BreakDowns
        {
            get
            {
                var ls = new List<BreakDownsDetails>();
                if (PandLDetails != null)
                {
                    foreach (var bd in PandLDetails.Where(x => !string.IsNullOrEmpty(x.BreakdownTitle)))
                    {
                        ls.Add(new BreakDownsDetails
                        {
                            AccountEntryDetailId = bd.AccountEntryDetailId,
                            BreakdownActualAmount = bd.BreakdownActualAmount,
                            BreakdownBudgetAmount = bd.BreakdownBudgetAmount,
                            BreakdownText = bd.BreakdownText,
                            BreakdownTitle = bd.BreakdownTitle
                        });
                    }
                }

                return ls;
            }
        }

        //Month of records being displayed
        public string PeriodMth => PandLDetails?.FirstOrDefault()?.PeriodMonth.ToString();

        //Year of records being displayed
        public string PeriodYr => PandLDetails?.FirstOrDefault()?.PeriodYear;

        //Total line for entire sheet
        public SectionTotals OverallTotals => new SectionTotals
        {
            ActualTotal = SubTypeSectionTotals.Sum(x => x.ActualTotal),
            BudgetTotal = SubTypeSectionTotals.Sum(x => x.BudgetTotal),
            QtdActualTotal = SubTypeSectionTotals.Sum(x => x.QtdActualTotal),
            QtdBudgetTotal = SubTypeSectionTotals.Sum(x => x.QtdBudgetTotal),
            YtdActualTotal = SubTypeSectionTotals.Sum(x => x.YtdActualTotal),
            YtdBudgetTotal = SubTypeSectionTotals.Sum(x => x.YtdBudgetTotal)
        };

        //Drop down list for months
        public List<SelectListItem> PandLMonths
        {
            get
            {
                var ls = new List<SelectListItem>();
                //for (var i =1; i <=12; i++)
                //{

                //    ls.Add(new SelectListItem
                //    {
                //        Value = i.ToString(),
                //        Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(i < 9 ? i + 4 : i -8)
                //    });
                //}
                ls.Add(new SelectListItem { Value = "1", Text = "May (4 wks)" });
                ls.Add(new SelectListItem { Value = "2", Text = "June (4 wks)" });
                ls.Add(new SelectListItem { Value = "3", Text = "July (5 wks)" });
                ls.Add(new SelectListItem { Value = "4", Text = "August (4 wks)" });
                ls.Add(new SelectListItem { Value = "5", Text = "September (4 wks)" });
                ls.Add(new SelectListItem { Value = "6", Text = "October (5 wks)" });
                ls.Add(new SelectListItem { Value = "7", Text = "November (4 wks)" });
                ls.Add(new SelectListItem { Value = "8", Text = "December (5 wks)" });
                ls.Add(new SelectListItem { Value = "9", Text = "January (4 wks)" });
                ls.Add(new SelectListItem { Value = "10", Text = "February (4 wks)" });
                ls.Add(new SelectListItem { Value = "11", Text = "March (4 wks)" });
                ls.Add(new SelectListItem { Value = "12", Text = "April (5 wks)" });

                ls.ForEach(x => x.Selected = x.Value == PeriodMth);

                return ls;
            }
        }

        //Drop down list for years
        public List<SelectListItem> PandLYears
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = 0; i < 4; i++)
                {
                    var yr = 2016 + i;
                    var yrTxt = (yr - 2000).ToString() + "/" + (yr - 1999).ToString();
                    ls.Add(new SelectListItem { Value = (yr+1).ToString(), Text = yrTxt });
                }

                ls.ForEach(x => x.Selected = x.Value == PeriodYr);

                return ls;
            }
        }

        //Currently selected items
        public string SelectedYear { get; set; }
        public string SelectedMonth { get; set; }

        //Add aggregated totals for given AccountEntrySubTypeId to list
        private void AddToSectionTotals(List<SectionTotals> ls, PandLView detail, List<PandLView> pAndL)
        {
            ls.Add(new SectionTotals
            {
                AccountEntryTypeId = detail.AccountEntryTypeId,
                AccountEntrySubTypeId = detail.AccountEntrySubTypeId,
                ActualTotal = pAndL.Sum(x => x.ActualAmount),
                BudgetTotal = pAndL.Sum(x => x.BudgetAmount),

                QtdActualTotal = pAndL.Sum(x => x.QtdActualAmount),
                QtdBudgetTotal = pAndL.Sum(x => x.QtdBudgetAmount),

                YtdActualTotal = pAndL.Sum(x => x.YtdActualAmount),
                YtdBudgetTotal = pAndL.Sum(x => x.YtdBudgetAmount),
            });
        }
    }


    public class BreakDownsDetails
    {
        public long AccountEntryDetailId { get; set; }
        public string BreakdownTitle { get; set; }
        public string BreakdownText { get; set; }

        public decimal? BreakdownActualAmount { get; set; }
        public decimal? BreakdownBudgetAmount { get; set; }
    }

    public class SectionTotals
    {
        public short? AccountEntryTypeId { get; set; }
        public int? AccountEntrySubTypeId { get; set; } 
        public decimal? ActualTotal { get; set; }
        public decimal? BudgetTotal { get; set; }
        public decimal? PercToBudgetTotal => Helpers.CalcPerc(ActualTotal, BudgetTotal);

        public decimal? QtdActualTotal { get; set; }
        public decimal? QtdBudgetTotal { get; set; }
        public decimal? QtdPercToBudgetTotal => Helpers.CalcPerc(QtdActualTotal, QtdBudgetTotal);

        public decimal? YtdActualTotal { get; set; }
        public decimal? YtdBudgetTotal { get; set; }
        public decimal? YtdPercToBudgetTotal => Helpers.CalcPerc(YtdActualTotal, YtdBudgetTotal);

        public decimal? var =>  ActualTotal - BudgetTotal;
        public decimal? Qtdvar => QtdActualTotal - QtdBudgetTotal;
        public decimal? YtdVar => YtdActualTotal - YtdBudgetTotal;
    }

    public class StorePandLDetailViewModel
    {
        public List<PandLView> PandLDetails { get; set; }
        public List<BreakDownsDetails> BreakDowns { get; set; }
        public SectionTotals OverallSectionTotals { get; set; }
        public List<SectionTotals> SubTypeSectionTotals { get; set;  }

    }
}
