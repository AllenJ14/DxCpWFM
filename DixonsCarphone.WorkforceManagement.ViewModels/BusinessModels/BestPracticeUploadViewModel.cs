using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class BestPracticeUploadViewModel : BaseViewModel
    {
        public List<SelectListItem> Categories
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "Best Practice", Text = "Best Practice" },
                    new SelectListItem { Value = "Faqs", Text = "Faqs" },
                    new SelectListItem { Value = "Star Handbook", Text = "Star Handbook" },
                };
            }
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        public string Summary { get; set; }
    }
}
