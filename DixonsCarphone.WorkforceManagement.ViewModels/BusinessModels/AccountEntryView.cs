using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class AccountEntryView 
    {
        public long AccountEntryHeaderId { get; set; }
        public string AccountEntryHeaderText { get; set; }
        public string PeriodYear { get; set; }
        public string PeriodMonth { get; set; }
        public int? StoreNumber { get; set; }
        public string ManagerName { get; set; }

        public List<AccountEntryDetailView> Details { get; set; }

        public List<SelectListItem> PandLMonths
        {
            get
            {
                var ls = Enumerable.Range(1, 12).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(x)
                }).ToList();

                ls.ForEach(x => x.Selected = x.Value == PeriodMonth);

                return ls;
            }
        }

        public List<SelectListItem> PandLYears
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = -3; i < 4; i++)
                {
                    var yr = DateTime.Now.AddYears(i).Year.ToString();
                    ls.Add(new SelectListItem { Value = yr, Text = yr });
                }

                ls.ForEach(x => x.Selected = x.Value == PeriodYear);

                return ls;
            }
        }
    }
}
