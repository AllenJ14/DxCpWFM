using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RFTPPeriodSummaryVm
    {
        public List<RFTPCaseStubView> Cases { get; set; }
        public List<RFTPCaseActionView> Actions { get; set; }
        public List<KronosEmpSummaryView> RegionManagers { get; set; }

        public IEnumerable<SelectListItem> ManagerDropdown
        {
            get
            {
                var managers = RegionManagers.OrderBy(x => x.PersonName).Select(x => new SelectListItem { Value = x.PersonNumber, Text = x.PersonName});
                return new SelectList(managers, "Value", "Text");
            }
        }
    }
}
