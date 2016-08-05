using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public interface IDataProvider : IController
    {
        PartialViewResult ProvideDataGridData(DataGridFilters request);
    }

    public interface IGridColumnProvider<TModel> : IController
        where TModel : BaseViewModel
    {
        IEnumerable<Column<TModel>> GetColumns(string tableId);
    }

    public interface IDataProvider<TModel> : IController
        where TModel : BaseViewModel
    {
        DataTableResponse<TModel> GetData(DataGridFilters request, TModel notUsed = null);
    }

    public interface IDataAndColumnProvider<TModel> : IDataProvider<TModel>, IGridColumnProvider<TModel>
        where TModel : BaseViewModel
    {

    }

}