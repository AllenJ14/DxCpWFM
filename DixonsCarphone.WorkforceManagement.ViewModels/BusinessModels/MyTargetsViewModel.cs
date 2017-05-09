using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class MyTargetsViewModel : BaseViewModel
    {
        public string SelectedMonth { get; set; }

        public List<SelectListItem> Months
        {
            get
            {
                var ls = Enumerable.Range(1, 12).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(x)
                }).ToList();

                ls.ForEach(x => x.Selected = x.Value == SelectedMonth);

                return ls;
            }
        }

        public List<DateTime> WeeksInMonth
        {
            get
            {
                return GetWeeksInMonth();
            }
        }

        public int FirstWeekOfMonthNumber
        {
            get
            {
                int i = 0;
                var firstDate = WeeksInMonth.OrderBy(x => x).FirstOrDefault();
                var wkStartingStr = firstDate.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]).ToString().Substring(4);

                return int.TryParse(wkStartingStr, out i) ? i : i;
            }
        }

        public Dictionary<string, decimal> PandLBudgetTargets { get; set; }
        public Dictionary<int, double?> SohBudgets { get; set; }

        private List<DateTime> GetWeeksInMonth()
        {
            var date = new DateTime(DateTime.Now.Year, int.Parse(SelectedMonth), 1);
            // first generate all dates in the month of 'date'
            var dates = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).Select(n => new DateTime(date.Year, date.Month, n));
            // then filter the only the start of weeks
            var weekends = dates.Where(x => x.DayOfWeek == DayOfWeek.Sunday).ToList();
            //from d in dates
            //           where d.DayOfWeek == DayOfWeek.Sunday
            //           select d;

            return weekends;
        }
    }
}
