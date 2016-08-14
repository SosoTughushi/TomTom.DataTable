using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public class DataGridFilterDisplay
    {
        public MvcHtmlString SubmitButton { get; private set; }
        public MvcHtmlString ItemsPerPage { get; private set; }

        private readonly List<Tuple<FilterOption, MvcHtmlString>> _filters;
        public DataGridFilterDisplay(List<Tuple<FilterOption, MvcHtmlString>> filters, MvcHtmlString submitButton, MvcHtmlString itemsPerPage)
        {
            SubmitButton = submitButton;
            ItemsPerPage = itemsPerPage;
            _filters = filters;
        }


        public IEnumerator<Tuple<FilterOption, MvcHtmlString>> GetEnumerator()
        {
            return _filters.GetEnumerator();
        }

        public MvcHtmlString this[string name]
        {
            get
            {
                var item = _filters.FirstOrDefault(f => f.Item1.PropName == name);
                if (item != null)
                    return item.Item2;
                return MvcHtmlString.Create(name);
            }
        }


        public MvcHtmlString this[int id]
        {
            get { return _filters.First(f => f.Item1.Id == id).Item2; }
        }
    }
}