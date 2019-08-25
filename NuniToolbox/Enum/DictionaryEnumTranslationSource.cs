using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Enum
{
    /// <summary>
    /// Specific class for a translation source which build upon dictionaries to translate an enum value into a display string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DictionaryEnumTranslationSource<T> : EnumTranslationSource<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private IDictionary<EnumTranslationType, IDictionary<T, string>> m_translations;

        /// <summary>
        /// The abbreviations for the enum values.
        /// </summary>
        public IDictionary<T, string> AbbreviationTranslations
        {
            get
            {
                return GetTranslationsForType(EnumTranslationType.Abbreviation);
            }

            set
            {
                m_translations[EnumTranslationType.Abbreviation] = value;
            }
        }

        /// <summary>
        /// The default (fallback) display string for the enum values.
        /// </summary>
        public IDictionary<T, string> DefaultTranslations
        {
            get
            {
                return GetTranslationsForType(EnumTranslationType.Default);
            }

            set
            {
                m_translations[EnumTranslationType.Default] = value;
            }
        }

        /// <summary>
        /// The plural for the enum values.
        /// </summary>
        public IDictionary<T, string> PluralTranslations
        {
            get
            {
                return GetTranslationsForType(EnumTranslationType.Plural);
            }

            set
            {
                m_translations[EnumTranslationType.Plural] = value;
            }
        }

        /// <summary>
        /// The singular vor the enum values.
        /// </summary>
        public IDictionary<T, string> SingularTranslations
        {
            get
            {
                return GetTranslationsForType(EnumTranslationType.Singular);
            }

            set
            {
                m_translations[EnumTranslationType.Singular] = value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="DictionaryEnumTranslationSource" />.
        /// </summary>
        public DictionaryEnumTranslationSource() : this(null) { }

        /// <summary>
        /// Creates a new <see cref="DictionaryEnumTranslationSource" />.
        /// </summary>
        /// <param name="language"></param>
        public DictionaryEnumTranslationSource(CultureInfo language)
            : base(language)
        {
            m_translations = new Dictionary<EnumTranslationType, IDictionary<T, string>>();
        }

        private IDictionary<T, string> GetTranslationsForType(EnumTranslationType translationType)
        {
            m_translations.TryGetValue(translationType, out IDictionary<T, string> translations);

            return translations;
        }

        /// <summary>
        /// Returns the display string of the specified enum value for the specified use case.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="translationType"></param>
        /// <returns></returns>
        public override string GetStringForValue(T enumValue, EnumTranslationType translationType = EnumTranslationType.Default)
        {
            string translation = GetStringForValueInternal(enumValue, translationType);

            if (translation == null && translationType != EnumTranslationType.Default)
            {
                translation = GetStringForValueInternal(enumValue, EnumTranslationType.Default);
            }

            if (translation == null)
            {
                translation = enumValue.ToString();
            }

            return translation;
        }

        private string GetStringForValueInternal(T enumValue, EnumTranslationType translationType)
        {
            IDictionary<T, string> translations = GetTranslationsForType(translationType);

            if (translations != null)
            {
                translations.TryGetValue(enumValue, out string translation);

                return translation;
            }
            else
            {
                return null;
            }
        }
    }
}
