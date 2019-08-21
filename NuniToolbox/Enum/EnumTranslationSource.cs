using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Enum
{
    public abstract class EnumTranslationSource<T> : IEnumTranslationSource<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        public Type EnumType
        {
            get
            {
                return typeof(T);
            }
        }

        public CultureInfo Language { get; private set; }

        public EnumTranslationSource() : this(null) { }

        public EnumTranslationSource(CultureInfo language)
        {
            if (!EnumType.IsEnum)
            {
                throw new ArgumentException("generic argument T must be an enum");
            }

            Language = language ?? CultureInfo.InvariantCulture;
        }

        public virtual string GetStringForValue(T enumValue)
        {
            return GetStringForValue(enumValue, EnumTranslationType.Default);
        }

        public abstract string GetStringForValue(T enumValue, EnumTranslationType translationType);
    }
}
