using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converter to apply boolean "and" operation and map to a <see cref="Visibility" />.
    /// </summary>
    public class BoolOrToVisibilityConverter : IMultiValueConverter
    {
        private readonly BoolOrConverter m_booleanOrConverter;
        private readonly BoolToVisibilityConverter m_boolToVisibilityConverter;

        /// <summary>
        /// The visibility value if the argument is false.
        /// </summary>
        public Visibility FalseValue
        {
            get
            {
                return m_boolToVisibilityConverter.FalseValue;
            }

            set
            {
                m_boolToVisibilityConverter.FalseValue = value;
            }
        }

        /// <summary>
        /// The visibility value if the argument is true.
        /// </summary>
        public Visibility TrueValue
        {
            get
            {
                return m_boolToVisibilityConverter.TrueValue;
            }

            set
            {
                m_boolToVisibilityConverter.TrueValue = value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="BoolOrToVisibilityConverter" />.
        /// </summary>
        public BoolOrToVisibilityConverter() : this(Visibility.Collapsed, Visibility.Visible) { }

        /// <summary>
        /// Creates a new <see cref="BoolOrToVisibilityConverter" />.
        /// </summary>
        /// <param name="falseValue">The visibility value if the argument is false</param>
        /// <param name="trueValue">The visibility value if the argument is true</param>
        public BoolOrToVisibilityConverter(Visibility falseValue, Visibility trueValue)
        {
            m_booleanOrConverter = new BoolOrConverter();
            m_boolToVisibilityConverter = new BoolToVisibilityConverter();

            FalseValue = falseValue;
            TrueValue = trueValue;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)m_booleanOrConverter.Convert(values, typeof(bool), parameter, culture);

            return m_boolToVisibilityConverter.Convert(b, targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
