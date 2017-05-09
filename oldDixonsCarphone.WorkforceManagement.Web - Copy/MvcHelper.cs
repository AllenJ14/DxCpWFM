using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DixonsCarphone.WorkforceManagement.Web
{
    public static class MvcHelper
    {
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
    }
}