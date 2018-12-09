using System;
using System.Globalization;

namespace TimeAnalyzer.Core.Static
{
    public static class TimeConverter
    {
        public static DateTime ToDateTime(string javasciptDateString)
        {
            return DateTime.ParseExact(javasciptDateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ToJSONString(DateTime time)
        {
            return time.Day + "/" + time.Month + "/" + time.Year + " " + time.Hour + ':' + time.Minute + ':' + time.Second;
        }
    }
}
