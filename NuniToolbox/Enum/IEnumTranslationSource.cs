using System.Globalization;

namespace NuniToolbox.Enum;

/// <summary>
/// The interface for a translation source which will be able to translate an enum value into a display string.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEnumTranslationSource<T> where T : struct, IComparable, IConvertible, IFormattable
{
    /// <summary>
    /// The type of the enum.
    /// </summary>
    Type EnumType { get; }

    /// <summary>
    /// The language of the display strings.
    /// </summary>
    CultureInfo Language { get; }

    /// <summary>
    /// Returns the display string of the specified enum value for the specified use case.
    /// </summary>
    /// <param name="enumValue"></param>
    /// <param name="translationType"></param>
    /// <returns></returns>
    string GetStringForValue(T enumValue, EnumTranslationType translationType = EnumTranslationType.Default);
}
