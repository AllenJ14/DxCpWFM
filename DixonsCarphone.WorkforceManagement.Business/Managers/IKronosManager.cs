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
        Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate);

        Task<ScheduleItems> GetStoreScheduleForWeek(string startDate, string endDate, List<string> personNumbers);

        List<Timesheet> GetTimesheet(DateTime[] dates, string personNumber);

        Task<bool> LogOff();
    }
}
