using System.Globalization;

namespace NuniToolbox.Time;

/// <summary>
/// Contains extensions methods for the <see cref="Weekday" /> enum.
/// </summary>
public static class WeekdayExtensions
{
    /// <summary>
    /// The number of the weeday starting by 1 for Monday to 7 for Sunday.
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns></returns>
    public static int Number(this Weekday weekday)
    {
        int dayNumber = (int)weekday;

        if (dayNumber >= (int)Weekday.Monday && dayNumber <= (int)Weekday.Sunday)
        {
            return (int)weekday;
        }
        else
        {
            throw new ArgumentException("invalid weekday argument", nameof(weekday));
        }
    }

    /// <summary>
    /// Converts the <see cref="DayOfWeek" /> into a <see cref="Weekday" />.
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    public static Weekday WeekdayForDayOfWeek(DayOfWeek dayOfWeek)
    {
        int weekdayNumber = (int)dayOfWeek;

        if (dayOfWeek == DayOfWeek.Sunday)
        {
            // Sunday is 0 but should be 7 for an ISO compliant weekday
            weekdayNumber = 7;
        }

        Weekday weekday = (Weekday)weekdayNumber;

        // check for a valid value
        weekday.Number();

        return weekday;
    }

    /// <summary>
    /// The full name of the weekday in the current culture.
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns></returns>
    public static string Name(this Weekday weekday)
    {
        return weekday.Name(CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// The full name of the weekday in the specified culture.
    /// </summary>
    /// <param name="weekday"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public static string Name(this Weekday weekday, CultureInfo culture)
    {
        return FormatWeekday(weekday, "dddd", culture);
    }

    /// <summary>
    /// The short name of the weekday in the current culture.
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns></returns>
    public static string ShortName(this Weekday weekday)
    {
        return ShortName(weekday, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// The short name of the weekday in the specified culture.
    /// </summary>
    /// <param name="weekday"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public static string ShortName(this Weekday weekday, CultureInfo culture)
    {
        return FormatWeekday(weekday, "ddd", culture);
    }

    private static string FormatWeekday(this Weekday weekday, string format, CultureInfo culture)
    {
        // use an arbitrary DateTime with the specified weekday and format it only with the full name of the weekday
        //     -> start with a Sunday and add the specfied weekday value
        DateTime dateTime = new DateTime(2019, Month.November.Number(), 3).AddDays(weekday.Number());

        return dateTime.ToString(format, culture);
    }
}
