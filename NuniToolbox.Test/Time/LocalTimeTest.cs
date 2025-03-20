using System.Globalization;

using NuniToolbox.Time;

namespace NuniToolbox.Test.Time;

public class LocalTimeTest
{
    public LocalTimeTest() { }

    [Fact]
    public void Test_CreateLocalTime_Ok()
    {
        LocalTime localTime = new(2, 4);

        Assert.Equal(2, localTime.Hour);
        Assert.Equal(4, localTime.Minute);
        Assert.Equal(0, localTime.Second);
        Assert.Equal(0, localTime.Millisecond);

        localTime = new(2, 4, 8);

        Assert.Equal(2, localTime.Hour);
        Assert.Equal(4, localTime.Minute);
        Assert.Equal(8, localTime.Second);
        Assert.Equal(0, localTime.Millisecond);

        localTime = new(2, 4, 8, 16);

        Assert.Equal(2, localTime.Hour);
        Assert.Equal(4, localTime.Minute);
        Assert.Equal(8, localTime.Second);
        Assert.Equal(16, localTime.Millisecond);

        localTime = new(new DateTime(2019, 10, 17, 21, 15, 32, 512));

        Assert.Equal(21, localTime.Hour);
        Assert.Equal(15, localTime.Minute);
        Assert.Equal(32, localTime.Second);
        Assert.Equal(512, localTime.Millisecond);
    }

