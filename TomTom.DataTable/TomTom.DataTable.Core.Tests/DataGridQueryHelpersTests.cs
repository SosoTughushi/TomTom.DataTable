using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomTom.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomTom.DataTable.Tests
{
    [TestClass()]
    public class DataGridQueryHelpersTests
    {
        private class SomeModel
        {
            public int CategoryId { get; internal set; }
            public string Description { get; internal set; }
            public int Id { get; internal set; }
            public int InStockCount { get; internal set; }
            public string Name { get; internal set; }
            public decimal Price { get; internal set; }
        }

        private List<SomeModel> _getModel()
        {
            int count = 0;

            return Enumerable
                .Range(1, 10)
                .Select(c => string.Format("Product{0}", c))
                .Select(name => new SomeModel
                {
                    Name = name,
                    Id = ++count,
                    CategoryId = count,
                    Description = name + name + name + name,
                    InStockCount = count * 3,
                    Price = count * 10
                }).ToList();
        }

        private DataTableResponse<SomeModel> _filter(DataGridFilters filters, List<SomeModel> model)
        {
            var resp1 = new DataTableResponse<SomeModel>
            {
                TotalRecords = model.DataTableEnumerableCount(filters),
                DataList = model.DataTableEnumerableFilter(filters).ToList()
            };
            var resp2 = new DataTableResponse<SomeModel>
            {
                TotalRecords = model.AsQueryable().DataTableQueryableCount(filters),
                DataList = model.AsQueryable().DataTableQueryableFilter(filters).ToList()
            };
            Assert.AreEqual(resp1.TotalRecords, resp2.TotalRecords);
            CollectionAssert.AreEqual(resp2.DataList, resp1.DataList);

            return resp2;
        }

        private DataTableResponse<SomeModel> _createAndFilter(DataGridFilters filters)
        {
            return _filter(filters, _getModel());
        }

        private static DataGridFilters _getRequestWithStringFIlter(string word)
        {
            return new DataGridFilters
            {
                StringFilterOptions = new List<FilterOption<string>>
                {
                    new FilterOption<string>
                    {
                        PropName = "Name",
                        OperationType = OperationType.Contains,
                        Val = new List<string> { word }
                    }
                }
            };
        }

        private static DataGridFilters _getRequestWithPriceFilter(decimal? from, decimal? to)
        {
            return new DataGridFilters
            {
                DecimalFilterOptionsNullable = new List<FilterOption<decimal?>>
                {
                    new FilterOption<decimal?>
                    {
                        PropName = "Price",
                        Val = new List<decimal?> { from, to },
                        OperationType = OperationType.Between
                    }
                }
            };
        }

        [TestMethod]
        public void GetData_should_return_10_elements()
        {
            var resp = _createAndFilter(new DataGridFilters());
            Assert.AreEqual(10, resp.DataList.Count);
            Assert.AreEqual(10, resp.TotalRecords);
        }

        [TestMethod]
        public void GetData_should_return_4_as_data_and_10_as_overall()
        {
            var resp = _createAndFilter(new DataGridFilters
            {
                Offset = 6,
                ItemsPerPage = 10
            });
            Assert.AreEqual(4, resp.DataList.Count);
            Assert.AreEqual(10, resp.TotalRecords);
        }


        [TestMethod]
        public void GetData_should_return_4_as_data_and_5_as_overall_count()
        {
            var resp = _createAndFilter(new DataGridFilters
            {
                IntFilterOptions = new List<FilterOption<int>>
                {
                    new FilterOption<int>
                    {
                        PropName = "Id",
                        OperationType = OperationType.MoreThen,
                        Val = new List<int> {5 }
                    }
                },
                ItemsPerPage = 4
            });
            Assert.AreEqual(4, resp.DataList.Count);
            Assert.AreEqual(5, resp.TotalRecords);
        }



        [TestMethod]
        public void GetData_filter_by_name_should_return_10()
        {
            var resp = _createAndFilter(_getRequestWithStringFIlter("Product"));
            Assert.AreEqual(10, resp.TotalRecords);
        }

        [TestMethod]
        public void GetData_filter_by_price_should_return_9()
        {
            var resp = _createAndFilter(_getRequestWithPriceFilter(15, null));
            Assert.AreEqual(9, resp.TotalRecords);
        }

        [TestMethod]
        public void GetData_filter_by_price_should_return_5()
        {
            var resp = _createAndFilter(_getRequestWithPriceFilter(null, 51));
            Assert.AreEqual(5, resp.TotalRecords);
        }

        [TestMethod]
        public void GetData_fiter_by_price_should_return_4()
        {
            var resp = _createAndFilter(_getRequestWithPriceFilter(15, 51));
            Assert.AreEqual(4, resp.TotalRecords);
        }

        [TestMethod]
        public void GetData_filter_by_null_should_return_10()
        {
            var resp = _createAndFilter(new DataGridFilters
            {
                IntFilterOptionsNullable = new List<FilterOption<int?>>
                {
                    new FilterOption<int?>
                    {
                        PropName = "CategoryId",
                        Val = new List<int?> {null },
                        OperationType = OperationType.Equals
                    },
                },
                DecimalFilterOptionsNullable = new List<FilterOption<decimal?>>
                {
                    new FilterOption<decimal?>
                    {
                        PropName = "Price",
                        Val = new List<decimal?> {null,null },
                        OperationType = OperationType.Between
                    }
                },
                StringFilterOptions = new List<FilterOption<string>>
                {
                    new FilterOption<string>
                    {
                        PropName = "Name",
                        Val = new List<string> {null },
                        OperationType = OperationType.Contains
                    }
                }
            });

            Assert.AreEqual(10, resp.TotalRecords);
        }
    }
}