using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FF = TomTom.Functional.Functional;

namespace TomTom.DataTable.Razor
{

    [Serializable]
    public class BaseViewModel
    {
        public virtual List<ActionItem> GetActions(UrlHelper helper, string tableId)
        {
            return new List<ActionItem>();
        }

        public virtual RowType GetRowType(string tableId)
        {
            return RowType.None;
        }
        public virtual string Detailurl(UrlHelper urlHelper, string tableId)
        {
            return "";
        }

        public virtual string GetPregeneratedDetailsTemplateName(string tableId)
        {
            return null;
        }
        public string Empty { get { return string.Empty; } }

        public virtual string GetRowData(string invokerId)
        {
            return string.Empty;
        }
        public virtual string GetRowClasses(string invokerId)
        {
            return string.Empty;
        }

        public static List<BaseViewModel<T>> CreateSource<T>(
            List<T> source,
            Func<T, string> getRowClasses = null,
            Func<T, string> getRowData = null,
            Func<T, string> pregeneratedDetailsTemplateName = null,
            Func<T, UrlHelper, string> detailsUrl = null,
            Func<T, RowType> getRowType = null,
            Func<T, UrlHelper, List<ActionItem>> getActions = null)
        {
            return source.Select(t =>
                new BaseViewModel<T>(
                    t,
                    getRowClasses,
                    getRowData,
                    pregeneratedDetailsTemplateName,
                    detailsUrl,
                    getRowType, getActions))
                    .ToList();
        }
    }

    /// <summary>
    /// with help of this class, there is no need anymore to have separate classes for every datagrid viewmodel.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [Serializable]
    public class BaseViewModel<T> : BaseViewModel
    {

        private readonly Func<T, string> _getRowClasses = null;
        private readonly Func<T, string> _getRowData = null;
        private readonly Func<T, string> _pregeneratedDetailsTemplateName = null;
        private readonly Func<T, UrlHelper, string> _detailsUrl = null;
        private readonly Func<T, RowType> _getRowType = null;
        private readonly Func<T, UrlHelper, List<ActionItem>> _getActions = null;
        public T Instance { get; private set; }

        public BaseViewModel(T t,
            Func<T, string> getRowClasses = null,
            Func<T, string> getRowData = null,
            Func<T, string> pregeneratedDetailsTemplateName = null,
            Func<T, UrlHelper, string> detailsUrl = null,
            Func<T, RowType> getRowType = null,
            Func<T, UrlHelper, List<ActionItem>> getActions = null
            )
        {
            _getRowClasses = getRowClasses;
            _getRowData = getRowData;
            _pregeneratedDetailsTemplateName = pregeneratedDetailsTemplateName;
            _detailsUrl = detailsUrl;
            _getRowType = getRowType;
            _getActions = getActions;
            Instance = t;
        }

        public override string Detailurl(UrlHelper urlHelper, string tableId)
        {
            return _detailsUrl != null ?
                _detailsUrl(Instance, urlHelper) :
                base.Detailurl(urlHelper, tableId);
        }

        public override List<ActionItem> GetActions(UrlHelper helper, string tableId)
        {
            return _getActions != null ?
            _getActions(Instance, helper) :
            base.GetActions(helper, tableId);
        }

        public override string GetRowClasses(string invokerId)
        {
            return _getRowClasses != null ?
            _getRowClasses(Instance)
            : base.GetRowClasses(invokerId);
        }

        public override string GetRowData(string invokerId)
        {
            return _getRowData != null ?
                _getRowData(Instance)
                : base.GetRowData(invokerId);
        }

        public override RowType GetRowType(string tableId)
        {
            return
                _getRowType?.Invoke(Instance) ?? base.GetRowType(tableId);
        }


        public override string GetPregeneratedDetailsTemplateName(string tableId)
        {
            return _pregeneratedDetailsTemplateName != null ?
                _pregeneratedDetailsTemplateName(Instance) :
                base.GetPregeneratedDetailsTemplateName(tableId);
        }


    }


}