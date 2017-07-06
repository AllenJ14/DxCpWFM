using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TicketTemplateView
    {
        public int QuestionId { get; set; }
        public int TicketTypeId { get; set; }
        public string Question { get; set; }
        public string ReturnType { get; set; }
        public Nullable<byte> GrpLimit { get; set; }
        public string apiIdent { get; set; }
    }
}
