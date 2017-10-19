using System.Collections.Generic;
using System.Linq;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PunchExceptionsVm : BaseViewModel
    {
        public List<PunchExceptionsView> Exceptions { get; set; }
        public List<PunchTrendView> Trend { get; set; }
        public short level { get; set; }

        public List<int?> WeekNumber {
            get
            {
                return Trend.GroupBy(x => x.WeekNumber).Select(x => x.Key).ToList();
            }
        }

        public List<int> RegionRowList
        {
            get
            {
                return Trend.Where(x => x.RegionNo != null).GroupBy(x => x.CST_CNTR_ID).Select(x => x.Key).ToList();
            }
        }

        public List<string> DivisionRowList
        {
            get
            {
                return Trend.Where(x => x.RegionNo != null).GroupBy(x => x.RegionNo).Select(x => x.Key).ToList();
            }
        }

        public List<string> EmpList
        {
            get
            {
                var data = Exceptions.GroupBy(x => x.FORENAME).Select(x => new { name = x.Key, compliance = x.Average(s => s.Compliance) });
                return data.OrderBy(x => x.compliance).Select(x => x.name).ToList();
            }
        }
    }
}
