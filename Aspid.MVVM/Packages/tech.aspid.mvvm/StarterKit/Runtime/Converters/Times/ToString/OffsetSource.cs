// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The offset <see cref="DateTimeOffsetFormatConverter"/> shows a moment at.
    /// </summary>
    public enum OffsetSource
    {
        /// <summary>
        /// The offset the moment arrived with.
        /// </summary>
        AsGiven,

        /// <summary>
        /// The player's own time zone.
        /// </summary>
        Local,

        /// <summary>
        /// A configured fixed offset.
        /// </summary>
        Override,
    }
}
