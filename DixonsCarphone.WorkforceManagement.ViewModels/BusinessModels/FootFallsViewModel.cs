using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FootFallsViewModel : BaseViewModel
    {
        public List<DailyFootfallView> DailyFootFalls { get; set;  }

        public List<WeeklyFootfallView> WeeklyFootFalls { get; set; }

        public List<double> WeeklyBau
        {
            get
            {
                var bau = WeeklyFootFalls.FirstOrDefault(x => x.ProfileType == "BAU" || string.IsNullOrEmpty(x.ProfileType));

                return GetWeeklyFootFall(bau);

            }
        }

        public List<double> WeeklyBankHoliday
        {
            get
            {
                var bh = WeeklyFootFalls.FirstOrDefault(x => x.ProfileType == "Bank Holiday");
                return GetWeeklyFootFall(bh);
            }
        }

        public List<double> DailyBau
        {
            get
            {
                var bau = DailyFootFalls.FirstOrDefault(x => x.ProfileType == "BAU");
                return GetDailyFootFalls(bau);
            }
        }

        public List<SelectListItem> Days
        {
            get
            {
                var days = Helpers.GetDays();
                var toRtn = days.Select(l => new SelectListItem { Selected = (l.Text == SelectedDay), Text = l.Text, Value = l.Value }).ToList();
                return toRtn;
                //return days; // Helpers.GetDays();
            }
        }

        public string SelectedDay { get; set; }

        public string SelectedDate { get; set; }

        public List<double> DailyBankHoliday
        {
            get
            {
                var bh = DailyFootFalls.FirstOrDefault(x => x.ProfileType == "Bank Holiday");
                return GetDailyFootFalls(bh);
            }
        }

        private List<double> GetDailyFootFalls(DailyFootfallView bau)
        {
            var ls = new List<double>();
            if (bau != null)
            {
                ls.AddRange
                    (new List<double>
                    {
                            bau.C0700.GetValueOrDefault() * 100,
                            bau.C0800.GetValueOrDefault() * 100,
                            bau.C0900.GetValueOrDefault() * 100,
                            bau.C1000.GetValueOrDefault() * 100,
                            bau.C1100.GetValueOrDefault() * 100,
                            bau.C1200.GetValueOrDefault() * 100,
                            bau.C1300.GetValueOrDefault() * 100,
                            bau.C1400.GetValueOrDefault() * 100,
                            bau.C1500.GetValueOrDefault() * 100,
                            bau.C1600.GetValueOrDefault() * 100,
                            bau.C1700.GetValueOrDefault() * 100,
                            bau.C1800.GetValueOrDefault() * 100,
                            bau.C1900.GetValueOrDefault() * 100,
                            bau.C2000.GetValueOrDefault() * 100,
                            bau.C2100.GetValueOrDefault() * 100,
                            bau.C2200.GetValueOrDefault() * 100
                    }
                    );
            }

            return ls;
        }

        private List<double> GetWeeklyFootFall(WeeklyFootfallView bau)
        {
            var ls = new List<double>();
            if (bau != null)
            {
                ls.AddRange
                    (new List<double>
                    {
                             bau.Sunday.GetValueOrDefault() * 100,
                             bau.Monday.GetValueOrDefault() * 100,
                            bau.Tuesday.GetValueOrDefault() * 100,
                            bau.Wednesday.GetValueOrDefault() * 100,
                            bau.Thursday.GetValueOrDefault() * 100,
                            bau.Friday.GetValueOrDefault() * 100,
                            bau.Saturday.GetValueOrDefault() * 100
                    }
                    );
            }

            return ls;
        }

    }
}
