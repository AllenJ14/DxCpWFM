using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.Model
{
    public class SingleOpeningTimeViewModel : BaseViewModel
    {
        public StoreOpeningTimeView TimeToEdit { get; set; }
        private IEnumerable<SelectListItem> _times;

        public IEnumerable<SelectListItem> Times
        {
            get
            {
                if (_times == null)
                    _times = Helpers.GetTimes();

                return _times;
            }
        }
    }
}
