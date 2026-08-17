// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="IndexToValueConverter{T}"/> does with an index outside the array.
    /// </summary>
    public enum IndexMode
    {
        /// <summary>
        /// Use the nearest end of the array.
        /// </summary>
        Clamp,

        /// <summary>
        /// Wrap around, so one past the end is the first entry.
        /// </summary>
        Wrap,

        /// <summary>
        /// Return the fallback.
        /// </summary>
        Fallback,
    }
}
