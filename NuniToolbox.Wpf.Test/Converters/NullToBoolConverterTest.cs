using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class NullToBoolConverterTest
{
    public NullToBoolConverterTest() { }

    [Theory]
    [InlineData(true, null, true)]
    [InlineData(true, 2, false)]
    [InlineData(false, null, false)]
    [InlineData(false, 2, true)]
    [InlineData(true, "", true)]
    [InlineData(true, "    ", true)]
    [InlineData(true, "2", false)]
    public void Test_Convert_Ok(bool nullValue, object value, bool expected)
    {
        NullToBoolConverter converter = new(nullValue);

        Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
    }
}
