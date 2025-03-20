using System.Globalization;

namespace NuniToolbox.Enum;

/// <summary>
/// Translator to translate enum values into display strings for specific languages and use cases (i.e. singular, plural, abbreviation).
/// This class is thread-safe.
/// First, register translations using the <see cref="RegisterTranslationSource{T}(IEnumTranslationSource{T})" /> method.
/// Second, get a display string using the <see cref="GetStringForValue{T}(T, CultureInfo, EnumTranslationType)" /> method.
/// </summary>
public class EnumTranslator
{
    private object m_lockObject = new();

    private readonly Dictionary<Type, IDictionary<string, object>> m_translationSourcesByType;

    /// <summary>
    /// Creates a new <see cref="EnumTranslator" />.
    /// </summary>
    public EnumTranslator()
    {
        m_translationSourcesByType = [];
    }

    /// <summary>
    /// Returns the display string of the specified enum value for the specified language and use case.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumValue"></param>
    /// <param name="language"></param>
    /// <param name="translationType"></param>
    /// <returns></returns>
    public string GetStringForValue<T>(T enumValue, CultureInfo language = null, EnumTranslationType translationType = EnumTranslationType.Default) where T : struct, IComparable, IConvertible, IFormattable
    {
        lock (m_lockObject)
        {
            Type type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException("generic argument T must be an enum");
            }

            IDictionary<string, object> translationSourcesByLanguage = m_translationSourcesByType[type];

            language ??= CultureInfo.InvariantCulture;

            string[] languageNames = GetHierarichalLanguageNamesDesc(language);

            for (int i = 0; i < languageNames.Length; i++)
            {
                if (translationSourcesByLanguage.TryGetValue(languageNames[i], out object rawTranslationSource))
                {
                    return ((IEnumTranslationSource<T>)rawTranslationSource).GetStringForValue(enumValue, translationType);
                }
            }

            throw new ArgumentException($"Display string for the specified language '{language.DisplayName}' and translation type '{translationType}' not found");
        }
    }

    /// <summary>
    /// Returns all enum values sorted by their display string for the specified language and use case.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="language"></param>
    /// <param name="translationType"></param>
    /// <returns></returns>
    public T[] GetValuesSortedByTranslation<T>(CultureInfo language = null, EnumTranslationType translationType = EnumTranslationType.Default) where T : struct, IComparable, IConvertible, IFormattable
    {
        lock (m_lockObject)
        {
            Type type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException("generic argument T must be an enum");
            }

            language ??= CultureInfo.InvariantCulture;

            List<EnumTranslationMapping<T>> mappings = [];
            Array arr = System.Enum.GetValues(type);

            for (int i = 0; i < arr.Length; i++)
            {
                T value = (T)arr.GetValue(i);
                mappings.Add(new EnumTranslationMapping<T>(value, GetStringForValue(value, language, translationType)));
            }

            return mappings
                .OrderBy(mapping => mapping.Translation, StringComparer.Create(language, true))
                .Select(mapping => mapping.EnumValue).ToArray();
        }
    }

    /// <summary>
    /// Registers a translation source to translate enum values into display strings.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="translationSource"></param>
    public void RegisterTranslationSource<T>(IEnumTranslationSource<T> translationSource) where T : struct, IComparable, IConvertible, IFormattable
    {
        lock (m_lockObject)
        {
            if (translationSource == null)
            {
                throw new ArgumentNullException("translationSource must not be null");
            }

            Type enumType = translationSource.EnumType;

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("generic argument T must be an enum");
            }

            if (!m_translationSourcesByType.TryGetValue(enumType, out IDictionary<string, object> value))
            {
                value = new Dictionary<string, object>();
                m_translationSourcesByType[enumType] = value;
            }

            string languageName = (translationSource.Language is not null ? translationSource.Language.Name : CultureInfo.InvariantCulture.Name).ToLower();

            value[languageName] = translationSource;
        }
    }

    private string[] SplitLanguageName(CultureInfo language)
    {
        return SplitLanguageName(language is not null ? language.Name : CultureInfo.InvariantCulture.Name);
    }

    private string[] SplitLanguageName(string languageName)
    {
        if (string.IsNullOrWhiteSpace(languageName) || languageName == CultureInfo.InvariantCulture.Name)
        {
            return [CultureInfo.InvariantCulture.Name];
        }

        List<string> parts = [];

        string[] split = languageName.Split('-');

        if (split.Length == 1)
        {
            parts.Add(split[0]);
        }
        else if (split.Length == 2)
        {
            parts.Add(split[0]);

            split = split[1].Split('/');

            if (split.Length == 1)
            {
                parts.Add(split[0]);
            }
            else if (split.Length == 2)
            {
                parts.Add(split[0]);
                parts.Add(split[1]);
            }
            else
            {
                throw new ArgumentException($"Not supported {nameof(languageName)} '{languageName}'");
            }
        }
        else
        {
            throw new ArgumentException($"Not supported {nameof(languageName)} '{languageName}'");
        }

        return parts.ToArray();
    }

    private string[] GetHierarichalLanguageNamesDesc(CultureInfo language)
    {
        string[] languageParts = SplitLanguageName(language);

        ISet<string> languageNames = new HashSet<string> { CultureInfo.InvariantCulture.Name };

        for (int i = 0; i < languageParts.Length; i++)
        {
            if (i == 0)
            {
                languageNames.Add(languageParts[0]);
            }
            else if (i == 1)
            {
                languageNames.Add($"{languageParts[0]}-{languageParts[1]}");
            }
            else if (i == 2)
            {
                languageNames.Add($"{languageParts[0]}-{languageParts[1]}/{languageParts[2]}");
            }
        }

        return languageNames
            .Select(languageName => languageName.ToLower())
            .OrderByDescending(languageName => languageName)
            .ToArray();
    }

    private class EnumTranslationMapping<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        public T EnumValue { get; set; }

        public string Translation { get; set; }

        public EnumTranslationMapping(T enumValue, string translation)
        {
            EnumValue = enumValue;
            Translation = translation;
        }
    }
}
