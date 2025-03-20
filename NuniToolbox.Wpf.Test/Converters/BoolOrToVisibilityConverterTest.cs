using System.Globalization;
using System.Windows;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class BoolOrToVisibilityConverterTest
{
    public BoolOrToVisibilityConverterTest() { }

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
        BoolOrToVisibilityConverter converter = new(falseValue, trueValue);

        Assert.Equal(expected, converter.Convert(values, typeof(Visibility), null, CultureInfo.InvariantCulture));
    }
}
