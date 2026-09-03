// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="StringMatchToBoolConverter"/> compares a bound string with the authored one.
    /// </summary>
    public enum StringMatchMode
    {
        /// <summary>
        /// The whole string must match.
        /// </summary>
        Equals,

        /// <summary>
        /// The string must contain the authored text.
        /// </summary>
        Contains,

        /// <summary>
        /// The string must begin with the authored text.
        /// </summary>
        StartsWith,

        /// <summary>
        /// The string must end with the authored text.
        /// </summary>
        EndsWith,
    }
}
