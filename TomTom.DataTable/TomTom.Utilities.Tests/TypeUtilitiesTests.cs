using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomTom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomTom.Utilities;

namespace TomTom.Tests
{
    [TestClass()]
    public class TypeUtilitiesTests
    {
        private enum SomeEnum { First, Second }
        private struct SomeStruct { }

        #region IsNullable
        [TestMethod]
        public void primitive_types_should_not_be_nullable()
        {
            Assert.IsFalse(typeof(int).IsNullable());
            Assert.IsFalse(typeof(char).IsNullable());
            Assert.IsFalse(typeof(decimal).IsNullable());
            Assert.IsFalse(typeof(bool).IsNullable());
            Assert.IsFalse(typeof(float).IsNullable());
            Assert.IsFalse(typeof(double).IsNullable());
        }

        [TestMethod]
        public void enums_end_structs_are_not_nullable()
        {
            Assert.IsFalse(typeof(SomeEnum).IsNullable());
            Assert.IsFalse(typeof(SomeStruct).IsNullable());
        }

        [TestMethod]
        public void string_is_nullable()
        {
            Assert.IsTrue(typeof(string).IsNullable());
        }

        [TestMethod]
        public void nullable_primitive_types_are_nullable()
        {
            Assert.IsTrue(typeof(int?).IsNullable());
            Assert.IsTrue(typeof(char?).IsNullable());
            Assert.IsTrue(typeof(decimal?).IsNullable());
            Assert.IsTrue(typeof(bool?).IsNullable());
            Assert.IsTrue(typeof(float?).IsNullable());
            Assert.IsTrue(typeof(double?).IsNullable());
        }

        [TestMethod]
        public void classes_are_nullable()
        {
            Assert.IsTrue(this.GetType().IsNullable());
            Assert.IsTrue(typeof(object).IsNullable());
            Assert.IsTrue(typeof(List<int>).IsNullable());
        }
        #endregion

        #region ConvertToObject
        [TestMethod]
        public void converting_empty_string_to_primitive_type_is_null()
        {
            Assert.AreEqual(null, "".ConvertToObject(typeof(int)));
        }

        [TestMethod]
        public void converting_emptu_string_to_array_type_is_empty_array()
        {
            var val = (int?[])"".ConvertToObject(typeof(int?[]));
            Assert.AreEqual(null, val[0]);
            Assert.AreEqual(1, val.Length);
        }

        [TestMethod]
        public void convert_1_to_int_is_1()
        {
            var val = "1".ConvertToObject(typeof(int));
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void convert_1_to_nullable_int_is_1()
        {
            var val = "1".ConvertToObject(typeof(int?));
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void convert_1_to_arr_of_int_is_1()
        {
            var val = (int[])"1".ConvertToObject(typeof(int[]));
            Assert.AreEqual(1, val[0]);
        }

        [TestMethod]
        public void convert_1_to_nullable_arr_of_int_is_1()
        {
            var val = (int?[])"1".ConvertToObject(typeof(int?[]));
            Assert.AreEqual(1, val[0]);
        }

        [TestMethod]
        public void convert_commas_to_array_causes_creation_of_multi_elemental_array()
        {
            Func<string, int[]> createArr = s => (int[])s.ConvertToObject(typeof(int[]));
            Assert.AreEqual(2, createArr(",").Length);
            Assert.AreEqual(3, createArr(",,").Length);
            Assert.AreEqual(2, createArr(",2,")[1]);
            Assert.AreEqual(4, createArr(",2,,4")[3]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void invalid_cast_throws_invalid_cast_exception()
        {
            "ffs".ConvertToObject(typeof(int?));
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void invalid_input_for_array_throws_exception()
        {
            "1,null,3".ConvertToObject(typeof(int?[]));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void creating_of_list_is_not_supported()
        {
            var resp = "1,2,3".ConvertToObject(typeof(List<int>));
        }

        [TestMethod]
        public void creation_of_list_when_type_provided_is_supported()
        {
            StringUtilities.AddConverter(typeof(List<int>), s =>
                s.Split(',').Select(int.Parse).ToList());
            var resp = (List<int>)"1,2,15".ConvertToObject(typeof(List<int>));
            Assert.AreEqual(1, resp[0]);
            Assert.AreEqual(2, resp[1]);
            Assert.AreEqual(15, resp[2]);
        }

        #endregion

    }
}