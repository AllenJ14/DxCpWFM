using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class StoresMenuViewModel
    {
        public List<SelectListItem> Stores { get; set; }

        public List<SelectListItem> Regions { get; set; }

        public string CurrentRegionNumber { get; set; }

        public string CurrentStoreName { get; set; }

        [Required]
        public string SelectedStore { get; set; }

        public enum RegionOrder
        {
            SAS = 0, SIS = 1, ROI = 2
        }
    }
}
