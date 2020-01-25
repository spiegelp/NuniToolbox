using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Adds 1 to an int or long for converting zero based indexes into human readable indexes.
    /// </summary>
    public class IndexPlusOneConverter : IValueConverter
    {
        /// <summary>
        /// Creates a new <see cref="IndexPlusOneConverter" />.
        /// </summary>
        public IndexPlusOneConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue + 1;
            }
            else if (value is long longValue)
            {
                return longValue + 1;
            }
            else
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
