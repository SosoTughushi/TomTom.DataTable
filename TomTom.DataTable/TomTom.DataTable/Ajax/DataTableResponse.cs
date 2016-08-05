using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public abstract class DataTableResponse
    {
        public int TotalRecords { get; set; }
        public abstract object Data { get; }
    }

    public class DataTableResponse<TModel> : DataTableResponse
    {
        public List<TModel> DataList { get; set; }

        public override object Data
        {
            get { return DataList; }
        }
    }
}