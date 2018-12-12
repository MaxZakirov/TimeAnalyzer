using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Core.TimeReports;
using TimeAnalyzer.Models;
using TimeAnalyzer.Models.Reports;

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
            IEnumerable<DayTimeReportViewModel> reports = await GetReportService().GetAllUserTimeReports();
            return Ok(reports);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDayTimeReport(string stringDate)
        {
            IEnumerable<DayTimeReportViewModel> reports = await GetReportService().GetDayTimeReportAsync(stringDate);
            return Ok(reports);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTimeReport([FromBody]DayTimeReportViewModel timeReport)
        {
            timeReport.Id = await GetReportService().AddTimeReport(timeReport);
            return Ok(timeReport);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateTimeReport([FromBody]DayTimeReportViewModel timeReport)
        {
            await GetReportService().Update(timeReport);
            return Ok();
        }

        private ITimeReportService GetReportService()
        {
            return timeReportService ?? timeReportServiceInsatller.Invoke();
        }
    }
}