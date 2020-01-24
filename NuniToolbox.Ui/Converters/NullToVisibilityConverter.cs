using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converts a null value to a <see cref="Visibility" />.
    /// </summary>
    public class NullToVisibilityConverter
    {
        /// <summary>
        /// The visibility value for a null value.
        /// </summary>
        public Visibility NullValue { get; set; }

        /// <summary>
        /// The visibility value for a not null value.
        /// </summary>
        public Visibility NotNullValue { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullToVisibilityConverter" />.
        /// </summary>
        public NullToVisibilityConverter() :this(Visibility.Collapsed, Visibility.Visible) { }

        /// <summary>
        /// Creates a new <see cref="NullToVisibilityConverter" />.
        /// </summary>
        /// <param name="nullValue">The visibility value for a null value</param>
        /// <param name="notNullValue">The visibility value for a not null value</param>
        public NullToVisibilityConverter(Visibility nullValue, Visibility notNullValue)
        {
            NullValue = nullValue;
            NotNullValue = notNullValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? NullValue : NotNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
