// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="RoundNumberConverter"/> drops the fraction.
    /// </summary>
    public enum RoundMode
    {
        /// <summary>
        /// To the nearest. Which way an exact half goes is the converter's midpoint rule, not part of
        /// this choice.
        /// </summary>
        Round,

        /// <summary>
        /// Towards negative infinity.
        /// </summary>
        Floor,

        /// <summary>
        /// Towards positive infinity.
        /// </summary>
        Ceil,

        /// <summary>
        /// Towards zero.
        /// </summary>
        Truncate,
    }
}
