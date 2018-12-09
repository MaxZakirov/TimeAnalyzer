using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Core.Activities;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Activity")]
    public class ActivityController : Controller
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllActivities()
        {
            IEnumerable<Activity> activities = await this.activityService.GetAllActivities();
            return Ok(activities);
        }
    }
}