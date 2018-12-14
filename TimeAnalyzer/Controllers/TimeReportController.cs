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
    [Route("api/[controller]/[action]")]
    public class TimeReportController : Controller
    {
        private readonly ITimeReportService timeReportService;
        private readonly Func<ITimeReportService> timeReportServiceInsatller;

        public TimeReportController(ITimeReportServiceFactory timeReportServiceFactory)
        {
            timeReportService = null;
            this.timeReportServiceInsatller = () => timeReportServiceFactory.CreateTimeReportService(HttpContext.User.Identity.Name);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllTimeReports()
        {
            IEnumerable<DayTimeReportViewModel> reports = await GetReportService().GetAllUserTimeReports();
            return Ok(reports);
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetDayTimeReports(string date)
        {
            IEnumerable<DayTimeReportViewModel> reports = await GetReportService().GetDayTimeReportAsync(date);
            return Ok(reports);
        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetTimeReportsInInterval(string startDate, string endDate)
        {
            TimeReportsIntervalViewModel reports = await GetReportService().GetTimeReportsInInterval(startDate,endDate);
            return Ok(reports);
        }

        [HttpPost()]
        public async Task<IActionResult> AddTimeReport([FromBody]DayTimeReportViewModel timeReport)
        {
            timeReport.Id = await GetReportService().AddTimeReport(timeReport);
            return Ok(timeReport);
        }

        [HttpPost()]
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