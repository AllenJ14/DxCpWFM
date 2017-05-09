using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ActivityAdminViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string ActivityName { get; set; }

        public string Summary { get; set; }

        public string Detail { get; set; }

        public string StoreNumber { get; set; }

        [Display(Name = "Type")]
        public List<SelectListItem> ActivityTypes
        {
            get
            {
                var ls = new List<SelectListItem>();
                foreach (var x in Enum.GetValues(typeof(ActivityType)).Cast<ActivityType>())
                {
                    ls.Add(new SelectListItem { Value = ((int)x).ToString(), Text = x.ToString() });
                }

                return ls;
            }
        }

        public List<SelectListItem> Priority
        {
            get
            {
                var ls = new List<SelectListItem>();
                foreach (var x in Enum.GetValues(typeof(PriorityType)).Cast<PriorityType>())
                {
                    ls.Add(new SelectListItem { Value = ((int)x).ToString(), Text = x.ToString() });
                }

                return ls;
            }
        }

        public string SelectedPriority { get; set; }

        [Required]
        public string SelectedType { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string ActivityDate { get; set; }

        [Required]
        [Display(Name = "Start Time ")]
        public string SelectedStartTime { get; set; }

        [Required]
        [Display(Name = "End Time ")]
        public string SelectedEndTime { get; set; }

        public List<SelectListItem> Times
        {
            get
            {
                return Helpers.GetTimes().ToList();
            }
        }
    }
}
