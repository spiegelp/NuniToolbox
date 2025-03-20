using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class BoolOrConverterTest
{
    [Fact]
    public void Test_Convert_Ok()
    {
        TestConvertOk([true], true);
        TestConvertOk([true, true], true);
        TestConvertOk([true, true, true, true], true);
        TestConvertOk([false], false);
        TestConvertOk([false, true], true);
        TestConvertOk([true, false], true);
        TestConvertOk([true, true, false, true], true);
        TestConvertOk([true, new object(), true], true);
        TestConvertOk([true, new object(), false], true);
    }

    private void TestConvertOk(object[] values, bool expected)
    {
        BoolOrConverter converter = new();

        Assert.Equal(expected, converter.Convert(values, typeof(bool), null, CultureInfo.InvariantCulture));
    }
}
