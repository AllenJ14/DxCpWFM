using DixonsCarphone.WorkforceManagement.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels
{
    public class AdminHoursReview
    {
        public StoreOpeningTimeView CurrentTime { get; set; }
        public StoreOpeningTimeView ProposedTime { get; set; }
        public List<TimeSpan> Differences { get; set; }
        public List<StoreOpeningTimeView> OutstandingChanges { get; set; }
        public List<OpeningTimeCommentView> Comments { get; set; }

        public void CalcDifferences()
        {
            Differences = new List<TimeSpan>();
            Differences.Add(ProposedTime.SundayClose.Subtract(ProposedTime.SundayOpen).Subtract(CurrentTime.SundayClose.Subtract(CurrentTime.SundayOpen)));
            Differences.Add(ProposedTime.MondayClose.Subtract(ProposedTime.MondayOpen).Subtract(CurrentTime.MondayClose.Subtract(CurrentTime.MondayOpen)));
            Differences.Add(ProposedTime.TuesdayClose.Subtract(ProposedTime.TuesdayOpen).Subtract(CurrentTime.TuesdayClose.Subtract(CurrentTime.TuesdayOpen)));
            Differences.Add(ProposedTime.WednesdayClose.Subtract(ProposedTime.WednesdayOpen).Subtract(CurrentTime.WednesdayClose.Subtract(CurrentTime.WednesdayOpen)));
            Differences.Add(ProposedTime.ThursdayClose.Subtract(ProposedTime.ThursdayOpen).Subtract(CurrentTime.ThursdayClose.Subtract(CurrentTime.ThursdayOpen)));
            Differences.Add(ProposedTime.FridayClose.Subtract(ProposedTime.FridayOpen).Subtract(CurrentTime.FridayClose.Subtract(CurrentTime.FridayOpen)));
            Differences.Add(ProposedTime.SaturdayClose.Subtract(ProposedTime.SaturdayOpen).Subtract(CurrentTime.SaturdayClose.Subtract(CurrentTime.SaturdayOpen)));
            var total = Differences.Sum(x => x.TotalMinutes);
            Differences.Add(TimeSpan.FromMinutes(total));
        }
    }
}
