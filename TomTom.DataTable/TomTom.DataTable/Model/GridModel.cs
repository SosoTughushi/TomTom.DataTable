using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{
    public class GridModel

    {
        public GridModel()
        {
            FilterOptionCollection = new DataGridFilters();

        }
        public IEnumerable<GridColumn> GridColumns { get; set; }


        public List<GridRow> GridData { get; set; }

        public DataGridParameters Parameters { get; set; }


        public List<GridRow> Footers { get; set; }

        public bool IsAjaxTable { get; set; }

        public DataGridFilters FilterOptionCollection { get; set; }
    }
}