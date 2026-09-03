using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Per-channel access to a <see cref="Color"/> by <see cref="ColorChannels"/>.
    /// </summary>
    public static class ColorChannelsExtensions
    {
        /// <summary>
        /// Indicates whether <paramref name="channels"/> selects at least one declared channel.
        /// </summary>
        /// <param name="channels">The channel mask.</param>
        /// <returns><see langword="true"/> when a declared channel is selected; otherwise <see langword="false"/>.</returns>
        public static bool SelectsAny(this ColorChannels channels) =>
            (channels & ColorChannels.All) is not ColorChannels.None;

        /// <summary>
        /// Returns <paramref name="color"/> with every selected channel set to <paramref name="value"/>.
        /// </summary>
        /// <param name="color">The color to copy.</param>
        /// <param name="channels">The channels to write.</param>
        /// <param name="value">The channel value.</param>
        /// <returns>The color with the selected channels replaced.</returns>
        public static Color With(this Color color, ColorChannels channels, float value)
        {
            if ((channels & ColorChannels.R) is not ColorChannels.None) color.r = value;
            if ((channels & ColorChannels.G) is not ColorChannels.None) color.g = value;
            if ((channels & ColorChannels.B) is not ColorChannels.None) color.b = value;
            if ((channels & ColorChannels.A) is not ColorChannels.None) color.a = value;

            return color;
        }

        /// <summary>
        /// Returns the first selected channel of <paramref name="color"/>, in the order R, G, B, A.
        /// </summary>
        /// <param name="color">The color to read.</param>
        /// <param name="channels">The channels to choose from.</param>
        /// <returns>The channel value, or <c>0</c> when no declared channel is selected.</returns>
        public static float Get(this Color color, ColorChannels channels)
        {
            if ((channels & ColorChannels.R) is not ColorChannels.None) return color.r;
            if ((channels & ColorChannels.G) is not ColorChannels.None) return color.g;
            if ((channels & ColorChannels.B) is not ColorChannels.None) return color.b;
            if ((channels & ColorChannels.A) is not ColorChannels.None) return color.a;

            return 0f;
        }
    }
}
