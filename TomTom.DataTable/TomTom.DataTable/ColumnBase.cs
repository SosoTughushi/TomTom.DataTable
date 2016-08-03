using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{
    public abstract class ColumnBase
    {
        protected ColumnBase()
        {
            Colspan = 1;
            CellData = o => "";
            VisibilityRule = o => true;
        }

        public Align Align { get; set; }

        public int? ColumnWidth { get; set; }

        public int ColumnWidthPercent
        {
            set { ColumnWidth = value; }
            // ReSharper disable once PossibleInvalidOperationException
            get { return ColumnWidth.Value; }
        }

        public string DisplayTemplateName { get; set; }

        public string HeaderTemplateName { get; set; }

        public bool HasFooter { get; set; }

        public Type EnumType { get; set; }

        public int Colspan { get; set; }

        public Func<object, string> CellData { get; set; }

        public string Title { get; set; }

        public Func<object, bool> VisibilityRule { get; protected set; }

        public bool IsHidden { get; set; }

        public bool IsFiltrable { get; set; }

        public IEnumerable<OperationType> AllowedFilterOperationTypes { get; set; }

        public int ColumnId { get; set; }

        public IEnumerable<SelectListItem> AwaibleValues { get; set; }

        public string FilterEditorTemplateName { get; set; }

        public string ExcellWorksheetName { get; set; }

        public bool CreateNullableFilter { get; set; }

        public object DefaultFilterValues { get; set; }

        public string SummaryText { get; set; }
        public bool IsSortable { get; set; }

        public Type PropertyType { get; set; }
        public abstract Func<object, int, MvcHtmlString> DrawFunc { get;  }

    }
}