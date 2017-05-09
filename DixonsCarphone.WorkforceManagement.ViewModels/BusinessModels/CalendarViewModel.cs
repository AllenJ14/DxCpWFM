using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class CalendarViewModel : BaseViewModel
    {
        public List<ActivityView> Activities { get; set; }

        // public List<TasksAndEvents> events { get; set; }

        public List<TasksAndEvents> Events
        {
            get
            {
                var ls = new List<TasksAndEvents>();
                if (Activities != null)
                {
                    foreach(var x in Activities)
                    {
                        DateTime startDate;
                        DateTime endDate;
                        var startStr = string.Format("{0:yyyy-MM-dd} {1}", x.ActivityDate.GetValueOrDefault(), x.StartTime.PadLeft(5, '0'));
                        var endStr = string.Format("{0:yyyy-MM-dd} {1}", x.ActivityDate.GetValueOrDefault(), x.EndTime.PadLeft(5, '0'));

                        ls.Add(new TasksAndEvents
                        {
                            title = x.ActivityName,
                            backgroundColor = GetColor(x.ActivityTypeId.GetValueOrDefault()), 
                            borderColor = "#f56954",
                            start = DateTime.TryParseExact(startStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out startDate) ? (DateTime?)startDate : null,
                            end = DateTime.TryParseExact(endStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out endDate) ? (DateTime?)endDate : null,
                        });

                    }
                };

                return ls;
            }
        }

        private string GetColor(int activityType)
        {
            // "#f39c12", //yellow "#0073b7", //Blue "#00c0ef", "#00c0ef", //Info (aqua) "#00a65a", //Success (green) "#3c8dbc", //Primary (light-blue)

            var toRtn = "#f56954";
            switch (activityType)
            {
                case 1:
                    toRtn = "#00c0ef";
                    break;
                case 2:
                    toRtn = "#00a65a";
                    break;
            }

            return toRtn;
        }
    }

    public class TasksAndEvents
    {
        public string title { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
    }
}
