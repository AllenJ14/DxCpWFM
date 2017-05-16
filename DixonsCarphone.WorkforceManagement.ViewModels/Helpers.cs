using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace DixonsCarphone.WorkforceManagement.ViewModels
{
    public static class Helpers
    {
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public static void SerializeObject<T>(this T serializableObject, string fileName) where T : new()
        {
            if (serializableObject == null) { return; }

            try
            {
                var dir = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                var xmlString = xmlDocument.OuterXml;

                using (var read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    var serializer = new XmlSerializer(outType);
                    using (var reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        public static DateTime GetFirstDayOfWeek(this DateTime currentDate)
        {
            var currentDay = currentDate.DayOfWeek;

            var firstDayInWeek = currentDate.AddDays(-(int)currentDay);

            return firstDayInWeek;
        }

        public static decimal? CalcPerc(decimal? actual, decimal? budget)
        {
            if (actual.GetValueOrDefault() != 0 && budget.GetValueOrDefault() != 0)
            {
                if (budget < 0 && actual > 0)
                {
                    return (actual / Math.Abs((decimal)budget) + 2) * 100;
                }
                else
                {
                    return actual / budget * 100;
                }
            }
            return 0;
        }

        public static double? CalcPerc(double? actual, double? budget)
        {
            if (actual.GetValueOrDefault() != 0 && budget.GetValueOrDefault() != 0)
            {
                if (budget < 0)
                {
                    return (actual / Math.Abs((double)budget) + 2) * 100;
                }
                else
                {
                    return actual / budget * 100;
                }
            }
            return 0;
        }

        public static string GetProgressBarCss(this int val)
        {
            var toRtn = string.Empty;
            if (val <= 30)
                toRtn = "progress-bar-danger";
            else if (val > 30 && val <= 55)
                toRtn = "progress-bar-yellow";
            else if (val > 55 && val <= 70)
                toRtn = "progress-bar-primary";
            else
                toRtn = "progress-bar-success";


            return toRtn;
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(this DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static int GetWeekOfYear(this DateTime time, string startDateDayMonth = null)
        {
            var currentYear = DateTime.Now.Year.ToString();

            if (!string.IsNullOrEmpty(startDateDayMonth))
            {
                var dayMth = startDateDayMonth.Split('/');
                if (dayMth.Length > 1)
                {
                    var dy = int.Parse(dayMth[0]);
                    var mth = int.Parse(dayMth[1]);
                    var i = time.Month > mth || time.Month == mth ? 0 : 0; // -1;
                    currentYear = time.AddYears(i).Year.ToString();
                }
            }
            else
                startDateDayMonth = "01/01"; //default 1st day of year

            var newYrDateString = string.Format("1/1/{0}", DateTime.Now.Year);
            var newYear = DateTime.Parse(newYrDateString);

            var startDate = !string.IsNullOrEmpty(currentYear) ? DateTime.Parse(string.Format("{0}/{1}", startDateDayMonth, currentYear)) : newYear;

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Sunday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(4);
            }

            var culture = CultureInfo.CurrentCulture;
            var weekOfYear = culture.Calendar.GetWeekOfYear(
                time.Add(newYear - startDate),
                CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);

            var wkOfYrStr = string.Format("{0}{1}", currentYear, weekOfYear.ToString().PadLeft(2, '0'));
            var wkOfYr = int.Parse(wkOfYrStr);

            return wkOfYr; // weekOfYear; // ms;
        }

        public static string FormatNumber(this double? val)
        {
            var s = string.Format("{0:0.00}", val);

            if (s.EndsWith("00"))
            {
                return ((int)val).ToString();
            }
            else
            {
                return s;
            }
        }

        public static string FormatNumber(this double val)
        {
            var s = string.Format("{0:0.00}", val);

            if (s.EndsWith("00"))
            {
                return ((int)val).ToString();
            }
            else
            {
                return s;
            }
        }

        public static DateTime? GetDateFromWeekNumber(this int? weekCommence)
        {
            if (!weekCommence.HasValue || weekCommence.ToString().Length < 6) return null;

            var year = int.Parse(weekCommence.ToString().Substring(0, 4));
            var weekOfYear = int.Parse(weekCommence.ToString().PadLeft(2, '0').Substring(4, 2));

            var ci = CultureInfo.InvariantCulture;
            var jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            var firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }

            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        public static string ToTime(this double num)
        {
            var t = num / 4;
            var dec = t.ToString().Split('.');
            return CalculateTime(dec);
        }

        // Populate select list for opening times
        public static IEnumerable<SelectListItem> GetTimes()
        {
            TimeSpan TimeCounter = new TimeSpan(7, 0, 0);
            TimeSpan TimeIncrement = new TimeSpan(0, 15, 0);
            TimeSpan MaxTime = new TimeSpan(23, 15, 0);

            var times = new List<SelectListItem>();

            times.Add(new SelectListItem { Text = "Closed", Value = "00:00:00" });
            while (TimeCounter < MaxTime)
            {
                times.Add(new SelectListItem { Text = TimeCounter.ToString("hh\\:mm"), Value = TimeCounter.ToString() });
                TimeCounter = TimeCounter.Add(TimeIncrement);
            }

            return new SelectList(times, "Value", "Text");
        }

        public static List<SelectListItem> GetDays()
        {
            var days = Enum.GetValues(typeof(DayOfWeek))
                              .OfType<DayOfWeek>()
                              //.Skip(1)
                              .Select(x => new SelectListItem { Value = ((int)x).ToString(), Text = x.ToString() }).ToList();
            return days;
        }

        // Populate select list with current week plus 4 future and 8 past
        public static List<SelectListItem> GetFinancialWeeks(DateTime lastDate, List<int?> weekNumbers)
        {
            var ls = new List<SelectListItem>();
            var _date = lastDate;

            for (int i = 0; i < weekNumbers.Count(); i++)
            {
                ls.Add(new SelectListItem { Value = weekNumbers[i].ToString(), Text = string.Format("{0} (wc {1})", weekNumbers[i].ToString(), _date.ToShortDateString()) });
                _date = _date.AddDays(-7);
            }

            return ls;
        }

        private static string CalculateTime(string[] dec)
        {
            var fullVal = dec.Length > 1 ? Convert.ToDouble(dec[0]) + Convert.ToDouble(Convert.ToDouble("0." + dec[1]) * 0.6) : Convert.ToDouble(dec[0]);
            return string.Format("{0:0.00}", fullVal);
        }

        //Transforms single row of DailyDeploymentView into array form for graph representation
        public static List<DailyDeploymentArrayForm> TransformDeployment(DailyDeploymentView rawData)
        {
            if (rawData == null)
            {
                return new List<DailyDeploymentArrayForm>();
            }

            var result = new List<DailyDeploymentArrayForm>();

            result.Add(new DailyDeploymentArrayForm { DayName = "Sun", Required = rawData.SundayReq * 100, Deployed = Math.Round((rawData.SundayDeployed / rawData.DeployedTotal) * 100, 1) });
            result.Add(new DailyDeploymentArrayForm { DayName = "Mon", Required = rawData.MondayReq * 100, Deployed = Math.Round((rawData.MondayDeployed / rawData.DeployedTotal) * 100, 1) });
            result.Add(new DailyDeploymentArrayForm { DayName = "Tue", Required = rawData.TuesdayReq * 100, Deployed = Math.Round((rawData.TuesdayDeployed / rawData.DeployedTotal) * 100, 1) });
            result.Add(new DailyDeploymentArrayForm { DayName = "Wed", Required = rawData.WednesdayReq * 100, Deployed = Math.Round((rawData.WednesdayDeployed / rawData.DeployedTotal) * 100, 1) });
            result.Add(new DailyDeploymentArrayForm { DayName = "Thu", Required = rawData.ThursdayReq * 100, Deployed = Math.Round((rawData.ThursdayDeployed / rawData.DeployedTotal) * 100, 1) });
            result.Add(new DailyDeploymentArrayForm { DayName = "Fri", Required = rawData.FridayReq * 100, Deployed = Math.Round((rawData.FridayDeployed / rawData.DeployedTotal) * 100, 1) });
            result.Add(new DailyDeploymentArrayForm { DayName = "Sat", Required = rawData.SaturdayReq * 100, Deployed = Math.Round((rawData.SaturdayDeployed / rawData.DeployedTotal) * 100, 1) });

            return result;
        }

        public static string BuildGraphArray(List<DailyDeploymentArrayForm> rawData, string type)
        {
            string result = "[";

            if (rawData == null)
            {
                return "";
            }

            if(type == "deployed")
            {
                foreach(var item in rawData)
                {
                    result = result + item.Deployed.ToString() + ",";
                }
                result = result.TrimEnd(',') + "]";
            }
            else
            {
                foreach (var item in rawData)
                {
                    result = result + item.Required.ToString() + ",";
                }
                result = result.TrimEnd(',') + "]";
            }

            return result;
        }

        //Generate commentary detail for DailyDeploymentViewModel
        public static List<string> GenerateDeploymentCommentary(DailyDeploymentView rawData)
        {
            if (rawData == null)
            {
                return new List<string>();
            }

            var result = new List<string>();
            result.Add(GetText(rawData.SundayMove));
            result.Add(GetText(rawData.MondayMove));
            result.Add(GetText(rawData.TuesdayMove));
            result.Add(GetText(rawData.WednesdayMove));
            result.Add(GetText(rawData.ThursdayMove));
            result.Add(GetText(rawData.FridayMove));
            result.Add(GetText(rawData.SaturdayMove));

            return result;
        }

        private static string GetText(decimal moveFigure)
        {
            int roundedFigure = (int)Math.Round(moveFigure);

            if (roundedFigure == 0)
            {
                return "You've got it right here";
            }
            else if (roundedFigure > 0)
            {
                return string.Format("Put up to {0} hours in a better day", roundedFigure);
            }
            else
            {
                return string.Format("Move {0} hours to here", Math.Abs(roundedFigure));
            }

        }
    }
}
