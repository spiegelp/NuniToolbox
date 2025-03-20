using System.Globalization;
using System.Windows.Data;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class IndexPlusOneConverterTest
{
    public IndexPlusOneConverterTest() { }

    [Theory]
    [InlineData(2, 3)]
    [InlineData(4L, 5L)]
    [InlineData(null, null)]
    [InlineData("test", null)]
    public void Test_Convert_Ok(object value, object expected)
    {
        if (expected == null)
        {
            expected = Binding.DoNothing;
        }

        IndexPlusOneConverter converter = new();

        Assert.Equal(expected, converter.Convert(value, value?.GetType(), null, CultureInfo.InvariantCulture));
    }
}
