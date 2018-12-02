using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activityRepository;
        private const string DefaultIconPath = "DefaultIcon";

        public ActivityService(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        public async Task<IEnumerable<Activity>> GetAllActivities()
        {
            IEnumerable<Activity> activities = await activityRepository.GetAll();
            foreach(var a in activities)
            {
                a.IconPath = a.IconPath ?? DefaultIconPath;
            }

            return activities;
        }
    }
}
