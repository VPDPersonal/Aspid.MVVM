// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which end <see cref="TruncateStringConverter"/> cuts from.
    /// </summary>
    public enum TruncateSide
    {
        /// <summary>
        /// Keep the start, cut the end.
        /// </summary>
        End,

        /// <summary>
        /// Keep the end, cut the start.
        /// </summary>
        Start,

        /// <summary>
        /// Keep both ends, cut the middle.
        /// </summary>
        Middle,
    }
}
