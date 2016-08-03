using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public class GridRow
    {
        public GridRow()
        {
            Actions = new List<ActionItem>();
        }
        public string Id { get; set; }
        public List<GridCell> GridCells { get; set; }

        public List<ActionItem> Actions { get; set; }

        public RowType RowType { get; set; }

        public string DetailUrl { get; set; }

        public bool HasDetails { get; set; }

        public bool IsFooter { get; set; }


        public MvcHtmlString PregeneratedDetails { get; set; }

        public string RowData { get; set; }

        public string RowClasses { get; set; }

        public int? SummaryTextDisplayColumnIndex { get; set; }

        public string SummaryText { get; set; }
    }
}