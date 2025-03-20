namespace NuniToolbox.Time;

/// <summary>
/// Represents a time without any time zone information as an immutable struct.
/// Methods of <see cref="LocalTime" /> may depend on <see cref="DateTime" /> to reuse its logic.
/// Therefore <see cref="LocalTime" /> can be seen as some kind of wrapper around <see cref="DateTime" /> providing only local time relevant APIs.
/// </summary>
public struct LocalTime
{
    /// <summary>
    /// The minimum <see cref="LocalTime" /> of 00:00:00.000.
    /// </summary>
    public static readonly LocalTime Min = new(0, 0, 0, 0);

    /// <summary>
    /// The maximum <see cref="LocalTime" /> of 23:59:59.999.
    /// </summary>
    public static readonly LocalTime Max = new(23, 59, 59, 999);

    private readonly int m_hour;
    private readonly int m_minute;
    private readonly int m_second;
    private readonly int m_millisecond;

    /// <summary>
    /// Returns the hour component.
    /// </summary>
    public int Hour
    {
        get
        {
            return m_hour;
        }
    }

    /// <summary>
    /// Returns the minute component.
    /// </summary>
    public int Minute
    {
        get
        {
            return m_minute;
        }
    }

    /// <summary>
    /// Returns the millisecond component.
    /// </summary>
    public int Millisecond
    {
        get
        {
            return m_millisecond;
        }
    }

    /// <summary>
    /// Returns the current time.
    /// </summary>
    public static LocalTime Now
    {
        get
        {
            return new LocalTime(DateTime.Now);
        }
    }

    /// <summary>
    /// Returns the second component.
    /// </summary>
    public int Second
    {
        get
        {
            return m_second;
        }
    }

