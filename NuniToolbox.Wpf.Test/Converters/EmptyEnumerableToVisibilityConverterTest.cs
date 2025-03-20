using System.Globalization;
using System.Windows;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class EmptyEnumerableToVisibilityConverterTest
{
    public EmptyEnumerableToVisibilityConverterTest() { }

    [Theory]
    [InlineData(Visibility.Collapsed, Visibility.Visible, null, Visibility.Collapsed)]
    [InlineData(Visibility.Visible, Visibility.Hidden, null, Visibility.Visible)]
    [InlineData(Visibility.Visible, Visibility.Hidden, new object[] { 2, 4 }, Visibility.Hidden)]
    [InlineData(Visibility.Collapsed, Visibility.Visible, new object[] { 2, 4 }, Visibility.Visible)]
    [InlineData(Visibility.Hidden, Visibility.Visible, 2, Visibility.Hidden)]
    [InlineData(Visibility.Visible, Visibility.Collapsed, 4, Visibility.Visible)]
    public void Test_Convert_Ok(Visibility emptyValue, Visibility notEmptyValue, object value, Visibility expected)
    {
        EmptyEnumerableToVisibilityConverter converter = new(emptyValue, notEmptyValue);

        Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
    }
}
