using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class WeWorkingVm : BaseViewModel
    {
        public int DisplayType { get; set; }
        public List<WeWorkingView> DetailCollection { get; set; }
        public List<DayDates> HeaderCollection { get; set; }
        public List<BrList> BranchList { get; set; }

        public List<WeWorkingView> SecDetailCollection { get; set; }
        public List<DayDates> SecHeaderCollection { get; set; }
        public List<BrList> SecBranchList { get; set; }

        public void PopulateHeader(List<WeWorkingView> a)
        {
            DetailCollection = a;
            HeaderCollection = new List<DayDates>();
            BranchList = new List<BrList>();

            var data = a.GroupBy(x => new { x.Date, x.Day }).Select(x => new { Date = x.Key.Date, Day = x.Key.Day }).OrderBy(x => x.Date).ToList();
            foreach (var item in data)
            {
                HeaderCollection.Add(new DayDates {
                    Date = item.Date,
                    Day = Enum.GetName(typeof(DayOfWeek), item.Day - 1)
                });
            }

            var data2 = a.GroupBy(x => new { x.BranchNum, x.BranchName }).OrderBy(x => x.Key.BranchNum).Select(x => new { num = x.Key.BranchNum, name = x.Key.BranchName }).ToList();
            foreach (var item in data2)
            {
                BranchList.Add(new BrList
                {
                    BranchNum = item.num,
                    BranchName = item.name
                });
            }
        }

        public void PopulateSecHeader(List<WeWorkingView> a)
        {
            SecDetailCollection = a;
            SecHeaderCollection = new List<DayDates>();
            SecBranchList = new List<BrList>();

            var data = a.GroupBy(x => new { x.Date, x.Day }).Select(x => new { Date = x.Key.Date, Day = x.Key.Day }).OrderBy(x => x.Date).ToList();
            foreach (var item in data)
            {
                SecHeaderCollection.Add(new DayDates
                {
                    Date = item.Date,
                    Day = Enum.GetName(typeof(DayOfWeek), item.Day - 1)
                });
            }

            var data2 = a.GroupBy(x => new { x.BranchNum, x.BranchName }).OrderBy(x => x.Key.BranchNum).Select(x => new { num = x.Key.BranchNum, name = x.Key.BranchName }).ToList();
            foreach (var item in data2)
            {
                SecBranchList.Add(new BrList
                {
                    BranchNum = item.num,
                    BranchName = item.name
                });
            }
        }
    }

    public class DayDates
    {
        public DateTime Date { get; set; }
        public string Day { get; set; }
    }

    public class BrList
    {
        public int BranchNum { get; set; }
        public string BranchName { get; set; }
    }
}
