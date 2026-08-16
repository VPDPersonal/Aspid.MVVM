// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How a converter compares the bound value with the one it is configured with.
    /// </summary>
    /// <remarks>
    /// Read every member as <c>bound &lt;op&gt; configured</c>: <see cref="LessThan"/> asks whether the
    /// bound value is below the configured one, not the other way round. Append new members rather than
    /// inserting one — the order is the serialized value.
    /// </remarks>
    public enum Comparisons
    {
        /// <summary>Equal. Numeric converters compare approximately, scaled to the magnitude.</summary>
        Equal,

        /// <summary>Not equal — the negation of <see cref="Equal"/>, tolerance included.</summary>
        NotEqual,

        /// <summary>The bound value is below the configured one.</summary>
        LessThan,

        /// <summary>The bound value is above the configured one.</summary>
        GreaterThan,

        /// <summary>The bound value is below the configured one or exactly on it.</summary>
        LessThanOrEqual,

        /// <summary>The bound value is above the configured one or exactly on it.</summary>
        GreaterThanOrEqual,
    }
}
