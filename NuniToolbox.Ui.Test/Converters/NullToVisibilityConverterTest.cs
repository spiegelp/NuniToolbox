using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class NullToVisibilityConverterTest
    {
        [Theory]
        [InlineData(Visibility.Hidden, Visibility.Visible, null, Visibility.Hidden)]
        [InlineData(Visibility.Visible, Visibility.Hidden, null, Visibility.Visible)]
        [InlineData(Visibility.Hidden, Visibility.Visible, 2, Visibility.Visible)]
        [InlineData(Visibility.Visible, Visibility.Hidden, 4, Visibility.Hidden)]
        public void Test_Convert_Ok(Visibility nullValue, Visibility notNullValue, object value, Visibility expected)
        {
            NullToVisibilityConverter converter = new NullToVisibilityConverter(nullValue, notNullValue);

            Assert.Equal(expected, converter.Convert(value, typeof(Visibility), null, CultureInfo.InvariantCulture));
        }
    }
}
