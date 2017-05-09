using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
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
            });
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
        }
    }

    public class StoreOpeningTimeProfile : Profile
    {
        public StoreOpeningTimeProfile()
        {
            CreateMap<StoreOpeningTimeView, StoreOpeningTime>();
            CreateMap<StoreOpeningTime, StoreOpeningTimeView>();
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
}