using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class RoleReferenceView
    {
        public string Role { get; set; }
        public short Reporting_Role_Flag { get; set; }
        public short Sales_Role_Flag { get; set; }
    }
}