    /// <summary>
    /// The time as total milliseconds after 00:00:00.000.
    /// </summary>
    public long TotalMilliseconds
    {
        get
        {
            return m_millisecond + m_second * 1000 + m_minute * 60 * 1000 + m_hour * 60 * 60 * 1000;
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

        m_hour = hours;
        m_minute = minutes;
        m_second = seconds;
        m_millisecond = milliseconds;
    }

    /// <summary>
    /// Create a new <see cref="LocalTime" /> out of the time components of the <see cref="DateTime" />.
    /// </summary>
    /// <param name="dateTime"></param>
    public LocalTime(DateTime dateTime)
    {
        m_hour = dateTime.Hour;
        m_minute = dateTime.Minute;
        m_second = dateTime.Second;
        m_millisecond = dateTime.Millisecond;
    }

    /// <summary>
    /// Converts this <see cref="LocalTime" /> into a <see cref="DateTime" />.
    /// </summary>
    /// <returns></returns>
    public DateTime ToDateTime()
    {
        return DateTime.Today
            .AddHours(m_hour)
            .AddMinutes(m_minute)
            .AddSeconds(m_second)
            .AddMilliseconds(m_millisecond);
    }

    private static void CheckHours(int hours)
    {
        int minHours = 0;
        int maxHours = 23;

        if (hours < minHours || hours > maxHours)
        {
            throw new ArgumentOutOfRangeException($"{nameof(hours)} must be between {minHours} and {maxHours}");
        }
    }

    private static void CheckMinutes(int minutes)
    {
        int minMinutes = 0;
        int maxMinutes = 59;

        if (minutes < minMinutes || minutes > maxMinutes)
        {
            throw new ArgumentOutOfRangeException($"{nameof(minutes)} must be between {minMinutes} and {maxMinutes}");
        }
    }

    private static void CheckSeconds(int seconds)
    {
        int minSeconds = 0;
        int maxSeconds = 59;

        if (seconds < minSeconds || seconds > maxSeconds)
        {
            throw new ArgumentOutOfRangeException($"{nameof(seconds)} must be between {minSeconds} and {maxSeconds}");
        }
    }

    private static void CheckMilliseconds(int milliseconds)
    {
        int minMilliseconds = 0;
        int maxMilliseconds = 999;

        if (milliseconds < minMilliseconds || milliseconds > maxMilliseconds)
        {
            throw new ArgumentOutOfRangeException($"{milliseconds} must be between {minMilliseconds} and {maxMilliseconds}");
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

        return new LocalTime(hours, m_minute, m_second, m_millisecond);
    }

    /// <summary>
    /// Returns a new <see cref="LocalTime" /> with the specified minutes.
    /// </summary>
    /// <param name="minutes"></param>
    /// <returns></returns>
    public LocalTime WithMinutes(int minutes)
    {
        CheckMinutes(minutes);

        return new LocalTime(m_hour, minutes, m_second, m_millisecond);
    }

    /// <summary>
    /// Returns a new <see cref="LocalTime" /> with the specified seconds.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public LocalTime WithSeconds(int seconds)
    {
        CheckSeconds(seconds);

        return new LocalTime(m_hour, m_minute, seconds, m_millisecond);
    }

    /// <summary>
    /// Returns a new <see cref="LocalTime" /> with the specified milliseconds.
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public LocalTime WithMilliseconds(int milliseconds)
    {
        CheckMilliseconds(milliseconds);

        return new LocalTime(m_hour, m_minute, m_second, milliseconds);
    }

    /// <summary>
    /// Returns a new <see cref="LocalTime" /> by adding the specified amount of hours.
    /// </summary>
    /// <param name="hours"></param>
    /// <returns></returns>
    public LocalTime AddHours(int hours)
    {
        if (hours == 0)
        {
            return new LocalTime(m_hour, m_minute, m_second, m_millisecond);
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
            return new LocalTime(m_hour, m_minute, m_second, m_millisecond);
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
            return new LocalTime(m_hour, m_minute, m_second, m_millisecond);
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
            return new LocalTime(m_hour, m_minute, m_second, m_millisecond);
        }

        return new LocalTime(ToDateTime().AddMilliseconds(milliseconds));
    }

    /// <summary>
    /// Combines this <see cref="LocalTime" /> with the specified <see cref="LocalDate" /> to a full <see cref="DateTime" />.
    /// </summary>
    /// <param name="localDate"></param>
    /// <returns></returns>
    public DateTime AtDate(LocalDate localDate)
    {
        return new DateTime(localDate.Year, localDate.Month.Number(), localDate.Day, m_hour, m_minute, m_second, m_millisecond);
    }

    public override bool Equals(object obj)
    {
        if (obj is LocalTime otherLocalTime)
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
        hashCode = hashCode * -1521134295 + m_hour.GetHashCode();
        hashCode = hashCode * -1521134295 + m_minute.GetHashCode();
        hashCode = hashCode * -1521134295 + m_second.GetHashCode();
        hashCode = hashCode * -1521134295 + m_millisecond.GetHashCode();

        return hashCode;
    }

    /// <summary>
    /// Formats this <see cref="LocalTime" /> as ISO 8601 compliant time string (HH:mm:ss.fff -> 02:04:08.512).
    /// </summary>
    /// <returns></returns>
    public string ToIsoString()
    {
        return string.Format("{0}:{1}:{2}.{3}", m_hour.ToString("00"), m_minute.ToString("00"), m_second.ToString("00"), m_millisecond.ToString("000"));
    }

    /// <summary>
    /// Formats this <see cref="LocalTime" /> as ISO 8601 compliant time string (HH:mm:ss.fff -> 02:04:08.512).
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return ToIsoString();
    }

    /// <summary>
    /// Formats this <see cref="LocalTime" /> using the specified format.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToString(string format)
    {
        return ToDateTime().ToString(format);
    }

    /// <summary>
    /// Formats this <see cref="LocalTime" /> using the specified format provider.
    /// </summary>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    public string ToString(IFormatProvider formatProvider)
    {
        return ToDateTime().ToString(formatProvider);
    }


    /// <summary>
    /// Formats this <see cref="LocalTime" /> using the specified format and format provider.
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
        return localTime.m_hour == otherLocalTime.m_hour
            && localTime.m_minute == otherLocalTime.m_minute
            && localTime.m_second == otherLocalTime.m_second
            && localTime.m_millisecond == otherLocalTime.m_millisecond;
    }

    public static bool operator !=(LocalTime localTime, LocalTime otherLocalTime)
    {
        return !(localTime == otherLocalTime);
    }

    public static bool operator <(LocalTime localTime, LocalTime otherLocalTime)
    {
        return localTime.TotalMilliseconds < otherLocalTime.TotalMilliseconds;
    }

    public static bool operator <=(LocalTime localTime, LocalTime otherLocalTime)
    {
        return localTime.TotalMilliseconds <= otherLocalTime.TotalMilliseconds;
    }

    public static bool operator >(LocalTime localTime, LocalTime otherLocalTime)
    {
        return localTime.TotalMilliseconds > otherLocalTime.TotalMilliseconds;
    }

    public static bool operator >=(LocalTime localTime, LocalTime otherLocalTime)
    {
        return localTime.TotalMilliseconds >= otherLocalTime.TotalMilliseconds;
    }
}
