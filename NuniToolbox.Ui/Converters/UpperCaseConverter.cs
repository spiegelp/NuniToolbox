using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converts a string to upper case.
    /// </summary>
    public class UpperCaseConverter : IValueConverter
    {
        /// <summary>
        /// Creates a new <see cref="UpperCaseConverter" />.
        /// </summary>
        public UpperCaseConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string str)
            {
                return str.ToUpper(culture).Replace("ß", "SS");
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
