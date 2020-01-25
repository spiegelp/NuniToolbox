using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class IndexPlusOneConverterTest
    {
        [Theory]
        [InlineData(2, 3)]
        [InlineData(4L, 5L)]
        [InlineData(null, null)]
        [InlineData("test", null)]
        public void Test_Convert_Ok(object value, object expected)
        {
            if (expected == null)
            {
                expected = Binding.DoNothing;
            }

            IndexPlusOneConverter converter = new IndexPlusOneConverter();

            Assert.Equal(expected, converter.Convert(value, value?.GetType(), null, CultureInfo.InvariantCulture));
        }
    }
}
