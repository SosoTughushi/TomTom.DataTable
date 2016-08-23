using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF = TomTom.Functional.Functional;

namespace TomTom.DataTable.Razor.Tests
{
    [TestClass]
    public class BaseViewModelAdapterTests
    {


        private List<TestViewModel> _getModel()
        {
            int count = 0;
            var create = FF.Parse((string name, int nuberm) =>
                new TestViewModel()
                {
                    Name = name,
                    Number = nuberm,
                    Id = ++count
                });

            return new List<TestViewModel>()
                {
                    create("sleepy",3),
                    create("sun",1427),
                    create("martyr's",3421),
                    create("martyr's",12),
                    create("martyr's",4),
                    create("martyr's",-23),
                    create("martyr's",-109),
                    create("martyr's",0),
                    create("martyr's",4),
                    create("mantgra",33221),
                    create("Hiu",421),
                };
        }

        [TestMethod]
        public void getRowClasses_assert()
        {
            var viewModels = BaseViewModel.CreateSource(_getModel(),
                getRowClasses: (instance) => string.Format("required{0}", instance.Name));

            var expectedClasses = _getModel().Select(c => string.Format("required{0}", c.Name)).ToList();
            var actualClasses = viewModels.Select(c => c.GetRowClasses(null)).ToList();
            CollectionAssert.AreEqual(expectedClasses, actualClasses);
        }

        [TestMethod]
        public void getRowData_assert()
        {
            var viewModels = BaseViewModel.CreateSource(_getModel(),
                getRowData: (instance) => string.Format("data-id='{0}'", instance.Id));

            var expected = _getModel().Select(c => string.Format("data-id='{0}'", c.Id)).ToList();
            var actual = viewModels.Select(c => c.GetRowData(null)).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void getPregeneratedDetailsTemplateName_assert()
        {
            var models = _getModel();
            var viewModels = BaseViewModel.CreateSource(models,
                pregeneratedDetailsTemplateName: (instance) =>
                 "A.D.I.D.A.S");

            var details1 = viewModels[0].GetPregeneratedDetailsTemplateName("Korn");
            Assert.AreEqual("A.D.I.D.A.S", details1);
        }

        [TestMethod]
        public void detailsUrl_assert()
        {
            var models = _getModel();
            var viewModels = BaseViewModel.CreateSource(models,
                detailsUrl: (instance, helper) => string.Format("/Home/Index/{0}", instance.Id));

            var actual = viewModels.Select(c => c.Detailurl(null, null)).ToList();
            var expected = models.Select(instance => string.Format("/Home/Index/{0}", instance.Id)).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void getRowType_assert()
        {
            var models = _getModel();
            var viewModels = BaseViewModel.CreateSource(models,
                getRowType: (instance) => RowType.Bold);

            var areAllBold = viewModels.All(c => c.GetRowType("table") == RowType.Bold);

            Assert.IsTrue(areAllBold);
        }

        [TestMethod]
        public void getActions_assert()
        {
            var models = _getModel();
            var viewModels = BaseViewModel.CreateSource(models,
                getActions: (instance, helper) => new List<ActionItem>()
                {
                        new ActionItem()
                        {
                            Link = instance.Name
                        }
                });

            var actual = viewModels.Select(c => c.GetActions(null, null))
                .SelectMany(c => c.Select(t => t.Link)).ToList();
            var expected = models.Select(c => c.Name).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
