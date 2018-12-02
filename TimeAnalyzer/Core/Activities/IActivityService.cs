using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.Activities
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllActivities();
    }
}
