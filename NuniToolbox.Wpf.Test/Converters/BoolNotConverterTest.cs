using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class BoolNotConverterTest
{
    public BoolNotConverterTest() { }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(2, false)]
    [InlineData(null, false)]
    public void Test_Convert_Ok(object value, bool expected)
    {
        BoolNotConverter converter = new BoolNotConverter();

        Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(2, false)]
    [InlineData(null, false)]
    public void Test_ConvertBack_Ok(object value, bool expected)
    {
        BoolNotConverter converter = new();

        Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
    }
}
