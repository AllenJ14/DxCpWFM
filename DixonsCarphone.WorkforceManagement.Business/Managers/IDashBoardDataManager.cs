using DixonsCarphone.WorkforceManagement.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IDashBoardDataManager
    {
        Task<DashBoardData_v2> GetStoreDashBoardData(int storeNumber, int weekNumber = 1);
        Task<List<DashBoardData_v2>> GetStoreDashBoardData(int storeNumber, int weekNumberStart, int weekNumberEnd);
        Task<List<sp_RegionDashboardData_v2_Result>> GetRegionDashboardData(string region, int weekNumberStart, int weekNumberEnd);
        Task<List<sp_DivisionDashboardData_v2_Result>> GetDivisionDashboardData(string division, int weekNumberStart, int weekNumberEnd);
        Task<List<sp_ChannelDashboardData_v2_Result>> GetChannelDashboardData(string channel, int weekNumberStart, int weekNumberEnd);
        Task<List<DashBoardData_v2>> GetAllRegionDashBoardData(int region, int weekNumberStart, int weekNumberEnd);
        Task<List<sp_AllDivisionDashboardData_v2_Result>> GetAllDivisionDashboardData(string division, int weekNumberStart);
        Task<List<sp_AllChannelDashboardData_v2_Result>> GetAllChannelDashboardData(string channel, int weekNumberStart);
    }
}
