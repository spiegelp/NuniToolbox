using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Maps a null or empty <see cref="IEnumerable" /> to a <see cref="Visibility" />.
    /// </summary>
    public class EmptyEnumerableVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The visibility value of an empty or null <see cref="IEnumerable" />.
        /// </summary>
        public Visibility EmptyValue { get; set; }

        /// <summary>
        /// The visibility value of a non empty <see cref="IEnumerable" />.
        /// </summary>
        public Visibility NotEmptyValue { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmptyEnumerableVisibilityConverter" />.
        /// </summary>
        public EmptyEnumerableVisibilityConverter() : this(Visibility.Collapsed, Visibility.Visible) { }

        /// <summary>
        /// Creates a new <see cref="EmptyEnumerableVisibilityConverter" />.
        /// </summary>
        /// <param name="emptyValue">The visibility value of an empty or null <see cref="IEnumerable" /></param>
        /// <param name="notEmptyValue">The visibility value of a non empty <see cref="IEnumerable" /></param>
        public EmptyEnumerableVisibilityConverter(Visibility emptyValue, Visibility notEmptyValue)
        {
            EmptyValue = emptyValue;
            NotEmptyValue = notEmptyValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable enumerable)
            {
                return enumerable.GetEnumerator().MoveNext() ? NotEmptyValue : EmptyValue;
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
