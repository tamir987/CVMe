using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Common.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime? ToNullableDate(this DateTime? date)
        {
            return date.HasValue ? new DateTime(date.Value.Year, date.Value.Month, date.Value.Day) : (DateTime?)null;
        }

        public static int ToUnixTime(this DateTime date)
        {
            var startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)Math.Round((date.ToUniversalTime() - startDate).TotalSeconds);
        }

        public static int? ToUnixTime(this DateTime? date)
        {
            return date?.ToUnixTime();
        }

        public static DateTime GetNextLeapDate(this DateTime date)
        {
            int year = date.Year;

            // start in the next year if we’re already in March, or february 29
            if (date.Month > 2 || (date.Month == 2 && date.Day == 29))
                year++;

            // find next leap year
            while (!DateTime.IsLeapYear(year))
                year++;

            // get last of February
            return new DateTime(year, 2, 29, date.Hour, date.Minute, date.Second);
        }

        public static DateTime GetNextOrCurrentLeapDate(this DateTime date)
        {
            if (date.Month == 2 && date.Day == 29) return date;
            return date.GetNextLeapDate();
        }

        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            int startDayOfWeek = (int)date.DayOfWeek;
            int targetDayOfWeek = (int)dayOfWeek;

            if (targetDayOfWeek <= startDayOfWeek)
                targetDayOfWeek += 7;

            return date.AddDays(targetDayOfWeek - startDayOfWeek);
        }

        public static DateTime NextOrCurrent(this DateTime date, DayOfWeek dayOfWeek)
        {
            int startDayOfWeek = (int)date.DayOfWeek;
            int targetDayOfWeek = (int)dayOfWeek;

            return startDayOfWeek == targetDayOfWeek ? date : date.Next(dayOfWeek);
        }

        public static DateTime? Next(this DateTime date, int targetMonth, int targetDayOfMonth)
        {
            // Check for not exist date pairs
            var monthsWith31 = new List<int> { 1, 3, 5, 7, 8, 10, 12 };
            var monthsWith30 = new List<int> { 4, 6, 9, 11 };

            if (monthsWith31.Contains(targetMonth) && targetDayOfMonth > 31) return null;
            if (monthsWith30.Contains(targetMonth) && targetDayOfMonth > 30) return null;
            if (targetMonth == 2 && targetDayOfMonth > 29) return null;

            // For february 29, get the date of the next leap year
            if (targetMonth == 2 && targetDayOfMonth == 29) return date.GetNextLeapDate();

            int resultYear;
            // Check if the target date has not passed already in the date year
            if (date.Month < targetMonth || (date.Month == targetMonth && date.Day < targetDayOfMonth))
                resultYear = date.Year;
            else
                resultYear = date.Year + 1;

            return new DateTime(resultYear, targetMonth, targetDayOfMonth, date.Hour, date.Minute, date.Second);
        }

        public static DateTime? NextOrCurrent(this DateTime date, int targetMonth, int targetDayOfMonth)
        {
            return date.Month == targetMonth && date.Day == targetDayOfMonth ? date : date.Next(targetMonth, targetDayOfMonth);
        }

        /// <summary>
        /// Return the last day in the month of the given date, while keeping the time portion
        /// </summary>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return date.AddDays(-date.Day + 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime NextLastDayOfMonth(this DateTime date, int targetMonth)
        {
            if (date.Month == targetMonth && date != date.LastDayOfMonth()) return date.LastDayOfMonth();

            DateTime nextFirstDayOfTheMonth = date.Month < targetMonth
                ? new DateTime(date.Year, targetMonth, 1, date.Hour, date.Minute, date.Second)
                : new DateTime(date.Year + 1, targetMonth, 1, date.Hour, date.Minute, date.Second);

            return nextFirstDayOfTheMonth.LastDayOfMonth();
        }

        public static DateTime NextOrCurrentLastDayOfMonth(this DateTime date, int targetMonth)
        {
            return date.Month == targetMonth && date == date.LastDayOfMonth() ? date : date.NextLastDayOfMonth(targetMonth);
        }
    }
}
