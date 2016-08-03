using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomTom
{
    public static class TypeUtilities
    {
        public static bool IsNullable(this Type type)
        {
            return type.IsClass || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static Type ConvertToNullableType(this Type type)
        {
            if (type.IsNullable())
                return type;
            if (type.IsClass)
                return type;
            return typeof(Nullable<>).MakeGenericType(type);
        }
    }
}
