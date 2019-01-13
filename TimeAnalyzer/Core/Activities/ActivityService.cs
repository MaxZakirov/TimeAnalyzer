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
        private readonly IActivityTypeRepository activityTypeRepository;
        private const string DefaultIconPath = "DefaultIcon";

        public ActivityService(
            IActivityRepository activityRepository,
            IActivityTypeRepository activityTypeRepository
            )
        {
            this.activityRepository = activityRepository;
            this.activityTypeRepository = activityTypeRepository;
        }

        public async Task<IEnumerable<Activity>> GetAllActivities()
        {
            IEnumerable<Activity> activities = await activityRepository.GetAll();
            await LoadActivityTypes(activities);
            return activities;
        }

        public void Create(Activity activity)
        {
            activityRepository.Add(activity);
        }

        public void Update(Activity activity)
        {
            activityRepository.Update(activity);
        }

        public void Remove(int activityId)
        {
            activityRepository.Remove(activityId);
        }

        private async Task LoadActivityTypes(IEnumerable<Activity> activities)
        {
            var activityTypes = await activityTypeRepository.GetAll();

            foreach (var a in activities)
            {
                a.Type = activityTypes.First(type => a.TypeId == type.Id);
            }
        }
    }
}
