using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NuniToolbox.Enum
{
    public class DictionaryEnumTranslationSource<T> : EnumTranslationSource<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private IDictionary<EnumTranslationType, IDictionary<T, string>> m_translations;

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

        public DictionaryEnumTranslationSource() : this(null) { }

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

        public override string GetStringForValue(T enumValue, EnumTranslationType translationType)
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
