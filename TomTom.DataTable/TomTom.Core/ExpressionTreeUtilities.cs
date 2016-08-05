using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TomTom.Utilities;

namespace TomTom.DataTable
{

    public static class ExpressionTreeUtilities
    {
        private static readonly MethodInfo StringContainsMethodInfo = typeof(string).GetMethod("Contains");
        private static readonly MethodInfo ContainsInfo = typeof(Enumerable).GetMethods().First(m => m.Name == "Contains");

        public static Expression<Func<TEntity, bool>> GenerateFilter<TEntity>(this FilterOption filterOption)
        {

            try
            {
                var left = Expression.Parameter(typeof(TEntity), "param");


                Expression property = null;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var fieldName in filterOption.PropName.Split('.'))
                {
                    property = property == null
                        ? Expression.Property(left, fieldName)
                        : Expression.Property(property, fieldName);
                }

                //Expression property = FieldName.GetPropertyExpression<TEntity>(false);
                ConstantExpression param;
                Array array = null;
                var type = Nullable.GetUnderlyingType(filterOption.ValueType) ?? filterOption.ValueType;

                if (!filterOption.ValueType.IsArray)
                {
                    var val = filterOption.Value.ConvertToObject(type);
                    if (val == null)
                        return null;

                    param = Expression.Constant(val, Nullable.GetUnderlyingType(filterOption.ValueType) ?? filterOption.ValueType);
                }
                else
                {
                    array = (Array)filterOption.Value.ConvertToObject(filterOption.ValueType);
                    param = Expression.Constant(array, filterOption.ValueType);
                }
                Expression body = null;
                switch (filterOption.OperationType)
                {
                    case OperationType.Equals:
                        body = Expression.Equal(property, param);
                        break;
                    case OperationType.MoreThen:
                        body = Expression.GreaterThan(property, param);
                        break;
                    case OperationType.LessThen:
                        body = Expression.LessThan(property, param);
                        break;
                    case OperationType.Contains:
                        // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                        body = Expression.Call(property, StringContainsMethodInfo, param);
                        break;
                    case OperationType.In:
                        body = Expression.Call(null, ContainsInfo.MakeGenericMethod(filterOption.ValueType.GetElementType()), param, property);
                        break;
                    case OperationType.Between:
                        var first = array.GetValue(0);
                        var second = array.GetValue(1);
                        // ReSharper disable once PossibleNullReferenceException

                        if (first == null && second == null)
                        {
                            break;
                        }
                        if (first == null)
                        {
                            var secondParam = Expression.Constant(second, second.GetType());
                            body = Expression.LessThanOrEqual(property, secondParam);
                        }
                        else if (second == null)
                        {
                            var firstParam = Expression.Constant(first, first.GetType());
                            body = Expression.GreaterThanOrEqual(property, firstParam);
                        }
                        else
                        {
                            var secondParam = Expression.Constant(second, second.GetType());
                            var firstParam = Expression.Constant(first, first.GetType());
                            body = Expression.And(
                                Expression.GreaterThanOrEqual(property, firstParam),
                                Expression.LessThanOrEqual(property, secondParam));
                        }

                        break;
                }
                if (body == null) return null;
                return
                    Expression.Lambda<Func<TEntity, bool>>(
                        body,
                        new[] { left });
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Can not generate filter for {0} ({1})", filterOption.PropName, typeof(TEntity).FullName), e);
            }
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
