using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TomTom.DataTable.Razor;
using TomTom.DataTable;

namespace System.Web.Mvc
{
    public static class HtmlExtentions
    {

        /// <summary>
        /// Draws Data Grid
        /// </summary>
        /// <typeparam name="T">type of elements in Data Grid</typeparam>
        /// <param name="html">this</param>
        /// <param name="source">elements</param>
        /// <param name="parameters">parameters</param>
        /// <param name="columns">data grid columns</param>
        /// <returns></returns>
        public static MvcHtmlString DataTable<T>(this HtmlHelper html, IEnumerable<T> source,
            DataGridParameters parameters, params Column<T>[] columns) where T : BaseViewModel
        {
            var dataTable = new DataTable<T>(html, source, parameters, columns);
            return dataTable.Generate();
        }

        /// <summary>
        /// Draws Data Grid
        /// </summary>
        /// <typeparam name="T">type of elements in Data Grid</typeparam>
        /// <param name="html">this</param>
        /// <param name="source">elements</param>
        /// <param name="summeries">summary rows</param>
        /// <param name="parameters">parameters</param>
        /// <param name="columns">data grid columns</param>
        /// <returns></returns>
        public static MvcHtmlString DataTable<T>(this HtmlHelper html, IEnumerable<T> source, IEnumerable<Summary<T>> summeries,
            DataGridParameters parameters, params Column<T>[] columns) where T : BaseViewModel
        {
            var dataTable = new DataTable<T>(html, source, summeries, parameters, columns);
            return dataTable.Generate();
        }

        /// <summary>
        /// Draws Data Grid
        /// </summary>
        /// <typeparam name="TKey">grouping element type</typeparam>
        /// <typeparam name="T">type of elements in Data Grid</typeparam>
        /// <param name="html">this</param>
        /// <param name="source">source</param>
        /// <param name="parameters">parameters</param>
        /// <param name="columns">columns</param>
        /// <param name="groupingColumns">columns for grouping reprezentation</param>
        /// <param name="summeries">summary rows</param>
        /// <returns></returns>
        public static MvcHtmlString DataTableWithGrouping<TKey, T>(this HtmlHelper html,
            IEnumerable<GridGrouping<TKey, T>> source,
            DataGridParameters parameters, List<Column<T>> columns, List<Column<TKey>> groupingColumns, IEnumerable<Summary<T>> summeries = null)
            where T : BaseViewModel
            where TKey : BaseViewModel
        {
            var dataTable = new DataTable<TKey, T>(html, source, parameters, columns, groupingColumns, summeries);

            return dataTable.Generate();
        }

        /// <summary>
        /// draws data grid. data is retrived via ajax call (IDataProvider). 
        /// </summary>
        /// <typeparam name="T">type of Controller from where data must be retrived after ajax call</typeparam>
        /// <typeparam name="TBaseViewModel">type of containing elements</typeparam>
        /// <param name="html">this</param>
        /// <param name="parameters">parameters</param>
        /// <param name="columns">columns</param>
        /// <returns></returns>
        public static MvcHtmlString DataGridAjax<T, TBaseViewModel>(
            this HtmlHelper html, 
            DataGridParameters parameters, 
            params Column<TBaseViewModel>[] columns)

            where TBaseViewModel : BaseViewModel
            where T : IDataProvider<TBaseViewModel>
        {
            var ajaxDataTable = new AjaxDataTable<T, TBaseViewModel>(html, parameters, columns, CreateResolver());
            return ajaxDataTable.Generate();
        }

        /// <summary>
        /// draws data grid. data is retrived via ajax call (IDataProvider). controller also provides columns (IGridColumnProvider)
        /// </summary>
        /// <typeparam name="T">type of Controller from where data must be retrived after ajax call</typeparam>
        /// <typeparam name="TBaseViewModel">type of containing elements</typeparam>
        /// <param name="html">this</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public static MvcHtmlString DataGridAjax<T, TBaseViewModel>(
            this HtmlHelper html, 
            DataGridParameters parameters)

            where TBaseViewModel : BaseViewModel
            where T : IDataAndColumnProvider<TBaseViewModel>
        {
            var ajaxDataTable = new AjaxDataTable<T, TBaseViewModel>(html, parameters, new List<Column<TBaseViewModel>>(), CreateResolver());
            return ajaxDataTable.Generate();
        }

        /// <summary>
        /// Must Resolver ITableMetaDataStorage
        /// </summary>
        public static Func<IDependencyResolver> CreateResolver { set; private get; } 

    }
}