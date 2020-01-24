using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class EmptyEnumerableToBoolConverterTest
    {
        [Theory]
        [InlineData(true, null, true)]
        [InlineData(false, null, false)]
        [InlineData(true, new object[] { 2, 4 }, false)]
        [InlineData(false, new object[] { 2, 4 }, true)]
        [InlineData(true, 2, true)]
        [InlineData(false, 4, false)]
        public void Test_Convert_Ok(bool emptyValue, object value, bool expected)
        {
            EmptyEnumerableToBoolConverter converter = new EmptyEnumerableToBoolConverter(emptyValue);

            Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
        }
    }
}
