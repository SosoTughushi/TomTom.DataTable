using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TomTom.DataTable.Razor
{

    public static class DataTableHelpers
    {
        public static List<Property<T>> ExtractPropertiesAndGridAttributes<T>(IEnumerable<Column<T>> columns) where T : BaseViewModel
        {
            return columns.Where(c => c != null && c.IsVisible)
                .Select(c =>
                {
                    var ret = GetPropertyFromExpression(c.Property, c.DefaultValue, c.PropertyType);
                    ret.GridColumnAttribute = c;
                    ret.ColumnId = c.ColumnId;
                    return ret;
                }
                ).ToList();
        }

        public static Property<T> GetPropertyFromExpression<T>(Expression<Func<T, object>> expression, object defaultValue, Type propertyType)
        {
            PropertyInfo propertyInfo;
            if (expression.Body is ParameterExpression)
                propertyInfo = null;
            else if (expression.Body is MemberExpression)
                propertyInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
            else
                propertyInfo = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member as PropertyInfo;

            return new Property<T>()
            {
                ObjectGetter = expression.Compile(),
                DisplayAttribute = propertyInfo != null ? GetDisplayAttribute(propertyInfo) : new DisplayAttribute(),
                DisplayFormatAttribute = propertyInfo != null ? propertyInfo.GetCustomAttribute<DisplayFormatAttribute>() : null,
                Name = expression.ExtractPropertyNameFromExpression(),
                Type = propertyType ?? expression.GetPropertyTypeFromExpression(),
                DefaultValue = defaultValue
            };
        }

        public static GridModel GetGridModel<T>(HtmlHelper html, IEnumerable<T> source, DataGridParameters parameters, List<Property<T>> properties, IEnumerable<Summary<T>> summeries = null)
            where T : BaseViewModel
        {

            var gridColumns = properties.Select(pr => ExtractColumn(pr, html))
                .ToList();

            var gridData = new List<GridRow>();
            int i = -1;
            if (source != null)
                foreach (var item in source)
                {
                    i++;
                    var gridRow = GetGridRow(html, parameters, properties, item, i);
                    gridData.Add(gridRow);
                }

            var footers = GetFooters(html, properties, summeries);

            var gridModel = new GridModel
            {
                GridColumns = gridColumns,
                GridData = gridData,
                Parameters = parameters,
                Footers = footers
            };
            return gridModel;
        }

        public static DisplayAttribute GetDisplayAttribute(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return new DisplayAttribute();
            var display = propertyInfo.GetCustomAttribute<DisplayAttribute>() ?? new DisplayAttribute();
            return display;
        }

        public static GridColumn ExtractColumn<T>(Property<T> property, HtmlHelper helper)
        {
            var display = property.DisplayAttribute;
            string name = null;
            if (property.GridColumnAttribute.HeaderTemplateName != null)
            {
                name = helper.Partial(string.Format("DisplayTemplates/{0}", property.GridColumnAttribute.HeaderTemplateName), 0).ToString();
            }
            var gridColumn = new GridColumn
            {
                ColumnName = name ?? property.GridColumnAttribute.Title ?? (display != null ? display.GetName() : ""),
                ColumnWidthPercent = property.GridColumnAttribute.ColumnWidth,
                Align = property.GridColumnAttribute.Align,
                Colspan = property.GridColumnAttribute.Colspan,
                IsHidden = property.GridColumnAttribute.IsHidden,
                IsOrderable = property.GridColumnAttribute.IsSortable,
                Id = property.GridColumnAttribute.ColumnId
            };
            return gridColumn;
        }

        private static readonly Random Random = new Random();

        public static GridRow GetGridRow<T>(HtmlHelper html, DataGridParameters parameters, IEnumerable<Property<T>> properties, T item, int i)
            where T : BaseViewModel
        {
            var index = i;
            var item1 = item;
            var gridCells = properties
                .Select(property => ExtractGridCell(property, item1, html, index))
                .ToList();
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var gridRow = new GridRow()
            {
                GridCells = gridCells,
                Actions = item1.GetActions(urlHelper,parameters.TableId),
                RowType = item1.GetRowType(parameters.TableId),
                DetailUrl = parameters.HasDetails
                    ? item.Detailurl(urlHelper, parameters.TableId)
                    : null,
                HasDetails = parameters.HasDetails,
                Id = parameters.TableId + index + Random.Next(),
                RowData = item1.GetRowData(parameters.TableId),
                RowClasses = item1.GetRowClasses(parameters.TableId)
            };
            if (item1.GetPregeneratedDetailsTemplateName(parameters.TableId) != null)
            {
                gridRow.PregeneratedDetails = html.Partial(item1.GetPregeneratedDetailsTemplateName(parameters.TableId), item1);
            }
            return gridRow;
        }

        public static GridCell ExtractGridCell<T>(Property<T> property, T item, HtmlHelper html, int index)
        {
            var value = property.ObjectGetter(item);

            var gridCell = new GridCell()
            {
                Colspan = property.GridColumnAttribute.Colspan,
                CellData = property.GridColumnAttribute.CellData(item),
                DataTitle = property.GridColumnAttribute.Title ?? property.DisplayAttribute.GetName(),
                PropertyName = property.Name,
                IsCellVisible = property.GridColumnAttribute.VisibilityRule(item),
                SummaryText = property.GridColumnAttribute.SummaryText
            };
            if (property.GridColumnAttribute.DrawFunc != null)
            {
                gridCell.Value = property.GridColumnAttribute.DrawFunc(item, index);
                return gridCell;
            }
            if (!string.IsNullOrEmpty(property.GridColumnAttribute.DisplayTemplateName))
            {
                gridCell.Value = html.Partial(string.Format("DisplayTemplates/{0}", property.GridColumnAttribute.DisplayTemplateName), value, new ViewDataDictionary());

                return gridCell;
            }
            gridCell.Value = value;
            if (property.GridColumnAttribute.EnumType != null)
            {
                gridCell.Value = ((Enum)Enum.ToObject(property.GridColumnAttribute.EnumType, value));
            }

            gridCell.DisplayFormatAttribute = property.DisplayFormatAttribute;
            return gridCell;
        }

        public static List<GridRow> GetFooters<T>(HtmlHelper html, List<Property<T>> properties, IEnumerable<Summary<T>> summeries) where T : BaseViewModel
        {
            var footers = new List<GridRow>();
            if (summeries != null)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var summery in summeries)
                {
                    var item1 = summery.Instance;
                    var gridCells = properties
                        .Select((property, i) =>
                            property.GridColumnAttribute.HasFooter
                                ? ExtractGridCell(property, item1, html, i)
                                : new GridCell() { Value = "" }
                        )
                        .ToList();

                    footers.Add(new GridRow()
                    {
                        GridCells = gridCells,
                        IsFooter = summery.IsFooter,
                        SummaryTextDisplayColumnIndex = summery.SummaryTextColumnIndex,
                        SummaryText = summery.Name
                    });
                }
            }
            return footers;
        }
    }
}