// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="ColorTintConverter"/> combines two colors.
    /// </summary>
    public enum ColorBlend
    {
        /// <summary>
        /// Multiply each channel, the alpha included — a tint that is not fully opaque fades the
        /// result.
        /// </summary>
        Multiply,

        /// <summary>
        /// Add the tint to each color channel and hold the sum inside 0..1, keeping the original
        /// alpha.
        /// </summary>
        Add,

        /// <summary>
        /// Move toward the tint by the configured amount, the alpha included.
        /// </summary>
        Lerp,

        /// <summary>
        /// Replace the color with the tint, keeping the original alpha.
        /// </summary>
        Replace,
    }
}
