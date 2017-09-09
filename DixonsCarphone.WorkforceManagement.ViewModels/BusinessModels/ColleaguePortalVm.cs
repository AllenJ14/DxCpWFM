using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ColleaguePortalVm : BaseViewModel
    {
        public List<PayCalendarRefView> rawMenu { get; set; }

        public List<SelectListItem> menu { get
            {
                List<SelectListItem> a = new List<SelectListItem>();
                foreach (var item in rawMenu)
                {
                    a.Add(new SelectListItem { Text = string.Format("{0} - Paid: {1:D}", item.Period, item.PayDate), Value = item.Period });
                }
                return a;
            } }
    }
}
