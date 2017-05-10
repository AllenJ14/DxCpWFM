using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IStoreManager
    {
        Task<UserAccess> GetAuthLevel(string userName);

        Task<Store> GetStoreDetails(string ip);

        Task<List<Store>> GetAllStoreDetails(string ip);

        Task<Store> GetStoreDetailsFullIP(string ip);

        Task SubmitNewIdStoreRecord(int storeNum, string ip);

        Task<Store> GetStoreDetails(int storeNumber);

        Task<List<StoreOpeningTime>> GetStoreOpeningTimes(int storeNumber);

        Task<StoreOpeningTime> GetSingleTime(int entryId, int storeNumber, string status);

        Task<StoreOpeningTime> GetSingleTime(int entryId);

        Task<int> EditSingleTime(StoreOpeningTimeView vm, string userName, int storeNumber);

        Task<int> ConfirmPeakEntry(int? entryId, string userName, int storeNumber);

        Task<int> CancelRequest(int entryId, string userName, int storeNumber);

        Task<List<udsp_GetPandL_Result>> GetStorePandL(int storeNumber);

        Task<List<udsp_GetPandL_Result>> GetStorePandL(int storeNumber, string year, int? month);

        Task<List<udsp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber);

        Task<List<udsp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber, string year, int? month);

        Task ProposeNewStoreOpeningTime(ChangeStoreOpeningTimeViewModel changeVm, int storeNumber, string userName);

        Task<List<StoreHoursForApproval>> GetPendingStoreOpeningTimes();

        Task<StoreOpeningTime> GetCurrentOpeningTimes(int storeNumber);

        Task<List<OpeningTimesComment>> GetAllComments(int entryId);

        Task ApproveRejectPendingRequest(int entryId, bool approved);

        Task AddOpeningTimesComment(int EntryID, string Comment, string userName);

        Task<List<StoreOpeningTime>> GetPendingStoreOpeningTimes(int storeNumber, int entryId);

        //Task ApproveRejectOpeningTimes(List<PendingOpeningTimeView> openingTimes);

        Task<List<WeeklyFootfall>> GetWeeklyFootFall(int storeNumber, int weekNumber, string invGrpYear);

        Task<List<DailyFootfall>> GetDailyFootFall(int storeNumber, int dayNumber, int weekNum);
        
        Task<List<Store>> GetStoresForRegion(string regionNo);

        Task<List<Store>> GetRegionsForDivision(string divisionName);

        Task<List<Store>> GetDivisionsForChannel(string channelName);

        Task<DailyDeployment> GetDailyDeployment(int storeNumber, int weekNum);

        Task<List<int?>> GetWeekNumbers(DateTime startDate, DateTime endDate);

        int? GetSingleWeek(DateTime dt);

        Task<List<EmpComplianceDetail>> GetComplianceDetail(int storeNumber, int weekNum);

        Task<List<udsp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName);

        Task<List<udsp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName, string year, int? month);

        Task<List<udsp_GetPandLChannel_Result>> GetChannelPandL(string channelName);

        Task<List<udsp_GetPandLChannel_Result>> GetChannelPandL(string channelName, string year, int? month);

        Task LogUnknownBranch(string ipRange, int branch);

        Task<List<CPW_Clocking_Data>> GetStorePunch(int storeNumber, int weekNum);

        Task<List<ScheduleData>> GetBranchSchedule(int storeNumber, int weekNumber);

        Task<List<sp_RegionPunchCompliance_Result>> GetRegionPunch(string regionNo, int weekNum);

        Task<List<Store>> GetAllRegions();

        Task<List<sp_DivisionPunchCompliance_Result>> GetDivisionPunch(string Division, int weekNum);

        Task<List<sp_ChannelPunchCompliance_Result>> GetChannelPunch(string Channel, int weekNum);
    }
}
