using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TomTom.DataTable.Razor.Tests
{

    [TestClass]
    public class SummaryTests
    {
        [TestMethod]
        public void constructor_initialization_tests()
        {
            var vm = new TestViewModel();
            var summary = new Summary<TestViewModel>(vm, "SomeName", true, 15);
            Assert.AreEqual(vm, summary.Instance);
            Assert.AreEqual("SomeName", summary.Name);
            Assert.AreEqual(true, summary.IsFooter);
            Assert.AreEqual(15, summary.SummaryTextColumnIndex);
        }
    }
}
