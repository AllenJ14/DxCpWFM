using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class HolidayPlanningVM : BaseViewModel
    {
        public int Level { get; set; }
        public string TakenArray { get; set; }
        public string SchedArray { get; set; }
        public string GuideArray { get; set; }

        public List<HolidayDetail> DetailCollection { get; set; }

        public int TotalToTake { get; set; }
        public int TotalTaken { get; set; }
        public float PercToTake { get; set; }

        public float PercTaken
        {
            get
            {
                return (float)TotalTaken / (float)TotalToTake;
            }
            set
            {
                PercTaken = value;
            }
        }

        public void populate(List<HolidayPlanningStoreBM> storeDetail, List<HolidayPlanningEmpBM> empDetail, int l, int weekNumber)
        {
            Level = 1;

            var past = storeDetail.Where(x => x.WeekNumber <= weekNumber);

            TotalToTake = past.Select(x => x.Guideline).Sum();
            PercToTake = (float)TotalToTake / (float)storeDetail.Select(x => x.Guideline).Sum();
            TotalTaken = storeDetail.Select(x => x.Taken).Sum();

            TakenArray = ToGraphArray(storeDetail.Select(x => x.Taken).ToArray());
            SchedArray = ToGraphArray(storeDetail.Select(x => x.Scheduled).ToArray());
            GuideArray = ToGraphArray(storeDetail.Select(x => x.Guideline).ToArray());

            DetailCollection = new List<HolidayDetail>();
            foreach (var item in empDetail)
            {
                DetailCollection.Add(new HolidayDetail()
                {
                    ID = item.EmployeeName,
                    BalanceRemaining = item.Balance,
                    HolidayTaken = item.Taken,
                    Planned = item.Scheduled
                });
            }

            return;
        }

        private string ToGraphArray(int[] a)
        {
            string result = "[";

            if (a == null)
            {
                return "";
            }
            foreach (var item in a)
            {
                result = result + item.ToString() + ",";
            }
            result = result.TrimEnd(',') + "]";

            return result;
        }
    }
}
