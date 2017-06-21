using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using System.Data.Entity;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class DashBoardDataManager : IDashBoardDataManager
    {
        // Retrieve dashboard data record for specific store and week
        public async Task<DashBoardData> GetStoreDashBoardData(int storeNumber, int weekNumber = 1)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.DashBoardDatas.Where(x => x.BranchNumber == storeNumber && (x.WeekNumber == weekNumber)).SingleOrDefaultAsync();
                return result;
            }
        }

        // Retrieve all dashboard data records for specific store between two week numbers (inclusive)
        public async Task<List<DashBoardData>> GetStoreDashBoardData(int storeNumber, int weekNumberStart, int weekNumberEnd)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.DashBoardDatas.Where(x => x.BranchNumber == storeNumber &&
                (x.WeekNumber >= weekNumberStart && x.WeekNumber <= weekNumberEnd)).OrderBy(x => x.WeekNumber).ToListAsync();
                return result;
            }
        }

        // Retrieve region data records between two week numbers (inclusive)
        public async Task<List<sp_RegionDashboardData_Result>> GetRegionDashboardData(string region, int weekNumberStart, int weekNumberEnd)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.sp_RegionDashboardData(region, weekNumberStart, weekNumberEnd));
                return result.OrderBy(x => x.WeekNumber).ToList();
            }
        }

        // Retrieve all dashboard data records for specific region between two week numbers (inclusive)
        public async Task<List<DashBoardData>> GetAllRegionDashBoardData(int region, int weekNumberStart, int weekNumberEnd)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.DashBoardDatas.Where(x => x.Region == region &&
                (x.WeekNumber >= weekNumberStart && x.WeekNumber <= weekNumberEnd)).OrderBy(x=> x.BranchNumber).ThenBy(x => x.WeekNumber).ToListAsync();
                return result;
            }
        }

        // Retrieve division data records between two week numbers (inclusive)
        public async Task<List<sp_DivisionDashboardData_Result>> GetDivisionDashboardData(string division, int weekNumberStart, int weekNumberEnd)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.sp_DivisionDashboardData(division, weekNumberStart, weekNumberEnd));
                return result.OrderBy(x => x.WeekNumber).ToList();
            }
        }

        // Retrieve regions aggregates for single week and division, including subtotal row
        public async Task<List<sp_AllDivisionDashboardData_Result>> GetAllDivisionDashboardData(string division, int weekNumberStart)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.sp_AllDivisionDashboardData(division, weekNumberStart));
                return result.ToList();
            }
        }

        // Retrieve channel data records between two week numbers (inclusive)
        public async Task<List<sp_ChannelDashboardData_Result>> GetChannelDashboardData(string channel, int weekNumberStart, int weekNumberEnd)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.sp_ChannelDashboardData(channel, weekNumberStart, weekNumberEnd));
                return result.OrderBy(x => x.WeekNumber).ToList();
            }
        }
        
        // Retrieve division aggregates for single week and channel, including subtotal row
        public async Task<List<sp_AllChannelDashboardData_Result>> GetAllChannelDashboardData(string channel, int weekNumberStart)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.sp_AllChannelDashboardData(channel, weekNumberStart));
                return result.OrderBy(x => x.WeekNumber).ToList();
            }
        }

    }
}
