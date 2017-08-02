using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using System.Data.Entity;
using System.Globalization;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.Business.Helpers;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using DixonsCarphone.WorkforceManagement.ViewModels;
using System.Configuration;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class StoreManager : IStoreManager
    {
        public enum RegionOrder
        {
            SAS = 0, SIS = 1, ROI = 2
        }

        public async Task<List<DailyFootfall>> GetDailyFootFall(int storeNumber, int dayNumber, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var day = ((DayOfWeek)dayNumber).ToString();
                var result = await dbContext.DailyFootfalls.Where(x => x.Branch == storeNumber && x.WeekNumber == weekNum && x.Day == day).ToListAsync();
                return result;
            }
        }

        //Get Auth Level
        public async Task<List<UserAccess>> GetAuthLevel(string userName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.UserAccesses.Where(x => x.User == userName).ToListAsync();
            }
        }

        //Check CPWC GM
        public int CheckCPWCAuth(string payroll)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = dbContext.CPCW_Managers.Where(x => x.Emp_num == payroll);
                return result.Count() > 0 ? 1 : 0;
            }
        }

        //Get week list
        public async Task<List<int?>> GetWeekNumbers(DateTime startDate, DateTime endDate)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.udsp_GetWeekList(startDate.Date, endDate.Date));
                return result.ToList();
            }
        }

        //Get single week
        public int? GetSingleWeek(DateTime dt)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = Task.Run(() => dbContext.udsp_GetWeekList(dt, dt));
                return result.Result.Single();
            }
        }

        //Get daily deployment record for specific store & week
        public async Task<DailyDeployment> GetDailyDeployment(int storeNumber, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.DailyDeployments.FirstOrDefaultAsync(x => x.StoreNumber == storeNumber && x.WeekNumber == weekNum);
                return result;
            }
        }

        //Get compliance detail record for specific store & week
        public async Task<List<EmpComplianceDetail>> GetComplianceDetail(int storeNumber, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.EmpComplianceDetails.Where(x => x.StoreNumber == storeNumber && x.WeekNumber == weekNum).ToListAsync();
                return result;
            }
        }

        //Get punch detail for branch
        public async Task<List<CPW_Clocking_Data>> GetStorePunch(int storeNumber, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.CPW_Clocking_Data.AsNoTracking().Where(x => x.CST_CNTR_int == storeNumber && x.FNCL_WK_NUM == weekNum).ToListAsync();
                return result;
            }
        }

        //Get punch detail for region
        public async Task<List<sp_RegionPunchCompliance_Result>> GetRegionPunch(string regionNo, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_RegionPunchCompliance(regionNo, weekNum));
                return data.ToList();
            }
        }

        //Get punch detail for region BMs
        public async Task<List<CPW_Clocking_Data>> GetRegionBMPunch(string regionNo, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.CPW_Clocking_Data.AsNoTracking().Where(x => x.REGION_CD == regionNo && x.ROLE == "1" && x.FNCL_WK_NUM == weekNum).ToListAsync();
                return result;
            }
        }

        //Get punch detail for Division
        public async Task<List<sp_DivisionPunchCompliance_Result>> GetDivisionPunch(string Division, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_DivisionPunchCompliance(Division, weekNum));
                return data.ToList();
            }
        }

        //Get punch detail for Channel
        public async Task<List<sp_ChannelPunchCompliance_Result>> GetChannelPunch(string Channel, int weekNum)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_ChannelPunchCompliance(Channel, weekNum));
                return data.ToList();
            }
        }

        ////Get compliance detail record for specific region & week
        //public async Task<List<EmpComplianceDetail>> GetComplianceDetailRegion(int regionNumber, int weekNum)
        //{
        //    using (var dbContext = new DxCpWfmContext())
        //    {
        //        var result = await dbContext.EmpComplianceDetails.Where(x => x.StoreNumber == storeNumber && x.WeekNumber == weekNum).ToListAsync();
        //        return result;
        //    }
        //}

        //Returns list of all regions
        public async Task<List<Store>> GetAllRegions()
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.GroupBy(x => x.RegionNo).Select(x => x.FirstOrDefault()).OrderBy(x => x.Channel).ToListAsync();
                return result;
            }
        }

        //Returns list of all stores in a given region
        public async Task<List<Store>> GetStoresForRegion(string regionNo)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.RegionNo == regionNo).ToListAsync();
                return result;
            }
        }

        //Returns list of all regions in a given division
        public async Task<List<Store>> GetRegionsForDivision(string divisionName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.Division == divisionName).GroupBy(x => x.RegionNo).Select(x => x.FirstOrDefault()).OrderBy(x => x.RegionNo).ToListAsync();
                return result;
            }
        }

        //Returns list of all stores in a given channel
        public async Task<List<Store>> GetDivisionsForChannel(string channelName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.Channel == channelName).GroupBy(x => x.Division).Select(x => x.FirstOrDefault()).OrderBy(x => x.RegionNo).ToListAsync();
                return result;
            }
        }

        //Returns list of all stores in a given channel
        public async Task<List<Store>> GetAllROIStores()
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.Channel == "ROI").OrderBy(x => x.CST_CNTR_ID).ToListAsync();
                return result;
            }
        }

        //Get Store from branch number
        public async Task<Store> GetStoreDetails(int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.FirstOrDefaultAsync(x => x.CST_CNTR_ID == storeNumber);
                return result;
            }
        }

        //Log unidentified branch request
        public async Task LogUnknownBranch(string ipRange, int branch)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                IncorrectBranchLog data = new IncorrectBranchLog
                {
                    ChangeToBranch = (short)branch,
                    IpSource = ipRange,
                    Timestamp = DateTime.Now,
                    ReasonForChange = "UnknownStore"
                };

                dbContext.IncorrectBranchLogs.Add(data);
                await dbContext.SaveChangesAsync();
            }
            return;
        }

        //Get schedule detail for week and branch
        public async Task<List<ScheduleData>> GetBranchSchedule(int storeNumber, int weekNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.ScheduleDatas.Where(x => x.WeekNumber == weekNumber && (x.HomeBranch == storeNumber || x.TransferBranch == ("UK " + storeNumber.ToString()) || x.TransferBranch == ("IE " + storeNumber.ToString()))).OrderBy(x => x.JobCode).ThenBy(x => x.FullName).ThenBy(x => x.DayNum).ToListAsync();
                return result;
            }
        }

        //Get BM schedule detail for week and region
        public async Task<List<sp_GetRegionBMSchedule_Result>> GetRegionBMSchedule(string regionNumber, int weekNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await Task.Run(() => dbContext.sp_GetRegionBMSchedule(regionNumber, weekNumber).OrderBy(x => x.HomeBranch).ThenBy(x => x.FullName).ThenBy(x => x.DayNum).ToList());
                return result;
            }
        }

        //Get most recent P&L recordset for store
        public async Task<List<udsp_GetPandL_Result>> GetStorePandL(int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandL(storeNumber, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<udsp_GetPandL_Result>();
            }
        }

        //Get single P&L recordset for store, year, month
        public async Task<List<udsp_GetPandL_Result>> GetStorePandL(int storeNumber, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 -2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandL(storeNumber, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get most recent P&L recordset for region
        public async Task<List<udsp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandLRegion(regionNumber, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<udsp_GetPandLRegion_Result>();
            }
        }

        //Get single P&L recordset for region, year, month
        public async Task<List<udsp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandLRegion(regionNumber, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get most recent P&L recordset for division
        public async Task<List<udsp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandLDivision(divisionName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<udsp_GetPandLDivision_Result>();
            }
        }

        //Get single P&L recordset for division, year, month
        public async Task<List<udsp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandLDivision(divisionName, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get most recent P&L recordset for channel
        public async Task<List<udsp_GetPandLChannel_Result>> GetChannelPandL(string channelName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandLChannel(channelName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<udsp_GetPandLChannel_Result>();
            }
        }

        //Get single P&L recordset for channel, year, month
        public async Task<List<udsp_GetPandLChannel_Result>> GetChannelPandL(string channelName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandLChannel(channelName, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get P&L Summary channel
        public async Task<List<udsp_GetPandLChannelSummary_Result>> GetChannelPLSummary(string channelName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandLChannelSummary(channelName, year, (short?)month, (short?)qtdStart, 1));

                return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
            }
        }

        //Get most recent P&L Summary channel
        public async Task<List<udsp_GetPandLChannelSummary_Result>> GetChannelPLSummary(string channelName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandLChannelSummary(channelName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
                }
                return new List<udsp_GetPandLChannelSummary_Result>();
            }
        }

        //Get P&L Summary division
        public async Task<List<udsp_GetPandLDivisionSummary_Result>> GetDivisionPLSummary(string divisionName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandLDivisionSummary(divisionName, year, (short?)month, (short?)qtdStart, 1));

                return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
            }
        }

        //Get most recent P&L Summary division
        public async Task<List<udsp_GetPandLDivisionSummary_Result>> GetDivisionPLSummary(string divisionName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandLDivisionSummary(divisionName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
                }
                return new List<udsp_GetPandLDivisionSummary_Result>();
            }
        }

        //Get P&L Summary channel
        public async Task<List<udsp_GetPandLRegionSummary_Result>> GetRegionPLSummary(string region, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.udsp_GetPandLRegionSummary(region, year, (short?)month, (short?)qtdStart, 1));

                return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
            }
        }

        //Get most recent P&L Summary channel
        public async Task<List<udsp_GetPandLRegionSummary_Result>> GetRegionPLSummary(string region)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.udsp_GetPandLRegionSummary(region, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
                }
                return new List<udsp_GetPandLRegionSummary_Result>();
            }
        }

        //Get footfall for branch, week, year
        public async Task<List<footfall_raw>> GetBranchFootfall(int branchNumber, string year, int weeknumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.footfall_raw.Where(x => x.Branch_Number == branchNumber && x.Invoice_Group_Week_Number == weeknumber && x.Invoice_Group_Year_Name == year).ToListAsync();
            }
        }

        //Get footfall for region, week, year
        public async Task<List<sp_GetRegionFootfall_Result>> GetRegionFootfall(string regionNumber, string year, int weeknumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await Task.Run(() => dbContext.sp_GetRegionFootfall(regionNumber, (short?)weeknumber, year).ToList());
            }
        }

        //Get footfall for division, week, year
        public async Task<List<sp_GetDivisionFootfall_Result>> GetDivisionFootfall(string divisionName, string year, int weeknumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await Task.Run(() => dbContext.sp_GetDivisionFootfall(divisionName, (short?)weeknumber, year).ToList());
            }
        }

        // Queries Stores table for cost centre matching the given IP address
        public async Task<Store> GetStoreDetails(string ip)
        {
            var ipBase = ip == "::1" ? "127.0.1" : ip.Substring(0, ip.LastIndexOf("."));
            
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.IpRange == ipBase).ToListAsync();

                if(result.Count() > 1)
                {
                    return new Store { IpRange = "DUPLICATE" };
                }
                else
                {
                    return result.FirstOrDefault();
                }
            }
        }

        // Queries Stores table for ALL cost centre matching the given IP address
        public async Task<List<Store>> GetAllStoreDetails(string ip)
        {
            var ipBase = ip == "::1" ? "127.0.1" : ip.Substring(0, ip.LastIndexOf("."));

            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.IpRange == ipBase).ToListAsync();

                return result;
            }
        }

        // Queries Stores table for ALL cost centre matching the given IP address
        public async Task<Store> GetStoreDetailsFullIP(string ip)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var storenum = await dbContext.IpRefs.Where(x => x.IpRange == ip).FirstOrDefaultAsync();

                if(storenum != null)
                {
                    var result = await dbContext.Stores.Where(x => x.CST_CNTR_ID == storenum.storeNumber).FirstOrDefaultAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        //Submit new full IP identification record
        public async Task SubmitNewIdStoreRecord(int storeNum, string ip)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                IpRef _ipRef = new IpRef
                {
                    storeNumber = storeNum,
                    IpRange = ip
                };
                dbContext.IpRefs.Add(_ipRef);
                await dbContext.SaveChangesAsync();
            }
            return;
        }

        //Submit new ROI full IP identification record
        public async Task SubmitROIStoreRecord(int storeNum, string ip)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                ROIIpRef _ipRef = new ROIIpRef
                {
                    storeNumber = storeNum,
                    IpRange = ip
                };
                dbContext.ROIIpRefs.Add(_ipRef);
                await dbContext.SaveChangesAsync();
            }
            return;
        }

        //Get current opening time for specific store
        public async Task<StoreOpeningTime> GetCurrentOpeningTimes(int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.StoreOpeningTimes.Where(x => x.StoreNumber == storeNumber && x.Status == "Live").SingleOrDefaultAsync();
                return result;
            }
        }

        //Get all opening times that are not expired/rejected for specific store
        public async Task<List<StoreOpeningTime>> GetStoreOpeningTimes(int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var list = new[] { "Live", "Pending", "PendingApproval", "PeakPending", "PeakConfirmed" };
                var result = await dbContext.StoreOpeningTimes.Where(x => x.StoreNumber == storeNumber && list.Contains(x.Status)).ToListAsync();
                return result;
            }
        }

        //Get all opening times that are pending
        public async Task<List<StoreHoursForApproval>> GetPendingStoreOpeningTimes()
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.StoreOpeningTimes.Where(x => x.Status == "PendingApproval").OrderBy(x => x.StoreNumber).Select(x => new StoreHoursForApproval { EntryId = x.EntryID, StoreNumber = x.StoreNumber, DateTimeSubmitted = x.DateTimeSubmitted, EffectiveDate = x.EffectiveDate }).ToListAsync();
                return result;
            }
        }

        //Get all comments associated with request
        public async Task<List<OpeningTimesComment>> GetAllComments(int entryId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.OpeningTimesComments.Where(x => x.EntryID == entryId).ToListAsync();
                return result;
            }
        }

        //Add comment to request
        public async Task AddOpeningTimesComment(int EntryID, string Comment, string userName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                OpeningTimesComment _newComment = new OpeningTimesComment{
                    EntryID = EntryID,
                    Comment = Comment,
                    AddedBy = userName,
                    Datetime = DateTime.Now
                };
                dbContext.OpeningTimesComments.Add(_newComment);
                await dbContext.SaveChangesAsync();
            }
        }

        //Approve pending opening time
        public async Task ApproveRejectPendingRequest(int entryId, bool approved)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = dbContext.StoreOpeningTimes.Find(entryId);
                result.Status = approved ? "Pending" : "Declined";
                await dbContext.SaveChangesAsync();
            }
        }

        //Get all opening times that are pending for specific store, excluding single
        public async Task<List<StoreOpeningTime>> GetPendingStoreOpeningTimes(int storeNumber, int entryId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.StoreOpeningTimes.Where(x => x.Status == "PendingApproval" && x.StoreNumber == storeNumber && x.EntryID != entryId).ToListAsync();
                return result;
            }
        }

        //Get single opening time by entry id
        public async Task<StoreOpeningTime> GetSingleTime(int entryId, int storeNumber, string status)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.StoreOpeningTimes.Where(x => x.EntryID == entryId && x.StoreNumber == storeNumber && x.Status.StartsWith(status)).SingleOrDefaultAsync();
                return result;
            }
        }

        //Get single opening time by entry id
        public async Task<StoreOpeningTime> GetSingleTime(int entryId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.StoreOpeningTimes.Where(x => x.EntryID == entryId).SingleOrDefaultAsync();
                return result;
            }
        }

        //Edit single opening time
        public async Task<int> EditSingleTime(StoreOpeningTimeView editVm, string userName, int storeNumber)
        {
            if (editVm == null) return 0;

            editVm.ModifiedByUser = userName;
            editVm.DateTimeModified = DateTime.Now;
            
            using (var dbContext = new DxCpWfmContext())
            {
                var existing =  await dbContext.StoreOpeningTimes.FirstOrDefaultAsync(x => x.EntryID == editVm.EntryID && x.StoreNumber == storeNumber && x.Status.StartsWith("Peak"));
                if (existing != null)
                {
                    //set status depending on peak or normal request
                    if (existing.Status.Contains("Peak"))
                    {
                        editVm.Status = "PeakPending";
                    }
                    else
                    {
                        editVm.Status = "PendingApproval";
                    }

                    editVm.DateTimeSubmitted = existing.DateTimeSubmitted;
                    editVm.EffectiveDate = existing.EffectiveDate;
                    editVm.EndDate = existing.EndDate;
                    editVm.ReasonForChange = existing.ReasonForChange;
                    editVm.StoreNumber = existing.StoreNumber;
                    editVm.SubmittedByUser = existing.SubmittedByUser;
                    editVm.TemporaryChange = existing.TemporaryChange;

                    var updates = MapSingleViewModelToPending(editVm);
                    dbContext.Entry(existing).CurrentValues.SetValues(updates);
                    await dbContext.SaveChangesAsync();
                    return 1;
                }
            }
            return 0;
        }

        //Confirm peak opening entry
        public async Task<int> ConfirmPeakEntry(int? entryId, string userName, int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var existing = dbContext.StoreOpeningTimes.Where(x => x.EntryID == entryId && x.StoreNumber == storeNumber && x.Status == "PeakPending").FirstOrDefault();

                if(existing != null)
                {
                    existing.Status = "PeakConfirmed";
                    existing.ModifiedByUser = userName;
                    existing.DateTimeModified = DateTime.Now;
                    await dbContext.SaveChangesAsync();
                    return 1;
                }
                
            }
            return 0;
        }

        //Cancel pending change request
        public async Task<int> CancelRequest(int entryId, string userName, int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var existing = dbContext.StoreOpeningTimes.Where(x => x.EntryID == entryId && x.StoreNumber == storeNumber && x.Status.StartsWith("Pending")).FirstOrDefault();

                if(existing != null)
                {
                    existing.Status = "Cancelled";
                    existing.ModifiedByUser = userName;
                    existing.DateTimeModified = DateTime.Now;
                    await dbContext.SaveChangesAsync();
                    return 1;
                }
            }
            return 0;
        }

        //Submit new opening time
        public async Task ProposeNewStoreOpeningTime(ChangeStoreOpeningTimeViewModel changeVm, int storeNumber, string userName)
        {
            if (changeVm == null) return;

            using (var dbContext = new DxCpWfmContext())
            {
                var store = await dbContext.Stores.FirstOrDefaultAsync(x => x.CST_CNTR_ID == storeNumber);

                var proposed = MapChangeViewModelToPending(changeVm, store.CST_CNTR_ID);
                proposed.SubmittedByUser = userName;

                dbContext.StoreOpeningTimes.Add(proposed);

                await dbContext.SaveChangesAsync();
            }
        }

        // Map view model to entity
        private StoreOpeningTime MapChangeViewModelToPending(ChangeStoreOpeningTimeViewModel changeVm, int storeNumber)
        {
            var proposed = new StoreOpeningTime();
            proposed.DateTimeModified = DateTime.Now;
            proposed.DateTimeSubmitted = DateTime.Now;

            proposed.MondayOpen = TimeSpan.ParseExact(changeVm.NewMonOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.MondayClose = TimeSpan.ParseExact(changeVm.NewMonClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.TuesdayOpen = TimeSpan.ParseExact(changeVm.NewTuesOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.TuesdayClose = TimeSpan.ParseExact(changeVm.NewTuesClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.WednesdayOpen = TimeSpan.ParseExact(changeVm.NewWedOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.WednesdayClose = TimeSpan.ParseExact(changeVm.NewWedClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.ThursdayOpen = TimeSpan.ParseExact(changeVm.NewThursOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.ThursdayClose = TimeSpan.ParseExact(changeVm.NewThursClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.FridayOpen = TimeSpan.ParseExact(changeVm.NewFriOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.FridayClose = TimeSpan.ParseExact(changeVm.NewFriClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.SaturdayOpen = TimeSpan.ParseExact(changeVm.NewSatOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.SaturdayClose = TimeSpan.ParseExact(changeVm.NewSatClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.SundayOpen = TimeSpan.ParseExact(changeVm.NewSunOpening, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            proposed.SundayClose = TimeSpan.ParseExact(changeVm.NewSunClosing, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);

            proposed.StoreNumber = storeNumber;
            proposed.EffectiveDate = changeVm.EffectiveDate;
            proposed.ReasonForChange = changeVm.ReasonForChange;
            proposed.Status = "PendingApproval";
            proposed.TemporaryChange = false;

            return proposed;
        }

        // Map view model to entity
        private StoreOpeningTime MapSingleViewModelToPending(StoreOpeningTimeView changeVm)
        {
            var edit = new StoreOpeningTime();
            edit.MondayOpen = changeVm.MondayOpen;
            edit.MondayClose = changeVm.MondayClose;
            edit.TuesdayOpen = changeVm.TuesdayOpen;
            edit.TuesdayClose = changeVm.TuesdayClose;
            edit.WednesdayOpen = changeVm.WednesdayOpen;
            edit.WednesdayClose = changeVm.WednesdayClose;
            edit.ThursdayOpen = changeVm.ThursdayOpen;
            edit.ThursdayClose = changeVm.ThursdayClose;
            edit.FridayOpen = changeVm.FridayOpen;
            edit.FridayClose = changeVm.FridayClose;
            edit.SaturdayOpen = changeVm.SaturdayOpen;
            edit.SaturdayClose = changeVm.SaturdayClose;
            edit.SundayOpen = changeVm.SundayOpen;
            edit.SundayClose = changeVm.SundayClose;
            edit.EntryID = changeVm.EntryID;
            edit.DateTimeModified = changeVm.DateTimeModified;
            edit.DateTimeSubmitted = changeVm.DateTimeSubmitted;
            edit.EffectiveDate = changeVm.EffectiveDate;
            edit.EndDate = changeVm.EndDate;
            edit.ModifiedByUser = changeVm.ModifiedByUser;
            edit.ReasonForChange = changeVm.ReasonForChange;
            edit.Status = changeVm.Status;
            edit.StoreNumber = changeVm.StoreNumber;
            edit.SubmittedByUser = changeVm.SubmittedByUser;
            edit.TemporaryChange = changeVm.TemporaryChange;

            return edit;
        }

        public async Task<List<WeeklyFootfall>> GetWeeklyFootFall(int storeNumber, int weekNumber, string invGrpYear)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var result = await dbContext.WeeklyFootfalls.Where(x => x.Branch == storeNumber && x.InvoiceGroupWeekNumber == weekNumber && x.InvoiceGroupYearName == invGrpYear).ToListAsync();
                return result;
            }
        }

        public async Task<List<PublishedBudgetsBranch_Result>> GetBudgetsBranch(int storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.PublishedBudgetsBranch(storeNumber));

                return data.ToList();
            }
        }

        public async Task<List<PublishedBudgetsRegion_Result>> GetBudgetsRegion(string regionNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.PublishedBudgetsRegion(regionNumber));

                return data.ToList();
            }
        }

        public async Task<List<HolidayPlanningStore>> GetStoreHoliday(string storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.HolidayPlanningStores.Where(x => x.BranchNumber == storeNumber).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<HolidayPlanningEmp>> GetEmpHoliday(string storeNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var branch = int.Parse(storeNumber);
                return await dbContext.HolidayPlanningEmps.Where(x => x.BranchNumber == branch).OrderBy(x => x.EmployeeName).ToListAsync();
            }
        }

        public async Task<List<HolidayPlanningStore>> GetRegionHoliday(string regionNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_RegionHolidayPlanning(regionNumber));

                return data.ToList();
            }
        }

        public async Task<List<HolidayPlanningEmp>> GetRegionHolidayAll(string regionNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_RegionHolidayPlanningEmp(regionNumber));

                return data.ToList();
            }
        }

        public async Task<List<HolidayPlanningStore>> GetDivisionHoliday(string division)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_DivisionHolidayPlanning(division));

                return data.ToList();
            }
        }

        public async Task<List<HolidayPlanningEmp>> GetDivisionHolidayAll(string division)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_DivisionHolidayPlanningEmp(division));

                return data.ToList();
            }
        }

        public async Task<List<BmWeWorking>> GetBmWeWorking(string regionNumber)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var limit = DateTime.Today.AddDays(-28);
                var crit = short.Parse(regionNumber);
                var data = await dbContext.BmWeWorkings.Where(x => x.RegionNum == crit && x.Date >= limit).OrderBy(x => x.BranchNum).ThenBy(x => x.Date).ToListAsync();
                return data;
            }
        }

        public async Task<List<sp_DivisionBMWorking_Result>> GetDivisionBmWeWorking(string divisionName)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_DivisionBMWorking(divisionName).ToList());
                return data;
            }
        }

        public async Task<List<sp_ChannelBMWorking_Result>> GetChannelBmWeWorking(string channel)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                var data = await Task.Run(() => dbContext.sp_ChannelBMWorking(channel).ToList());
                return data;
            }
        }

        private AccountEntryView MapToAccountEntryView(AccountEntryHeader data)
        {
            var toRtn = new AccountEntryView
            {
                AccountEntryHeaderId = data.AccountEntryHeaderId,
                AccountEntryHeaderText = data.AccountEntryHeaderText,
                ManagerName = data.ManagerName,
                //PeriodMonth = data.PeriodMonth,
                PeriodYear = data.PeriodYear,
                StoreNumber = data.StoreNumber,
                Details = MapAccountEntryDetails(data?.AccountEntryDetails.ToList())
            };

            return toRtn;
        }

        private List<AccountEntryDetailView> MapAccountEntryDetails(List<AccountEntryDetail> accountEntryDetails)
        {
            accountEntryDetails = accountEntryDetails ?? new List<AccountEntryDetail>();
            var toRtn = new List<AccountEntryDetailView>();

            foreach (var detail in accountEntryDetails)
            {
                toRtn.Add(new AccountEntryDetailView
                {
                    AccountEntryDetailId = detail.AccountEntryDetailId,
                    AccountEntrySubTypeId = detail.AccountEntrySubTypeId,
                    AccountEntryHeaderId = detail.AccountEntryHeaderId,
                    AccountEntryTypeId = detail?.AccountEntySubType?.AccountEntryTypeId,
                    AccountEntrySubTypeText = detail?.AccountEntySubType?.AccountEntySubTypeName,
                    ActualAmount = detail.ActualAmount,
                    BudgetAmount = detail.BudgetAmount,
                    DetailText = detail.DetailText,
                    DetailTitle = detail.DetailTitle,
                    AccountEntryDetailBreakdowns = MappDetailBreakdowns(detail.AccountEntryDetailBreakdowns)
                });
            }

            return toRtn;
        }

        private List<AccountEntryDetailBreakdownView> MappDetailBreakdowns(ICollection<AccountEntryDetailBreakdown> accountEntryDetailBreakdowns)
        {
            var toRtn = new List<AccountEntryDetailBreakdownView>();
            if (accountEntryDetailBreakdowns != null)
            {
                foreach (var detail in accountEntryDetailBreakdowns)
                {
                    toRtn.Add(new AccountEntryDetailBreakdownView
                    {
                        AccountEntryDetailBreakDownId = detail.AccountEntryDetailBreakDownId,
                        AccountEntryDetailId = detail.AccountEntryDetailId,
                        ActualAmount = detail.ActualAmount,
                        BreakdownText = detail.BreakdownText,
                        BreakdownTitle = detail.BreakdownTitle,
                        BudgetAmount = detail.BudgetAmount
                    });
                }
            }

            return toRtn;
        }

    }
}
