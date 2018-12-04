using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Common.HelperMethods
{
    public static class DateHelper
    {
        /// <summary>
        /// Returns the minimum between two dates
        /// </summary>
        public static DateTime Min(DateTime date1, DateTime date2)
        {
            return date1 < date2 ? date1 : date2;
        }

        /// <summary>
        /// Returns the minimum between two nullable dates. Null value is returned when both dates are null, and null value for one date is ignored.
        /// </summary>
        public static DateTime? Min(DateTime? date1, DateTime? date2)
        {
            if (date1 == null) return date2;
            if (date2 == null) return date1;
            return Min(date1.Value, date2.Value);
        }

        /// <summary>
        /// Returns the maximum between two dates
        /// </summary>
        public static DateTime Max(DateTime date1, DateTime date2)
        {
            return date1 > date2 ? date1 : date2;
        }

        /// <summary>
        /// Returns the maximum between two nullable dates. Null value is returned when both dates are null, and null value for one date is ignored.
        /// </summary>
        public static DateTime? Max(DateTime? date1, DateTime? date2)
        {
            if (date1 == null) return date2;
            if (date2 == null) return date1;
            return Max(date1.Value, date2.Value);
        }
    }
}
