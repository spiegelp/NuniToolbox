using System;
using System.Collections.Generic;
using System.Text;

namespace NuniToolbox.Time
{
    /// <summary>
    /// Represents a time without any time zone information as an immutable struct.
    /// Methods of <see cref="LocalTime" /> may depend on <see cref="DateTime" /> to reuse its logic.
    /// Therefore <see cref="LocalTime" /> can be seen as some kind of wrapper around <see cref="DateTime" /> providing only local time relevant APIs.
    /// </summary>
    public struct LocalTime
    {
        private readonly int m_hours;
        private readonly int m_minutes;
        private readonly int m_seconds;
        private readonly int m_milliseconds;

        /// <summary>
        /// Returns the hours component.
        /// </summary>
        public int Hours
        {
            get
            {
                return m_hours;
            }
        }

        /// <summary>
        /// Returns the minutes component.
        /// </summary>
        public int Minutes
        {
            get
            {
                return m_minutes;
            }
        }

        /// <summary>
        /// Returns the milliseconds component.
        /// </summary>
        public int Milliseconds
        {
            get
            {
                return m_milliseconds;
            }
        }

        /// <summary>
        /// Returns the seconds component.
        /// </summary>
        public int Seconds
        {
            get
            {
                return m_seconds;
            }
        }

        /// <summary>
        /// Create a new <see cref="LocalTime" />.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        public LocalTime(int hours, int minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Create a new <see cref="LocalTime" />.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public LocalTime(int hours, int minutes, int seconds) : this(hours, minutes, seconds, 0) { }

        /// <summary>
        /// Create a new <see cref="LocalTime" />.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="milliseconds"></param>
        public LocalTime(int hours, int minutes, int seconds, int milliseconds)
        {
            CheckHours(hours);
            CheckMinutes(minutes);
            CheckSeconds(seconds);
            CheckMilliseconds(milliseconds);

            m_hours = hours;
            m_minutes = minutes;
            m_seconds = seconds;
            m_milliseconds = milliseconds;
        }

        /// <summary>
        /// Create a new <see cref="LocalTime" /> out of the time components of the <see cref="DateTime" />.
        /// </summary>
        /// <param name="dateTime"></param>
        public LocalTime(DateTime dateTime)
        {
            m_hours = dateTime.Hour;
            m_minutes = dateTime.Minute;
            m_seconds = dateTime.Second;
            m_milliseconds = dateTime.Millisecond;
        }

        /// <summary>
        /// Converts this <see cref="LocalTime" /> into a <see cref="DateTime" />.
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return DateTime.Today
                .AddHours(m_hours)
                .AddMinutes(m_minutes)
                .AddSeconds(m_seconds)
                .AddMilliseconds(m_milliseconds);
        }

        private static void CheckHours(int hours)
        {
            if (hours < 0 || hours > 23)
            {
                throw new ArgumentException($"{hours} must be between 0 and 23");
            }
        }

        private static void CheckMinutes(int minutes)
        {
            if (minutes < 0 || minutes > 59)
            {
                throw new ArgumentException($"{minutes} must be between 0 and 59");
            }
        }

        private static void CheckSeconds(int seconds)
        {
            if (seconds < 0 || seconds > 59)
            {
                throw new ArgumentException($"{seconds} must be between 0 and 59");
            }
        }

