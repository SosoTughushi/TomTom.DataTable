using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor.Ajax
{
    public static class HtmlExtentions
    {
        



        private static IDependencyResolver CreateResolver()
        {
            return DependencyResolver.Current;
        }

        /// <summary>
        /// draws data grid. data is retrived via ajax call (IDataProvider). 
        /// </summary>
        /// <typeparam name="T">type of Controller from where data must be retrived after ajax call</typeparam>
        /// <typeparam name="TBaseViewModel">type of containing elements</typeparam>
        /// <param name="html">this</param>
        /// <param name="parameters">parameters</param>
        /// <param name="columnFactory">helper column factory to reduce code size</param>
        /// <returns></returns>
        public static MvcHtmlString DataGridAjax<T, TBaseViewModel>(
            this HtmlHelper html,
            DataGridParameters parameters,
            Func<ColumnFactory<TBaseViewModel>, ColumnFactory<TBaseViewModel>> columnFactory)

            where TBaseViewModel : BaseViewModel
            where T : IDataProvider<TBaseViewModel>
        {
            var ajaxDataTable = new AjaxDataTable<T, TBaseViewModel>(html, parameters, columnFactory(new ColumnFactory<TBaseViewModel>()).GetColumns(), CreateResolver());
            return ajaxDataTable.Generate();
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
    }
}
