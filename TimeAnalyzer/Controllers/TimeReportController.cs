using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Core.TimeReports;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/TimeReport")]
    public class TimeReportController : Controller
    {
        private readonly ITimeReportService timeReportService;

        public TimeReportController(ITimeReportServiceFactory timeReportService)
        {
            this.timeReportService = timeReportService.CreateTimeReportService(HttpContext.User.Identity.Name);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserTimeReports()
        {
            var reports = await this.timeReportService.GetAllUserTimeReports();
            return Ok(reports);
        }

        [HttpPost("[action]")]
        public IActionResult AddTimeReport(TimeReportViewModel timeReport)
        {
            timeReport.Id = this.timeReportService.AddTimeReport(timeReport);
            return Ok(timeReport);
        }
    }
}