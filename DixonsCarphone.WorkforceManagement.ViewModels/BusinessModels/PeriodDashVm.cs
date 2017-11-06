using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeriodDashVm : BaseViewModel
    {
        public List<PeriodDashView> collection { get; set; }

        public List<int?> WeekNumbers { get
            {
                return collection.GroupBy(x => x.WeekNumber).Select(x => x.Key).ToList();
            } }

        public List<PeriodDashView> PeriodTotal { get
            {
                return collection.Where(x => x.WeekNumber == null).ToList();
            } }

        public short? Period { get
            {
                return collection.FirstOrDefault().Period;
            } }

        public string selectedDate { get; set; }

        public PeriodDashVm()
        {
            WeeksOfYear = new List<SelectListItem>();
            string[] yrs =  ConfigurationManager.AppSettings["FinancialYear"].ToString().Split('/');
            short a = short.Parse(yrs[0]);
            short b = short.Parse(yrs[1]);

            for(int j = 0; j < 2; j++)
            {
                for (int i = 1; i < 13; i++)
                {
                    WeeksOfYear.Add(new SelectListItem
                    {
                        Value = string.Format("{0}/{1}_{2}", a + j, b + j, i),
                        Text = string.Format("P{0} {1}/{2}", i, a + j, b + j)
                    });
                }
            }
        }
    }
}
