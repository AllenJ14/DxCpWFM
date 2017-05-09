using DixonsCarphone.WorkforceManagement.Business.Entities;
using DixonsCarphone.WorkforceManagement.ViewModels.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Managers
{
    public interface IActivityManager
    {
        Task<List<Activity>> GetActivities(int? storeNumber = null);

        Task<List<Activity>> GetActivitiesForActivityType(int activityTypeId, int? storeNumber = null);

        Task<Activity> GetActivity(long activityId);

        Task<bool> AddActivity(ActivityAdminViewModel model);

        Task LogFeedback(string feedbackText, short branch);
    }
}
