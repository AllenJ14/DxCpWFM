﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DixonsCarphone.WorkforceManagement.Business.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DxCpWfmContext : DbContext
    {
        public DxCpWfmContext()
            : base("name=DxCpWfmContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<DailyFootfall> DailyFootfalls { get; set; }
        public virtual DbSet<HrFeed> HrFeeds { get; set; }
        public virtual DbSet<StaffType> StaffTypes { get; set; }
        public virtual DbSet<WeeklyFootfall> WeeklyFootfalls { get; set; }
        public virtual DbSet<BestPracticeCategoryType> BestPracticeCategoryTypes { get; set; }
        public virtual DbSet<BestPracticeContent> BestPracticeContents { get; set; }
        public virtual DbSet<StoreRegion> StoreRegions { get; set; }
        public virtual DbSet<AccountEntryDetail> AccountEntryDetails { get; set; }
        public virtual DbSet<AccountEntryHeader> AccountEntryHeaders { get; set; }
        public virtual DbSet<AccountEntryType> AccountEntryTypes { get; set; }
        public virtual DbSet<AccountEntySubType> AccountEntySubTypes { get; set; }
        public virtual DbSet<ContentDetail> ContentDetails { get; set; }
        public virtual DbSet<ContentHeader> ContentHeaders { get; set; }
        public virtual DbSet<AccountEntryDetailBreakdown> AccountEntryDetailBreakdowns { get; set; }
        public virtual DbSet<PriorityType> PriorityTypes { get; set; }
        public virtual DbSet<OpeningTimesComment> OpeningTimesComments { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreOpeningTime> StoreOpeningTimes { get; set; }
        public virtual DbSet<DailyDeployment> DailyDeployments { get; set; }
        public virtual DbSet<EmpComplianceDetail> EmpComplianceDetails { get; set; }
        public virtual DbSet<UserAccess> UserAccesses { get; set; }
        public virtual DbSet<DashBoardData> DashBoardDatas { get; set; }
        public virtual DbSet<IncorrectBranchLog> IncorrectBranchLogs { get; set; }
        public virtual DbSet<CPW_Clocking_Data> CPW_Clocking_Data { get; set; }
        public virtual DbSet<SiteFeedback> SiteFeedbacks { get; set; }
        public virtual DbSet<ScheduleData> ScheduleDatas { get; set; }
        public virtual DbSet<FileUploadRecord> FileUploadRecords { get; set; }
        public virtual DbSet<IpRef> IpRefs { get; set; }
        public virtual DbSet<ROIIpRef> ROIIpRefs { get; set; }
        public virtual DbSet<ContractBaseDetail> ContractBaseDetails { get; set; }
        public virtual DbSet<HolidayPlanningStore> HolidayPlanningStores { get; set; }
        public virtual DbSet<HolidayPlanningEmp> HolidayPlanningEmps { get; set; }
        public virtual DbSet<TicketAnswer> TicketAnswers { get; set; }
        public virtual DbSet<TicketAudit> TicketAudits { get; set; }
        public virtual DbSet<TicketComment> TicketComments { get; set; }
        public virtual DbSet<TicketStub> TicketStubs { get; set; }
        public virtual DbSet<TicketTemplate> TicketTemplates { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<TicketEscalation> TicketEscalations { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }
    
        public virtual ObjectResult<udsp_GetPandL_Result> udsp_GetPandL(Nullable<int> storeNumber, string periodYear, Nullable<short> periodMonth, Nullable<short> qtdStartMonth, Nullable<short> ytdStartMonth)
        {
            var storeNumberParameter = storeNumber.HasValue ?
                new ObjectParameter("StoreNumber", storeNumber) :
                new ObjectParameter("StoreNumber", typeof(int));
    
            var periodYearParameter = periodYear != null ?
                new ObjectParameter("PeriodYear", periodYear) :
                new ObjectParameter("PeriodYear", typeof(string));
    
            var periodMonthParameter = periodMonth.HasValue ?
                new ObjectParameter("PeriodMonth", periodMonth) :
                new ObjectParameter("PeriodMonth", typeof(short));
    
            var qtdStartMonthParameter = qtdStartMonth.HasValue ?
                new ObjectParameter("QtdStartMonth", qtdStartMonth) :
                new ObjectParameter("QtdStartMonth", typeof(short));
    
            var ytdStartMonthParameter = ytdStartMonth.HasValue ?
                new ObjectParameter("YtdStartMonth", ytdStartMonth) :
                new ObjectParameter("YtdStartMonth", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<udsp_GetPandL_Result>("udsp_GetPandL", storeNumberParameter, periodYearParameter, periodMonthParameter, qtdStartMonthParameter, ytdStartMonthParameter);
        }
    
        [DbFunction("DxCpWfmContext", "fn_LatestPLRecord")]
        public virtual IQueryable<fn_LatestPLRecord_Result> fn_LatestPLRecord()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fn_LatestPLRecord_Result>("[DxCpWfmContext].[fn_LatestPLRecord]()");
        }
    
        public virtual ObjectResult<udsp_GetPandLRegion_Result> udsp_GetPandLRegion(string regionNumber, string periodYear, Nullable<short> periodMonth, Nullable<short> qtdStartMonth, Nullable<short> ytdStartMonth)
        {
            var regionNumberParameter = regionNumber != null ?
                new ObjectParameter("RegionNumber", regionNumber) :
                new ObjectParameter("RegionNumber", typeof(string));
    
            var periodYearParameter = periodYear != null ?
                new ObjectParameter("PeriodYear", periodYear) :
                new ObjectParameter("PeriodYear", typeof(string));
    
            var periodMonthParameter = periodMonth.HasValue ?
                new ObjectParameter("PeriodMonth", periodMonth) :
                new ObjectParameter("PeriodMonth", typeof(short));
    
            var qtdStartMonthParameter = qtdStartMonth.HasValue ?
                new ObjectParameter("QtdStartMonth", qtdStartMonth) :
                new ObjectParameter("QtdStartMonth", typeof(short));
    
            var ytdStartMonthParameter = ytdStartMonth.HasValue ?
                new ObjectParameter("YtdStartMonth", ytdStartMonth) :
                new ObjectParameter("YtdStartMonth", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<udsp_GetPandLRegion_Result>("udsp_GetPandLRegion", regionNumberParameter, periodYearParameter, periodMonthParameter, qtdStartMonthParameter, ytdStartMonthParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> udsp_GetWeekList(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("udsp_GetWeekList", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<udsp_GetPandLChannel_Result> udsp_GetPandLChannel(string channelName, string periodYear, Nullable<short> periodMonth, Nullable<short> qtdStartMonth, Nullable<short> ytdStartMonth)
        {
            var channelNameParameter = channelName != null ?
                new ObjectParameter("ChannelName", channelName) :
                new ObjectParameter("ChannelName", typeof(string));
    
            var periodYearParameter = periodYear != null ?
                new ObjectParameter("PeriodYear", periodYear) :
                new ObjectParameter("PeriodYear", typeof(string));
    
            var periodMonthParameter = periodMonth.HasValue ?
                new ObjectParameter("PeriodMonth", periodMonth) :
                new ObjectParameter("PeriodMonth", typeof(short));
    
            var qtdStartMonthParameter = qtdStartMonth.HasValue ?
                new ObjectParameter("QtdStartMonth", qtdStartMonth) :
                new ObjectParameter("QtdStartMonth", typeof(short));
    
            var ytdStartMonthParameter = ytdStartMonth.HasValue ?
                new ObjectParameter("YtdStartMonth", ytdStartMonth) :
                new ObjectParameter("YtdStartMonth", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<udsp_GetPandLChannel_Result>("udsp_GetPandLChannel", channelNameParameter, periodYearParameter, periodMonthParameter, qtdStartMonthParameter, ytdStartMonthParameter);
        }
    
        public virtual ObjectResult<udsp_GetPandLDivision_Result> udsp_GetPandLDivision(string divisionName, string periodYear, Nullable<short> periodMonth, Nullable<short> qtdStartMonth, Nullable<short> ytdStartMonth)
        {
            var divisionNameParameter = divisionName != null ?
                new ObjectParameter("DivisionName", divisionName) :
                new ObjectParameter("DivisionName", typeof(string));
    
            var periodYearParameter = periodYear != null ?
                new ObjectParameter("PeriodYear", periodYear) :
                new ObjectParameter("PeriodYear", typeof(string));
    
            var periodMonthParameter = periodMonth.HasValue ?
                new ObjectParameter("PeriodMonth", periodMonth) :
                new ObjectParameter("PeriodMonth", typeof(short));
    
            var qtdStartMonthParameter = qtdStartMonth.HasValue ?
                new ObjectParameter("QtdStartMonth", qtdStartMonth) :
                new ObjectParameter("QtdStartMonth", typeof(short));
    
            var ytdStartMonthParameter = ytdStartMonth.HasValue ?
                new ObjectParameter("YtdStartMonth", ytdStartMonth) :
                new ObjectParameter("YtdStartMonth", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<udsp_GetPandLDivision_Result>("udsp_GetPandLDivision", divisionNameParameter, periodYearParameter, periodMonthParameter, qtdStartMonthParameter, ytdStartMonthParameter);
        }
    
        public virtual ObjectResult<sp_BranchPunchCompliance_Result> sp_BranchPunchCompliance(Nullable<int> storeNumber, Nullable<int> weekNumber)
        {
            var storeNumberParameter = storeNumber.HasValue ?
                new ObjectParameter("storeNumber", storeNumber) :
                new ObjectParameter("storeNumber", typeof(int));
    
            var weekNumberParameter = weekNumber.HasValue ?
                new ObjectParameter("weekNumber", weekNumber) :
                new ObjectParameter("weekNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BranchPunchCompliance_Result>("sp_BranchPunchCompliance", storeNumberParameter, weekNumberParameter);
        }
    
        public virtual ObjectResult<sp_RegionPunchCompliance_Result> sp_RegionPunchCompliance(string regionNumber, Nullable<int> weekNumber)
        {
            var regionNumberParameter = regionNumber != null ?
                new ObjectParameter("regionNumber", regionNumber) :
                new ObjectParameter("regionNumber", typeof(string));
    
            var weekNumberParameter = weekNumber.HasValue ?
                new ObjectParameter("weekNumber", weekNumber) :
                new ObjectParameter("weekNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RegionPunchCompliance_Result>("sp_RegionPunchCompliance", regionNumberParameter, weekNumberParameter);
        }
    
        public virtual ObjectResult<sp_ChannelPunchCompliance_Result> sp_ChannelPunchCompliance(string channelName, Nullable<int> weekNumber)
        {
            var channelNameParameter = channelName != null ?
                new ObjectParameter("ChannelName", channelName) :
                new ObjectParameter("ChannelName", typeof(string));
    
            var weekNumberParameter = weekNumber.HasValue ?
                new ObjectParameter("weekNumber", weekNumber) :
                new ObjectParameter("weekNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ChannelPunchCompliance_Result>("sp_ChannelPunchCompliance", channelNameParameter, weekNumberParameter);
        }
    
        public virtual ObjectResult<sp_DivisionPunchCompliance_Result> sp_DivisionPunchCompliance(string divisionName, Nullable<int> weekNumber)
        {
            var divisionNameParameter = divisionName != null ?
                new ObjectParameter("DivisionName", divisionName) :
                new ObjectParameter("DivisionName", typeof(string));
    
            var weekNumberParameter = weekNumber.HasValue ?
                new ObjectParameter("weekNumber", weekNumber) :
                new ObjectParameter("weekNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_DivisionPunchCompliance_Result>("sp_DivisionPunchCompliance", divisionNameParameter, weekNumberParameter);
        }
    
        public virtual ObjectResult<PublishedBudgetsRegion_Result> PublishedBudgetsRegion(string region)
        {
            var regionParameter = region != null ?
                new ObjectParameter("Region", region) :
                new ObjectParameter("Region", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PublishedBudgetsRegion_Result>("PublishedBudgetsRegion", regionParameter);
        }
    
        public virtual ObjectResult<PublishedBudgetsBranch_Result> PublishedBudgetsBranch(Nullable<int> branch)
        {
            var branchParameter = branch.HasValue ?
                new ObjectParameter("Branch", branch) :
                new ObjectParameter("Branch", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PublishedBudgetsBranch_Result>("PublishedBudgetsBranch", branchParameter);
        }
    
        public virtual ObjectResult<sp_RegionContractBase_Result> sp_RegionContractBase(string region)
        {
            var regionParameter = region != null ?
                new ObjectParameter("Region", region) :
                new ObjectParameter("Region", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RegionContractBase_Result>("sp_RegionContractBase", regionParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningStore> sp_RegionHolidayPlanning(string regionNo)
        {
            var regionNoParameter = regionNo != null ?
                new ObjectParameter("RegionNo", regionNo) :
                new ObjectParameter("RegionNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningStore>("sp_RegionHolidayPlanning", regionNoParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningStore> sp_RegionHolidayPlanning(string regionNo, MergeOption mergeOption)
        {
            var regionNoParameter = regionNo != null ?
                new ObjectParameter("RegionNo", regionNo) :
                new ObjectParameter("RegionNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningStore>("sp_RegionHolidayPlanning", mergeOption, regionNoParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningEmp> sp_RegionHolidayPlanningEmp(string regionNo)
        {
            var regionNoParameter = regionNo != null ?
                new ObjectParameter("RegionNo", regionNo) :
                new ObjectParameter("RegionNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningEmp>("sp_RegionHolidayPlanningEmp", regionNoParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningEmp> sp_RegionHolidayPlanningEmp(string regionNo, MergeOption mergeOption)
        {
            var regionNoParameter = regionNo != null ?
                new ObjectParameter("RegionNo", regionNo) :
                new ObjectParameter("RegionNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningEmp>("sp_RegionHolidayPlanningEmp", mergeOption, regionNoParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningStore> sp_DivisionHolidayPlanning(string division)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningStore>("sp_DivisionHolidayPlanning", divisionParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningStore> sp_DivisionHolidayPlanning(string division, MergeOption mergeOption)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningStore>("sp_DivisionHolidayPlanning", mergeOption, divisionParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningEmp> sp_DivisionHolidayPlanningEmp(string division)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningEmp>("sp_DivisionHolidayPlanningEmp", divisionParameter);
        }
    
        public virtual ObjectResult<HolidayPlanningEmp> sp_DivisionHolidayPlanningEmp(string division, MergeOption mergeOption)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HolidayPlanningEmp>("sp_DivisionHolidayPlanningEmp", mergeOption, divisionParameter);
        }
    
        public virtual ObjectResult<sp_AllChannelDashboardData_Result> sp_AllChannelDashboardData(string channel, Nullable<int> beginWeek)
        {
            var channelParameter = channel != null ?
                new ObjectParameter("Channel", channel) :
                new ObjectParameter("Channel", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AllChannelDashboardData_Result>("sp_AllChannelDashboardData", channelParameter, beginWeekParameter);
        }
    
        public virtual ObjectResult<sp_AllDivisionDashboardData_Result> sp_AllDivisionDashboardData(string division, Nullable<int> beginWeek)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AllDivisionDashboardData_Result>("sp_AllDivisionDashboardData", divisionParameter, beginWeekParameter);
        }
    
        public virtual ObjectResult<sp_ChannelComplianceDetail_Result> sp_ChannelComplianceDetail(string channelName, Nullable<int> weeknumber)
        {
            var channelNameParameter = channelName != null ?
                new ObjectParameter("ChannelName", channelName) :
                new ObjectParameter("ChannelName", typeof(string));
    
            var weeknumberParameter = weeknumber.HasValue ?
                new ObjectParameter("weeknumber", weeknumber) :
                new ObjectParameter("weeknumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ChannelComplianceDetail_Result>("sp_ChannelComplianceDetail", channelNameParameter, weeknumberParameter);
        }
    
        public virtual ObjectResult<sp_ChannelDashboardData_Result> sp_ChannelDashboardData(string channel, Nullable<int> beginWeek, Nullable<int> endWeek)
        {
            var channelParameter = channel != null ?
                new ObjectParameter("Channel", channel) :
                new ObjectParameter("Channel", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            var endWeekParameter = endWeek.HasValue ?
                new ObjectParameter("EndWeek", endWeek) :
                new ObjectParameter("EndWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ChannelDashboardData_Result>("sp_ChannelDashboardData", channelParameter, beginWeekParameter, endWeekParameter);
        }
    
        public virtual ObjectResult<sp_DivisionComplianceDetail_Result> sp_DivisionComplianceDetail(string divisionName, Nullable<int> weeknumber)
        {
            var divisionNameParameter = divisionName != null ?
                new ObjectParameter("DivisionName", divisionName) :
                new ObjectParameter("DivisionName", typeof(string));
    
            var weeknumberParameter = weeknumber.HasValue ?
                new ObjectParameter("weeknumber", weeknumber) :
                new ObjectParameter("weeknumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_DivisionComplianceDetail_Result>("sp_DivisionComplianceDetail", divisionNameParameter, weeknumberParameter);
        }
    
        public virtual ObjectResult<sp_DivisionDashboardData_Result> sp_DivisionDashboardData(string division, Nullable<int> beginWeek, Nullable<int> endWeek)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            var endWeekParameter = endWeek.HasValue ?
                new ObjectParameter("EndWeek", endWeek) :
                new ObjectParameter("EndWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_DivisionDashboardData_Result>("sp_DivisionDashboardData", divisionParameter, beginWeekParameter, endWeekParameter);
        }
    
        public virtual ObjectResult<sp_RegionComplianceDetail_Result> sp_RegionComplianceDetail(Nullable<int> regionNumber, Nullable<int> weeknumber)
        {
            var regionNumberParameter = regionNumber.HasValue ?
                new ObjectParameter("regionNumber", regionNumber) :
                new ObjectParameter("regionNumber", typeof(int));
    
            var weeknumberParameter = weeknumber.HasValue ?
                new ObjectParameter("weeknumber", weeknumber) :
                new ObjectParameter("weeknumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RegionComplianceDetail_Result>("sp_RegionComplianceDetail", regionNumberParameter, weeknumberParameter);
        }
    
        public virtual ObjectResult<sp_RegionDashboardData_Result> sp_RegionDashboardData(string region, Nullable<int> beginWeek, Nullable<int> endWeek)
        {
            var regionParameter = region != null ?
                new ObjectParameter("Region", region) :
                new ObjectParameter("Region", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            var endWeekParameter = endWeek.HasValue ?
                new ObjectParameter("EndWeek", endWeek) :
                new ObjectParameter("EndWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RegionDashboardData_Result>("sp_RegionDashboardData", regionParameter, beginWeekParameter, endWeekParameter);
        }
    
        public virtual ObjectResult<udsp_GetPandLRegionSummary_Result> udsp_GetPandLRegionSummary(string regionNumber, string periodYear, Nullable<short> periodMonth, Nullable<short> qtdStartMonth, Nullable<short> ytdStartMonth)
        {
            var regionNumberParameter = regionNumber != null ?
                new ObjectParameter("RegionNumber", regionNumber) :
                new ObjectParameter("RegionNumber", typeof(string));
    
            var periodYearParameter = periodYear != null ?
                new ObjectParameter("PeriodYear", periodYear) :
                new ObjectParameter("PeriodYear", typeof(string));
    
            var periodMonthParameter = periodMonth.HasValue ?
                new ObjectParameter("PeriodMonth", periodMonth) :
                new ObjectParameter("PeriodMonth", typeof(short));
    
            var qtdStartMonthParameter = qtdStartMonth.HasValue ?
                new ObjectParameter("QtdStartMonth", qtdStartMonth) :
                new ObjectParameter("QtdStartMonth", typeof(short));
    
            var ytdStartMonthParameter = ytdStartMonth.HasValue ?
                new ObjectParameter("YtdStartMonth", ytdStartMonth) :
                new ObjectParameter("YtdStartMonth", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<udsp_GetPandLRegionSummary_Result>("udsp_GetPandLRegionSummary", regionNumberParameter, periodYearParameter, periodMonthParameter, qtdStartMonthParameter, ytdStartMonthParameter);
        }
    
        public virtual ObjectResult<udsp_GetPandLDivisionSummary_Result> udsp_GetPandLDivisionSummary(string divisionName, string periodYear, Nullable<short> periodMonth, Nullable<short> qtdStartMonth, Nullable<short> ytdStartMonth)
        {
            var divisionNameParameter = divisionName != null ?
                new ObjectParameter("DivisionName", divisionName) :
                new ObjectParameter("DivisionName", typeof(string));
    
            var periodYearParameter = periodYear != null ?
                new ObjectParameter("PeriodYear", periodYear) :
                new ObjectParameter("PeriodYear", typeof(string));
    
            var periodMonthParameter = periodMonth.HasValue ?
                new ObjectParameter("PeriodMonth", periodMonth) :
                new ObjectParameter("PeriodMonth", typeof(short));
    
            var qtdStartMonthParameter = qtdStartMonth.HasValue ?
                new ObjectParameter("QtdStartMonth", qtdStartMonth) :
                new ObjectParameter("QtdStartMonth", typeof(short));
    
            var ytdStartMonthParameter = ytdStartMonth.HasValue ?
                new ObjectParameter("YtdStartMonth", ytdStartMonth) :
                new ObjectParameter("YtdStartMonth", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<udsp_GetPandLDivisionSummary_Result>("udsp_GetPandLDivisionSummary", divisionNameParameter, periodYearParameter, periodMonthParameter, qtdStartMonthParameter, ytdStartMonthParameter);
        }
    
        public virtual ObjectResult<sp_GetClosedFormsByGroup_Result> sp_GetClosedFormsByGroup(Nullable<int> groupId, string type)
        {
            var groupIdParameter = groupId.HasValue ?
                new ObjectParameter("GroupId", groupId) :
                new ObjectParameter("GroupId", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetClosedFormsByGroup_Result>("sp_GetClosedFormsByGroup", groupIdParameter, typeParameter);
        }
    
        public virtual ObjectResult<sp_GetClosedFormsByTPC_Result> sp_GetClosedFormsByTPC(string user, string type)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetClosedFormsByTPC_Result>("sp_GetClosedFormsByTPC", userParameter, typeParameter);
        }
    
        public virtual ObjectResult<sp_GetClosedFormsByUser_Result> sp_GetClosedFormsByUser(string user, string type)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetClosedFormsByUser_Result>("sp_GetClosedFormsByUser", userParameter, typeParameter);
        }
    
        public virtual ObjectResult<sp_GetOpenFormsByGroup_Result> sp_GetOpenFormsByGroup(Nullable<int> groupId, string type)
        {
            var groupIdParameter = groupId.HasValue ?
                new ObjectParameter("GroupId", groupId) :
                new ObjectParameter("GroupId", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetOpenFormsByGroup_Result>("sp_GetOpenFormsByGroup", groupIdParameter, typeParameter);
        }
    
        public virtual ObjectResult<sp_GetOpenFormsByTPC_Result> sp_GetOpenFormsByTPC(string user, string type)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetOpenFormsByTPC_Result>("sp_GetOpenFormsByTPC", userParameter, typeParameter);
        }
    
        public virtual ObjectResult<sp_GetOpenFormsByUser_Result> sp_GetOpenFormsByUser(string user, string type)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetOpenFormsByUser_Result>("sp_GetOpenFormsByUser", userParameter, typeParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_CheckAccessRight(Nullable<int> groupId, Nullable<int> ticketId, string username)
        {
            var groupIdParameter = groupId.HasValue ?
                new ObjectParameter("GroupId", groupId) :
                new ObjectParameter("GroupId", typeof(int));
    
            var ticketIdParameter = ticketId.HasValue ?
                new ObjectParameter("TicketId", ticketId) :
                new ObjectParameter("TicketId", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_CheckAccessRight", groupIdParameter, ticketIdParameter, usernameParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ChkInteractLvl(Nullable<int> level, Nullable<int> ticketTypeId)
        {
            var levelParameter = level.HasValue ?
                new ObjectParameter("level", level) :
                new ObjectParameter("level", typeof(int));
    
            var ticketTypeIdParameter = ticketTypeId.HasValue ?
                new ObjectParameter("TicketTypeId", ticketTypeId) :
                new ObjectParameter("TicketTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ChkInteractLvl", levelParameter, ticketTypeIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_ChkInteractLvl(Nullable<int> level, Nullable<int> ticketTypeId)
        {
            var levelParameter = level.HasValue ?
                new ObjectParameter("level", level) :
                new ObjectParameter("level", typeof(int));
    
            var ticketTypeIdParameter = ticketTypeId.HasValue ?
                new ObjectParameter("TicketTypeId", ticketTypeId) :
                new ObjectParameter("TicketTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_ChkInteractLvl", levelParameter, ticketTypeIdParameter);
        }
    
        public virtual ObjectResult<sp_EscalationOptions_Result> sp_EscalationOptions(Nullable<int> ticketType, Nullable<int> level)
        {
            var ticketTypeParameter = ticketType.HasValue ?
                new ObjectParameter("TicketType", ticketType) :
                new ObjectParameter("TicketType", typeof(int));
    
            var levelParameter = level.HasValue ?
                new ObjectParameter("Level", level) :
                new ObjectParameter("Level", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_EscalationOptions_Result>("sp_EscalationOptions", ticketTypeParameter, levelParameter);
        }
    }
}
