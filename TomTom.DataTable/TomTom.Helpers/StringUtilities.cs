using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomTom.Utilities
{
    public static class StringUtilities
    {

        public static object ConvertToObject(this string source, Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            if (!type.IsArray)
            {
                if (source.IsEmpty())
                {
                    return null;
                }
                return Convert.ChangeType(source, t);
            }

            var values = source.Split(',');
            var array = (Array)Activator.CreateInstance(type, values.Count());
            for (int i = 0; i < values.Count(); i++)
            {
                if (values[i].IsEmpty())
                {
                    continue;
                }
                var elementType = type.GetElementType();
                var arrValue = Convert.ChangeType(values[i], Nullable.GetUnderlyingType(elementType) ?? elementType);
                array.SetValue(arrValue, i);
            }

            return array;
        }
    }
}
