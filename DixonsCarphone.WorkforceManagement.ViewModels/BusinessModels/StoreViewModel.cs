using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int CST_CNTR_ID { get; set; }
        public string RegionNo { get; set; }
        public string Division { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public string Tel { get; set; }
        public string StoreEmail { get; set; }
        public string IpRange { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string KronosStoreName { get; set; }
        public string Channel { get; set; }
    }
}
