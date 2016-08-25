using System.Collections.Generic;

namespace TomTom.DataTable
{

    public class DataGridFilters : Filters
    {
        public PagingAndOrderingInfo PagingAndOrderingInfo { get; set; }
        public DataGridParameters Parameters { get; set; }

        public DataGridFilters(List<FilterOption> filterOptions, string tableId) : base(filterOptions, tableId)
        {
            Parameters = new DataGridParameters();
            PagingAndOrderingInfo = new PagingAndOrderingInfo();
        }

        public DataGridFilters()
        {
            Parameters = new DataGridParameters();
            PagingAndOrderingInfo = new PagingAndOrderingInfo();
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


    }
}