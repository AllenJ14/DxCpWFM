using System;
using System.Collections.Generic;
using System.Linq;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FutureDeploymentVm : BaseViewModel
    {
        public List<FutureDeploymentView> collection { get; set; }

        public List<PeakHCView> peakHC { get; set; }

        public List<string> BranchList(string d)
            {
                return collection.Where(x => x.division == d && x.Area != null).GroupBy(x => new { x.Area, x.SortOrder }).OrderBy(x => x.Key.SortOrder).Select(x => x.Key.Area).ToList();
            }

        public List<string> BranchListAll { get
            {
                return collection.GroupBy(x => new { x.Area, x.SortOrder }).OrderBy(x => x.Key.SortOrder).Select(x => x.Key.Area).ToList();
            } }

        public List<int?> WeekList { get
            {
                return collection.GroupBy(x => x.WeekNumber).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            } }

        public List<string> DivisionList { get
            {
                return collection.GroupBy(x => x.division).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            } }

        public List<DateTime> DateList { get
            {
                return peakHC.GroupBy(x => x.Date).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            } }
    }
}
