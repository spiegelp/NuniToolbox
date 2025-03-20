using NuniToolbox.Time;

namespace NuniToolbox.Test.Time;

public class YearUtilTest
{
    public YearUtilTest() { }

    [Theory]
    [InlineData(1600, true)]
    [InlineData(2000, true)]
    [InlineData(2001, false)]
    [InlineData(2002, false)]
    [InlineData(2003, false)]
    [InlineData(2004, true)]
    [InlineData(2005, false)]
    [InlineData(2006, false)]
    [InlineData(2007, false)]
    [InlineData(2008, true)]
    [InlineData(2009, false)]
    [InlineData(2400, true)]
    [InlineData(1800, false)]
    [InlineData(1900, false)]
    [InlineData(2100, false)]
    [InlineData(2200, false)]
    public void Test_IsLeapYear_Ok(int year, bool isleapYear)
    {
        Assert.Equal(isleapYear, YearUtil.IsLeapYear(year));
    }
}
