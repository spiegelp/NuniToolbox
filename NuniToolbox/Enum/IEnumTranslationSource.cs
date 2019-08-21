using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Enum
{
    public interface IEnumTranslationSource<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        Type EnumType { get; }

        CultureInfo Language { get; }

        string GetStringForValue(T enumValue);

        string GetStringForValue(T enumValue, EnumTranslationType translationType);
    }
}
