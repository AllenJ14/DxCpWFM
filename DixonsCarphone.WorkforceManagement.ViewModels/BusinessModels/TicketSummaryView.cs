using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TicketSummaryView
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string RaisedBy { get; set; }
        public byte EscalationLevel { get; set; }
        public Nullable<short> BranchNumber { get; set; }
    }
}
