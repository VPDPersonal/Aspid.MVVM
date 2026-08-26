// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="ColorAlphaConverter"/> applies its alpha.
    /// </summary>
    public enum AlphaMode
    {
        /// <summary>
        /// Replace the alpha outright.
        /// </summary>
        Set,

        /// <summary>
        /// Scale the existing alpha.
        /// </summary>
        Multiply,

        /// <summary>
        /// Add to the existing alpha.
        /// </summary>
        Add,
    }
}
