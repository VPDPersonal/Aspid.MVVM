// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The moment <see cref="DateTimeCompareConverter"/> compares against.
    /// </summary>
    public enum ReferenceSource
    {
        /// <summary>
        /// A configured fixed moment.
        /// </summary>
        FixedMoment,

        /// <summary>
        /// The current local time.
        /// </summary>
        Now,

        /// <summary>
        /// The current UTC time.
        /// </summary>
        UtcNow,
    }
}
