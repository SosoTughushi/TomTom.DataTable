using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtentions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> tEnumerable)
        {
            if (tEnumerable == null)
                return true;

            return !tEnumerable.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> tEnumerable)
        {
            return !tEnumerable.IsEmpty();
        }

        //public static DataTableResponse<T> Filter<T>(this IQueryable<T> source,FilterRequest meta)
        //{
        //    return new DataTableResponse<T>
        //    {
        //        DataList = source.FilterQueryable(meta).ToList(),
        //        TotalRecords = source.CountQueryable(meta)
        //    };
        //}
        //public static int CountQueryable<T>(this IQueryable<T> source, FilterRequest meta)
        //{
        //    var filters = meta.Filters.Where(m => m.OperationType != OperationType.Empty && m.Value.IsNotEmpty()).ToList();
        //    source = filterQuery(source, filters);
        //    return source.Count();
        //}
        //public static IQueryable<T> FilterQueryable<T>(this IQueryable<T> source, FilterRequest meta)
        //{
        //    var filters = meta.Filters.Where(m => m.OperationType != OperationType.Empty && m.Value.IsNotEmpty()).ToList();
        //    source = filterQuery(source, filters);

        //    if (meta.Count == 0)
        //    {
        //        meta.Count = null;
        //    }
        //    if (meta.Offset.HasValue || meta.Count.HasValue)
        //    {
        //        if (meta.OrderingField.IsNotEmpty())
        //        {
        //            var orderByField = meta.OrderingField;
        //            var orderByFieldExp = orderByField.GetPropertyExpression<T>();
        //            var orderBy = meta.IsOrderingAscending ? "OrderBy" : "OrderByDescending";
        //            var orderByMethodInfo =
        //            typeof(Queryable)
        //            .GetMethods(BindingFlags.Static | BindingFlags.Public)//TODO: Change GetMethodS.First to GetMethod
        //            .First(c => c.Name == orderBy)
        //            .MakeGenericMethod(typeof(T), ((LambdaExpression)orderByFieldExp).ReturnType);

        //            source = (IQueryable<T>)orderByMethodInfo.Invoke(null, new object[] { source, ((LambdaExpression)orderByFieldExp) });
        //        }
        //        if (meta.Offset.HasValue && meta.Offset.Value > 0)
        //        {
        //            source = source.Skip(meta.Offset.Value);
        //        }
        //        if (meta.Count.HasValue && meta.Count.Value > 0)
        //        {
        //            source = source.Take(meta.Count.Value);
        //        }
        //    }

        //    return source;
        //}

        //private static IQueryable<T> filterQuery<T>(IQueryable<T> source, List<Filter> filters)
        //{
        //    if (filters.IsNotEmpty())
        //    {
        //        foreach (var exp in filters.Select(GenerateFilter<T>))
        //        {
        //            source = source.Where(exp);
        //        }
        //    }

        //    return source;
        //}

        //private static Expression<Func<TEntity, bool>> GenerateFilter<TEntity>(Filter filter)
        //{
        //    var left = Expression.Parameter(typeof(TEntity), "param");
        //    Expression property = null;
        //    // ReSharper disable once LoopCanBeConvertedToQuery
        //    foreach (var fieldName in filter.PopertyName.Split('.'))
        //    {
        //        property = property == null
        //            ? Expression.Property(left, fieldName)
        //            : Expression.Property(property, fieldName);
        //    }
        //    ConstantExpression param;
        //    Array array = null;
        //    var type = Nullable.GetUnderlyingType(filterOption.ValueType) ?? filterOption.ValueType;
        //}
    }
}
