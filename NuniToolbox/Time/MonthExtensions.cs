using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Time
{
    public static class MonthExtensions
    {
        /// <summary>
        /// Returns the number of days for the month.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isLeapYear">Consider a leap year for February</param>
        /// <returns></returns>
        public static int Days(this Month month, bool isLeapYear = false)
        {
            switch (month)
            {
                case Month.January:
                case Month.March:
                case Month.May:
                case Month.July:
                case Month.August:
                case Month.October:
                case Month.December:
                    return 31;

                case Month.April:
                case Month.June:
                case Month.September:
                case Month.November:
                    return 30;

                case Month.February:
                    return isLeapYear ? 29 : 28;

                default:
                    throw new ArgumentException("invalid month argument", nameof(month));
            }
        }

        /// <summary>
        /// The number of the month starting by 1 for January.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int Number(this Month month)
        {
            int monthNumber = (int)month;

            if (monthNumber >= (int)Month.January && monthNumber <= (int)Month.December)
            {
                return (int)month;
            }
            else
            {
                throw new ArgumentException("invalid month argument", nameof(month));
            }
        }

        /// <summary>
        /// The full name of the month in the current culture.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string Name(this Month month)
        {
            return month.Name(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// The full name of the month in the specified culture.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string Name(this Month month, CultureInfo culture)
        {
            return FormatMonth(month, "MMMM", culture);
        }

        /// <summary>
        /// The short name of the month in the current culture.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string ShortName(this Month month)
        {
            return ShortName(month, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// The short name of the month in the specified culture.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ShortName(this Month month, CultureInfo culture)
        {
            return FormatMonth(month, "MMM", culture);
        }

        private static string FormatMonth(this Month month, string format, CultureInfo culture)
        {
            // use an arbitrary DateTime in the specified month and format it only with the full name of the month
            DateTime dateTime = new DateTime(2019, month.Number(), 1);

            return dateTime.ToString(format, culture);
        }
    }
}
