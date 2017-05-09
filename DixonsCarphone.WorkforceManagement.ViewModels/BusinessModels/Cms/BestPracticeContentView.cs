using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms
{
    public class BestPracticeContentView
    {
        public int BestPracticeContentId { get; set; }
        public short CategoryId { get; set; }
        public string SubCategory { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string BestPracticeText { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string SelectedCategory { get; set; }

        public virtual BestPracticeCategoryType BestPracticeCategoryType { get; set; }
    }

    public class BestPracticeCategoryType
    {
        public short CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
