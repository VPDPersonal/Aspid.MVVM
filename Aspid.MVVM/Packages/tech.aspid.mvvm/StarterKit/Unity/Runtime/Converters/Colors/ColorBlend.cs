#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="ColorTintConverter"/> combines two colours.
    /// </summary>
    public enum ColorBlend
    {
        /// <summary>
        /// Multiply each channel.
        /// </summary>
        Multiply,

        /// <summary>
        /// Add each channel.
        /// </summary>
        Add,

        /// <summary>
        /// Move towards the tint by the configured amount.
        /// </summary>
        Lerp,

        /// <summary>
        /// Replace the colour with the tint, keeping the original alpha.
        /// </summary>
        Replace,
    }
}
