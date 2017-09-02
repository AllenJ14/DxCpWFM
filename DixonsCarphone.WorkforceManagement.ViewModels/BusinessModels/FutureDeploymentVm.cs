using System.Collections.Generic;
using System.Linq;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FutureDeploymentVm : BaseViewModel
    {
        public List<FutureDeploymentView> collection { get; set; }

        public List<string> BranchList { get
            {
                return collection.GroupBy(x => new { x.Area, x.SortOrder }).OrderBy(x => x.Key.SortOrder).Select(x => x.Key.Area).ToList();
            } }

        public List<int?> WeekList { get
            {
                return collection.GroupBy(x => x.WeekNumber).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            } }
    }
}
