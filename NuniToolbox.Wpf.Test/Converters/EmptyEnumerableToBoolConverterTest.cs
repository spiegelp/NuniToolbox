using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class EmptyEnumerableToBoolConverterTest
{
    public EmptyEnumerableToBoolConverterTest() { }

    [Theory]
    [InlineData(true, null, true)]
    [InlineData(false, null, false)]
    [InlineData(true, new object[] { 2, 4 }, false)]
    [InlineData(false, new object[] { 2, 4 }, true)]
    [InlineData(true, 2, true)]
    [InlineData(false, 4, false)]
    public void Test_Convert_Ok(bool emptyValue, object value, bool expected)
    {
        EmptyEnumerableToBoolConverter converter = new(emptyValue);

        Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
    }
}
