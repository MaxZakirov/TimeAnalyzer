using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Controllers
{
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
            var activities = await this.activityTypeRepository.GetAll();
            return Ok(activities);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ActivityType activityType)
        {
            this.activityTypeRepository.Add(activityType);
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] ActivityType activityType)
        {
            this.activityTypeRepository.Update(activityType);
            return Ok();
        }

        [HttpPost]
        public IActionResult Delete([FromBody] ActivityType activityType)
        {
            this.activityTypeRepository.Remove(activityType.Id);
            return Ok();
        }
    }
}