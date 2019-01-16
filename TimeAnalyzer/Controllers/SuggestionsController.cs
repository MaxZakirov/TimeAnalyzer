using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Core.Activities;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {
        private readonly ISuggestionsService suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService)
        {
            this.suggestionsService = suggestionsService;
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetForDay(string date)
        {
            try
            {
                var suggestions = await suggestionsService.GetSuggestions(HttpContext.User.Identity.Name, date);
                return Ok(suggestions.Select(kv => new { ActivityType = kv.Key, Activities = kv.Value }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}