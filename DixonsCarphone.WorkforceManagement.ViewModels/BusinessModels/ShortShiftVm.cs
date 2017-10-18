using System.Collections.Generic;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ShortShiftVm : BaseViewModel
    {
        public short type { get; set; }
        public List<ShortShiftView> ShortShifts { get; set; }
        public List<RegionShortShiftView> RegionShortShifts { get; set; }
    }
}
