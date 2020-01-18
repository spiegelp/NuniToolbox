using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converter to apply a boolean "and" operation.
    /// </summary>
    public class BoolAndConverter : IMultiValueConverter
    {
        /// <summary>
        /// Creates a new <see cref="BoolAndConverter" />.
        /// </summary>
        public BoolAndConverter() { }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool[] booleanValues = values.Select(value => value as bool?)
                .Where(b => b.HasValue)
                .Select(b => b.Value)
                .ToArray();

            if (booleanValues.Length > 0)
            {
                bool result = booleanValues[0];

                for (int i = 1; i < booleanValues.Length && result; i++)
                {
                    result = result && booleanValues[i];
                }

                return result;
            }
            else
            {
                return false;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
