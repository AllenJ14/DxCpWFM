using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ChangeStoreOpeningTimeViewModel : BaseViewModel
    {
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

        [Display(Name = "Effective Date")]
        public IEnumerable<SelectListItem> AvailableWeeks
        {
            get
            {
                return GetAvailableWeeks();
            }
        }

        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [Display(Name = "Reason for Change")]
        public string ReasonForChange { get; set; }

        public string NewSunOpening { get; set; }
        public string NewSunClosing { get; set; }

        public string NewMonOpening { get; set; }
        public string NewMonClosing { get; set; }

        public string NewTuesOpening { get; set; }
        public string NewTuesClosing { get; set; }

        public string NewWedOpening { get; set; }
        public string NewWedClosing { get; set; }

        public string NewThursOpening { get; set; }
        public string NewThursClosing { get; set; }

        public string NewFriOpening { get; set; }
        public string NewFriClosing { get; set; }

        public string NewSatOpening { get; set; }
        public string NewSatClosing { get; set; }

        // Populate effective date drop down
        private IEnumerable<SelectListItem> GetAvailableWeeks()
        {
            DateTime CurrentDate = DateTime.Now;
            DateTime SundayDate = CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek);

            List<SelectListItem> weeks = new List<SelectListItem>();
            for (int i = 0; i < 13; i++)
            {
                weeks.Add(new SelectListItem { Text = SundayDate.ToShortDateString(), Value = SundayDate.ToShortDateString() });
                SundayDate = SundayDate.AddDays(7);
            }

            return new SelectList(weeks, "Value", "Text");
        }
    }

}
