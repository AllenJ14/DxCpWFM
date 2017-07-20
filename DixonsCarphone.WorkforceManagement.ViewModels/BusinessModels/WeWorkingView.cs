using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class WeWorkingView
    {
        public int EntryId { get; set; }
        public DateTime Date { get; set; }
        public byte Day { get; set; }
        public short BranchNum { get; set; }
        public string BranchName { get; set; }
        public short? RegionNum { get; set; }
        public byte Worked { get; set; }
    }
}
