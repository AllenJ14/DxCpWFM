using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class DeploymentViewModel : BaseViewModel
    {
        public DashBoardView _dashboardView { get; set; }

        public List<DailyDeploymentArrayForm> DailyDetails { get; set; }

        public string RequiredGraphArray { get; set; }

        public string DeployedGraphArray { get; set; }

        public List<string> DailyDetailAdvisory { get; set; }

        public string SelectedDate { get; set; }

        public List<DashBoardView> _dashboardViewCollection { get; set; }
    }
}
