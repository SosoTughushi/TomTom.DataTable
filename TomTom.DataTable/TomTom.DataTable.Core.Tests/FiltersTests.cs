using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF = TomTom.Functional.Functional;

namespace TomTom.DataTable.Core.Tests
{
    [TestClass]
    public class FiltersTests
    {
        private List<ICollection> GetEnumerables(Filters filters)
        {
            return new List<ICollection>
                {
                    filters.StringFilterOptions,
                    filters.IntFilterOptions,
                    filters.DoubleFilterOptions,
                    filters.DateTimeFilterOptions,
                    filters.DateTimeFilterOptionsNullable,
                    filters.DecimalFilterOptions,
                    filters.BoolFilterOptions,
                    filters.FloatFilterOptions,
                    filters.IntFilterOptionsNullable,
                    filters.DoubleFilterOptionsNullable,
                    filters.DecimalFilterOptionsNullable,
                    filters.BoolFilterOptionsNullable,
                    filters.FloatFilterOptionsNullable,
            };
        }

        [TestMethod]
        public void constructor_initializaiton_asserts()
        {
            var filters = new Filters(null, "TableId");
            Assert.AreEqual(filters.TableId, "TableId");

            var areEmpty = GetEnumerables(filters).All(e => e.Count == 0);

            Assert.IsTrue(areEmpty);

        }
        private void assertCollectionCount<T>(Func<Filters, List<FilterOption<T>>> selector)
        {
            var filters = new Filters(new List<FilterOption>
                {
                    new FilterOption<T>(),
                    new FilterOption<T>(),
                }, "");
            var list = selector(filters);
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(GetEnumerables(filters).Where(e => e != list)
                .All(c => c.Count == 0));
        }


        [TestMethod]
        public void filters_correctly_initialized()
        {
            assertCollectionCount(filters => filters.StringFilterOptions);
            assertCollectionCount(filters => filters.IntFilterOptions);
            assertCollectionCount(filters => filters.DoubleFilterOptions);
            assertCollectionCount(filters => filters.DateTimeFilterOptions);
            assertCollectionCount(filters => filters.DateTimeFilterOptionsNullable);
            assertCollectionCount(filters => filters.DecimalFilterOptions);
            assertCollectionCount(filters => filters.BoolFilterOptions);
            assertCollectionCount(filters => filters.FloatFilterOptions);
            assertCollectionCount(filters => filters.IntFilterOptionsNullable);
            assertCollectionCount(filters => filters.DoubleFilterOptionsNullable);
            assertCollectionCount(filters => filters.DecimalFilterOptionsNullable);
            assertCollectionCount(filters => filters.BoolFilterOptionsNullable);
            assertCollectionCount(filters => filters.FloatFilterOptionsNullable);
        }

        [TestMethod]
        public void getFilterOptions_contains_all_the_items_passed_to_it()
        {
            List<FilterOption> initialFilters = _get_filter_for_all_types();

            var filters = new Filters(initialFilters, "");
            var returned = filters.GetFilterOptions();
            Assert.AreEqual(initialFilters.Count, returned.Count);

            var areSame = initialFilters.All(f => returned.Contains(f));
            Assert.IsTrue(areSame);
        }

        private static List<FilterOption> _get_filter_for_all_types()
        {
            var retList = new List<FilterOption>()
                {
                    new FilterOption<string>(),
                    new FilterOption<int>(),
                    new FilterOption<int?>(),
                    new FilterOption<DateTime>(),
                    new FilterOption<DateTime?>(),
                    new FilterOption<decimal>(),
                    new FilterOption<decimal?>(),
                    new FilterOption<bool>(),
                    new FilterOption<bool?>(),
                    new FilterOption<float>(),
                    new FilterOption<float?>(),
                    new FilterOption<double>(),
                    new FilterOption<double?>(),
                };

            int c = 0;
            retList.ForEach(f =>
            {
                c++;
                f.Id = c;
                f.OperationType = (OperationType)(c % 9);
                f.PropName = Guid.NewGuid().ToString();
            });
            return retList;
        }
        
    }
}
