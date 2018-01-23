using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RFTPPeriodSummaryVm : BaseViewModel
    {
        public List<RFTPCaseStubView> Cases { get; set; }
        public List<RFTPCaseActionView> Actions { get; set; }
        public List<KronosEmpSummaryView> RegionManagers { get; set; }
        public string AccessLevel { get; set; }
        public string displayType { get; set; }

        public List<SelectListItem> ManagerDropdown
        {
            get
            {
                var managers = RegionManagers.OrderBy(x => x.PersonName).Select(x => new SelectListItem { Value = x.PersonNumber, Text = x.PersonName}).ToList();
                return managers;
            }
        }

        public List<short?> regionList { get
            {
                return RegionManagers.GroupBy(x => x.Region).Select(x => x.Key).OrderBy(x => x).ToList();
            } }
    }
}
