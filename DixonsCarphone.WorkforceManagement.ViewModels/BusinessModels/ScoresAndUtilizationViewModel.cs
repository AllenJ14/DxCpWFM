using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class ScoresAndUtilizationViewModel : BaseViewModel
    {
        public double? Total { get; set; }
        public double? Score { get; set; }
        public string Title { get; set; }

        public ScoreType ScoreType { get; set; }

        public double Perc
        {
            get
            {
                return Helpers.CalcPerc(Score, Total).GetValueOrDefault();
                    //return Score > 0 && Total > 0 ? (Score / Total) * 100 : 0;
            }
        }

        public string ProgressColor
        {
            get
            {
                var toRtn = "green";
                if (Perc <= 30)
                    toRtn = "red";
                else if (Perc > 30 && Perc <= 50)
                    toRtn = "yellow";
                else if (Perc > 50 && Perc <= 80)
                    toRtn = "aqua";

                return toRtn;
            }
        }
    }

    public enum ScoreType
    {
        Compliance,
        Deployment,
        ColleaguesUnderContract
    }
}
