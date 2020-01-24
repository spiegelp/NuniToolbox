using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converter to map a boolean to a <see cref="Visibility" />.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The visibility value if the argument is false.
        /// </summary>
        public Visibility FalseValue { get; set; }

        /// <summary>
        /// The visibility value if the argument is true.
        /// </summary>
        public Visibility TrueValue { get; set; }

        /// <summary>
        /// Creates a new <see cref="BoolToVisibilityConverter" />.
        /// </summary>
        public BoolToVisibilityConverter() : this(Visibility.Collapsed, Visibility.Visible) { }

        /// <summary>
        /// Creates a new <see cref="BoolToVisibilityConverter" />.
        /// </summary>
        /// <param name="falseValue">The visibility value if the argument is false</param>
        /// <param name="trueValue">The visibility value if the argument is true</param>
        public BoolToVisibilityConverter(Visibility falseValue, Visibility trueValue)
        {
            FalseValue = falseValue;
            TrueValue = trueValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && ((bool)value) == true)
            {
                return TrueValue;
            }
            else
            {
                return FalseValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
