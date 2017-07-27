using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FootfallVm :BaseViewModel
    {
        public string DailyDetail { get; set; }
        public string SunHourly { get; set; }
        public string MonHourly { get; set; }
        public string TueHourly { get; set; }
        public string WedHourly { get; set; }
        public string ThuHourly { get; set; }
        public string FriHourly { get; set; }
        public string SatHourly { get; set; }
        public int?[] test { get; set; }

        //Currently selected items
        public string SelectedYear { get; set; }
        public string SelectedWeek { get; set; }

        public void Populate(List<FootfallView> a)
        {
            var b = a.GroupBy(x => x.Invoice_Day_Name).Select(x => new { x.Key, Footfall = x.Sum(y => y.Footfall_Volume) });
            DailyDetail = "[";

            for (int i = 0; i < 7; i++)
            {
                DailyDetail = DailyDetail + (b.Where(x => x.Key == Enum.GetName(typeof(DayOfWeek), i)).Select(x => x.Footfall).FirstOrDefault() ?? 0).ToString() + ",";
            }
            DailyDetail = DailyDetail.TrimEnd(',') + "]";

            SunHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Sunday"));
            MonHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Monday"));
            TueHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Tuesday"));
            WedHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Wednesday"));
            ThuHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Thursday"));
            FriHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Friday"));
            SatHourly = DailyArray(a.Where(x => x.Invoice_Day_Name == "Saturday"));

            SelectedYear = a.First().Invoice_Group_Year_Name;
            SelectedWeek = a.First().Invoice_Group_Week_Number.ToString();
        }

        private string DailyArray(IEnumerable<FootfallView> a)
        {
            int count = a.Max(x => x.Hour_In_Day_24) ?? 18;
            count = count < 18 ? 18 : count;
            string toReturn = "[";
            for (int i = 8; i < count; i++)
            {
                toReturn = toReturn + (a.Where(x => x.Hour_In_Day_24 == i).Select(x => x.Footfall_Volume).FirstOrDefault() ?? 0).ToString() + ",";
            }
            toReturn = toReturn.TrimEnd(',') + "]";

            return toReturn;
        }

        //Drop down list for years
        public List<SelectListItem> Years
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = 0; i < 4; i++)
                {
                    var yr = 2016 + i;
                    var yrTxt = (yr - 2000).ToString() + "/" + (yr - 1999).ToString();
                    ls.Add(new SelectListItem { Value = yrTxt, Text = yrTxt });
                }

                ls.ForEach(x => x.Selected = x.Value == SelectedYear);

                return ls;
            }
        }

        public List<SelectListItem> Weeks
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = 1; i <= 52; i++)
                {
                    ls.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ls.ForEach(x => x.Selected = x.Value == SelectedWeek);
                return ls;
            }
        }
    }
}
