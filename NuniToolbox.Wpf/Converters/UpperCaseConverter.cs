using System.Globalization;
using System.Windows.Data;

namespace NuniToolbox.Wpf.Converters;

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
        if (value is string str)
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
