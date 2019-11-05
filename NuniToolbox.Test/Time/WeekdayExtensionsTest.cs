using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Time;

namespace NuniToolbox.Test.Time
{
    public class WeekdayExtensionsTest
    {
        [Theory]
        [InlineData(Weekday.Monday, 1)]
        [InlineData(Weekday.Tuesday, 2)]
        [InlineData(Weekday.Wednesday, 3)]
        [InlineData(Weekday.Thursday, 4)]
        [InlineData(Weekday.Friday, 5)]
        [InlineData(Weekday.Saturday, 6)]
        [InlineData(Weekday.Sunday, 7)]
        public void Test_Number_Ok(Weekday weekday, int expectedNumber)
        {
            Assert.Equal(expectedNumber, weekday.Number());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(8)]
        public void Test_Number_ArgumentException(int weekdayValue)
        {
            Assert.Throws<ArgumentException>(() => ((Weekday)weekdayValue).Number());
        }

        [Theory]
        [InlineData(Weekday.Monday, DayOfWeek.Monday)]
        [InlineData(Weekday.Tuesday, DayOfWeek.Tuesday)]
        [InlineData(Weekday.Wednesday, DayOfWeek.Wednesday)]
        [InlineData(Weekday.Thursday, DayOfWeek.Thursday)]
        [InlineData(Weekday.Friday, DayOfWeek.Friday)]
        [InlineData(Weekday.Saturday, DayOfWeek.Saturday)]
        [InlineData(Weekday.Sunday, DayOfWeek.Sunday)]
        public void Test_WeekdayForDayOfWeek_Ok(Weekday weekday, DayOfWeek dayOfWeek)
        {
            Assert.Equal(weekday, WeekdayExtensions.WeekdayForDayOfWeek(dayOfWeek));
        }

        [Fact]
        public void Test_Name_Ok()
        {
            Weekday weekday = Weekday.Tuesday;
            CultureInfo enCultureInfo = CultureInfo.CreateSpecificCulture("en");
            CultureInfo deCultureInfo = CultureInfo.CreateSpecificCulture("de");

            Assert.Equal("Tuesday", weekday.Name(enCultureInfo));
            Assert.Equal("Dienstag", weekday.Name(deCultureInfo));
        }

        [Fact]
        public void Test_ShortName_Ok()
        {
            Weekday weekday = Weekday.Tuesday;
            CultureInfo enCultureInfo = CultureInfo.CreateSpecificCulture("en");
            CultureInfo deCultureInfo = CultureInfo.CreateSpecificCulture("de");

            Assert.Equal("Tue", weekday.ShortName(enCultureInfo));
            Assert.Equal("Di", weekday.ShortName(deCultureInfo));
        }
    }
}
