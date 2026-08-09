// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The casing <see cref="TextCaseConverter"/> applies.
    /// </summary>
    public enum TextCase
    {
        /// <summary>
        /// Every letter upper case.
        /// </summary>
        Upper,

        /// <summary>
        /// Every letter lower case.
        /// </summary>
        Lower,

        /// <summary>
        /// The first letter of the string upper case, the rest untouched.
        /// </summary>
        FirstUpper,

        /// <summary>
        /// The first letter of every word upper case, the rest lower.
        /// </summary>
        Title,
    }
}
