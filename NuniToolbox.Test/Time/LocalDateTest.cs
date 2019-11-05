using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Time;

namespace NuniToolbox.Test.Time
{
    public class LocalDateTest
    {
        [Fact]
        public void Test_CreateLocalDate_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.October, 23);

            Assert.Equal(2019, localDate.Year);
            Assert.Equal(Month.October, localDate.Month);
            Assert.Equal(23, localDate.Day);

            localDate = new LocalDate(2000, Month.February, 29);

            Assert.Equal(2000, localDate.Year);
            Assert.Equal(Month.February, localDate.Month);
            Assert.Equal(29, localDate.Day);

            localDate = new LocalDate(new DateTime(2019, 10, 23, 21, 15, 32, 512));

            Assert.Equal(2019, localDate.Year);
            Assert.Equal(Month.October, localDate.Month);
            Assert.Equal(23, localDate.Day);
        }

        [Fact]
        public void Test_CreateLocalDate_ArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(0, Month.January, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(10000, Month.January, 1));
            Assert.Throws<ArgumentException>(() => new LocalDate(2019, (Month)0, 1));
            Assert.Throws<ArgumentException>(() => new LocalDate(2019, (Month)13, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.January, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.January, 32));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2001, Month.February, 29));
        }

        [Fact]
        public void Test_ToDateTime_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            DateTime dateTime = localDate.ToDateTime();

            Assert.Equal(localDate.Year, dateTime.Year);
            Assert.Equal(localDate.Month.Number(), dateTime.Month);
            Assert.Equal(localDate.Day, dateTime.Day);
        }

        [Fact]
        public void Test_Min_Ok()
        {
            LocalDate localDate = LocalDate.Min;

            Assert.Equal(1, localDate.Year);
            Assert.Equal(Month.January, localDate.Month);
            Assert.Equal(1, localDate.Day);
        }

        [Fact]
        public void Test_Max_Ok()
        {
            LocalDate localDate = LocalDate.Max;

            Assert.Equal(9999, localDate.Year);
            Assert.Equal(Month.December, localDate.Month);
            Assert.Equal(31, localDate.Day);
        }

        [Fact]
        public void Test_IsLeapDay_Ok()
        {
            Assert.True(new LocalDate(2000, Month.February, 29).IsLeapDay());
            Assert.True(new LocalDate(2020, Month.February, 29).IsLeapDay());
            Assert.False(new LocalDate(2000, Month.February, 28).IsLeapDay());
            Assert.False(new LocalDate(2000, Month.April, 4).IsLeapDay());
            Assert.False(new LocalDate(1999, Month.April, 4).IsLeapDay());
        }

        [Fact]
        public void Test_WithYear_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5).WithYear(2020);
            LocalDate localDateResult = new LocalDate(2020, Month.November, 5);

            Assert.Equal(localDateResult, localDate);

            localDate = new LocalDate(2000, Month.February, 29).WithYear(2020);
            localDateResult = new LocalDate(2020, Month.February, 29);

            Assert.Equal(localDateResult, localDate);

            localDate = new LocalDate(2000, Month.February, 29).WithYear(1999);
            localDateResult = new LocalDate(1999, Month.February, 28);

            Assert.Equal(localDateResult, localDate);
        }

        [Fact]
        public void Test_WithYear_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.November, 5).WithYear(10000));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.November, 5).WithYear(-1));
        }

        [Fact]
        public void Test_WithMonth_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5).WithMonth(Month.October);
            LocalDate localDateResult = new LocalDate(2019, Month.October, 5);

            Assert.Equal(localDateResult, localDate);

            localDate = new LocalDate(2019, Month.March, 31).WithMonth(Month.April);
            localDateResult = new LocalDate(2019, Month.April, 30);

            Assert.Equal(localDateResult, localDate);

            localDate = new LocalDate(2020, Month.March, 31).WithMonth(Month.February);
            localDateResult = new LocalDate(2020, Month.February, 29);

            Assert.Equal(localDateResult, localDate);

            localDate = new LocalDate(2019, Month.March, 31).WithMonth(Month.February);
            localDateResult = new LocalDate(2019, Month.February, 28);

            Assert.Equal(localDateResult, localDate);
        }

        [Fact]
        public void Test_WithMonth_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new LocalDate(2000, Month.March, 31).WithMonth((Month)0));
            Assert.Throws<ArgumentException>(() => new LocalDate(2000, Month.March, 31).WithMonth((Month)13));
        }

        [Fact]
        public void Test_WithDay_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5).WithDay(4);
            LocalDate localDateResult = new LocalDate(2019, Month.November, 4);

            Assert.Equal(localDateResult, localDate);
        }

        [Fact]
        public void Test_WithDay_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2000, Month.November, 30).WithDay(31));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.November, 5).WithDay(0));
        }

        [Fact]
        public void Test_AddYears_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            int yearsToAdd = 2;
            LocalDate localDateResult = new LocalDate(2021, Month.November, 5);

            Assert.Equal(localDateResult, localDate.AddYears(yearsToAdd));

            localDate = new LocalDate(2019, Month.November, 5); ;
            yearsToAdd = -2;
            localDateResult = new LocalDate(2017, Month.November, 5);

            Assert.Equal(localDateResult, localDate.AddYears(yearsToAdd));

            localDate = new LocalDate(2020, Month.February, 29);
            yearsToAdd = 1;
            localDateResult = new LocalDate(2021, Month.February, 28);

            Assert.Equal(localDateResult, localDate.AddYears(yearsToAdd));
        }

        [Fact]
        public void Test_AddYears_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.November, 5).AddYears(10000));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(2019, Month.November, 5).AddYears(-10000));
        }

        [Fact]
        public void Test_AddMonths_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            int monthsToAdd = 1;
            LocalDate localDateResult = new LocalDate(2019, Month.December, 5);

            Assert.Equal(localDateResult, localDate.AddMonths(monthsToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            monthsToAdd = -2;
            localDateResult = new LocalDate(2019, Month.September, 5);

            Assert.Equal(localDateResult, localDate.AddMonths(monthsToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            monthsToAdd = 14;
            localDateResult = new LocalDate(2021, Month.January, 5);

            Assert.Equal(localDateResult, localDate.AddMonths(monthsToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            monthsToAdd = -28;
            localDateResult = new LocalDate(2017, Month.July, 5);

            Assert.Equal(localDateResult, localDate.AddMonths(monthsToAdd));

            localDate = new LocalDate(2019, Month.March, 31);
            monthsToAdd = 1;
            localDateResult = new LocalDate(2019, Month.April, 30);

            Assert.Equal(localDateResult, localDate.AddMonths(monthsToAdd));
        }

        [Fact]
        public void Test_AddMonths_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(9999, Month.November, 1).AddMonths(12));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LocalDate(1, Month.November, 1).AddMonths(-12));
        }

        [Fact]
        public void Test_AddDays_Ok()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            int daysToAdd = 3;
            LocalDate localDateResult = new LocalDate(2019, Month.November, 8);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            daysToAdd = 29;
            localDateResult = new LocalDate(2019, Month.December, 4);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            daysToAdd = 60;
            localDateResult = new LocalDate(2020, Month.January, 4);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            daysToAdd = -3;
            localDateResult = new LocalDate(2019, Month.November, 2);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2019, Month.November, 5);
            daysToAdd = -37;
            localDateResult = new LocalDate(2019, Month.September, 29);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2020, Month.February, 28);
            daysToAdd = 1;
            localDateResult = new LocalDate(2020, Month.February, 29);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2020, Month.February, 28);
            daysToAdd = 2;
            localDateResult = new LocalDate(2020, Month.March, 1);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2019, Month.February, 28);
            daysToAdd = 1;
            localDateResult = new LocalDate(2019, Month.March, 1);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2020, Month.March, 1);
            daysToAdd = -1;
            localDateResult = new LocalDate(2020, Month.February, 29);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));

            localDate = new LocalDate(2019, Month.March, 1);
            daysToAdd = -1;
            localDateResult = new LocalDate(2019, Month.February, 28);

            Assert.Equal(localDateResult, localDate.AddDays(daysToAdd));
        }

        [Fact]
        public void Test_AddDays_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => LocalDate.Max.AddDays(1));
            Assert.Throws<ArgumentOutOfRangeException>(() => LocalDate.Min.AddDays(-1));
        }

        [Fact]
        public void Test_AtTime()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            LocalTime localTime = new LocalTime(20, 47, 32, 512);
            DateTime expected = new DateTime(2019, 11, 5, 20, 47, 32, 512);

            Assert.Equal(expected, localDate.AtTime(localTime));
        }

        [Fact]
        public void Test_ToIsoString()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            string expected = "2019-11-05";

            Assert.Equal(expected, localDate.ToIsoString());
        }

        [Fact]
        public void Test_ToString()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            string expected = "2019-11-05";

            Assert.Equal(expected, localDate.ToString());
        }

        [Fact]
        public void TestToString_WithFormat()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            string expected = "05.11.2019";

            Assert.Equal(expected, localDate.ToString("dd.MM.yyyy"));
        }

        [Fact]
        public void TestToString_WithFormatAndFormatProvider()
        {
            LocalDate localDate = new LocalDate(2019, Month.November, 5);
            string expected = "5. November 2019";
            CultureInfo cultureInfo = new CultureInfo("de-AT");

            Assert.Equal(expected, localDate.ToString("d. MMMM yyyy", cultureInfo));
        }

        [Fact]
        public void Test_Equals_True()
        {
            LocalDate localDate1 = new LocalDate(2019, Month.November, 5);
            LocalDate localDate2 = new LocalDate(2019, Month.November, 5);

            Assert.True(localDate1 == localDate2);
            Assert.True(localDate1.Equals(localDate2));
        }

        [Fact]
        public void Test_Equals_False()
        {
            LocalDate localDate1 = new LocalDate(2019, Month.November, 4);
            LocalDate localDate2 = new LocalDate(2019, Month.November, 5);

            Assert.False(localDate1 == localDate2);
            Assert.False(localDate1.Equals(localDate2));
        }

        [Fact]
        public void Test_NotEquals_True()
        {

            LocalDate localDate1 = new LocalDate(2019, Month.November, 4);
            LocalDate localDate2 = new LocalDate(2019, Month.November, 5);

            Assert.True(localDate1 != localDate2);
        }

        [Fact]
        public void Test_NotEquals_False()
        {
            LocalDate localDate1 = new LocalDate(2019, Month.November, 5);
            LocalDate localDate2 = new LocalDate(2019, Month.November, 5);

            Assert.False(localDate1 != localDate2);
        }

        [Fact]
        public void Test_Less_True()
        {
            Assert.True(new LocalDate(2018, Month.November, 5) < new LocalDate(2019, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.October, 5) < new LocalDate(2019, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.November, 4) < new LocalDate(2019, Month.November, 5));
        }

        [Fact]
        public void Test_Less_False()
        {
            Assert.False(new LocalDate(2019, Month.November, 5) < new LocalDate(2018, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.November, 5) < new LocalDate(2019, Month.October, 5));
            Assert.False(new LocalDate(2019, Month.November, 5) < new LocalDate(2019, Month.November, 4));
            Assert.False(new LocalDate(2019, Month.November, 5) < new LocalDate(2019, Month.November, 5));
        }

        [Fact]
        public void Test_LessOrEqual_True()
        {
            Assert.True(new LocalDate(2018, Month.November, 5) <= new LocalDate(2019, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.October, 5) <= new LocalDate(2019, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.November, 4) <= new LocalDate(2019, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.November, 5) <= new LocalDate(2019, Month.November, 5));
        }

        [Fact]
        public void Test_LessOrEqual_False()
        {
            Assert.False(new LocalDate(2019, Month.November, 5) <= new LocalDate(2018, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.November, 5) <= new LocalDate(2019, Month.October, 5));
            Assert.False(new LocalDate(2019, Month.November, 5) <= new LocalDate(2019, Month.November, 4));
        }

        [Fact]
        public void Test_Greater_True()
        {
            Assert.True(new LocalDate(2019, Month.November, 5) > new LocalDate(2018, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.November, 5) > new LocalDate(2019, Month.October, 5));
            Assert.True(new LocalDate(2019, Month.November, 5) > new LocalDate(2019, Month.November, 4));
        }

        [Fact]
        public void Test_Greater_False()
        {
            Assert.False(new LocalDate(2018, Month.November, 5) > new LocalDate(2019, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.October, 5) > new LocalDate(2019, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.November, 4) > new LocalDate(2019, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.November, 5) > new LocalDate(2019, Month.November, 5));
        }

        [Fact]
        public void Test_GreaterOrEqual_True()
        {
            Assert.True(new LocalDate(2019, Month.November, 5) >= new LocalDate(2018, Month.November, 5));
            Assert.True(new LocalDate(2019, Month.November, 5) >= new LocalDate(2019, Month.October, 5));
            Assert.True(new LocalDate(2019, Month.November, 5) >= new LocalDate(2019, Month.November, 4));
            Assert.True(new LocalDate(2019, Month.November, 5) >= new LocalDate(2019, Month.November, 5));
        }

        [Fact]
        public void Test_GreaterOrEqual_False()
        {
            Assert.False(new LocalDate(2018, Month.November, 5) >= new LocalDate(2019, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.October, 5) >= new LocalDate(2019, Month.November, 5));
            Assert.False(new LocalDate(2019, Month.November, 4) >= new LocalDate(2019, Month.November, 5));
        }

        [Fact]
        public void Test_GetHashCode_Equal()
        {
            LocalDate localDate1 = new LocalDate(2019, Month.November, 5);
            LocalDate localDate2 = new LocalDate(2019, Month.November, 5);

            int hashCode1 = localDate1.GetHashCode();
            int hashCode2 = localDate2.GetHashCode();

            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Test_GetHashCode_NotEqual()
        {
            LocalDate localDate1 = new LocalDate(2019, Month.November, 5);
            LocalDate localDate2 = new LocalDate(2019, Month.October, 5);

            int hashCode1 = localDate1.GetHashCode();
            int hashCode2 = localDate2.GetHashCode();

            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}
