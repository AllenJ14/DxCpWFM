using DixonsCarphone.WorkforceManagement.Business.Kronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IKronosManager
    {
        Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate, string sessionID = null);

        Task<ScheduleItems> GetStoreScheduleForWeek(string startDate, string endDate, List<string> personNumbers, string sessionID = null);

        List<Timesheet> GetTimesheet(DateTime[] dates, string personNumber, string sessionID = null);

        //Task<bool> LogOff(string sessionID = null);

        //Task<bool> LogOn(string sessionID = null);

        Task<List<Timesheet>> GetTimesheetForStore(DateTime date, string[] personList, string sessionID = null);

        Task<List<PunchStatus>> GetPunchStatus(List<string> employeeList, string sessionID = null);
    }
}
