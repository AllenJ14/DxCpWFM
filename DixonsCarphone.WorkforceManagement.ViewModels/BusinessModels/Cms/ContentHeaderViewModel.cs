using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms
{
    public class ContentHeaderViewModel
    {
        public int ContentHeaderId { get; set; }

        [Display(Name = "Header Title")]
        public string HeaderTitle { get; set; }

        [Display(Name = "Description")]
        public string HeaderDescription { get; set; }

        public bool? isActive { get; set; }

        public virtual ICollection<ContentDetailViewModel> ContentDetails { get; set; }
    }
}
