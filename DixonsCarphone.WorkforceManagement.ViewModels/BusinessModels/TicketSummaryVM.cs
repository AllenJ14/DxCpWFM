using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TicketSummaryVM : BaseViewModel
    {
        public List<TicketSummaryView> _TicketCollection { get; set; }
        public List<SelectListItem> _TPCMenu { get; set; }
        public List<SelectListItem> _TypeMenu { get; set; }
    }
}
