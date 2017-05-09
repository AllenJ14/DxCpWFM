using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class BaseViewModel
    {
        public string Message { get; set; }

        public string PageBlurb { get; set; }

        public MessageType MessageType { get; set; }

        public List<FooterContent> Footer { get; set; }

        public string GetAlertType
        {
            get
            {
                var toRtn = "alert-info";
                switch (MessageType)
                {
                    case MessageType.Info:
                        toRtn = "alert-success";
                        break;
                    case MessageType.Warning:
                        toRtn = "alert-warning";
                        break;
                    case MessageType.Error:
                        toRtn = "alert-danger";
                        break;
                }

                return toRtn;
            }
        }

        public List<SelectListItem> WeeksOfYear { get; set; }

        public void GetWeeksOfYear(DateTime lastDate, List<int?> weekNumbers)
        {
            WeeksOfYear = Helpers.GetFinancialWeeks(lastDate, weekNumbers);
        }
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
    
}
