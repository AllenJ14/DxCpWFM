using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class SohSpendViewModel : BaseViewModel
    {
        public SohSpendViewModel()
        {
            SohBudgets = new Dictionary<int, double?>();
            SohSpends = new Dictionary<int, double?>();
            Management = new Dictionary<int, double?>();
            Sales = new Dictionary<int, double?>();
            SalesFt = new Dictionary<int, double?>();
            SalesPt = new Dictionary<int, double?>();
            GeekSquad = new Dictionary<int, double?>();
            L1Engineer = new Dictionary<int, double?>();
            L2Engineer = new Dictionary<int, double?>();
            SmartAcademy = new Dictionary<int, double?>();
        }

        public Dictionary<int, double?> SohBudgets { get; set; }
        public Dictionary<int, double?> SohSpends { get; set; }
        public Dictionary<int, double?> Management { get; set; }

        public Dictionary<int, double?> Sales { get; set; }

        public Dictionary<int, double?> SalesFt { get; set; }
        public Dictionary<int, double?> SalesPt { get; set; }
        public Dictionary<int, double?> GeekSquad { get; set; }
        public Dictionary<int, double?> L1Engineer { get; set; }
        public Dictionary<int, double?> L2Engineer { get; set; }
        public Dictionary<int, double?> SmartAcademy { get; set; }

        public List<string> WeekNumbers
        {
            get
            {
                var ls = new List<string>();
                var currWk = DateTime.Now.GetWeekOfYear(ConfigurationManager.AppSettings["FinancialYearStart"]);
                var wk = currWk - 8;

                for(var i = 0; i < 13; i++)
                {
                    var str = i == 8 ? "Current Week" : string.Format("Week {0}", (wk + i).ToString().Substring(4));
                    ls.Add(str);

                }

                return ls;
            }
        }

    }
}
