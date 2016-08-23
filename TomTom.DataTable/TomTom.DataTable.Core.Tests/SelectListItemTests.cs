using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TomTom.DataTable.Core.Tests
{
    [TestClass]
    public class SelectListItemTests
    {
        [TestMethod]
        public void constructor_with_parameters_should_initialize_properties()
        {
            var item = new SelectListItem("Text", "Value");
            Assert.AreEqual("Text", item.Text);
            Assert.AreEqual("Value", item.Value);
        }
    }
}
