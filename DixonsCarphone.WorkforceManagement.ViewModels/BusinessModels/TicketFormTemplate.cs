using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class TicketFormTemplate
    {
        public List<TicketTemplateView> QuestionList { get; set; }
        public string FormName { get; set; }

    }
}
