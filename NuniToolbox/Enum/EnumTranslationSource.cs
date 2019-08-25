using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Enum
{
    /// <summary>
    /// The abstract base class for a translation source which will be able to translate an enum value into a display string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EnumTranslationSource<T> : IEnumTranslationSource<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// The type of the enum.
        /// </summary>
        public Type EnumType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// The language of the display strings.
        /// </summary>
        public CultureInfo Language { get; private set; }

        /// <summary>
        /// Creates a new <see cref="EnumTranslationSource" />.
        /// </summary>
        public EnumTranslationSource() : this(null) { }

        /// <summary>
        /// Creates a new <see cref="EnumTranslationSource" />.
        /// </summary>
        /// <param name="language"></param>
        public EnumTranslationSource(CultureInfo language)
        {
            if (!EnumType.IsEnum)
            {
                throw new ArgumentException("generic argument T must be an enum");
            }

            Language = language ?? CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Returns the display string of the specified enum value for the specified use case.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="translationType"></param>
        /// <returns></returns>
        public abstract string GetStringForValue(T enumValue, EnumTranslationType translationType = EnumTranslationType.Default);
    }
}
