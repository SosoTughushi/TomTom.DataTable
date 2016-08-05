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

        protected DataTableController(IDataTableMetaDataStorage storage)
        {
            _metaDataStorage = storage;
        }

        private class FakeView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            {
                throw new InvalidOperationException();
            }
        }


        public virtual PartialViewResult ProvideDataGridData(DataGridFilters request)
        {
            var metaData = _metaDataStorage[request.TableId];

            var response =
                (DataTableResponse)this.GetType()
                    .GetMethod("GetData", new[] { typeof(DataGridFilters), metaData.Type })
                    .Invoke(this, new object[] { request, null });

            var viewContext = new ViewContext(ControllerContext, new FakeView(), ViewData, TempData, TextWriter.Null); //this is hack actually to create htmlHelper and without htmlHelper I wouldn't be able to render partial views
            var htmlHelper = new HtmlHelper(viewContext, new ViewPage());

            var data = (GridModel)metaData.GetType()
                .GetMethod("GenerateList", new[] { response.Data.GetType(), typeof(HtmlHelper) })
                .Invoke(metaData, new[] { response.Data, htmlHelper });

            data.FilterOptionCollection.Offset = request.Offset;
            data.FilterOptionCollection.ItemsPerPage = request.ItemsPerPage;
            data.FilterOptionCollection.CurrentPage = request.CurrentPage;
            data.FilterOptionCollection.TotalRecords = response.TotalRecords;
            data.FilterOptionCollection.OrderingColumnIndex = request.OrderingColumnIndex;
            data.FilterOptionCollection.IsSortDirectionAscending = request.IsSortDirectionAscending;
            data.Parameters.HasPaging = metaData.Parameters.HasPaging;

            return PartialView("DataTable/DataTableBody", data);
        }

        //public virtual ActionResult GenerateDefaultExcel(DataGridFilters request)
        //{
        //    request.ItemsPerPage = 0;
        //    var metaData = _metaDataStorage[request.TableId];

        //    var response = (DataTableResponse)GetType().GetMethod("GetData", new[] { typeof(DataGridFilters), metaData.Type })
        //            .Invoke(this, new object[] { request, null });

        //    byte[] excel = metaData.GenerateExcel(response.Data);

        //    return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", (metaData.Parameters.ExcelFileName ?? request.TableId) + ".xlsx");
        //}

    }

}