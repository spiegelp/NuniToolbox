using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converter to apply a boolean "not" operation.
    /// </summary>
    public class BoolNotConverter : IValueConverter
    {
        /// <summary>
        /// Creates a new <see cref="BoolNotConverter" />.
        /// </summary>
        public BoolNotConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertValue(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertValue(value);
        }

        private object ConvertValue(object value)
        {
            if (value is bool b)
            {
                return !b;
            }
            else
            {
                return false;
            }
        }
    }
}
