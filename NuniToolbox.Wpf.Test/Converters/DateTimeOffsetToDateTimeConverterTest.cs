using System.Globalization;

using NuniToolbox.Wpf.Converters;

namespace NuniToolbox.Wpf.Test.Converters;

public class DateTimeOffsetToDateTimeConverterTest
{
    // tests commented out, because .NET only takes the operating system's time zone
    //     so the tests will fail for any other timezone than CET

    /*[Fact]
    public void Test_Convert_Ok()
    {
        TestConvertOk(new DateTimeOffset(2020, 1, 24, 21, 30, 16, new TimeSpan(0, 0, 0)), new DateTime(2020, 1, 24, 22, 30, 16, DateTimeKind.Local));
        TestConvertOk(new DateTimeOffset(2020, 1, 24, 23, 30, 16, new TimeSpan(0, 0, 0)), new DateTime(2020, 1, 25, 0, 30, 16, DateTimeKind.Local));
    }

    private void TestConvertOk(DateTimeOffset value, DateTime expected)
    {
        DateTimeOffsetToDateTimeConverter converter = new();

        Assert.Equal(expected, converter.Convert(value, typeof(DateTime), null, CultureInfo.InvariantCulture));
    }

    [Fact]
    public void Test_ConvertBack_Ok()
    {
        TestConvertBackOk(new DateTime(2020, 1, 24, 22, 30, 16, DateTimeKind.Local), new DateTimeOffset(2020, 1, 24, 21, 30, 16, new TimeSpan(0, 0, 0)));
        TestConvertBackOk(new DateTime(2020, 1, 25, 0, 30, 16, DateTimeKind.Local), new DateTimeOffset(2020, 1, 24, 23, 30, 16, new TimeSpan(0, 0, 0)));
    }

    private void TestConvertBackOk(DateTime value, DateTimeOffset expected)
    {
        DateTimeOffsetToDateTimeConverter converter = new();

        Assert.Equal(expected, converter.ConvertBack(value, typeof(DateTime), null, CultureInfo.InvariantCulture));
    }*/
}
