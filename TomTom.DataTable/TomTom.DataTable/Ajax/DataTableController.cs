using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public abstract class DataTableController : Controller, IDataProvider
    {
        private readonly IDataTableMetaDataStorage _metaDataStorage;

        protected DataTableController( IDataTableMetaDataStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");
            _metaDataStorage = storage;
        }

        private class FakeView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            {
                throw new InvalidOperationException();
            }
        }

        internal DataTableMetaData GetMetaData(DataGridFilters request)
        {
            return _metaDataStorage[request.TableId];
        }


        public virtual PartialViewResult ProvideDataGridData(DataGridFilters request)
        {
            var metaData = GetMetaData(request);

            var response =
                GetDataTableResponse(request, metaData);

            var viewContext = new ViewContext(ControllerContext, new FakeView(), ViewData, TempData, TextWriter.Null); //this is hack actually to create htmlHelper and without htmlHelper I wouldn't be able to render partial views
            var htmlHelper = new HtmlHelper(viewContext, new ViewPage());

            var gridModel = GetGridModel(metaData, response, htmlHelper);

            gridModel.FilterOptionCollection.PagingAndOrderingInfo = request.PagingAndOrderingInfo;
            gridModel.Parameters.HasPaging = metaData.Parameters.HasPaging;

            return PartialView("DataTable/DataTableBody", gridModel);
        }

        internal virtual GridModel GetGridModel(DataTableMetaData metaData, DataTableResponse response, HtmlHelper htmlHelper)
        {
            var generateListMethod = metaData.GetType()
                .GetMethod("GenerateList", new[] {response.Data.GetType(), typeof (HtmlHelper)});

            return (GridModel)
                generateListMethod.Invoke(metaData, new[] { response.Data, htmlHelper });
        }

        internal virtual DataTableResponse GetDataTableResponse(DataGridFilters request, DataTableMetaData metaData)
        {
            var getDataMethod = this.GetType()
                .GetMethod("GetData", new[] { typeof(DataGridFilters), metaData.Type });

            return (DataTableResponse)getDataMethod
                .Invoke(this, new object[] { request, null });
        }
    }

}