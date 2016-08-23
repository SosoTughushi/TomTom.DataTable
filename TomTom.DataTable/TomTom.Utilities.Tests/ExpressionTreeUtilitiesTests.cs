using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TomTom.Utilities.Tests
{

    [TestClass]
    public class ExpressionTreeUtilitiesTests
    {

        private enum SomeEnum { First, Second }
        private class SomeClass
        {
            public string SomeProperty { get; set; }
            public SomeClass Child { get; set; }
            public SomeEnum Enum { get; set; }
        }

        #region GetPropertyTypeFromExpression
        [TestMethod]
        public void int_property_type_is_int()
        {
            Expression<Func<int[], object>> exp = arr => arr.Length;
            var expressionReturnType = exp.GetPropertyTypeFromExpression();
            Assert.AreEqual(typeof(int), expressionReturnType);
        }

        [TestMethod]
        public void object_property_type_is_object()
        {
            Expression<Func<int[], object>> exp = arr => new object();
            var expressionReturnType = exp.GetPropertyTypeFromExpression();
            Assert.AreEqual(typeof(object), expressionReturnType);
        }

        [TestMethod]
        public void unary_expression_test()
        {
            Expression<Func<int[], object>> exp = arr => !true;
            var expressionReturnType = exp.GetPropertyTypeFromExpression();
            Assert.AreEqual(typeof(bool), expressionReturnType);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void null_expression_throws_NullReferenceException()
        {
            Expression<Func<int[], object>> exp = null;
            var expressionReturnType = exp.GetPropertyTypeFromExpression();
        }
        #endregion

        #region ExtractPropertyNameFromExpression
        [TestMethod]
        public void some_class_SomeProperty()
        {
            Expression<Func<SomeClass, string>> exp = arr => arr.SomeProperty;
            Assert.AreEqual("SomeProperty", exp.ExtractPropertyNameFromExpression());
        }

        [TestMethod]
        public void some_class_nested_SomeProperty()
        {
            Expression<Func<SomeClass, string>> exp = arr => arr.Child.SomeProperty;
            Assert.AreEqual("Child.SomeProperty", exp.ExtractPropertyNameFromExpression());
        }

        [TestMethod]
        public void some_class_convert_unary_expression()
        {
            Expression<Func<SomeClass, object>> exp = arr => arr.Child.SomeProperty;
            Assert.AreEqual("Child.SomeProperty", exp.ExtractPropertyNameFromExpression());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "can't extract property name from expression specified")]
        public void some_class_method_call_throws_exception()
        {
            Expression<Func<SomeClass, bool>> exp = arr => arr.Child.SomeProperty.IsNotEmpty();
            exp.ExtractPropertyNameFromExpression();
        }

        [TestMethod]
        public void some_class_null_check()
        {
            Expression<Func<SomeClass, string>> exp = arr => arr.Child.SomeProperty ?? "Some Value";
            var name = exp.ExtractPropertyNameFromExpression();
            Assert.AreEqual("Child.SomeProperty", name);
        }

        [TestMethod]
        public void some_class_returns_himself()
        {
            Expression<Func<SomeClass, SomeClass>> exp = arr => arr;
            var name = exp.ExtractPropertyNameFromExpression();
            Assert.AreEqual("", name);
        }

        [ExpectedException(typeof(Exception), "can't extract property name from expression specified")]
        [TestMethod]
        public void returning_constant_is_not_supported()
        {
            Expression<Func<SomeClass, object>> exp = arr => 3;
            var name = exp.ExtractPropertyNameFromExpression();
        }

        [ExpectedException(typeof(Exception), "can't extract property name from expression specified")]
        [TestMethod]
        public void returning_logal_variable_not_supported()
        {
            var local = 3;
            Expression<Func<SomeClass, object>> exp = arr => local;
            exp.ExtractPropertyNameFromExpression();
        }

        [ExpectedException(typeof(Exception), "can't extract property name from expression specified")]
        [TestMethod]
        public void returning_new_objects_not_supported()
        {
            Expression<Func<SomeClass, object>> exp = arr => new object();
            exp.ExtractPropertyNameFromExpression();
        }

        #endregion

        #region GetPropertyExpression
        [TestMethod]
        public void null_strings_return_null()
        {
            Assert.AreEqual(null, ((string)null).GetPropertyExpression<char>());
        }

        [TestMethod]
        public void string_length_type_is_string_int()
        {
            var type = "Length".GetPropertyExpression<string>().GetType();
            Assert.AreEqual(typeof(Expression<Func<string, int>>), type);
        }
        [TestMethod]
        public void string_length_with_convert_type_is_string_object()
        {
            var type = "Length".GetPropertyExpression<string>(true, true).GetType();
            Assert.AreEqual(typeof(Expression<Func<string, object>>), type);
        }

        [TestMethod]
        public void string_length_name_is_length()
        {
            var exp = (Expression<Func<string, int>>)"Length".GetPropertyExpression<string>();
            Assert.AreEqual("Length", exp.ExtractPropertyNameFromExpression());
        }

        [TestMethod]
        public void some_class_child_SomeProperty_is_child_someProperty()
        {
            var exp = (Expression<Func<SomeClass, string>>)"Child.SomeProperty".GetPropertyExpression<SomeClass>();
            Assert.AreEqual("Child.SomeProperty", exp.ExtractPropertyNameFromExpression());
        }

        [TestMethod]
        public void enum_property_is_converted_to_int()
        {
            var exp = (Expression<Func<SomeClass, int>>)"Child.Child.Enum".GetPropertyExpression<SomeClass>();
            Assert.AreEqual("Child.Child.Enum", exp.ExtractPropertyNameFromExpression());
        }

        [TestMethod]
        public void enum_convert_to_object_is_int()
        {
            var exp = (Expression<Func<SomeClass, object>>)"Child.Child.Enum".GetPropertyExpression<SomeClass>(true, true);
            var type = exp.GetPropertyTypeFromExpression();
            Assert.AreEqual(typeof(int), type);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "can't extract property name from expression specified")]
        public void enum_convert_to_object_name_extract_name_fails_because_of_nested_convertions()
        {
            var exp = (Expression<Func<SomeClass, object>>)"Child.Child.Enum".GetPropertyExpression<SomeClass>(true, true);
            Assert.AreEqual("Child.Child.Enum", exp.ExtractPropertyNameFromExpression());
        }

        #endregion
    }
}
