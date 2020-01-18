using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class NullToBoolConverterTest
    {
        [Theory]
        [InlineData(true, null, true)]
        [InlineData(true, 2, false)]
        [InlineData(false, null, false)]
        [InlineData(false, 2, true)]
        [InlineData(true, "", true)]
        [InlineData(true, "    ", true)]
        [InlineData(true, "2", false)]
        public void Test_Convert_Ok(bool nullValue, object value, bool expected)
        {
            NullToBoolConverter converter = new NullToBoolConverter(nullValue);

            Assert.Equal(expected, converter.Convert(value, typeof(bool), null, CultureInfo.InvariantCulture));
        }
    }
}
