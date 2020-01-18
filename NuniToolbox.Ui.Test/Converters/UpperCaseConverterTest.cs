using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class UpperCaseConverterTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("  ", "  ")]
        [InlineData(".NET Core", ".NET CORE")]
        [InlineData("Schließen", "SCHLIESSEN")]
        [InlineData(2, null)]
        [InlineData(null, null)]
        public void Test_Convert_Ok(object str, string expected)
        {
            UpperCaseConverter converter = new UpperCaseConverter();

            Assert.Equal(expected, converter.Convert(str, typeof(string), null, CultureInfo.InvariantCulture));
        }
    }
}
