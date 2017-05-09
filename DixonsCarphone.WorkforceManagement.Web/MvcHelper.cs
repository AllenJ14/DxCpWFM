using AutoMapper;
using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels.Cms;
using DixonsCarphone.WorkforceManagement.Web.App_Start;
using DixonsCarphone.WorkforceManagement.Web.Mapping;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace DixonsCarphone.WorkforceManagement.Web
{
    public static class MvcHelper
    {
        // Returns IP address of request
        public static string GetIPHelper()
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            return ip;
        }

        public static T GetSessionObject<T>(this HttpContext current, string sessionObjectName) where T : new()
        {
            return current != null ? (T)current.Session[sessionObjectName] : default(T);
        }

        public static string ToTime(this double? num)
        {
            if (!num.HasValue) return string.Empty;

            var t = num / 4;
            var dec = t.ToString().Split('.');

            var fullVal = dec.Length > 1 ? Convert.ToDouble(dec[0]) + Convert.ToDouble(Convert.ToDouble("0." + dec[1]) * 0.6) : Convert.ToDouble(dec[0]);
            return string.Format("{0:0.00}", fullVal);
        }

        public static void LoadStaticCache()
        {
            IMapper mapper;
            MapperConfiguration config = AutoMapperWebConfiguration.MapperConfig;
            mapper = config.CreateMapper();
            
            var bestPracticeList = (ContentViewModel)HttpContext.Current.Cache.Get("bestPracticeCache");
            if (bestPracticeList == null)
            {
                bestPracticeList = new ContentViewModel();
                var filePath = HttpContext.Current.Server.MapPath("~/CacheDependencyFiles/BestPractice.txt");
                var _contentManager = new ContentManager();
                var data = Task.Run(() => _contentManager.GetAllContentHeaderAndDetails()).Result;
                bestPracticeList.ContentCollection = mapper.Map<List<ContentHeaderViewModel>>(data);
                HttpRuntime.Cache.Insert("bestPracticeCache", bestPracticeList, new CacheDependency(filePath));
            }
        }

        //Load Help & Support headers
        public static ContentViewModel GetBestPracticeCache()
        {
            if (HttpRuntime.Cache["bestPracticeCache"] == null)
                LoadStaticCache();

            var data = HttpRuntime.Cache["bestPracticeCache"] as ContentViewModel;

            return data;
        }

        public static void LogOut()
        {
            HttpContext.Current.Session.Abandon();
            IAuthenticationManager authenticationManager = HttpContext.Current?.Request?.GetOwinContext()?.Authentication;
            authenticationManager?.SignOut(CpwWfmAuthentication.ApplicationCookie);

            HttpContext.Current.Session.Abandon();
        }
    }
}