﻿using System.Globalization;
using System.Windows.Data;

namespace NuniToolbox.Wpf.Converters;

/// <summary>
/// Converter to apply a boolean "or" operation.
/// </summary>
public class BoolOrConverter : IMultiValueConverter
{
    /// <summary>
    /// Creates a new <see cref="BoolOrConverter" />.
    /// </summary>
    public BoolOrConverter() { }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        bool result = false;

        if (values is not null)
        {
            for (int i = 0; i < values.Length && !result; i++)
            {
                bool? b = values[i] as bool?;

                if (b.HasValue)
                {
                    result = result || b.Value;
                }
            }
        }

        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
