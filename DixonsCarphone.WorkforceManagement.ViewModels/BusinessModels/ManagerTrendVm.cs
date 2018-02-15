using System.Collections.Generic;
using System.Linq;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ManagerTrendVm : BaseViewModel
    {
        public List<RFTPCaseStubView> cases { get; set; }
        public List<Last12MonthDetailView> monthList { get; set; }
        public List<KronosEmpSummaryView> empDetails { get; set; }
        public string displayType { get; set; }

        public List<KronosEmpSummaryView> gmList
        {
            get
            {
                return empDetails.Where(x => x.ReportingRoleFlag == 1 && x.Active == true).OrderBy(x => x.PersonName).ToList();
            }
        }

        public List<KronosEmpSummaryView> nonGMList
        {
            get
            {
                return empDetails.Where(x => x.ReportingRoleFlag != 1 && x.Active == true).OrderBy(x => x.PersonName).ToList();
            }
        }
    }
}
