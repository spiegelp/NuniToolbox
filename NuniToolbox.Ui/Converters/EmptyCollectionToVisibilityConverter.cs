﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converts an empty collection to a <see cref="Visibility" />.
    /// </summary>
    public class EmptyCollectionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The visibility value of an empty or null collection.
        /// </summary>
        public Visibility EmptyValue { get; set; }

        /// <summary>
        /// The visibility value of a non empty collection.
        /// </summary>
        public Visibility NotEmptyValue { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmptyCollectionToVisibilityConverter" />.
        /// </summary>
        public EmptyCollectionToVisibilityConverter() : this(Visibility.Collapsed, Visibility.Visible) { }

        /// <summary>
        /// Creates a new <see cref="EmptyCollectionToVisibilityConverter" />.
        /// </summary>
        /// <param name="emptyValue">The visibility value of an empty or null collection</param>
        /// <param name="notEmptyValue">The visibility value of a non empty collection</param>
        public EmptyCollectionToVisibilityConverter(Visibility emptyValue, Visibility notEmptyValue)
        {
            EmptyValue = emptyValue;
            NotEmptyValue = notEmptyValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null
                    && ((value is ICollection && ((ICollection)value).Count > 0)
                        || (value is Collection<object> && ((Collection<object>)value).Count > 0)))
            {
                return NotEmptyValue;
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
