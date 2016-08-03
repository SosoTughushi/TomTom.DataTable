using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public class Column<T> : ColumnBase
    {
        //TODO: refactor into other library as extention method or something
        ///// <summary>
        ///// Only Used with AjaxDataGrid
        ///// </summary>
        ///// <param name="filter">Reprezentation of Filtrable Property. filter name must match property name of T.</param>
        ///// <param name="columnWidth">Percentage of Column Width</param>
        ///// <param name="displayTemplateName">When Specified, Uses Partial View Specified for rendering cell. Partial View Name must same as property type</param>
        ///// <param name="align">align of content in cell</param>
        ///// <param name="headerTemplateName">when specified, uses view specified for rendering header.</param>
        ///// <param name="hasFooter">When used with Summary parameters, specifies weather cell in Summary row has content </param>
        ///// <param name="isVisible">If Set to false, column will not be rendered at all</param>
        ///// <param name="enumType">if property is enum, specifies what enum type is property for translating.</param>
        ///// <param name="colspan">colspan</param>
        ///// <param name="cellData">html Attributes for cell container element</param>
        ///// <param name="defaultValue">value that is used when property value is null</param>
        ///// <param name="title">Title of Column. </param>
        ///// <param name="visibilityRule">column will be drawn according to rule specified</param>
        ///// <param name="isHidden">if set to true, column will be rendered, but it will be hidden</param>
        ///// <param name="filterEditorTemplateName">if Specified, Overrides Filter Reprezentation View. Editor Template model must be of Type FilterOption . Only used with AjaxDataGrid and when isFiltrable is set to true.</param>
        ///// <param name="excellWorksheetName">name of excel worksheet. Only used with AjaxDataGrid and when Grid has default export in excel</param>
        ///// <param name="createNullableFilter">if set to true, Filter For Non Nullable Types will be Nullable. For instance, If property type is int, it's filter type will be Nullable int</param>
        ///// <param name="defaultFilterValues"></param>
        ///// <param name="summaryText"></param>
        ///// <param name="drawFunc"></param>
        ///// <param name="overallVisibilityRule"></param>
        //public Column(Filter filter,
        //    int? columnWidth = null,
        //    string displayTemplateName = null,
        //    Align align = Align.Left,
        //    string headerTemplateName = null,
        //    bool hasFooter = false,
        //    bool isVisible = true,
        //    Type enumType = null,
        //    int colspan = 1,
        //    Func<T, string> cellData = null,
        //    object defaultValue = null,
        //    string title = null,
        //    Func<T, bool> visibilityRule = null,
        //    bool isHidden = false,
        //    string filterEditorTemplateName = "",
        //    string excellWorksheetName = null,
        //    bool createNullableFilter = false,
        //    object defaultFilterValues = null,
        //    string summaryText = null,
        //    bool isFiltrable = true,
        //    bool isSortable = true,
        //    Func<T, int, MvcHtmlString> drawFunc = null)
        //    : this(
        //        (Expression<Func<T, object>>)filter.Name.GetPropertyExpression<T>(true, true),
        //        columnWidth, displayTemplateName, align, headerTemplateName, hasFooter, isVisible, enumType, colspan, cellData, defaultValue ?? filter.DefautlValue, title, visibilityRule, isHidden, isFiltrable, filterEditorTemplateName,
        //        filter.AllowedFilterOperations, null, excellWorksheetName, createNullableFilter, summaryText, drawFunc, isSortable)
        //{
        //    ColumnId = filter.Id;
        //    if (filter.AllowedFilterValues != null)
        //    {
        //        AwaibleValues = filter.AllowedFilterValues.Select(f => new SelectListItem()
        //        {
        //            Text = f.Text,
        //            Value = f.Value
        //        }).ToList();
        //    }

        //    DefaultFilterValues = defaultFilterValues;
        //    PropertyType = Type.GetType("System." + filter.ValueType);
        //    if (filter.ValueIsNullable)
        //    {
        //        PropertyType = PropertyType.ConvertToNullableType();
        //    }
        //}



        /// <summary>
        /// creates instance of column
        /// </summary>
        /// <param name="property">Property Of Element In cell</param>
        /// <param name="columnWidth">Percentage of Column Width</param>
        /// <param name="displayTemplateName">When Specified, Uses Partial View Specified for rendering cell. Partial View Name must same as property type</param>
        /// <param name="align">align of content in cell</param>
        /// <param name="headerTemplateName">when specified, uses view specified for rendering header.</param>
        /// <param name="hasFooter">When used with Summary parameters, specifies weather cell in Summary row has content </param>
        /// <param name="isVisible">If Set to false, column will not be rendered at all</param>
        /// <param name="enumType">if property is enum, specifies what enum type is property for translating.</param>
        /// <param name="colspan">colspan</param>
        /// <param name="cellData">html Attributes for cell container element</param>
        /// <param name="defaultValue">value that is used when property value is null</param>
        /// <param name="title">Title of Column. </param>
        /// <param name="visibilityRule">column will be drawn according to rule specified</param>
        /// <param name="isHidden">if set to true, column will be rendered, but it will be hidden</param>
        /// <param name="isFiltrable">indicates if column is filtrable. Only used with AjaxDataGrid</param>
        /// <param name="filterEditorTemplateName">if Specified, Overrides Filter Reprezentation View. Editor Template model must be of Type FilterOption . Only used with AjaxDataGrid and when isFiltrable is set to true.</param>
        /// <param name="allowedFilterOperationTypes">if specified, indicates operations allowed for filtering this column. Only used with AjaxDataGrid</param>
        /// <param name="awaibleValues">available values for filter. Only used with AjaxDataGrid</param>
        /// <param name="excellWorksheetName">name of excel worksheet. Only used with AjaxDataGrid and when Grid has default export in excel</param>
        /// <param name="summaryText"></param>
        /// <param name="drawFunc"></param>
        public Column(Expression<Func<T, object>> property,
            int? columnWidth = null,
            string displayTemplateName = null,
            Align align = Align.Left,
            string headerTemplateName = null,
            bool hasFooter = false,
            bool isVisible = true,
            Type enumType = null,
            int colspan = 1,
            Func<T, string> cellData = null,
            object defaultValue = null,
            string title = null,
            Func<T, bool> visibilityRule = null,
            bool isHidden = false,
            bool isFiltrable = false,
            string filterEditorTemplateName = null,
            IEnumerable<OperationType> allowedFilterOperationTypes = null,
            IEnumerable<SelectListItem> awaibleValues = null
            , string excellWorksheetName = null,
            bool createNullableFilter = false,
            string summaryText = null,
            Func<T, int, MvcHtmlString> drawFunc = null,
            bool isSortable = false
            )
        {
            SummaryText = summaryText;
            DrawFunction = drawFunc;
            Property = property;
            ColumnWidth = columnWidth;
            DisplayTemplateName = displayTemplateName;
            Align = align;
            HeaderTemplateName = headerTemplateName;
            HasFooter = hasFooter;
            IsVisible = isVisible;
            EnumType = enumType;
            Colspan = colspan;
            if (cellData != null)
            {
                CellData = o => cellData((T)o);
            }
            else
            {
                CellData = o => "";
            }
            DefaultValue = defaultValue;
            Title = title;
            VisibilityRule = s => visibilityRule?.Invoke((T) s) ?? true;

            IsHidden = isHidden;
            IsFiltable = isFiltrable;
            AllowedFilterOperationTypes = allowedFilterOperationTypes;
            AwaibleValues = awaibleValues;
            FilterEditorTemplateName = filterEditorTemplateName;
            ExcellWorksheetName = excellWorksheetName;
            CreateNullableFilter = createNullableFilter;
            IsSortable = isSortable;
        }



        public Column<TChild> Cast<TChild>() where TChild : T
        {
            var body = Property.ExtractPropertyNameFromExpression()
                .GetPropertyExpression<TChild>(true, true);

            Expression<Func<TChild, object>> childExpression = (Expression<Func<TChild, object>>)body;

            return new Column<TChild>(
                childExpression,
                ColumnWidth,
                DisplayTemplateName,
                Align,
                HeaderTemplateName,
                HasFooter,
                IsVisible,
                EnumType,
                Colspan,
                 c => CellData(c),
                DefaultValue,
                Title,
                c => VisibilityRule(c),
                IsHidden)
            {
                ColumnId = ColumnId
            };
        }

        public Expression<Func<T, object>> Property { get; set; }
        
        public bool IsVisible { get; set; }

        public object DefaultValue { get; set; }

        public bool IsFiltable { get; set; }


        public Func<T, int, MvcHtmlString> DrawFunction { get; set; }

        public override Func<object, int, MvcHtmlString> DrawFunc
        {
            get
            {
                Func<object, int, MvcHtmlString> drawFunc = null;
                if (DrawFunction != null)
                {
                    drawFunc = (o, i) => DrawFunction((T)o, i);
                }
                return drawFunc;
            }
        }
        
    }
}