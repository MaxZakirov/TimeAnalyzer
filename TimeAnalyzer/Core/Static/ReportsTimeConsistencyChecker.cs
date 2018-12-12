using System.Collections.Generic;
using System.Linq;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.Static
{
    public static class ReportsTimeConsistencyChecker
    {
        static readonly short MinutesInDay = 1440;

        public static bool CheckDayConsistency(IEnumerable<TimeReport> timeReports)
        {
            return MinutesInDay - timeReports.Sum((tr) => (int)tr.Duration) >= 0;
        }

        public static bool CheckDayConsistency(IEnumerable<TimeReport> timeReports, int newDuration)
        {
            return MinutesInDay - newDuration - timeReports.Sum((tr) => (int)tr.Duration) >= 0;
        }

        public static bool CheckTimeRangeConsistency(IEnumerable<TimeReport> timeReports, int numbersOfDay)
        {
            return (MinutesInDay * numbersOfDay) - timeReports.Sum((tr) => (int)tr.Duration) >= 0;
        }
    }
}
