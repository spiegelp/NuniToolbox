using System.Globalization;
using System.Windows.Data;

namespace NuniToolbox.Wpf.Converters;

/// <summary>
/// Converts a null value to the specified NullValue, otherwise !NullValue.
/// A special logic for strings will be applied to handle empty or whitespace only strings like null values.
/// </summary>
public class NullToBoolConverter : IValueConverter
{
    /// <summary>
    /// The boolean value returned for null values.
    /// </summary>
    public bool NullValue { get; set; }

    /// <summary>
    /// Creates a new <see cref="NullToBoolConverter" />.
    /// </summary>
    public NullToBoolConverter() : this(false) { }

    /// <summary>
    /// Creates a new <see cref="NullToBoolConverter" />.
    /// </summary>
    /// <param name="nullValue">The boolean value returned for null values</param>
    public NullToBoolConverter(bool nullValue)
    {
        NullValue = nullValue;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return NullValue;
            }
            else
            {
                return !NullValue;
            }
        }
        else
        {
            if (value == null)
            {
                return NullValue;
            }
            else
            {
                return !NullValue;
            }
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
