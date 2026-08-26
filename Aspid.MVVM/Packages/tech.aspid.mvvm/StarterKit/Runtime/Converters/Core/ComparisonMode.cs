// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How a converter compares the bound value with the one it is configured with.
    /// </summary>
    public enum ComparisonMode
    {
        /// <summary>
        /// Equal, within the converter's tolerance where it has one.
        /// </summary>
        Equal,

        /// <summary>
        /// Not equal, tolerance included.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Below the configured value, by more than the tolerance.
        /// </summary>
        LessThan,

        /// <summary>
        /// Above the configured value, by more than the tolerance.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Below the configured value, or within the tolerance of it.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Above the configured value, or within the tolerance of it.
        /// </summary>
        GreaterThanOrEqual,
    }
}
