using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class EmptyEnumerableVisibilityConverterTest
    {
        [Theory]
        [InlineData(Visibility.Collapsed, Visibility.Visible, null, Visibility.Collapsed)]
        [InlineData(Visibility.Visible, Visibility.Hidden, null, Visibility.Visible)]
        [InlineData(Visibility.Visible, Visibility.Hidden, new object[] { 2, 4 }, Visibility.Hidden)]
        [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { 2, 4 }, Visibility.Visible)]
        [InlineData(Visibility.Hidden, Visibility.Visible, 2, Visibility.Hidden)]
        [InlineData(Visibility.Visible, Visibility.Collapsed, 4, Visibility.Visible)]
        public void Test_Convert_Ok(Visibility emptyValue, Visibility notEmptyValue, object value, Visibility expected)
        {
            EmptyEnumerableVisibilityConverter converter = new EmptyEnumerableVisibilityConverter(emptyValue, notEmptyValue);

            Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
        }
    }
}
