// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The casing <see cref="TextCaseConverter"/> applies.
    /// </summary>
    /// <remarks>
    /// New members are appended rather than inserted: the order is the serialized value, so moving
    /// one silently rewrites every converter already authored in a scene.
    /// </remarks>
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

        /// <summary>
        /// The first letter of every sentence upper case, the rest lower. A sentence ends at a full
        /// stop, an exclamation mark or a question mark.
        /// </summary>
        Sentence,

        /// <summary>
        /// Every upper-case letter lowered and every lower-case letter raised.
        /// </summary>
        Invert,
    }
}
