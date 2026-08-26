// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="WrapNumberConverter"/> folds a value back into its range.
    /// </summary>
    public enum NumberWrapMode
    {
        /// <summary>
        /// Past the end, start again from the beginning.
        /// </summary>
        Repeat,

        /// <summary>
        /// Past the end, travel back toward the beginning.
        /// </summary>
        PingPong,
    }
}
