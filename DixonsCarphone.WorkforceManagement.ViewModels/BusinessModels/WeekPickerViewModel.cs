using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class WeekPickerViewModel : BaseViewModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }

        [Required]
        [Display(Name = "Select Date")]
        public string SelectedDate { get; set; }

    }
}
