using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public class Summary<T>
    {
        public Summary(T instance, string name, bool isFooter = true, int? summaryTextColumnIndex = null)
        {
            this.Instance = instance;
            this.Name = name;
            this.IsFooter = isFooter;
            SummaryTextColumnIndex = summaryTextColumnIndex;
        }

        public T Instance { get; set; }

        public string Name { get; set; }

        public bool IsFooter { get; set; }

        public int? SummaryTextColumnIndex { get; set; }
    }
}