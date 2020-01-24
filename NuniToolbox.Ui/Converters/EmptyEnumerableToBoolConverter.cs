using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Maps a null or empty <see cref="IEnumerable" /> to a boolean value.
    /// </summary>
    public class EmptyEnumerableToBoolConverter : IValueConverter
    {
        /// <summary>
        /// The boolean value, if the <see cref="IEnumerable" /> is empty or null.
        /// </summary>
        public bool EmptyValue { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmptyEnumerableToBoolConverter" />.
        /// </summary>
        public EmptyEnumerableToBoolConverter() { }

        /// <summary>
        /// Creates a new <see cref="EmptyEnumerableToBoolConverter" />.
        /// </summary>
        /// <param name="emptyValue">The boolean value, if the <see cref="IEnumerable" /> is empty or null</param>
        public EmptyEnumerableToBoolConverter(bool emptyValue)
        {
            EmptyValue = emptyValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable enumerable)
            {
                return enumerable.GetEnumerator().MoveNext() ? !EmptyValue : EmptyValue;
            }
            else
            {
                return EmptyValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
