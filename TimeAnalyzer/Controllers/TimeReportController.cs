using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Core.TimeReports;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/TimeReport")]
    public class TimeReportController : Controller
    {
        private readonly ITimeReportService timeReportService;

        public TimeReportController(ITimeReportService timeReportService)
        {
            this.timeReportService = timeReportService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserTimeReports()
        {
            var reports = await this.timeReportService.GetAllUserTimeReports(HttpContext.User.Identity.Name);
            return Ok(reports);
        }
    }
}