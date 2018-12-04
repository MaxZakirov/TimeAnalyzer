using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeAnalyzer.Core.TimeReports;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/TimeReport")]
    public class TimeReportController : Controller
    {
        private readonly ITimeReportService timeReportService;
        private readonly Func<ITimeReportService> timeReportServiceInsatller;

        public TimeReportController(ITimeReportServiceFactory timeReportServiceFactory)
        {
            timeReportService = null;
            this.timeReportServiceInsatller = () => timeReportServiceFactory.CreateTimeReportService(HttpContext.User.Identity.Name);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserTimeReports()
        {
            var reports = await GetReportService().GetAllUserTimeReports();
            return Ok(reports);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTimeReport([FromBody]TimeReportViewModel timeReport)
        {
            timeReport.Id = await GetReportService().AddTimeReport(timeReport);
            return Ok(timeReport);
        }

        private ITimeReportService GetReportService()
        {
            return timeReportService ?? timeReportServiceInsatller.Invoke();
        }
    }
}