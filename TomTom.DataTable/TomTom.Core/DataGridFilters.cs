using System.Collections.Generic;

namespace TomTom.DataTable
{

    public class DataGridFilters : Filters
    {
        public DataGridParameters Parameters { get; set; }

        public DataGridFilters(List<FilterOption> filterOptions, string tableId) : base(filterOptions, tableId)
        {

        }

        public DataGridFilters()
        {

        }

        //TODO: refactor as extention method in another library
        //public FilterRequest ToFilterRequest()
        //{
        //    return new FilterRequest()
        //    {
        //        Filters = GetFilterOptions().Select(c => c.GetFilter()).ToList(),
        //        Count = ItemsPerPage,
        //        IsOrderingAscending = IsSortDirectionAscending,
        //        Offset = Offset,
        //        OrderingColumnIndex = OrderingColumnIndex,
        //        TableId = TableId
        //    };
        //}

        public int TotalRecords { get; set; }

    }
}