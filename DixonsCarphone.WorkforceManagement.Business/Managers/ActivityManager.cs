using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DixonsCarphone.WorkforceManagement.Business.Entities;
using System.Data.Entity;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using DixonsCarphone.WorkforceManagement.ViewModels;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public class ActivityManager : IActivityManager
    {
        // Retrieve all activities from 'Activity' table, store specific & universal
        public async Task<List<Activity>> GetActivities(int? storeNumber = null)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.Activities.Include(x => x.ActivityType).Where(x => x.StoreNumber == null || x.StoreNumber == storeNumber).ToListAsync();
            }
        }

        // Retrieve all activities of a specific type, store specific & universal. Activity types referenced in ActivityType table
        public async Task<List<Activity>> GetActivitiesForActivityType(int activityTypeId, int? storeNumber = null)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.Activities.Include(x => x.ActivityType)
                    .Where(x => x.ActivityTypeId == activityTypeId && (x.StoreNumber == null || x.StoreNumber == storeNumber))
                    .ToListAsync();
            }
        }

        // Retrieve specific activity by it's ID
        public async Task<Activity> GetActivity(long activityId)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                return await dbContext.Activities
                    .Include(x => x.ActivityType)
                    .FirstOrDefaultAsync(x => x.Activityid == activityId);
            }
        }

        // Add new activity to Activity table
        public async Task<bool> AddActivity(ActivityAdminViewModel model)
        {
            var toRtn = false;
            DateTime dt;

            // Check ActivityDate can be parsed to datetime
            if (DateTime.TryParse(model.ActivityDate, out dt))
            {
                var activityType = int.Parse(model.SelectedType);
                using (var dbContext = new DxCpWfmContext())
                {
                    //check for existing activity with matching ActivityName, ActivityDate and ActivityTypeId
                    var activity = await dbContext.Activities
                        .FirstOrDefaultAsync(x => x.ActivityName == model.ActivityName && x.ActivityDate == dt && x.ActivityTypeId == activityType);

                    var exists = activity != null;
                    //map fields of viewmodel to model ready for insert
                    activity = MapModelToActivity(activity, model, activityType);

                    //if not already existing add to context
                    if (!exists)
                        dbContext.Activities.Add(activity);
                    
                    //commit
                    await dbContext.SaveChangesAsync();
                    toRtn = true;
                }
            }

            return toRtn;
        }

        // Map ActivityAdminViewModel fields to relevant fields of Activity object ready for insert
        private Activity MapModelToActivity(Activity existing, ActivityAdminViewModel model,int activityType)
        {
            int storeNum;
            short priority;
            existing = existing ?? new Activity
            {
                ActivityTypeId = activityType,
                ActivityDate = DateTime.Parse(model.ActivityDate),
                ActivityName = model.ActivityName,
                CreatedBy = "Admin",
                DateCreated = DateTime.Now,
            };

            //existing.StartTime = int.Parse(model.SelectedStartTime).ToTime().Replace('.', ':');
            //existing.EndTime = int.Parse(model.SelectedEndTime).ToTime().Replace('.', ':');
            existing.DateModified = DateTime.Now;
            existing.Summary = model.Summary;
            existing.Detail = model.Detail;
            existing.StoreNumber = !string.IsNullOrEmpty(model.StoreNumber) && int.TryParse(model.StoreNumber, out storeNum) ? (int?)storeNum : null;
            existing.PriorityTypeId = !string.IsNullOrEmpty(model.SelectedPriority) && short.TryParse(model.SelectedPriority, out priority) ? (short?)priority : null;

            return existing;
        }

        //Log unidentified branch request
        public async Task LogFeedback(string feedbackText, short branch)
        {
            using (var dbContext = new DxCpWfmContext())
            {
                SiteFeedback data = new SiteFeedback
                {
                    StoreNumber = branch,
                    FeedbackText = feedbackText,
                    DateTimeSubmitted = DateTime.Now
                };

                dbContext.SiteFeedbacks.Add(data);
                await dbContext.SaveChangesAsync();
            }
            return;
        }
    }
}
