using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.Business.Kronos;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;

namespace DixonsCarphone.WorkforceManagement.Web.Mapping
{
    public static class AutoMapperWebConfiguration
    {
        private static MapperConfiguration _mapperConfig;

        public static MapperConfiguration MapperConfig
        {
            get
            {
                if (_mapperConfig == null) Configure();

                return _mapperConfig;
            }
        }

        public static void Configure()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
                cfg.AddProfile(new DailyFootfallProfile());
                cfg.AddProfile(new WeeklyFootfallProfile());
                cfg.AddProfile(new HrFeedProfile());
                cfg.AddProfile(new StoreOpeningTimeProfile());
                cfg.AddProfile(new ActivityTypeProfile());
                cfg.AddProfile(new ContentProfile());
                cfg.AddProfile(new DashBoardProfile());
                cfg.AddProfile(new PandLProfile());
                cfg.AddProfile(new RegionDashboardProfile());
                cfg.AddProfile(new MaintenanceProfile());
                cfg.AddProfile(new ScheduleProfile());
                cfg.AddProfile(new WorkflowProfile());
                cfg.AddProfile(new TimesheetProfile());
            });
        }
    }

    public class MaintenanceProfile : Profile
    {
        public MaintenanceProfile()
        {
            CreateMap<StoreReference, StoreReferenceView>();
            CreateMap<StoreReferenceView, StoreReference>();
            CreateMap<RoleReference, RoleReferenceView>();
            CreateMap<RoleReferenceView, RoleReference>();
            CreateMap<UserAccessDetail, UserAccess>();
            CreateMap<UserAccess, UserAccessDetail>();
            CreateMap<Store, StoreViewModel>();
            CreateMap<StoreViewModel, Store>();
            CreateMap<Business.Entities.FileUploadRecord, ViewModels.BusinessModels.FileUploadRecord>();
            CreateMap<ViewModels.BusinessModels.FileUploadRecord, Business.Entities.FileUploadRecord>();
        }
    }

    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            CreateMap<TicketTemplate, TicketTemplateView>();
            CreateMap<TicketQ_A, TicketAnswer>();
            CreateMap<sp_GetOpenFormsByUser_Result, TicketSummaryView>();
            CreateMap<sp_GetOpenFormsByTPC_Result, TicketSummaryView>();
            CreateMap<sp_GetOpenFormsByGroup_Result, TicketSummaryView>();
            CreateMap<sp_GetClosedFormsByUser_Result, TicketSummaryView>();
            CreateMap<sp_GetClosedFormsByTPC_Result, TicketSummaryView>();
            CreateMap<sp_GetClosedFormsByGroup_Result, TicketSummaryView>();
        }
    }

    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<ActivityView, Activity>();
            CreateMap<Activity, ActivityView>();
        }
    }

    public class PandLProfile : Profile
    {
        public PandLProfile()
        {
            CreateMap<PandLView, udsp_GetPandL_Result>();
            CreateMap<udsp_GetPandL_Result, PandLView>();
            CreateMap<PandLView, udsp_GetPandLRegion_Result>();
            CreateMap<udsp_GetPandLRegion_Result, PandLView>();
            CreateMap<PandLView, udsp_GetPandLDivision_Result>();
            CreateMap<udsp_GetPandLDivision_Result, PandLView>();
            CreateMap<PandLView, udsp_GetPandLChannel_Result>();
            CreateMap<udsp_GetPandLChannel_Result, PandLView>();
            CreateMap<udsp_GetPandLDivisionSummary_Result, PandLSummaryDetail>();
            CreateMap<udsp_GetPandLRegionSummary_Result, PandLSummaryDetail>();
            CreateMap<udsp_GetPandLChannelSummary_Result, PandLSummaryDetail>();
        }
    }

    public class RegionDashboardProfile : Profile
    {
        public RegionDashboardProfile()
        {
            CreateMap<DashBoardData_v2, sp_RegionDashboardData_v2_Result>();
            CreateMap<sp_RegionDashboardData_v2_Result, DashBoardData_v2>();
            CreateMap<sp_RegionDashboardData_v2_Result, DashBoardView>();
            CreateMap<sp_DivisionDashboardData_v2_Result, DashBoardData_v2>();
            CreateMap<sp_DivisionDashboardData_v2_Result, DashBoardView>();
            CreateMap<DashBoardData_v2, sp_DivisionDashboardData_v2_Result>();
            CreateMap<sp_ChannelDashboardData_v2_Result, DashBoardData_v2>();
            CreateMap<DashBoardData_v2, sp_ChannelDashboardData_v2_Result>();
            CreateMap<DashBoardView, sp_AllChannelDashboardData_v2_Result>();
            CreateMap<sp_AllChannelDashboardData_v2_Result, DashBoardView>();
            CreateMap<DashBoardView, sp_AllDivisionDashboardData_v2_Result>();
            CreateMap<sp_AllDivisionDashboardData_v2_Result, DashBoardView>();
            CreateMap<DashboardViewChannel, sp_AllChannelDashboardData_v2_Result>();
            CreateMap<sp_AllChannelDashboardData_v2_Result, DashboardViewChannel>();
            CreateMap<DashboardViewChannel, sp_AllDivisionDashboardData_v2_Result>();
            CreateMap<sp_AllDivisionDashboardData_v2_Result, DashboardViewChannel>();
            CreateMap<PublishedBudgetsBranch_Result, PublishedBudgetBranch>();
            CreateMap<PublishedBudgetBranch, PublishedBudgetsBranch_Result>();
            CreateMap<PublishedBudgetsRegion_Result, PublishedBudgetBranch>();
            CreateMap<PublishedBudgetBranch, PublishedBudgetsRegion_Result>();
        }
    }

    public class DailyFootfallProfile : Profile
    {
        public DailyFootfallProfile()
        {
            CreateMap<DailyFootfallView, DailyFootfall>();
            CreateMap<DailyFootfall, DailyFootfallView>();
            CreateMap<footfall_raw, FootfallView>();
            CreateMap<sp_GetRegionFootfall_Result, FootfallView>();
            CreateMap<sp_GetDivisionFootfall_Result, FootfallView>();
        }
    }

    public class WeeklyFootfallProfile : Profile
    {
        public WeeklyFootfallProfile()
        {
            CreateMap<WeeklyFootfallView, WeeklyFootfall>();
            CreateMap<WeeklyFootfall, WeeklyFootfallView>();
        }
    }

    public class HrFeedProfile : Profile
    {
        public HrFeedProfile()
        {
            CreateMap<HrFeedView, HrFeed>();
            CreateMap<HrFeed, HrFeedView>();
            CreateMap<ContractBaseDetail, ContractBaseView>();
            CreateMap<ContractBaseView, ContractBaseDetail>();
            CreateMap<sp_RegionContractBase_Result, RegionContractBase>();
            CreateMap<RegionContractBase, sp_RegionContractBase_Result>();
            CreateMap<HolidayPlanningEmp, HolidayPlanningEmpBM>();
            CreateMap<HolidayPlanningEmpBM, HolidayPlanningEmp>();
            CreateMap<HolidayPlanningStore, HolidayPlanningStoreBM>();
            CreateMap<HolidayPlanningStoreBM, HolidayPlanningStore>();
            CreateMap<PayCalendarDate, PayCalendarDateView>();
            CreateMap<PayCalendarRef, PayCalendarRefView>();
            CreateMap<ColleaguePayData, ColleaguePayDataView>();
        }
    }

    public class StoreOpeningTimeProfile : Profile
    {
        public StoreOpeningTimeProfile()
        {
            CreateMap<StoreOpeningTimeView, StoreOpeningTime>();
            CreateMap<StoreOpeningTime, StoreOpeningTimeView>();
            CreateMap<OpeningTimesComment, OpeningTimeCommentView>();
            CreateMap<OpeningTimeCommentView, OpeningTimesComment>();
        }
    }

    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleData, ScheduleDetail>();
            CreateMap<ScheduleDetail, ScheduleData>();
            CreateMap<sp_GetRegionBMSchedule_Result, ScheduleBMView>();
            CreateMap<BmWeWorking, WeWorkingView>();
            CreateMap<sp_DivisionBMWorking_Result, WeWorkingView>();
            CreateMap<sp_ChannelBMWorking_Result, WeWorkingView>();
            CreateMap<sp_RegionContractStatus_Result, RegionContractStatusView>();
            CreateMap<sp_BranchContractStatus_Result, RegionContractStatusView>();
            CreateMap<sp_RegionFutureDeployment_Result, FutureDeploymentView>();
            CreateMap<sp_DivisionFutureDeployment_Result, FutureDeploymentView>();
            CreateMap<PeakData, PeakDataView>();
            CreateMap<sp_PeakFlexRegion_Result, PeakFlexRegionView>();
            CreateMap<sp_PeakFlexDivision_Result, PeakFlexDivisionView>();
        }
    }

    public class TimesheetProfile : Profile
    {
        public TimesheetProfile()
        {
            CreateMap<Timesheet, TimesheetView>();
            CreateMap<Employee, EmployeeView>();
            CreateMap<PeriodTotalData, PeriodTotalDataView>();
            CreateMap<Period, PeriodView>();
            CreateMap<TimeFramePeriod, TimeFramePeriodView>();
            CreateMap<PeriodTotals, PeriodTotalsView>();
            CreateMap<Totals, TotalsView>();
            CreateMap<Total, TotalView>();
            CreateMap<PersonIdentity, PersonIdentityView>();
            CreateMap<HyperFindResult, HyperFindResultView>();
            CreateMap<PersonData, PersonDataView>();
            CreateMap<Person, PersonView>();
            CreateMap<KronosEmployeeSummary, KronosEmpSummaryView>();
        }
    }

    public class DashBoardProfile : Profile
    {
        public DashBoardProfile()
        {
            CreateMap<DashBoardView, DashBoardData_v2>();
            CreateMap<DashBoardData_v2, DashBoardView>();
            CreateMap<DailyDeploymentView, DailyDeployment>();
            CreateMap<DailyDeployment, DailyDeploymentView>();
            CreateMap<EmpComplianceDetail, EmpComplianceDetailView>();
            CreateMap<EmpComplianceDetailView, EmpComplianceDetail>();
            CreateMap<PunchCompView, CPW_Clocking_Data>();
            CreateMap<CPW_Clocking_Data, PunchCompView>();
            CreateMap<sp_RegionPunchExceptions_Result, PunchExceptionsView>();
            CreateMap<sp_DivisionPunchExceptions_Result, PunchExceptionsView>();
            CreateMap<sp_RegionPunchTrend_Result, PunchTrendView>();
            CreateMap<sp_DivisionPunchTrend_Result, PunchTrendView>();
            CreateMap<sp_RegionPunchCompliance_Result, RegionPunchComplianceItem>();
            CreateMap<RegionPunchComplianceItem, sp_RegionPunchCompliance_Result>();
            CreateMap<sp_DivisionPunchCompliance_Result, RegionPunchComplianceItem>();
            CreateMap<RegionPunchComplianceItem, sp_DivisionPunchCompliance_Result>();
            CreateMap<sp_ChannelPunchCompliance_Result, RegionPunchComplianceItem>();
            CreateMap<RegionPunchComplianceItem, sp_ChannelPunchCompliance_Result>();
            CreateMap<ShortShift, ShortShiftView>();
            CreateMap<sp_RegionShortShifts_Result, RegionShortShiftView>();
        }
    }

    public class ActivityTypeProfile : Profile
    {
        public ActivityTypeProfile()
        {
            CreateMap<ActivityTypeView, ActivityType>();
            CreateMap<ActivityType, ActivityTypeView>();
        }
    }

    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<ContentHeaderViewModel, ContentHeader>();
            CreateMap<ContentDetailViewModel, ContentDetail>();

            CreateMap<ContentHeader, ContentHeaderViewModel>();
            CreateMap<ContentDetail, ContentDetailViewModel>();
        }
    }
}