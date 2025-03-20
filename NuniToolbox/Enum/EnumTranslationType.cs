namespace NuniToolbox.Enum;

/// <summary>
/// The translation types for an enum value.
/// </summary>
public enum EnumTranslationType : byte
{
    /// <summary>
    /// A default (fallback) display string.
    /// </summary>
    Default,

    /// <summary>
    /// A singular display string.
    /// </summary>
    Singular,

    /// <summary>
    /// A plural display string.
    /// </summary>
    Plural,

    /// <summary>
    /// An abbreviated display string.
    /// </summary>
    Abbreviation
}
