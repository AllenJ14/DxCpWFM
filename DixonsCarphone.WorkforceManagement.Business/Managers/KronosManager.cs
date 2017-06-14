using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Kronos;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class KronosManager : IKronosManager
    {
        public KronosManager(bool isOffice = false)
        {
             Logon();
        }

        ~KronosManager()
        {
            LogOff();
        }

        public async Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate)
        {
            var hyperFindResult = await KronosApi.HyperfindResult(kronosStoreName, string.Format("{0}-{1}", startDate, endDate));
            //var personNumbers = hyperFindResult.Select(x => x.PersonNumber).ToList();

            return hyperFindResult;
        }

        public async Task<ScheduleItems> GetStoreScheduleForWeek(string startDate, string endDate, List<string> personNumbers)
        {
            var toRtn = new ScheduleItems();

            if (personNumbers.Count > 0)
            {
                var data = await KronosApi.RequestScheduleDetail(startDate, endDate, personNumbers);
                toRtn = data.FirstOrDefault();
            }

            return toRtn;
        }

        public async Task<bool> LogOff()
        {
            return await KronosApi.Logoff();
        }

        private void Logon()
        {
            var t = Task.Run(() => KronosApi.Logon());
            var result = t.Result;
        }
    }
}
