using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.TimeReports;
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
            timeReportServiceInsatller = () => timeReportServiceFactory.CreateTimeReportService(HttpContext.User.Identity.Name);
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetDayTimeReports(string date)
        {
            try
            {
                IEnumerable<DayTimeReportViewModel> reports = await GetReportService().GetDayTimeReportAsync(date);
                return Ok(reports);
            }
            catch (IncorrectInputDateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetTimeReportsInInterval(string startDate, string endDate)
        {
            try
            {
                TimeReportsIntervalViewModel reports = await GetReportService().GetTimeReportsInInterval(startDate, endDate);
                return Ok(reports);
            }
            catch (IncorrectInputDateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddTimeReport([FromBody]DayTimeReportViewModel timeReport)
        {
            try
            {
                timeReport.Id = await GetReportService().AddTimeReport(timeReport);
                return Ok(timeReport);
            }
            catch (IncorrectInputDateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateTimeReport([FromBody]DayTimeReportViewModel timeReport)
        {
            try
            {
                await GetReportService().Update(timeReport);
                return Ok();
            }
            catch (IncorrectInputDateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("{userId}/{activityId}/{duration}")]
        public async Task<IActionResult> AddReportIOT(int userId, int activityId, int duration)
        {
            try
            {
                IOTViewModel timeReport = new IOTViewModel()
                {
                    UserId = userId,
                    ActivityId = activityId,
                    Duration = duration
                };
                await GetReportService().AddTimeReportFromIOT(timeReport);
                return Ok();
            }
            catch (IncorrectInputDateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private ITimeReportService GetReportService()
        {
            return timeReportService ?? timeReportServiceInsatller.Invoke();
        }
    }
}