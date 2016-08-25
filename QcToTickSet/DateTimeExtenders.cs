using System;

namespace QcToTickSet
{
    public static partial class Extenders
    {
        private const string EST_TZNAME = "Eastern Standard Time";
        private const string UTC_TZNAME = "UTC";

        private static readonly TimeZoneInfo estTzi;
        private static readonly TimeZoneInfo utcTzi;

        static Extenders()
        {
            estTzi = TimeZoneInfo.FindSystemTimeZoneById(EST_TZNAME);
            utcTzi = TimeZoneInfo.FindSystemTimeZoneById(UTC_TZNAME);
        }

        public static DateTime ToEstFromUtc(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                throw new ArgumentOutOfRangeException("dateTime.Kind");

            return TimeZoneInfo.ConvertTime(dateTime, estTzi);
        }

        public static DateTime ToUtcFromEst(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Unspecified)
                throw new ArgumentOutOfRangeException("dateTime.Kind");

            return TimeZoneInfo.ConvertTime(dateTime, estTzi, utcTzi);
        }

        public static bool IsWeekday(this DateTime value)
        {
            if (value.Kind != DateTimeKind.Unspecified)
                throw new ArgumentOutOfRangeException(nameof(value));

            switch (value.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return false;
                default:
                    return true;
            }
        }

        //public static bool IsTradeDate(this DateTime value)
        //{
        //    if (!value.IsWeekday())
        //        return false;

        //    return !WellKnown.DatesToSkip.Contains(value.Date);
        //}

        //public static bool IsTickOn(this DateTime value)
        //{
        //    if (!value.IsTradeDate())
        //        return false;

        //    var timeOfDay = value.TimeOfDay;

        //    return (timeOfDay >= WellKnown.SessionStart)
        //        && (timeOfDay < WellKnown.SessionEnd);
        //}











        public static DateTime LastDayOfPriorMonth(this DateTime value) =>
            new DateTime(value.Year, value.Month, 1).AddDays(-1);

        public static string ToDateText(this DateTime value) =>
            value.ToString("MM/dd/yyyy");

        public static string ToTimeString(this DateTime value) =>
            value.ToString("HH:mm:ss.fff");

        public static string ToDateTimeString(this DateTime value) =>
            value.ToString("MM/dd/yyyy HH:mm:ss.fff");
    }
}