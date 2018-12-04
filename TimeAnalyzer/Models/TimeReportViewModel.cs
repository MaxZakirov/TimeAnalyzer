using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Models
{
    public class TimeReportViewModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public short Duration { get; set; } 

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