        private static void CheckMilliseconds(int milliseconds)
        {
            if (milliseconds < 0 || milliseconds > 999)
            {
                throw new ArgumentException($"{milliseconds} must be between 0 and 999");
            }
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> with the specified hours.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public LocalTime WithHours(int hours)
        {
            CheckHours(hours);

            return new LocalTime(hours, m_minutes, m_seconds, m_milliseconds);
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> with the specified minutes.
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public LocalTime WithMinutes(int minutes)
        {
            CheckMinutes(minutes);

            return new LocalTime(m_hours, minutes, m_seconds, m_milliseconds);
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> with the specified seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public LocalTime WithSeconds(int seconds)
        {
            CheckSeconds(seconds);

            return new LocalTime(m_hours, m_minutes, seconds, m_milliseconds);
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> with the specified milliseconds.
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public LocalTime WithMilliseconds(int milliseconds)
        {
            CheckMilliseconds(milliseconds);

            return new LocalTime(m_hours, m_minutes, m_seconds, milliseconds);
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> by adding the specified amount of hours.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public LocalTime AddHours(int hours)
        {
            /*if (hours == 0)
            {
                return new LocalTime(m_hours, m_minutes, m_seconds, m_milliseconds);
            }

            int newHours = Math.Abs(m_hours + hours) % 24;

            if (hours < 0)
            {
                newHours = 24 - newHours;
            }

            return new LocalTime(newHours, m_minutes, m_seconds, m_milliseconds);*/

            if (hours == 0)
            {
                return new LocalTime(m_hours, m_minutes, m_seconds, m_milliseconds);
            }

            return new LocalTime(ToDateTime().AddHours(hours));
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> by adding the specified amount of minutes.
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public LocalTime AddMinutes(int minutes)
        {
            if (minutes == 0)
            {
                return new LocalTime(m_hours, m_minutes, m_seconds, m_milliseconds);
            }

            return new LocalTime(ToDateTime().AddMinutes(minutes));
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> by adding the specified amount of seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public LocalTime AddSeconds(int seconds)
        {
            if (seconds == 0)
            {
                return new LocalTime(m_hours, m_minutes, m_seconds, m_milliseconds);
            }

            return new LocalTime(ToDateTime().AddSeconds(seconds));
        }

        /// <summary>
        /// Returns a new <see cref="LocalTime" /> by adding the specified amount of milliseconds.
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public LocalTime AddMilliseconds(int milliseconds)
        {
            if (milliseconds == 0)
            {
                return new LocalTime(m_hours, m_minutes, m_seconds, m_milliseconds);
            }

            return new LocalTime(ToDateTime().AddMilliseconds(milliseconds));
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is LocalTime otherLocalTime)
            {
                return this == otherLocalTime;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int hashCode = 1365636279;
            hashCode = hashCode * -1521134295 + m_hours.GetHashCode();
            hashCode = hashCode * -1521134295 + m_minutes.GetHashCode();
            hashCode = hashCode * -1521134295 + m_seconds.GetHashCode();
            hashCode = hashCode * -1521134295 + m_milliseconds.GetHashCode();

            return hashCode;
        }

        /// <summary>
        /// Formats this <see cref="LocalTime"/> as ISO 8601 compliant time string (HH:mm:ss.fff -> 02:04:08.512).
        /// </summary>
        /// <returns></returns>
        public string ToIsoString()
        {
            return string.Format("{0}:{1}:{2}.{3}", m_hours.ToString("00"), m_minutes.ToString("00"), m_seconds.ToString("00"), m_milliseconds.ToString("000"));
        }

        /// <summary>
        /// Formats this <see cref="LocalTime"/> as ISO 8601 compliant time string (HH:mm:ss.fff -> 02:04:08.512).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToIsoString();
        }

        /// <summary>
        /// Formats this <see cref="LocalTime"/> using the specified format.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return ToDateTime().ToString(format);
        }

        /// <summary>
        /// Formats this <see cref="LocalTime"/> using the specified format provider.
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return ToDateTime().ToString(formatProvider);
        }


        /// <summary>
        /// Formats this <see cref="LocalTime"/> using the specified format and format provider.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToDateTime().ToString(format, formatProvider);
        }

        public static bool operator ==(LocalTime localTime, LocalTime otherLocalTime)
        {
            return localTime.m_hours == otherLocalTime.m_hours
                && localTime.m_minutes == otherLocalTime.m_minutes
                && localTime.m_seconds == otherLocalTime.m_seconds
                && localTime.m_milliseconds == otherLocalTime.m_milliseconds;
        }

        public static bool operator !=(LocalTime localTime, LocalTime otherLocalTime)
        {
            return !(localTime == otherLocalTime);
        }
    }
}
