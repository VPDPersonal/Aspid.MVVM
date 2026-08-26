// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="RoundNumberConverter"/> drops the fraction.
    /// </summary>
    public enum RoundMode
    {
        /// <summary>
        /// To the nearest; which side an exact half lands on is the converter's own rule.
        /// </summary>
        Round,

        /// <summary>
        /// Toward negative infinity.
        /// </summary>
        Floor,

        /// <summary>
        /// Toward positive infinity.
        /// </summary>
        Ceil,

        /// <summary>
        /// Toward zero.
        /// </summary>
        Truncate,
    }
}