    [Fact]
    public void Test_CreateLocalTime_ArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(24, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(-1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(0, 60));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(0, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(0, 0, 60));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(0, 0, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(0, 0, 0, 1000));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LocalTime(0, 0, 0, -1));
    }

    [Fact]
    public void Test_ToDateTime_Ok()
    {
        LocalTime localTime = new(2, 4, 8, 16);
        DateTime dateTime = localTime.ToDateTime();

        Assert.Equal(localTime.Hour, dateTime.Hour);
        Assert.Equal(localTime.Minute, dateTime.Minute);
        Assert.Equal(localTime.Second, dateTime.Second);
        Assert.Equal(localTime.Millisecond, dateTime.Millisecond);
    }

    [Fact]
    public void Test_Min_Ok()
    {
        LocalTime localTime = LocalTime.Min;

        Assert.Equal(0, localTime.Hour);
        Assert.Equal(0, localTime.Minute);
        Assert.Equal(0, localTime.Second);
        Assert.Equal(0, localTime.Millisecond);
    }

    [Fact]
    public void Test_Max_Ok()
    {
        LocalTime localTime = LocalTime.Max;

        Assert.Equal(23, localTime.Hour);
        Assert.Equal(59, localTime.Minute);
        Assert.Equal(59, localTime.Second);
        Assert.Equal(999, localTime.Millisecond);
    }

    [Fact]
    public void Test_TotalMilliseconds_Ok()
    {
        LocalTime localTime = new(2, 4, 8, 512);

        Assert.Equal((long)localTime.ToDateTime().TimeOfDay.TotalMilliseconds, localTime.TotalMilliseconds);
    }

    [Fact]
    public void Test_WithHours_Ok()
    {
        LocalTime localTime = new LocalTime(2, 4).WithHours(4);
        LocalTime localTimeResult = new(4, 4);

        Assert.Equal(localTimeResult, localTime);
    }

    [Fact]
    public void Test_WithHours_ArgumentOutOfRangeException()
    {
        LocalTime localTime = new(2, 4);

        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithHours(64));
        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithHours(-2));
    }

    [Fact]
    public void Test_WithMinutes_Ok()
    {
        LocalTime localTime = new LocalTime(2, 4).WithMinutes(8);
        LocalTime localTimeResult = new(2, 8);

        Assert.Equal(localTimeResult, localTime);
    }

    [Fact]
    public void Test_WithMinutes_ArgumentOutOfRangeException()
    {
        LocalTime localTime = new(2, 4);

        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithMinutes(64));
        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithMinutes(-2));
    }

    [Fact]
    public void Test_WithSeconds_Ok()
    {
        LocalTime localTime = new LocalTime(2, 4, 8).WithSeconds(4);
        LocalTime localTimeResult = new(2, 4, 4);

        Assert.Equal(localTimeResult, localTime);
    }

    [Fact]
    public void Test_WithSeconds_ArgumentOutOfRangeException()
    {
        LocalTime localTime = new(2, 4, 8);

        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithSeconds(64));
        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithSeconds(-2));
    }

    [Fact]
    public void Test_WithMilliseconds_Ok()
    {
        LocalTime localTime = new LocalTime(2, 4, 8, 16).WithMilliseconds(512);
        LocalTime localTimeResult = new(2, 4, 8, 512);

        Assert.Equal(localTimeResult, localTime);
    }

    [Fact]
    public void Test_WithMilliseconds_ArgumentOutOfRangeException()
    {
        LocalTime localTime = new(2, 4, 8, 16);

        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithSeconds(1024));
        Assert.Throws<ArgumentOutOfRangeException>(() => localTime.WithSeconds(-2));
    }

    [Fact]
    public void Test_AddHours_Ok()
    {
        LocalTime localTime = new(2, 4);
        int hoursToAdd = 8;
        LocalTime localTimeResult = new(10, 4);

        Assert.Equal(localTimeResult, localTime.AddHours(hoursToAdd));

        localTime = new(2, 4);
        hoursToAdd = 25;
        localTimeResult = new(3, 4);

        Assert.Equal(localTimeResult, localTime.AddHours(hoursToAdd));

        localTime = new(16, 4);
        hoursToAdd = -4;
        localTimeResult = new(12, 4);

        Assert.Equal(localTimeResult, localTime.AddHours(hoursToAdd));

        localTime = new(16, 4);
        hoursToAdd = -20;
        localTimeResult = new(20, 4);

        Assert.Equal(localTimeResult, localTime.AddHours(hoursToAdd));

        localTime = new(16, 4);
        hoursToAdd = -26;
        localTimeResult = new(14, 4);

        Assert.Equal(localTimeResult, localTime.AddHours(hoursToAdd));

        localTime = new(16, 4);
        hoursToAdd = 0;
        localTimeResult = new(16, 4);

        Assert.Equal(localTimeResult, localTime.AddHours(hoursToAdd));
    }

    [Fact]
    public void Test_AddMinutes_Ok()
    {
        LocalTime localTime = new(2, 8);
        int minutesToAdd = 16;
        LocalTime localTimeResult = new(2, 24);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));

        localTime = new(2, 8);
        minutesToAdd = 52;
        localTimeResult = new(3, 0);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));

        localTime = new(2, 8);
        minutesToAdd = 64;
        localTimeResult = new(3, 12);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));

        localTime = new(2, 8);
        minutesToAdd = 128;
        localTimeResult = new(4, 16);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));

        localTime = new(2, 8);
        minutesToAdd = -4;
        localTimeResult = new(2, 4);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));

        localTime = new(2, 8);
        minutesToAdd = -16;
        localTimeResult = new(1, 52);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));

        localTime = new(2, 8);
        minutesToAdd = -124;
        localTimeResult = new(0, 4);

        Assert.Equal(localTimeResult, localTime.AddMinutes(minutesToAdd));
    }

    [Fact]
    public void Test_AddSeconds_Ok()
    {
        LocalTime localTime = new(2, 4, 8);
        int secondsToAdd = 16;
        LocalTime localTimeResult = new(2, 4, 24);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));

        localTime = new(2, 4, 8);
        secondsToAdd = 52;
        localTimeResult = new(2, 5, 0);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));

        localTime = new(2, 4, 8);
        secondsToAdd = 64;
        localTimeResult = new(2, 5, 12);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));

        localTime = new(2, 4, 8);
        secondsToAdd = 128;
        localTimeResult = new(2, 6, 16);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));

        localTime = new(2, 4, 8);
        secondsToAdd = -4;
        localTimeResult = new(2, 4, 4);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));

        localTime = new(2, 4, 8);
        secondsToAdd = -16;
        localTimeResult = new(2, 3, 52);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));

        localTime = new(2, 4, 8);
        secondsToAdd = -124;
        localTimeResult = new(2, 2, 4);

        Assert.Equal(localTimeResult, localTime.AddSeconds(secondsToAdd));
    }

    [Fact]
    public void Test_AddMilliseconds_Ok()
    {
        LocalTime localTime = new(2, 4, 8, 16);
        int millisecondsToAdd = 16;
        LocalTime localTimeResult = new(2, 4, 8, 32);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));

        localTime = new(2, 4, 8, 16);
        millisecondsToAdd = 984;
        localTimeResult = new(2, 4, 9, 0);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));

        localTime = new(2, 4, 8, 16);
        millisecondsToAdd = 1016;
        localTimeResult = new(2, 4, 9, 32);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));

        localTime = new(2, 4, 8, 16);
        millisecondsToAdd = 2016;
        localTimeResult = new(2, 4, 10, 32);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));

        localTime = new(2, 4, 8, 16);
        millisecondsToAdd = -8;
        localTimeResult = new(2, 4, 8, 8);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));

        localTime = new(2, 4, 8, 16);
        millisecondsToAdd = -32;
        localTimeResult = new(2, 4, 7, 984);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));

        localTime = new(2, 4, 8, 16);
        millisecondsToAdd = -2008;
        localTimeResult = new (2, 4, 6, 8);

        Assert.Equal(localTimeResult, localTime.AddMilliseconds(millisecondsToAdd));
    }

    [Fact]
    public void Test_AtDate()
    {
        LocalDate localDate = new(2019, Month.November, 5);
        LocalTime localTime = new(20, 47, 32, 512);
        DateTime expected = new(2019, 11, 5, 20, 47, 32, 512);

        Assert.Equal(expected, localTime.AtDate(localDate));
    }

    [Fact]
    public void Test_ToIsoString()
    {
        LocalTime localTime = new(2, 4, 8, 512);
        string expected = "02:04:08.512";

        Assert.Equal(expected, localTime.ToIsoString());
    }

    [Fact]
    public void Test_ToString()
    {
        LocalTime localTime = new(2, 4, 8, 512);
        string expected = "02:04:08.512";

        Assert.Equal(expected, localTime.ToString());
    }

    [Fact]
    public void TestToString_WithFormat()
    {
        LocalTime localTime = new(2, 4, 8, 512);
        string expected = "02 04 08.512";

        Assert.Equal(expected, localTime.ToString("HH mm ss.fff"));

        localTime = new(2, 4, 8, 512);
        expected = "2 4 8.512";

        Assert.Equal(expected, localTime.ToString("H m s.fff"));
    }

    [Fact]
    public void TestToString_WithFormatAndFormatProvider()
    {
        LocalTime localTime = new(14, 4, 8, 512);
        string expected = "02PM 04 08.512";
        CultureInfo cultureInfo = new("en-US");

        Assert.Equal(expected, localTime.ToString("hhtt mm ss.fff", cultureInfo));
    }

    [Fact]
    public void Test_Equals_True()
    {
        LocalTime localTime1 = new(2, 4);
        LocalTime localTime2 = new(2, 4, 0, 0);

        Assert.True(localTime1 == localTime2);
        Assert.True(localTime1.Equals(localTime2));
    }

    [Fact]
    public void Test_Equals_False()
    {
        LocalTime localTime1 = new(2, 4);
        LocalTime localTime2 = new(2, 4, 0, 1);

        Assert.False(localTime1 == localTime2);
        Assert.False(localTime1.Equals(localTime2));
    }

    [Fact]
    public void Test_NotEquals_True()
    {
        LocalTime localTime1 = new(2, 4);
        LocalTime localTime2 = new(2, 4, 0, 1);

        Assert.True(localTime1 != localTime2);
    }

    [Fact]
    public void Test_NotEquals_False()
    {
        LocalTime localTime1 = new(2, 4);
        LocalTime localTime2 = new(2, 4, 0, 0);

        Assert.False(localTime1 != localTime2);
    }

    [Fact]
    public void Test_Less_True()
    {
        Assert.True(new LocalTime(2, 4) < new LocalTime(3, 4));
        Assert.True(new LocalTime(3, 2) < new LocalTime(3, 4));
        Assert.True(new LocalTime(2, 4, 8) < new LocalTime(2, 4, 16));
        Assert.True(new LocalTime(2, 4, 8, 256) < new LocalTime(2, 4, 8, 512));
    }

    [Fact]
    public void Test_Less_False()
    {
        Assert.False(new LocalTime(3, 4) < new LocalTime(2, 4));
        Assert.False(new LocalTime(3, 4) < new LocalTime(3, 2));
        Assert.False(new LocalTime(2, 4, 16) < new LocalTime(2, 4, 8));
        Assert.False(new LocalTime(2, 4, 8, 512) < new LocalTime(2, 4, 8, 256));
        Assert.False(new LocalTime(2, 4) < new LocalTime(2, 4));
    }

    [Fact]
    public void Test_LessOrEqual_True()
    {
        Assert.True(new LocalTime(2, 4) <= new LocalTime(3, 4));
        Assert.True(new LocalTime(3, 2) <= new LocalTime(3, 4));
        Assert.True(new LocalTime(2, 4, 8) <= new LocalTime(2, 4, 16));
        Assert.True(new LocalTime(2, 4, 8, 256) <= new LocalTime(2, 4, 8, 512));
        Assert.True(new LocalTime(2, 4) <= new LocalTime(2, 4));
    }

    [Fact]
    public void Test_LessOrEqual_False()
    {
        Assert.False(new LocalTime(3, 4) <= new LocalTime(2, 4));
        Assert.False(new LocalTime(3, 4) <= new LocalTime(3, 2));
        Assert.False(new LocalTime(2, 4, 16) <= new LocalTime(2, 4, 8));
        Assert.False(new LocalTime(2, 4, 8, 512) <= new LocalTime(2, 4, 8, 256));
    }

    [Fact]
    public void Test_Greater_True()
    {
        Assert.True(new LocalTime(3, 4) > new LocalTime(2, 4));
        Assert.True(new LocalTime(3, 4) > new LocalTime(3, 2));
        Assert.True(new LocalTime(2, 4, 16) > new LocalTime(2, 4, 8));
        Assert.True(new LocalTime(2, 4, 8, 512) > new LocalTime(2, 4, 8, 256));
    }

    [Fact]
    public void Test_Greater_False()
    {
        Assert.False(new LocalTime(2, 4) > new LocalTime(3, 4));
        Assert.False(new LocalTime(3, 2) > new LocalTime(3, 4));
        Assert.False(new LocalTime(2, 4, 8) > new LocalTime(2, 4, 16));
        Assert.False(new LocalTime(2, 4, 8, 256) > new LocalTime(2, 4, 8, 512));
        Assert.False(new LocalTime(2, 4) > new LocalTime(2, 4));
    }

    [Fact]
    public void Test_GreaterOrEqual_True()
    {
        Assert.True(new LocalTime(3, 4) >= new LocalTime(2, 4));
        Assert.True(new LocalTime(3, 4) >= new LocalTime(3, 2));
        Assert.True(new LocalTime(2, 4, 16) >= new LocalTime(2, 4, 8));
        Assert.True(new LocalTime(2, 4, 8, 512) >= new LocalTime(2, 4, 8, 256));
        Assert.True(new LocalTime(2, 4) >= new LocalTime(2, 4));
    }

    [Fact]
    public void Test_GreaterOrEqual_False()
    {
        Assert.False(new LocalTime(2, 4) >= new LocalTime(3, 4));
        Assert.False(new LocalTime(3, 2) >= new LocalTime(3, 4));
        Assert.False(new LocalTime(2, 4, 8) >= new LocalTime(2, 4, 16));
        Assert.False(new LocalTime(2, 4, 8, 256) >= new LocalTime(2, 4, 8, 512));
    }

    [Fact]
    public void Test_GetHashCode_Equal()
    {
        LocalTime localTime1 = new(2, 4, 8, 16);
        LocalTime localTime2 = new(2, 4, 8, 16);

        int hashCode1 = localTime1.GetHashCode();
        int hashCode2 = localTime2.GetHashCode();

        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void Test_GetHashCode_NotEqual()
    {
        LocalTime localTime1 = new(2, 4, 8, 16);
        LocalTime localTime2 = new(2, 8, 4, 16);

        int hashCode1 = localTime1.GetHashCode();
        int hashCode2 = localTime2.GetHashCode();

        Assert.NotEqual(hashCode1, hashCode2);
    }
}
