using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public class GridColumn
    {
        public string ColumnName { get; set; }
        public int? ColumnWidthPercent { get; set; }

        public Align Align { get; set; }

        public int Colspan { get; set; }

        public bool IsHidden { get; set; }
        public bool IsOrderable { get; set; }

        public int Id { get; set; }
    }
}