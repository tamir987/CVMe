using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVMe.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            return s != null && int.TryParse(s.Trim(), out int n) ? n : (int?)null;
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            return s.ToNullableInt() ?? defaultValue;
        }

        public static bool IsInt(this string s)
        {
            return s.ToNullableInt().HasValue;
        }


        public static long? ToNullableLong(this string s)
        {
            return s != null && long.TryParse(s.Trim(), out long l) ? l : (long?)null;
        }

        public static long ToLong(this string s, long defaultValue = 0)
        {
            return s.ToNullableLong() ?? defaultValue;
        }

        public static bool IsLong(this string s)
        {
            return s.ToNullableLong().HasValue;
        }


        public static float? ToNullableFloat(this string s)
        {
            return s != null && float.TryParse(s.Trim(), out float f) ? f : (float?)null;
        }

        public static float ToFloat(this string s, float defaultValue = 0)
        {
            return s.ToNullableFloat() ?? defaultValue;
        }

        public static bool IsFloat(this string s)
        {
            return s.ToNullableFloat().HasValue;
        }


        public static double? ToNullableDouble(this string s)
        {
            return s != null && double.TryParse(s.Trim(), out double d) ? d : (double?)null;
        }

        public static double ToDouble(this string s, double defaultValue = 0)
        {
            return s.ToNullableDouble() ?? defaultValue;
        }

        public static bool IsDouble(this string s)
        {
            return s.ToNullableDouble().HasValue;
        }


        public static decimal? ToNullableDecimal(this string s)
        {
            return s != null && decimal.TryParse(s.Trim(), out decimal d) ? d : (decimal?)null;
        }

        public static decimal ToDecimal(this string s, decimal defaultValue = 0)
        {
            return s.ToNullableDecimal() ?? defaultValue;
        }

        public static bool IsDecimal(this string s)
        {
            return s.ToNullableDecimal().HasValue;
        }


        public static DateTime? ToNullableDateTime(this string s, string format = null)
        {
            DateTime date;

            if (format == null)
                return s != null && DateTime.TryParse(s.Trim(), out date) ? date : (DateTime?)null;

            return s != null && DateTime.TryParseExact(s.Trim(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ? date : (DateTime?)null;
        }

        public static DateTime? ToNullableDate(this string s, string format = null)
        {
            return s.ToNullableDateTime(format).ToNullableDate();
        }

        public static DateTime ToDateTime(this string s, DateTime? defaultValue = null, string format = null)
        {
            return s.ToNullableDateTime(format) ?? (defaultValue ?? DateTime.MinValue);
        }

        public static DateTime ToDate(this string s, DateTime? defaultValue = null, string format = null)
        {
            return s.ToNullableDate(format) ?? (defaultValue ?? DateTime.MinValue);
        }

        public static TEnum? ToNullableEnum<TEnum>(this string s) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("T must be an enumerated type");

            return !string.IsNullOrEmpty(s) && Enum.TryParse(s, out TEnum value) ? value : (TEnum?)null;
        }

        public static string RemoveBetween(this string s, char begin, char end)
        {
            if (string.IsNullOrEmpty(s)) return s;
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return new Regex(" +").Replace(regex.Replace(s, string.Empty), " ");
        }

        public static bool IsNullOrEmpty(this string value) => value == null || value.Length == 0;
    }
}
