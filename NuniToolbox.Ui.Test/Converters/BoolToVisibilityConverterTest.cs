using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class BoolToVisibilityConverterTest
    {
        [Theory]
        [InlineData(Visibility.Collapsed, Visibility.Visible, true, Visibility.Visible)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, false, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Hidden, true, Visibility.Hidden)]
        [InlineData(Visibility.Visible, Visibility.Hidden, false, Visibility.Visible)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, 64, Visibility.Collapsed)]
        public void Test_Convert_Ok(Visibility falseValue, Visibility trueValue, object value, Visibility expected)
        {
            BoolToVisibilityConverter converter = new BoolToVisibilityConverter(falseValue, trueValue);

            Assert.Equal(expected, converter.Convert(value, typeof(Visibility), null, CultureInfo.InvariantCulture));
        }
    }
}
