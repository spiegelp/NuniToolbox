using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class BoolNotConverterTest
    {
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(2, false)]
        [InlineData(null, false)]
        public void Test_Convert_Ok(object value, bool expected)
        {
            BoolNotConverter converter = new BoolNotConverter();

            Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(2, false)]
        [InlineData(null, false)]
        public void Test_ConvertBack_Ok(object value, bool expected)
        {
            BoolNotConverter converter = new BoolNotConverter();

            Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
        }
    }
}
