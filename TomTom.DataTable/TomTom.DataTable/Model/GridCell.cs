using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public class GridCell
    {
        public object Value { get; set; }

        public GridCell()
        {
            IsCellVisible = true;
        }

        public System.ComponentModel.DataAnnotations.DisplayFormatAttribute DisplayFormatAttribute { get; set; }

        public string DataTitle { get; set; }

        public int Colspan { get; set; }

        public string CellData { get; set; }

        public string PropertyName { get; set; }

        public bool IsCellVisible { get; set; }

        public bool IsHidden { get; set; }

        public string SummaryText { get; set; }
    }
}