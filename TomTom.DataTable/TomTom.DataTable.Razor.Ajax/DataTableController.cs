using System;
using System.IO;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor.Ajax
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

        public DataTableMetaData GetMetaData(DataGridFilters request)
        {
            return _metaDataStorage[request.TableId];
        }


        public PartialViewResult ProvideDataGridData(DataGridFilters request)
        {
            var metaData = GetMetaData(request);

            var response =
                GetDataTableResponse(request, metaData);

            var htmlHelper = CreateHtmlHelper();

            var gridModel = GetGridModel(metaData, response, htmlHelper);

            gridModel.FilterOptionCollection.PagingAndOrderingInfo = request.PagingAndOrderingInfo;
            request.PagingAndOrderingInfo.TableId = request.TableId;
            gridModel.Parameters.HasPaging = metaData.Parameters.HasPaging;
            gridModel.FilterOptionCollection.PagingAndOrderingInfo.TotalRecords = response.TotalRecords;

            return PartialView("DataTable/DataTableBody", gridModel);
        }

        public virtual HtmlHelper CreateHtmlHelper()
        {
            var viewContext = new ViewContext(ControllerContext, new FakeView(), ViewData, TempData, TextWriter.Null);
                //this is hack actually to create htmlHelper and without htmlHelper I wouldn't be able to render partial views
            var htmlHelper = new HtmlHelper(viewContext, new ViewPage());
            return htmlHelper;
        }

        public virtual GridModel GetGridModel(DataTableMetaData metaData, DataTableResponse response, HtmlHelper htmlHelper)
        {
            var generateListMethod = metaData.GetType()
                .GetMethod("GenerateList", new[] {response.Data.GetType(), typeof (HtmlHelper)});

            return (GridModel)
                generateListMethod.Invoke(metaData, new[] { response.Data, htmlHelper });
        }

        public virtual DataTableResponse GetDataTableResponse(DataGridFilters request, DataTableMetaData metaData)
        {
            var getDataMethod = this.GetType()
                .GetMethod("GetData", new[] { typeof(DataGridFilters), metaData.Type });

            return (DataTableResponse)getDataMethod
                .Invoke(this, new object[] { request, null });
        }
    }

}