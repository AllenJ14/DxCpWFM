using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class StoreHoursForApproval
    {
        public int EntryId { get; set; }
        public int StoreNumber { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
