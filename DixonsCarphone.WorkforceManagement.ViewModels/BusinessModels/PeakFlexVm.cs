using System.Collections.Generic;
using System.Linq;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeakFlexVm : BaseViewModel
    {
        public List<PeakFlexRegionView> regionCollection { get; set; }
        public List<PeakFlexDivisionView> divisionCollection { get; set; }

        public List<string> DivisionList { get
            {
                return divisionCollection.GroupBy(x => x.Division).Select(x => x.Key).Where(x => x != null).OrderBy(x => x).ToList();
            } }

        public PeakFlexDivisionView Chain { get
            {
                return divisionCollection.Where(x => x.Division == null).FirstOrDefault();
            } }
    }
}
