using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class BoolAndConverterTest
{
    public BoolAndConverterTest() { }

    [Fact]
    public void Test_Convert_Ok()
    {
        TestConvertOk([true], true);
        TestConvertOk([true, true], true);
        TestConvertOk([true, true, true, true], true);
        TestConvertOk([false], false);
        TestConvertOk([false, true], false);
        TestConvertOk([true, false], false);
        TestConvertOk([true, true, false, true], false);
        TestConvertOk([true, new object(), true], true);
        TestConvertOk([true, new object(), false], false);
    }

    private void TestConvertOk(object[] values, bool expected)
    {
        BoolAndConverter converter = new();

        Assert.Equal(expected, converter.Convert(values, typeof(bool), null, CultureInfo.InvariantCulture));
    }
}
