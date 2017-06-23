using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PandLSummaryvm : BaseViewModel
    {
        public List<PandLSummaryDetail> DetailCollection { get; set; }


        //Month of records being displayed
        public string PeriodMth => DetailCollection?.FirstOrDefault()?.Month.ToString();

        //Year of records being displayed
        public string PeriodYr => DetailCollection?.FirstOrDefault()?.Year;

        //Drop down list for months
        public List<SelectListItem> PandLMonths
        {
            get
            {
                var ls = new List<SelectListItem>();
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
                    ls.Add(new SelectListItem { Value = (yr + 1).ToString(), Text = yrTxt });
                }

                ls.ForEach(x => x.Selected = x.Value == PeriodYr);

                return ls;
            }
        }

        //Currently selected items
        public string SelectedYear { get; set; }
        public string SelectedMonth { get; set; }
    }
}
