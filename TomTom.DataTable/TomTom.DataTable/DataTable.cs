using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;

namespace TomTom.DataTable.Razor
{

    public class DataTable<TBaseViewModel> where TBaseViewModel : BaseViewModel
    {
        private readonly List<Property<TBaseViewModel>> _properties;
        protected readonly HtmlHelper Html;
        private readonly IEnumerable<TBaseViewModel> _source;
        protected readonly DataGridParameters Parameters;
        private readonly IEnumerable<Summary<TBaseViewModel>> _summeries;

        #region constructors

        public DataTable(HtmlHelper html, IEnumerable<TBaseViewModel> source,
            DataGridParameters parameters, params Column<TBaseViewModel>[] columns)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");

            Html = html;
            _source = source;
            Parameters = parameters;
            _properties = DataTableHelpers.ExtractPropertiesAndGridAttributes(columns);
        }

        public DataTable(HtmlHelper html, IEnumerable<TBaseViewModel> source, IEnumerable<Summary<TBaseViewModel>> summeries,
            DataGridParameters parameters, params Column<TBaseViewModel>[] columns)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");
            if (summeries == null)
                throw new ArgumentNullException("summeries");
            if (!summeries.Any())
            {
                // ReSharper disable once LocalizableElement
                throw new ArgumentOutOfRangeException("summeries", "summeries is empty");
            }

            Html = html;
            _source = source;
            Parameters = parameters;
            _properties = DataTableHelpers.ExtractPropertiesAndGridAttributes(columns);
            _summeries = summeries;
        }



        protected DataTable(HtmlHelper html, DataGridParameters parameters)
        {
            Html = html;
            Parameters = parameters;
        }

        #endregion

        public MvcHtmlString Generate()
        {

            if (Parameters == null)
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("parameters");

            var gridModel = GetGridModel();

            return Html.Partial("DataTable/DataTable", gridModel ?? new GridModel());
        }

        protected virtual GridModel GetGridModel()
        {
            return DataTableHelpers.GetGridModel(Html, _source, Parameters, _properties, _summeries);
        }

    }


    public class DataTable<TKey, TBaseViewModel> : DataTable<TBaseViewModel>
        where TKey : BaseViewModel
        where TBaseViewModel : BaseViewModel
    {
        private readonly GridModel _gridModel;
        public DataTable(HtmlHelper html,
            IEnumerable<GridGrouping<TKey, TBaseViewModel>> source,
            DataGridParameters parameters, IEnumerable<Column<TBaseViewModel>> columns, IEnumerable<Column<TKey>> groupingColumns, IEnumerable<Summary<TBaseViewModel>> summeries = null)
            : base(html, parameters)
        {
            var properties = DataTableHelpers.ExtractPropertiesAndGridAttributes(columns);
            var keyColumns = DataTableHelpers.ExtractPropertiesAndGridAttributes(groupingColumns);

            var gridData = source.Select((grouping, j) =>
            {
                var groupedRows = grouping.Select((c, i) => DataTableHelpers.GetGridRow(html, parameters, properties, c, ((j + 1) * 100) + i));
                var rows = new List<GridRow>();
                if (grouping.Key != null)
                {
                    rows.Add(
                        DataTableHelpers.GetGridRow(html, parameters, keyColumns, grouping.Key, j));
                }
                rows.AddRange(groupedRows);
                return rows;
            }).SelectMany(c => c).ToList();


            var gridColumns = properties.Select(pr => DataTableHelpers.ExtractColumn(pr, html))
                .ToList();


            var footers = DataTableHelpers.GetFooters(html, properties, summeries);

            _gridModel = new GridModel
            {
                GridColumns = gridColumns,
                GridData = gridData,
                Parameters = parameters,
                Footers = footers
            };
        }

        protected override GridModel GetGridModel()
        {
            return _gridModel;
        }
    }


    public class GridGrouping<TKey, T> : IGrouping<TKey, T>
    {
        private readonly IEnumerable<T> _values;
        private readonly IGrouping<TKey, T> _igrouping;

        public GridGrouping(TKey key, IEnumerable<T> values)
        {
            Key = key;
            _values = values;
        }

        public GridGrouping(IGrouping<TKey, T> igrouping)
        {
            Key = igrouping.Key;
            this._igrouping = igrouping;
        }

        public TKey Key { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            if (_igrouping == null)
                return _values.GetEnumerator();
            return _igrouping.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}