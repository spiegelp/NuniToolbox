using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class UpperCaseConverterTest
{
    public UpperCaseConverterTest() { }

    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "  ")]
    [InlineData(".NET Core", ".NET CORE")]
    [InlineData("Schließen", "SCHLIESSEN")]
    [InlineData(2, null)]
    [InlineData(null, null)]
    public void Test_Convert_Ok(object str, string expected)
    {
        UpperCaseConverter converter = new();

        Assert.Equal(expected, converter.Convert(str, typeof(string), null, CultureInfo.InvariantCulture));
    }
}
