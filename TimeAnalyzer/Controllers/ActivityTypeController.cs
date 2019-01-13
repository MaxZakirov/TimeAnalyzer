using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeRepository activityTypeRepository;

        public ActivityTypeController(IActivityTypeRepository activityTypeRepository)
        {
            this.activityTypeRepository = activityTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivityTypes()
        {
            var activities = await activityTypeRepository.GetAll();
            return Ok(activities);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ActivityType activityType)
        {
            activityTypeRepository.Add(activityType);
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] ActivityType activityType)
        {
            activityTypeRepository.Update(activityType);
            return Ok();
        }

        [HttpPost]
        public IActionResult Delete([FromBody] ActivityType activityType)
        {
            activityTypeRepository.Remove(activityType.Id);
            return Ok();
        }
    }
}