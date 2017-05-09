using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class MyTeamViewModel : BaseViewModel
    {
        //public double CurrentContractHours { get; set; }
        //public double ContractBaseTarget { get; set; }
        //public double? FT { get; set; }
        //public double? PT { get; set; }
        //public double AverageContractHours { get; set; }

        //public List<MyTeamDetail> Details { get; set; }

        public List<HrFeedView> MyTeam { get; set; }

    }

    //public class MyTeamDetail
    //{
    //    public string Title { get; set; }
    //    public int HeadCount { get; set; }
    //    public double ContractHours { get; set; }
    //}
}
