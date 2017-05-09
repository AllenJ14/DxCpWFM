using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class PeopleViewModel : BaseViewModel
    {
        private static Random r = new Random();

        public List<HrFeedView> Staff { get; set; }

        public Dictionary<int, double?> HolidaysTakenForYear = new Dictionary<int, double?>();

        public List<int> ExpectedForPeriod
        {
            get
            {
                var ls = new List<int>();

                for (var i = 0; i < 53; i++)
                {
                    ls.Add(r.Next(40, 60));
                }

                return ls;
            }
        }
    }
}
