using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TomTom.DataTable.Razor.Ajax;

namespace TomTom.DataTable.Razor.Tests
{
    [TestClass]
    public class DataTableControllerTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void controller_argument_null_throws_exception()
        {
            var instance = new DummyController((IDataTableMetaDataStorage)null);
        }

        [TestMethod]
        public void controller_non_null_argument_is_initialized()
        {
            var storage = new Mock<IDataTableMetaDataStorage>();
            new DummyController(storage);
        }

        [TestMethod]
        public void GetMetaData_data_calls_meta_data_storage()
        {
            var controller = _createInstance();
            controller.GetMetaData(new DataGridFilters()
            {
                TableId = "TestId"
            });
            controller.Storage.Verify(c => c["TestId"]);
        }

        [TestMethod]
        public void GetDataTableResponse_calls_GetData()
        {
            var metaData = new Mock<DataTableMetaData>();
            metaData.Setup(c => c.Type).Returns(typeof(TestViewModel));

            var gridFilters = new DataGridFilters();
            var controllerMock = new Mock<DummyController>(new Mock<IDataTableMetaDataStorage>());

            controllerMock.Object.GetDataTableResponse(gridFilters, metaData.Object);

            controllerMock.Verify(c => c.GetData(gridFilters, null));
        }

        [TestMethod]
        public void GetGridModel_calls_MetaData_GenerateList()
        {
            var dependencyRespolver = new Mock<IDependencyResolver>();
            var metaDataMock = new Mock<DataTableMetaData<TestViewModel>>(dependencyRespolver.Object);
            var response = new DataTableResponse<TestViewModel>()
            {
                DataList = new List<TestViewModel>()
            };
            var htmlHelper = _createHtmlHelper(new ViewDataDictionary());

            _createInstance().GetGridModel(metaDataMock.Object, response, htmlHelper);

            metaDataMock.Verify(m => m.GenerateList(response.DataList, htmlHelper));

        }

        [TestMethod]
        public void ProvideDataGridData_returns_GetGridModel_result()
        {
            var metaDataStorage = new Mock<IDataTableMetaDataStorage>();
            metaDataStorage.Setup(m => m[null]).Returns(new DataTableMetaData<TestViewModel>(null));
            var controllerMock = new Mock<DummyController>(metaDataStorage.Object);
            var request = new DataGridFilters();
            var htmlHelper = _createHtmlHelper(new ViewDataDictionary());
            controllerMock.Setup(c => c.CreateHtmlHelper()).Returns(htmlHelper);
            var instance = controllerMock.Object;

            var result = instance.ProvideDataGridData(request);

            controllerMock.Verify(c => c.CreateHtmlHelper());

            controllerMock.Verify(c => c.GetMetaData(request), Times.Once);
            var metaData = instance.GetMetaData(request);

            controllerMock.Verify(c => c.GetDataTableResponse(request, metaData), Times.Once);
            var response = instance.GetDataTableResponse(request, metaData);

            controllerMock.Verify(c => c.GetGridModel(metaData, response, htmlHelper), Times.Once);
            var gridModel = instance.GetGridModel(metaData, response, htmlHelper);

            Assert.AreEqual(result.ViewName, "DataTable/DataTableBody");
            Assert.AreEqual(result.Model, gridModel);
        }



        private static HtmlHelper _createHtmlHelper(ViewDataDictionary vd)
        {
            Mock<ViewContext> mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object
                ),
                new Mock<IView>().Object,
                vd,
                new TempDataDictionary(),
                new StreamWriter(new MemoryStream())
            );

            Mock<IViewDataContainer> mockDataContainer = new Mock<IViewDataContainer>();
            mockDataContainer.Setup(c => c.ViewData).Returns(vd);

            return new HtmlHelper(mockViewContext.Object, mockDataContainer.Object);
        }

        private static DummyController _createInstance()
        {
            var storage = new Mock<IDataTableMetaDataStorage>();
            var controller = new DummyController(storage);
            return controller;
        }
    }

    public class DummyController : DataTableController, IDataProvider<TestViewModel>
    {
        public Mock<IDataTableMetaDataStorage> Storage { get; }
        public DummyController(Mock<IDataTableMetaDataStorage> storage) : this(storage.Object)
        {
            Storage = storage;
        }

        public DummyController(IDataTableMetaDataStorage storage) : base(storage)
        {

        }


        public virtual DataTableResponse<TestViewModel> GetData(DataGridFilters request, TestViewModel notUsed = null)
        {
            return null;
        }
    }
}
