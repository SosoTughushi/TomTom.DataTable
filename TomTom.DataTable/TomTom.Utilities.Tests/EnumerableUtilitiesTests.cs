using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TomTom.Utilities.Tests
{

    [TestClass]
    public class EnumerableUtilitiesTests
    {
        [TestMethod]
        public void empty_array_is_empty_and_is_not_not_empty()
        {
            Assert.IsTrue(new int[0].IsEmpty());
            Assert.IsFalse(new int[0].IsNotEmpty());

            Assert.IsTrue("".IsEmpty());
            Assert.IsFalse("".IsNotEmpty());
        }

        [TestMethod]
        public void null_is_empty()
        {
            Assert.IsTrue(((string)null).IsEmpty());
            Assert.IsFalse(((string)null).IsNotEmpty());
            Assert.IsFalse(((List<int>)null).IsNotEmpty());
        }
    }
}
