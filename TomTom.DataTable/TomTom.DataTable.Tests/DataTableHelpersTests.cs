using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomTom.DataTable.Razor;

namespace TomTom.DataTable.Core.Tests
{

    [TestClass]
    public class DataTableHelpersTesters
    {
        private class TestModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        #region ExtractPropertiesAndGridAttributes tests
        [TestMethod]
        public void self_return_test()
        {
            var exp = DataTableHelpers.GetPropertyFromExpression<TestModel>(c => c, null, null);
            Assert.AreEqual("", exp.Name);
            Assert.AreEqual(typeof(TestModel), exp.Type);
            Assert.AreEqual(null, exp.DefaultValue);
        }

        [TestMethod]
        public void id_return_test()
        {
            var exp = DataTableHelpers.GetPropertyFromExpression<TestModel>(c => c.Id, null, null);
            Assert.AreEqual("Id", exp.Name);
            Assert.AreEqual(typeof(int), exp.Type);
        }

        [TestMethod]
        public void get_name_length_from_expression()
        {
            var exp = DataTableHelpers.GetPropertyFromExpression<TestModel>(c => c.Name.Length, null, null);
            Assert.AreEqual("Name.Length", exp.Name);
            Assert.AreEqual(typeof(int), exp.Type);
        }

        #endregion
    }
}
