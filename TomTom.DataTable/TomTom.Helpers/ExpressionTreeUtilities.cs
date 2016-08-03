using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TomTom
{

    public static class ExpressionTreeUtilities
    {
        public static Expression GetPropertyExpression<TObjectType>(this string propertyName, bool getLambda = true, bool convertToObject = false)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyName))
                    return null;

                var param = Expression.Parameter(typeof(TObjectType), "param");
                Expression property = null;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var fieldName in propertyName.Split('.'))
                {
                    property = property == null
                        ? Expression.Property(param, fieldName)
                        : Expression.Property(property, fieldName);
                }

                // ReSharper disable once PossibleNullReferenceException
                if (property.Type.IsEnum)
                {
                    property = Expression.Convert(property, typeof(int));
                }
                if (getLambda)
                {
                    if (convertToObject)
                    {
                        property = Expression.Convert(property, typeof(object));
                    }
                    return Expression.Lambda(property, param);
                }

                return property;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Can't create Property Expression for {0}", propertyName), e);
            }
        }

        public static string ExtractPropertyNameFromExpression<T, TFieldType>(this Expression<Func<T, TFieldType>> propertyExpression)
        {

            if (propertyExpression == null) return null;
            if (propertyExpression.Body is MemberExpression)
            {
                return _getMemberName((propertyExpression.Body as MemberExpression));
            }
            if (propertyExpression.Body is UnaryExpression)
            {
                var unaryExpression = propertyExpression.Body as UnaryExpression;

                if (!(unaryExpression.Operand is MemberExpression))
                    throw new Exception(string.Format("can't extract property name from expression specified"));
                return _getMemberName((unaryExpression.Operand as MemberExpression));

            }
            if (propertyExpression.Body is BinaryExpression)
            {
                var binaryBody = (propertyExpression.Body as BinaryExpression);
                return _getMemberName(binaryBody.Left as MemberExpression);
            }

            if (propertyExpression.Body is ParameterExpression)
                return "";
            throw new Exception(string.Format("can't extract property name from expression specified"));
        }

        public static Type GetPropertyTypeFromExpression<T>(this Expression<Func<T, object>> propertyExpression)
        {
            var body = propertyExpression.Body;
            if (body is UnaryExpression)
            {
                body = (body as UnaryExpression).Operand;
            }

            return (body).Type;
        }

        public static string ExtractPropertyNameFromMemberExpression(this MemberExpression memberExpression)
        {
            return _getMemberName(memberExpression);
        }
        private static string _getMemberName(MemberExpression memberExpression)
        {
            var member = memberExpression.Member;
            if (member.MemberType != MemberTypes.Property)
            {
                throw new Exception(string.Format("{0} is not Property", member.Name));
            }
            string prefix = "";
            if (memberExpression.Expression is MemberExpression)
                prefix = _getMemberName(memberExpression.Expression as MemberExpression) + ".";
            return prefix + member.Name;
        }
    }
}
