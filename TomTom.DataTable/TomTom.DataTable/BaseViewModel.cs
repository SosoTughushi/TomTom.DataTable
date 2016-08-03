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
        public virtual List<ActionItem> GetActions(UrlHelper helper)
        {
            return new List<ActionItem>();
        }

        public virtual RowType GetRowType()
        {
            return RowType.None;
        }


        public virtual string Detailurl(UrlHelper urlHelper, string invokerId)
        {
            return "";
        }

        public virtual string PredegeneratedDetailsTemplateName { get { return null; } }

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
            Func<T, string, string> getRowClasses = null,
            Func<T, string, string> getRowData = null,
            Func<T, string> pregeneratedDetailsTemplateName = null,
            Func<T, UrlHelper, string, string> detailsUrl = null,
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
        private readonly Func<UrlHelper, Func<string, string>> _detailsUrl;
        private readonly Func<UrlHelper, List<ActionItem>> _getActions;
        private readonly Func<string, string> _getRowClasses;
        private readonly Func<string, string> _getRowData;
        private readonly Func<RowType> _getRowType;
        private readonly Func<string> _pregeneratedDetailsTemplateName;

        public T Instance { get; private set; }

        public BaseViewModel(T t,
            Func<T, string, string> getRowClasses = null,
            Func<T, string, string> getRowData = null,
            Func<T, string> pregeneratedDetailsTemplateName = null,
            Func<T, UrlHelper, string, string> detailsUrl = null,
            Func<T, RowType> getRowType = null,
            Func<T, UrlHelper, List<ActionItem>> getActions = null
            )
        {
            Instance = t;

            if (getRowClasses != null)
                _getRowClasses = FF.Curry(getRowClasses)(t);

            if (getRowData != null)
                _getRowData = FF.Curry(getRowData)(t);

            if (pregeneratedDetailsTemplateName != null)
                _pregeneratedDetailsTemplateName = () => pregeneratedDetailsTemplateName(t);

            if (detailsUrl != null)
                _detailsUrl = FF.Curry(detailsUrl)(t);

            if (getRowType != null)
                _getRowType = () => getRowType(t);

            if (getActions != null)
                _getActions = FF.Curry(getActions)(t);
        }

        public override string Detailurl(UrlHelper urlHelper, string invokerId)
        {
            return _detailsUrl != null ?
                _detailsUrl(urlHelper)(invokerId) :
                base.Detailurl(urlHelper, invokerId);
        }

        public override List<ActionItem> GetActions(UrlHelper helper)
        {
            return _getActions != null ?
            _getActions(helper) :
            base.GetActions(helper);
        }

        public override string GetRowClasses(string invokerId)
        {
            return _getRowClasses != null ?
            _getRowClasses(invokerId)
            : base.GetRowClasses(invokerId);
        }

        public override string GetRowData(string invokerId)
        {
            return _getRowClasses != null ?
                _getRowClasses(invokerId)
                : base.GetRowData(invokerId);
        }

        public override RowType GetRowType()
        {
            return
                _getRowType != null ?
            _getRowType() : base.GetRowType();
        }


        public override string PredegeneratedDetailsTemplateName
        {
            get
            {
                return _pregeneratedDetailsTemplateName != null ?
                    _pregeneratedDetailsTemplateName() :
                    base.PredegeneratedDetailsTemplateName;
            }
        }


    }


}