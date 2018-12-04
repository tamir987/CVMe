using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Common.Helpers
{
    public abstract class AbstractEnumUtils<TClass> where TClass : class
    {
        public static TEnum Parse<TEnum>(string value) where TEnum : struct, TClass
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct, TClass
        {
            return Enum.TryParse(value, out result);
        }

        public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct, TClass
        {
            return Enum.TryParse(value, ignoreCase, out result);
        }
    }

    // Using constraint inheritance to enforce enum constraint for the compiler. http://stackoverflow.com/a/28527552
    public class EnumUtils : AbstractEnumUtils<Enum>
    {
    }
}
