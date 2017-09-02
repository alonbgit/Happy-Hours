using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Core
{
    public static class HappyHourTimestampProvider
    {
        private const string DATE_FORMAT = "dd'/'MM'/'yyyy";
        private const string TIME_FORMAT = "HH':'mm";

        private const string DATETIME_TIMESTAMP_FORMAT = "HH':'mm";
        private const string DATE_TIMESTAMP_FORMAT = "yyyy'-'dd'-'MM";

        public static string GetDateTimestamp(DateTime? date)
        {
            if (!date.HasValue)
                return null;
            return date.Value.ToString(DATE_TIMESTAMP_FORMAT);
        }

        public static string GetDateTimeTimestamp(DateTime? date)
        {
            if (!date.HasValue)
                return null;
            return date.Value.ToString(DATETIME_TIMESTAMP_FORMAT);
        }

        public static DateTime? ParseDate(string date)
        {
            return ParseString(DATE_FORMAT, date);
        }

        public static DateTime? ParseTime(string time)
        {
            return ParseString(TIME_FORMAT, time);
        }

        private static DateTime? ParseString(string format, string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            DateTime result;
            if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;
            return null;
        }
    }
}
