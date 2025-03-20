using System.Globalization;
using System.Windows;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class NullToVisibilityConverterTest
{
    public NullToVisibilityConverterTest() { }

    [Theory]
    [InlineData(Visibility.Hidden, Visibility.Visible, null, Visibility.Hidden)]
    [InlineData(Visibility.Visible, Visibility.Hidden, null, Visibility.Visible)]
    [InlineData(Visibility.Hidden, Visibility.Visible, 2, Visibility.Visible)]
    [InlineData(Visibility.Visible, Visibility.Hidden, 4, Visibility.Hidden)]
    public void Test_Convert_Ok(Visibility nullValue, Visibility notNullValue, object value, Visibility expected)
    {
        NullToVisibilityConverter converter = new(nullValue, notNullValue);

        Assert.Equal(expected, converter.Convert(value, typeof(Visibility), null, CultureInfo.InvariantCulture));
    }
}
