using System;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class FutureDeploymentView
    {
        public Nullable<short> SortOrder { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public string Area { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }
        public Nullable<double> Holiday { get; set; }

        public string deployedPerc { get
            {
                if(Target == 0)
                {
                    return "-";
                }
                return string.Format("{0:0.0%}", Actual / Target);
            } }

        public string division { get
            {
                if (SortOrder == null)
                {
                    return "NA";
                }
                else
                {
                    return SortOrder.ToString().Substring(0, 1);
                }
            } }
    }
}
