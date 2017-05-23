using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms;
using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        }
    }

    public class RegionDashboardProfile : Profile
    {
        public RegionDashboardProfile()
        {
            CreateMap<DashBoardData, sp_RegionDashboardData_Result>();
            CreateMap<sp_RegionDashboardData_Result, DashBoardData>();
            CreateMap<sp_DivisionDashboardData_Result, DashBoardData>();
            CreateMap<DashBoardData, sp_DivisionDashboardData_Result>();
            CreateMap<sp_ChannelDashboardData_Result, DashBoardData>();
            CreateMap<DashBoardData, sp_ChannelDashboardData_Result>();
            CreateMap<DashBoardView, sp_AllChannelDashboardData_Result>();
            CreateMap<sp_AllChannelDashboardData_Result, DashBoardView>();
            CreateMap<DashBoardView, sp_AllDivisionDashboardData_Result>();
            CreateMap<sp_AllDivisionDashboardData_Result, DashBoardView>();
            CreateMap<DashboardViewChannel, sp_AllChannelDashboardData_Result>();
            CreateMap<sp_AllChannelDashboardData_Result, DashboardViewChannel>();
            CreateMap<DashboardViewChannel, sp_AllDivisionDashboardData_Result>();
            CreateMap<sp_AllDivisionDashboardData_Result, DashboardViewChannel>();
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
        }
    }

    public class DashBoardProfile : Profile
    {
        public DashBoardProfile()
        {
            CreateMap<DashBoardView, DashBoardData>();
            CreateMap<DashBoardData, DashBoardView>();
            CreateMap<DailyDeploymentView, DailyDeployment>();
            CreateMap<DailyDeployment, DailyDeploymentView>();
            CreateMap<EmpComplianceDetail, EmpComplianceDetailView>();
            CreateMap<EmpComplianceDetailView, EmpComplianceDetail>();
            CreateMap<PunchCompView, CPW_Clocking_Data>();
            CreateMap<CPW_Clocking_Data, PunchCompView>();
            CreateMap<sp_RegionPunchCompliance_Result, RegionPunchComplianceItem>();
            CreateMap<RegionPunchComplianceItem, sp_RegionPunchCompliance_Result>();
            CreateMap<sp_DivisionPunchCompliance_Result, RegionPunchComplianceItem>();
            CreateMap<RegionPunchComplianceItem, sp_DivisionPunchCompliance_Result>();
            CreateMap<sp_ChannelPunchCompliance_Result, RegionPunchComplianceItem>();
            CreateMap<RegionPunchComplianceItem, sp_ChannelPunchCompliance_Result>();
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