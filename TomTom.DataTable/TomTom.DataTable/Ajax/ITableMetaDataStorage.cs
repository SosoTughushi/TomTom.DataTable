using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public interface IDataTableMetaDataStorage
    {
        DataTableMetaData this[string tableId] { get; set; }
    }

    public class DataTablaSessionMetaDataStorage : IDataTableMetaDataStorage
    {
        public DataTableMetaData this[string tableId]
        {
            get { return (DataTableMetaData)HttpContext.Current.Session[tableId]; }
            set { HttpContext.Current.Session[tableId] = value; }
        }
    }
}