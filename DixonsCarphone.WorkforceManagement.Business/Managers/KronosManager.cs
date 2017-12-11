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
        //public KronosManager(bool isOffice = false)
        //{
        //     Logon();
        //}

        public async Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate, string sessionID = null)
        {
            var hyperFindResult = await KronosApi.HyperfindResult(kronosStoreName, string.Format("{0}-{1}", startDate, endDate), sessionID);
            //var personNumbers = hyperFindResult.Select(x => x.PersonNumber).ToList();
            
            return hyperFindResult;
        }

        public List<Timesheet> GetTimesheet(DateTime[] dates, string personNumber, string sessionID = null)
        {
            var timesheetResult = KronosApi.RequestTimesheet(dates, personNumber, sessionID);
            
            return timesheetResult;
        }

        public async Task<List<Timesheet>> GetTimesheetForStore(DateTime date, string[] personList, string sessionID = null)
        {
            var timesheetResult = await KronosApi.RequestTimesheet(date, personList, sessionID);
            
            return timesheetResult;
        }

        public async Task<ScheduleItems> GetStoreScheduleForWeek(string startDate, string endDate, List<string> personNumbers, string sessionID = null)
        {
            var toRtn = new ScheduleItems();

            if (personNumbers.Count > 0)
            {
                var data = await KronosApi.RequestScheduleDetail(startDate, endDate, personNumbers, sessionID);
                toRtn = data.FirstOrDefault();
            }
            
            return toRtn;
        }

        public async Task<List<PunchStatus>> GetPunchStatus(List<string> employeeList, string sessionId = null)
        {
            var toRtn = new List<PunchStatus>();

            if(employeeList.Count > 0)
            {
                toRtn = await KronosApi.RequestPunchStatus(employeeList, sessionId);
            }
            
            return toRtn;
        }

        public async Task<bool> LogOff(string sessionID = null)
        {
            return await KronosApi.Logoff(sessionID);
        }

        public async Task<bool> LogOn(string sessionID = null)
        {
            return await KronosApi.Logon(sessionID);
        }
    }
}
