using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Time
{
    /// <summary>
    /// Represents a date without any time and time zone information as an immutable struct.
    /// Methods of <see cref="LocalDate" /> may depend on <see cref="DateTime" /> to reuse its logic.
    /// Therefore <see cref="LocalDate" /> can be seen as some kind of wrapper around <see cref="DateTime" /> providing only local date relevant APIs.
    /// </summary>
    public struct LocalDate
    {
        /// <summary>
        /// The minimum <see cref="LocalDate" /> of 0001-01-01.
        /// </summary>
        public static readonly LocalDate Min = new LocalDate(1, Month.January, 1);

        /// <summary>
        /// The maxmum <see cref="LocalDate" /> of 9999-12-31.
        /// </summary>
        public static readonly LocalDate Max = new LocalDate(9999, Month.December, 31);

        private readonly int m_year;
        private readonly Month m_month;
        private readonly int m_day;

        /// <summary>
        /// Returns the day component.
        /// </summary>
        public int Day
        {
            get
            {
                return m_day;
            }
        }

        /// <summary>
        /// Returns true, if this date is a 29th February of a leap year
        /// </summary>
        /// <returns></returns>
        public bool IsLeapDay
        {
            get
            {
                return YearUtil.IsLeapYear(m_year) && m_month == Month.February && m_day == Month.February.Days(true);
            }
        }

        /// <summary>
        /// Returns the month component.
        /// </summary>
        public Month Month
        {
            get
            {
                return m_month;
            }
        }

        /// <summary>
        /// Returns the current date.
        /// </summary>
        public static LocalDate Today
        {
            get
            {
                return new LocalDate(DateTime.Today);
            }
        }

        /// <summary>
        /// Returns the weekday of this date.
        /// </summary>
        public Weekday Weekday
        {
            get
            {
                return WeekdayExtensions.WeekdayForDayOfWeek(ToDateTime().DayOfWeek);
            }
        }

        /// <summary>
        /// Returns the year component.
        /// </summary>
        public int Year
        {
            get
            {
                return m_year;
            }
        }

        /// <summary>
        /// Creates a new <see cref="LocalDate" />.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public LocalDate(int year, Month month, int day)
        {
            CheckYear(year);
            CheckMonth(month);
            CheckDay(year, month, day);

            m_year = year;
            m_month = month;
            m_day = day;
        }

        /// <summary>
        /// Create a new <see cref="LocalDate" /> out of the date components of the <see cref="DateTime" />.
        /// </summary>
        /// <param name="dateTime"></param>
        public LocalDate(DateTime dateTime)
        {
            m_year = dateTime.Year;
            m_month = (Month)dateTime.Month;
            m_day = dateTime.Day;
        }

        /// <summary>
        /// Converts this <see cref="LocalDate" /> into a <see cref="DateTime" />.
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return new DateTime(m_year, m_month.Number(), m_day);
        }

        private static void CheckYear(int year)
        {
            int minYear = 1;
            int maxYear = 9999;

            if (year < minYear || year > maxYear)
            {
                throw new ArgumentOutOfRangeException($"{nameof(year)} must be between {minYear} and {maxYear}");
            }
        }

        private static void CheckMonth(Month month)
        {
            // Number() method checks the argument already
            month.Number();
        }

        private static void CheckDay(int year, Month month, int day)
        {
            int minDay = 1;
            int maxDay = month.Days(YearUtil.IsLeapYear(year));

            if (day < minDay || day > maxDay)
            {
                throw new ArgumentOutOfRangeException($"{nameof(day)} must be between {minDay} and {maxDay} for the month {month.Name(new CultureInfo("en-US"))}");
            }
        }

        /// <summary>
        /// Returns a new <see cref="LocalDate" /> with the specified year.
        /// If the day of month is invalid for the new year, it will be changed to the last valid day of the month.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public LocalDate WithYear(int year)
        {
            CheckYear(year);

            // adjust February 29 to February 28 if the new year is not a leap year
            Month month = m_month;
            int day = m_day;

            if (IsLeapDay && !YearUtil.IsLeapYear(year))
            {
                day = Month.February.Days(false);
            }

            return new LocalDate(year, month, day);
        }

        /// <summary>
        /// Returns a new <see cref="LocalDate" /> with the specified month.
        /// If the day of month is invalid for the new year, it will be changed to the last valid day of the month.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public LocalDate WithMonth(Month month)
        {
            CheckMonth(month);

            // adjust to the last valid day of the month, if the new month has fewer days than the old month
            int year = m_year;
            int day = m_day;

            int daysOfMonth = month.Days(YearUtil.IsLeapYear(m_year));

            if (daysOfMonth < day)
            {
                day = daysOfMonth;
            }

            return new LocalDate(year, month, day);
        }

        /// <summary>
        /// Returns a new <see cref="LocalDate" /> with the specified day.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public LocalDate WithDay(int day)
        {
            CheckDay(m_year, m_month, day);

            return new LocalDate(m_year, m_month, day);
        }

        /// <summary>
        /// Returns a new <see cref="LocalDate" /> by adding the specified amount of years.
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public LocalDate AddYears(int years)
        {
            if (years == 0)
            {
                return new LocalDate(m_year, m_month, m_day);
            }

            CheckYear(m_year + years);

            return new LocalDate(ToDateTime().AddYears(years));
        }

        /// <summary>
        /// Returns a new <see cref="LocalDate" /> by adding the specified amount of months.
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public LocalDate AddMonths(int months)
        {
            if (months == 0)
            {
                return new LocalDate(m_year, m_month, m_day);
            }

            return new LocalDate(ToDateTime().AddMonths(months));
        }

        /// <summary>
        /// Returns a new <see cref="LocalDate" /> by adding the specified amount of days.
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public LocalDate AddDays(int days)
        {
            if (days == 0)
            {
                return new LocalDate(m_year, m_month, m_day);
            }

            return new LocalDate(ToDateTime().AddDays(days));
        }

        /// <summary>
        /// Combines this <see cref="LocalDate" /> with the specified <see cref="LocalTime" /> to a full <see cref="DateTime" />.
        /// </summary>
        /// <param name="localDate"></param>
        /// <returns></returns>
        public DateTime AtTime(LocalTime localTime)
        {
            return new DateTime(m_year, m_month.Number(), m_day, localTime.Hour, localTime.Minute, localTime.Second, localTime.Millisecond);
        }

        /// <summary>
        /// Returns the first day (a Monday) of the week of this date.
        /// </summary>
        /// <returns></returns>
        public LocalDate FirstDayOfWeek()
        {
            return FirstDayOfWeek(this);
        }

        /// <summary>
        /// Returns the first day (a Monday) of the week of the specified <see cref="LocalDate" />.
        /// </summary>
        /// <param name="localDate"></param>
        /// <returns></returns>
        public static LocalDate FirstDayOfWeek(LocalDate localDate)
        {
            while (localDate.Weekday != Weekday.Monday)
            {
                localDate = localDate.AddDays(-1);
            }

            return localDate;
        }

        public override bool Equals(object obj)
        {
            if (obj is LocalDate otherLocalDate)
            {
                return this == otherLocalDate;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int hashCode = 854259334;
            hashCode = hashCode * -1521134295 + m_year.GetHashCode();
            hashCode = hashCode * -1521134295 + m_month.GetHashCode();
            hashCode = hashCode * -1521134295 + m_day.GetHashCode();

            return hashCode;
        }

        /// <summary>
        /// Formats this <see cref="LocalDate" /> as ISO 8601 compliant date string (yyyy-MM-dd -> 2019-11-05).
        /// </summary>
        /// <returns></returns>
        public string ToIsoString()
        {
            return string.Format("{0}-{1}-{2}", m_year.ToString("00"), m_month.Number().ToString("00"), m_day.ToString("00"));
        }

        /// <summary>
        /// Formats this <see cref="LocalDate" /> as ISO 8601 compliant date string (yyyy-MM-dd -> 2019-11-05).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToIsoString();
        }

        /// <summary>
        /// Formats this <see cref="LocalDate" /> using the specified format.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return ToDateTime().ToString(format);
        }

        /// <summary>
        /// Formats this <see cref="LocalDate" /> using the specified format provider.
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return ToDateTime().ToString(formatProvider);
        }


        /// <summary>
        /// Formats this <see cref="LocalDate" /> using the specified format and format provider.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToDateTime().ToString(format, formatProvider);
        }

        public static bool operator ==(LocalDate localDate, LocalDate otherLocalDate)
        {
            return localDate.m_year == otherLocalDate.m_year
                && localDate.m_month == otherLocalDate.m_month
                && localDate.m_day == otherLocalDate.m_day;
        }

        public static bool operator !=(LocalDate localDate, LocalDate otherLocalDate)
        {
            return !(localDate == otherLocalDate);
        }

        public static bool operator <(LocalDate localDate, LocalDate otherLocalDate)
        {
            return localDate.m_year < otherLocalDate.m_year
                || (localDate.m_year == otherLocalDate.m_year && localDate.m_month.Number() < otherLocalDate.m_month.Number())
                || (localDate.m_year == otherLocalDate.m_year && localDate.m_month.Number() == otherLocalDate.m_month.Number() && localDate.m_day < otherLocalDate.m_day);
        }

        public static bool operator <=(LocalDate localDate, LocalDate otherLocalDate)
        {
            return localDate < otherLocalDate || localDate == otherLocalDate;
        }

        public static bool operator >(LocalDate localDate, LocalDate otherLocalDate)
        {
            return localDate.m_year > otherLocalDate.m_year
                || (localDate.m_year == otherLocalDate.m_year && localDate.m_month.Number() > otherLocalDate.m_month.Number())
                || (localDate.m_year == otherLocalDate.m_year && localDate.m_month.Number() == otherLocalDate.m_month.Number() && localDate.m_day > otherLocalDate.m_day);
        }

        public static bool operator >=(LocalDate localDate, LocalDate otherLocalDate)
        {
            return localDate > otherLocalDate || localDate == otherLocalDate;
        }
    }
}
