using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converts between <see cref="DateTime" /> and <see cref="DateTimeOffset" />.
    /// </summary>
    public class DateTimeOffsetToDateTimeConverter : IValueConverter
    {
        // https://msdn.microsoft.com/en-us/library/bb546101(v=vs.110).aspx
        // https://msdn.microsoft.com/en-us/library/bb397769(v=vs.110).aspx

        /// <summary>
        /// Creates a new <see cref="DateTimeOffsetToDateTimeConverter" />.
        /// </summary>
        public DateTimeOffsetToDateTimeConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTimeOffset)
            {
                /*
                When you retrieve a DateTime value using the DateTimeOffset.LocalDateTime property, the property's get
                accessor first converts the DateTimeOffset value to UTC, then converts it to local time by calling the
                ToLocalTime method. This means that you can retrieve a value from the DateTimeOffset.LocalDateTime property
                to perform a time zone conversion at the same time that you perform a type conversion. It also means that
                the local time zone's adjustment rules are applied in performing the conversion.

                To convert UTC to local time, call the ToLocalTime method of the DateTime object whose time you want to convert.
                */

                return ((DateTimeOffset)value).LocalDateTime;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTime)
            {
                // local DateTime to a DateTime in UTC
                DateTime dateTimeUtc = TimeZoneInfo.ConvertTimeToUtc((DateTime)value);

                // DateTime with local time to a DateTimeOffset with the same time and the corresponding offset
                DateTimeOffset dateTimeOffsetUtc = dateTimeUtc;

                return dateTimeOffsetUtc;
            }
            else
            {
                return null;
            }
        }
    }
}
