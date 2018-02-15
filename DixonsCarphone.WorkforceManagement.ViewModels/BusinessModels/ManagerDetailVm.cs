using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ManagerDetailVm : BaseViewModel
    {
        public List<RFTPCaseStubView> caseDetails { get; set; }
    }
}
