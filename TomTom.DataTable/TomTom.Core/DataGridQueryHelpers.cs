using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TomTom.DataTable
{

    public static class DataGridQueryHelpers
    {
        public static IEnumerable<TEntity> DataTableEnumerableFilter<TEntity>(this IEnumerable<TEntity> query,
            DataGridFilters dataSelector)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var filter in dataSelector.GetFilterOptions())
            {
                var filterExpressionTree = filter.GenerateFilter<TEntity>();
                if (filterExpressionTree != null)
                {
                    query = query.Where(filterExpressionTree.Compile());
                }
            }

            var orderingField = dataSelector.OrderingField.GetPropertyExpression<TEntity>();
            if (orderingField != null)
            {
                //Calling Order By Dynamically
                var orderBy = dataSelector.IsSortDirectionAscending ? "OrderBy" : "OrderByDescending";
                var orderByMethodInfo =
                    typeof(Enumerable)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)//TODO: Change GetMethodS.First to GetMethod
                    .First(c => c.Name == orderBy)
                    .MakeGenericMethod(typeof(TEntity), ((LambdaExpression)orderingField).ReturnType);

                query = (IEnumerable<TEntity>)orderByMethodInfo.Invoke(null, new object[] { query, ((LambdaExpression)orderingField).Compile() });

            }
            if (dataSelector.ItemsPerPage > 0)
                query = query.Skip(dataSelector.Offset).Take(dataSelector.ItemsPerPage);
            return query;
        }

        public static int DataTableEnumerableCount<TEntity>(this IEnumerable<TEntity> query,
            DataGridFilters dataSelector)
        {
            foreach (var filter in dataSelector.GetFilterOptions())
            {
                var f = filter.GenerateFilter<TEntity>();
                if (f != null)
                    query = query.Where(f.Compile());
            }
            return query.Count();
        }

        public static IQueryable<TEntity> DataTableQueryableFilter<TEntity>(this IQueryable<TEntity> query,
            DataGridFilters dataSelector)
        {
            foreach (var filter in dataSelector.GetFilterOptions())
            {
                var filterExpressionTree = filter.GenerateFilter<TEntity>();
                if (filterExpressionTree != null)
                {
                    query = query.Where(filterExpressionTree);
                }
            }

            var orderingField = dataSelector.OrderingField.GetPropertyExpression<TEntity>();
            if (orderingField != null)
            {
                //Calling Order By Dynamically
                var orderBy = dataSelector.IsSortDirectionAscending ? "OrderBy" : "OrderByDescending";
                var orderByMethodInfo =
                    typeof(Queryable)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)//TODO: Change GetMethodS.First to GetMethod
                    .First(c => c.Name == orderBy)
                    .MakeGenericMethod(typeof(TEntity), ((LambdaExpression)orderingField).ReturnType);

                query = (IQueryable<TEntity>)orderByMethodInfo.Invoke(null, new object[] { query, ((LambdaExpression)orderingField) });

            }
            if (dataSelector.ItemsPerPage > 0)
                query = query.Skip(dataSelector.Offset).Take(dataSelector.ItemsPerPage);
            return query;
        }


        public static int DataTableQueryableCount<TEntity>(this IQueryable<TEntity> query,
            DataGridFilters dataSelector)
        {
            foreach (var filter in dataSelector.GetFilterOptions())
            {
                var f = filter.GenerateFilter<TEntity>();
                if (f != null)
                    query = query.Where(f);
            }
            return query.Count();
        }
    }
}
