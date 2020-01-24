using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class BoolOrToVisibilityConverterTest
    {
        [Theory]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { true, true }, Visibility.Visible)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { true, true, true }, Visibility.Visible)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { true, false }, Visibility.Visible)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { true, true, false }, Visibility.Visible)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { false, false }, Visibility.Collapsed)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { false, false, false }, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, new object[] { true, true }, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, new object[] { true, true, true }, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, new object[] { true, false }, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, new object[] { true, true, false }, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, new object[] { false, false }, Visibility.Visible)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, new object[] { false, false, false }, Visibility.Visible)]
        public void Test_Convert_Ok(Visibility falseValue, Visibility trueValue, object[] values, Visibility expected)
        {
            BoolOrToVisibilityConverter converter = new BoolOrToVisibilityConverter(falseValue, trueValue);

            Assert.Equal(expected, converter.Convert(values, typeof(Visibility), null, CultureInfo.InvariantCulture));
        }
    }
}
