using System.Collections.Generic;

namespace TomTom.DataTable
{
    public class DataGridParameters

    {
        public DataGridParameters()
        {
            HtmlAttributes = new Dictionary<string, object>();
            ExportButtons = new List<ActionItem>();
            TableData = new Dictionary<string, object>();
            ColorClass = "color-3";
        }
        /// <summary>
        /// indicates if grid row has details
        /// </summary>
        public bool HasDetails { get; set; }
        /// <summary>
        /// Html Attributes Of Data Grid Container Element
        /// </summary>
        public IDictionary<string, object> HtmlAttributes { get; set; }
        /// <summary>
        /// Html Attributes of Data Grid element
        /// </summary>
        public IDictionary<string, object> TableData { get; set; }
        /// <summary>
        /// Indicates if grid has paging. Only used with AjaxDataGrid
        /// </summary>
        public bool HasPaging { get; set; }
        /// <summary>
        /// indicates default value for grid rows per page. Only used with AjaxDataGrid
        /// </summary>
        public int? ItemsPerPage { get; set; }
        /// <summary>
        /// unique tableId. this id must be unique
        /// </summary>
        public string TableId { get; set; }
        /// <summary>
        /// title of data grid
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// indicates if table must be drawn with headers or it must be only data part
        /// </summary>
        public bool IsBodyOnly { get; set; }
        /// <summary>
        /// data grid color
        /// </summary>
        public string ColorClass { get; set; }
        /// <summary>
        /// if true, for each row one aditional hidden row is added for displaying message
        /// </summary>
        public bool HasMessageDisplay { get; set; }
        /// <summary>
        /// applied from ajaxGrid. Only used with AjaxDataGrid
        /// </summary>
        public System.Type DataTableControllerType { get; set; }
        /// <summary>
        /// applied from ajaxGrid. Only used with AjaxDataGrid
        /// </summary>
        public bool IsAjaxDataTable { get; set; }
        /// <summary>
        /// indicates if filters must be generated in header. Only used with AjaxDataGrid
        /// </summary>
        public bool IsFiltrable { get; set; }
        /// <summary>
        /// data grid actions
        /// </summary>
        public List<ActionItem> Actions { get; set; }
        /// <summary>
        /// action buttons for exporting grid results
        /// </summary>
        public List<ActionItem> ExportButtons { get; set; }
        /// <summary>
        /// When specified, overriders Default Filter Partial with partial view specified. Model type of View must be 'DataGridFilterDisplay'. Only used with AjaxDataGrid
        /// </summary>
        public string FilterPartialName { get; set; }
        /// <summary>
        /// When specified, sets name of exported excel worksheet name. Only used with AjaxDataGrid and if HasDefaultExcellExport is set to true
        /// </summary>
        public string ExcelDataWorksheetName { get; set; }
        /// <summary>
        /// Path of Excel Template File. Only used with AjaxDataGrid and if HasDefaultExcellExport is set to true
        /// </summary>
        public string ExcelTemplatePath { get; set; }
        /// <summary>
        /// Indicates if DataGrid has Default Export In Excel. Only used with AjaxDataGrid
        /// </summary>
        public bool HasDefaultExcellExport { get; set; }
        /// <summary>
        /// When Specified, Overrides Default Editor Template For Generating Items Per Page Control. Only used with AjaxDataGrid
        /// </summary>
        public string ItemsPerPageEditorTemplateName { get; set; }
        /// <summary>
        /// Indicates if Filters has Items Per Page Filter
        /// </summary>
        public bool HasItemsPerPageFilter { get; set; }

        public string ExcelFileName { get; set; }
    }
}