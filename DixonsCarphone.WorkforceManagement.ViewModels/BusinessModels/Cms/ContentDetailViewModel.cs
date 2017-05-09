using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms
{
    public class ContentDetailViewModel : BaseViewModel
    {
        public int ContentDetailId { get; set; }
        public int? ContentHeaderId { get; set; }

        [Display(Name = "Detail Title")]
        [Required]
        public string ContentDetailTitle { get; set; }

        [Display(Name = "Summary")]
        public string ContentDetailSummary { get; set; }

        [Display(Name = "Content")]
        [AllowHtml]
        public string ContentDetailText { get; set; }
    }
}