using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TomTom.DataTable.Core.Tests
{

    [TestClass]
    public class FilterOptionTests
    {
        #region Value Property Tests
        [TestMethod]
        public void initialization_with_null_causes_empty_list_of_val()
        {
            var option = new TomTom.DataTable.FilterOption<int>()
            {
                Value = null
            };
            CollectionAssert.AreEqual(new List<int>(), option.Val);
        }

        [TestMethod]
        public void if_value_is_string_Val_consists_of_one_element()
        {
            var option = new FilterOption<string>()
            {
                Value = "Hey,, Ho"
            };
            Assert.AreEqual("Hey,, Ho", option.Val[0]);
        }

        [TestMethod]
        public void if_value_is_not_string_it_is_assigned_to_val_using_change_type()
        {
            int counter = 0;
            var option = new FilterOption<int>(val =>
            {
                counter++;
                return int.Parse(val) + 4;
            })
            {
                Value = "1,3,2"
            };
            Assert.AreEqual(3, counter);//Verifiing that method has called
            CollectionAssert.AreEqual(new[] { 5, 7, 6 }, option.Val);
        }

        [TestMethod]
        public void get_value_joins_val_list_with_commas()
        {
            var option = new FilterOption<int>
            {
                Val = new List<int> { 3, 2, 1 }
            };
            Assert.AreEqual("3,2,1", option.Value);
        }

        [TestMethod]
        public void value_type_is_array_when_between_or_in()
        {
            var option = new FilterOption<decimal>()
            {
                OperationType = OperationType.Between
            };
            Assert.AreEqual(typeof(decimal[]), option.ValueType);
            option.OperationType = OperationType.In;
            Assert.AreEqual(typeof(decimal[]), option.ValueType);

            option.OperationType = OperationType.Empty;
            Assert.AreEqual(typeof(decimal), option.ValueType);

            option.OperationType = OperationType.Equals;
            Assert.AreEqual(typeof(decimal), option.ValueType);
        }

        #endregion
        

        #region GenerateFilter Tests (Extention from ExpressionTreeUtilities)

        //-----------------Strings---------------
        [TestMethod]
        public void empty_operation_type_returns_null()
        {
            var filterOption = new FilterOption<string>()
            {
                PropName = "Name",
                OperationType = OperationType.Empty
            };
            var tree = filterOption.GenerateFilter<TestViewModel>();
            Assert.AreEqual(null, tree);
        }

        [TestMethod]
        public void equals_operation_type_returns_tree()
        {
            var filterOption = new FilterOption<string>()
            {
                PropName = "Name",
                OperationType = OperationType.Equals,
                Value = "Hi"
            };

            var tree = filterOption.GenerateFilter<TestViewModel>();
            Assert.IsTrue(tree.Compile()(new TestViewModel { Name = "Hi" }));
            Assert.IsFalse(tree.Compile()(new TestViewModel { Name = "Hitr" }));
        }

        [TestMethod]
        public void contains_operation_type_returns_tree()
        {
            var filterOption = new FilterOption<string>()
            {
                PropName = "Name",
                OperationType = OperationType.Contains,
                Value = "H"
            };

            var tree = filterOption.GenerateFilter<TestViewModel>();
            Assert.IsTrue(tree.Compile()(new TestViewModel { Name = "afgHan" }));
            Assert.IsFalse(tree.Compile()(new TestViewModel { Name = "the dead weather" }));
        }
        [TestMethod]
        public void in_operation_type_returns_tree()
        {
            var filterOption = new FilterOption<string>
            {
                PropName = "Name",
                OperationType = OperationType.In,
                Value = "Superstar,Heaven on their minds,I Only wanna know,Yea"
            };
            var tree = filterOption.GenerateFilter<TestViewModel>();
            Assert.IsTrue(tree.Compile()(new TestViewModel { Name = "I Only wanna know" }));
            Assert.IsFalse(tree.Compile()(new TestViewModel { Name = "minds,I On" }));

        }

        [TestMethod]
        public void not_sypported_string_tests()
        {
            assertStringInvalidOperation(OperationType.MoreThen);
            assertStringInvalidOperation(OperationType.MoreOrEquealsThen);
            assertStringInvalidOperation(OperationType.LessThen);
            assertStringInvalidOperation(OperationType.LessOrEquealsThen);
            assertStringInvalidOperation(OperationType.Between);
        }

        private void assertStringInvalidOperation(OperationType opType)
        {
            _assertInvalidOperation<string, InvalidOperationException>(opType, "H");
        }

        private void _assertInvalidOperation<T, InnerExceptionType>(OperationType opType, string value) where InnerExceptionType : Exception
        {
            var filterOption = new FilterOption<T>
            {
                PropName = "Id",
                OperationType = opType,
                Value = value
            };
            Action act = () =>
                 filterOption.GenerateFilter<SomeGenericClass<T>>();

            act.ShouldThrow<Exception>()
                .WithInnerException<InnerExceptionType>();
        }

        [TestMethod]
        public void numeric_type_asserts()
        {
            _assertNumericType<int>();
            _assertNumericType<decimal>();
            _assertNumericType<float>();
            _assertNumericType<double>();
        }

        [TestMethod]
        public void assert_nullable_numbers()
        {
            AssertNullable<int>();
            AssertNullable<double>();
            AssertNullable<decimal>();
            AssertNullable<float>();
        }

        private void AssertNullable<T>() where T : struct
        {
            Func<int, T?> _convert =
                input => (T)Convert.ChangeType(input, typeof(T));

            optionTypeAsserts<T?>(OperationType.Equals, "1", null, false);
            optionTypeAsserts<T?>(OperationType.MoreThen, "1", null, false);
            optionTypeAsserts<T?>(OperationType.MoreOrEquealsThen, "1", null, false);
            optionTypeAsserts<T?>(OperationType.LessOrEquealsThen, "1", null, false);
            optionTypeAsserts<T?>(OperationType.LessThen, "1", null, false);
            optionTypeAsserts<T?>(OperationType.Between, "1", null, false);
            optionTypeAsserts<T?>(OperationType.In, "1", null, false);

            optionTypeAsserts(OperationType.Between, ",3", _convert(2), true);
            optionTypeAsserts(OperationType.Between, ",3", _convert(-1), true);

            optionTypeAsserts(OperationType.Between, "-3", _convert(-1), true);
            optionTypeAsserts(OperationType.Between, "-3,", _convert(-1), true);
            optionTypeAsserts(OperationType.Between, "-3,", _convert(3), true);
            optionTypeAsserts(OperationType.Between, "-3", _convert(3), true);

            optionTypeAsserts(OperationType.Between, "-3,5", _convert(2), true);
            optionTypeAsserts(OperationType.Between, "-3,5", _convert(7), false);
            optionTypeAsserts(OperationType.Between, "-3,5", _convert(-15), false);
            optionTypeAsserts(OperationType.Between, "-3,5,-1", _convert(2), true);//only first two elements are needed. others are ignored

            optionTypeAsserts(OperationType.In, "-3,5,,2", _convert(2), true);
            optionTypeAsserts(OperationType.In, "-3,5,,", _convert(2), false);


            _assertNumericType<T?>(true);
        }

        [TestMethod]
        public void assert_not_supported_numeric_type_operations()
        {
            _assertInvalidOperation<int, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<int?, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<decimal, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<decimal?, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<float, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<float?, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<double, ArgumentException>(OperationType.Contains, "105");
            _assertInvalidOperation<double?, ArgumentException>(OperationType.Contains, "105");
        }

        private void _assertNumericType<T>(bool isNullable = false)
        {
            Func<int, T> convert = i =>
                (T)Convert.ChangeType(i, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));

            optionTypeAsserts(OperationType.Equals, "1", convert(1), true);
            optionTypeAsserts(OperationType.MoreThen, "1", convert(1), false);
            optionTypeAsserts(OperationType.MoreThen, "2", convert(4), true);
            optionTypeAsserts(OperationType.MoreOrEquealsThen, "1", convert(1), true);
            optionTypeAsserts(OperationType.MoreOrEquealsThen, "2", convert(1), false);
            optionTypeAsserts(OperationType.LessThen, "1", convert(1), false);
            optionTypeAsserts(OperationType.LessThen, "0", convert(1), false);
            optionTypeAsserts(OperationType.LessThen, "4", convert(1), true);
            optionTypeAsserts(OperationType.LessOrEquealsThen, "1", convert(1), true);
            optionTypeAsserts(OperationType.Between, "1,3", convert(2), true);
            optionTypeAsserts(OperationType.Between, "1,3", convert(4), false);
            optionTypeAsserts(OperationType.Between, "1,3", convert(0), false);
            optionTypeAsserts(OperationType.Between, ",3", convert(2), true);
            optionTypeAsserts(OperationType.Between, ",3", convert(-50), isNullable); //IMPORTANT! from value is default(numeric) which is 0
            optionTypeAsserts(OperationType.Between, ",3", convert(10), false);
            optionTypeAsserts(OperationType.Between, "4", convert(5), true);
            optionTypeAsserts(OperationType.Between, "4", convert(3), false);

            optionTypeAsserts<T>(OperationType.In, "4,3,7,15", convert(3), true);
            optionTypeAsserts<T>(OperationType.Between, "1,0,2,4", convert(3), false);


        }

        private class SomeGenericClass<T>
        {
            public T Id { get; set; }
        }

        private void optionTypeAsserts<T>(OperationType opType, string value, T idValue, bool expectedResult = true)
        {
            var intOption = new FilterOption<T>
            {
                PropName = "Id",
                OperationType = opType,
                Value = value,
            };
            var tree = intOption.GenerateFilter<SomeGenericClass<T>>();
            Assert.AreEqual(expectedResult, tree.Compile()(new SomeGenericClass<T>() { Id = idValue }));
        }

        #endregion
    }
}
