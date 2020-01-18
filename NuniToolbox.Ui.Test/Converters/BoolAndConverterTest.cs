﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Ui.Converters;

namespace NuniToolbox.Ui.Test.Converters
{
    public class BoolAndConverterTest
    {
        [Fact]
        public void Test_Convert_Ok()
        {
            TestConvertOk(new object[] { true }, true);
            TestConvertOk(new object[] { true, true }, true);
            TestConvertOk(new object[] { true, true, true, true }, true);
            TestConvertOk(new object[] { false }, false);
            TestConvertOk(new object[] { false, true }, false);
            TestConvertOk(new object[] { true, false }, false);
            TestConvertOk(new object[] { true, true, false, true }, false);
            TestConvertOk(new object[] { true, new object(), true }, true);
            TestConvertOk(new object[] { true, new object(), false }, false);
        }

        private void TestConvertOk(object[] values, bool expected)
        {
            BoolAndConverter converter = new BoolAndConverter();

            Assert.Equal(expected, converter.Convert(values, typeof(bool), null, CultureInfo.InvariantCulture));
        }
    }
}
