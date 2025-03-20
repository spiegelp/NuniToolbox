using System.Globalization;
using System.Windows;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class BoolToVisibilityConverterTest
{
    public BoolToVisibilityConverterTest() { }

    [Theory]
    [InlineData(Visibility.Collapsed, Visibility.Visible, true, Visibility.Visible)]
    [InlineData(Visibility.Collapsed, Visibility.Visible, false, Visibility.Collapsed)]
    [InlineData(Visibility.Visible, Visibility.Hidden, true, Visibility.Hidden)]
    [InlineData(Visibility.Visible, Visibility.Hidden, false, Visibility.Visible)]
    [InlineData(Visibility.Collapsed, Visibility.Visible, 64, Visibility.Collapsed)]
    public void Test_Convert_Ok(Visibility falseValue, Visibility trueValue, object value, Visibility expected)
    {
        BoolToVisibilityConverter converter = new(falseValue, trueValue);

        Assert.Equal(expected, converter.Convert(value, typeof(Visibility), null, CultureInfo.InvariantCulture));
    }
}
