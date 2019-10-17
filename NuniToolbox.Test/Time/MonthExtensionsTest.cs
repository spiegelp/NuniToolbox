using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Time;

namespace NuniToolbox.Test.Time
{
    public class MonthExtensionsTest
    {
        [Theory]
        [InlineData(Month.January, 31)]
        [InlineData(Month.January, 31, true)]
        [InlineData(Month.February, 28)]
        [InlineData(Month.February, 29, true)]
        [InlineData(Month.March, 31)]
        [InlineData(Month.March, 31, true)]
        [InlineData(Month.April, 30)]
        [InlineData(Month.April, 30, true)]
        [InlineData(Month.May, 31)]
        [InlineData(Month.May, 31, true)]
        [InlineData(Month.June, 30)]
        [InlineData(Month.June, 30, true)]
        [InlineData(Month.July, 31)]
        [InlineData(Month.July, 31, true)]
        [InlineData(Month.August, 31)]
        [InlineData(Month.August, 31, true)]
        [InlineData(Month.September, 30)]
        [InlineData(Month.September, 30, true)]
        [InlineData(Month.October, 31)]
        [InlineData(Month.October, 31, true)]
        [InlineData(Month.November, 30)]
        [InlineData(Month.November, 30, true)]
        [InlineData(Month.December, 31)]
        [InlineData(Month.December, 31, true)]
        public void Test_Days_Ok(Month month, int expectedDays, bool isLeapYear = false)
        {
            Assert.Equal(expectedDays, month.Days(isLeapYear));
        }

        [Theory]
        [InlineData(Month.January, 1)]
        [InlineData(Month.February, 2)]
        [InlineData(Month.March, 3)]
        [InlineData(Month.April, 4)]
        [InlineData(Month.May, 5)]
        [InlineData(Month.June, 6)]
        [InlineData(Month.July, 7)]
        [InlineData(Month.August, 8)]
        [InlineData(Month.September, 9)]
        [InlineData(Month.October, 10)]
        [InlineData(Month.November, 11)]
        [InlineData(Month.December, 12)]
        public void Test_Number_Ok(Month month, int expectedNumber)
        {
            Assert.Equal(expectedNumber, month.Number());
        }

        [Fact]
        public void Test_Name_Ok()
        {
            Month month = Month.March;
            CultureInfo enCultureInfo = CultureInfo.CreateSpecificCulture("en");
            CultureInfo deCultureInfo = CultureInfo.CreateSpecificCulture("de");

            Assert.Equal("March", month.Name(enCultureInfo));
            Assert.Equal("März", month.Name(deCultureInfo));
        }

        [Fact]
        public void Test_ShortName_Ok()
        {
            Month month = Month.March;
            CultureInfo enCultureInfo = CultureInfo.CreateSpecificCulture("en");
            CultureInfo deCultureInfo = CultureInfo.CreateSpecificCulture("de");

            Assert.Equal("Mar", month.ShortName(enCultureInfo));
            Assert.Equal("Mrz", month.ShortName(deCultureInfo));
        }
    }
}
