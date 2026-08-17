// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="StringEmptyToBoolConverter"/> counts as an absent string.
    /// </summary>
    /// <remarks>
    /// <see cref="NullOrEmpty"/> is declared first so that converters serialized before this
    /// setting existed keep the behaviour they were authored with.
    /// </remarks>
    public enum StringEmptiness
    {
        /// <summary>
        /// The string is <see langword="null"/> or has no characters.
        /// </summary>
        NullOrEmpty,

        /// <summary>
        /// The string is <see langword="null"/>; an empty string counts as present.
        /// </summary>
        Null,

        /// <summary>
        /// The string is <see langword="null"/>, empty, or made up of whitespace only.
        /// </summary>
        NullOrWhiteSpace,
    }
}
