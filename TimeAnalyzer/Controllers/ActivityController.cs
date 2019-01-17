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
    [Route("api/Activity/[action]")]
    public class ActivityController : Controller
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            IEnumerable<Activity> activities = await this.activityService.GetAllActivities();
            return Ok(activities);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Activity activity)
        {
            this.activityService.Create(activity);
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] Activity activity)
        {
            this.activityService.Update(activity);
            return Ok();
        }

        [HttpPost]
        public IActionResult Delete([FromBody] Activity activity)
        {
            this.activityService.Remove(activity.Id);
            return Ok();
        }
    }
}