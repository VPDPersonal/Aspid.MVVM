using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Per-channel access to <see cref="Graphic.color"/>. An empty channel mask is reported as a configuration error.
    /// </summary>
    public static class GraphicExtensions
    {
        /// <summary>
        /// Sets the selected channels of <see cref="Graphic.color"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="graphic">The graphic to write.</param>
        /// <param name="channels">The channels to write.</param>
        /// <param name="value">The channel value.</param>
        public static void SetColorChannels(this Graphic graphic, ColorChannels channels, float value)
        {
            if (!channels.SelectsAny())
            {
                ReportEmptyMask(channels, graphic, "The color is left unchanged.");
                return;
            }

            graphic.color = graphic.color.With(channels, value);
        }

        /// <summary>
        /// Returns the first selected channel of <see cref="Graphic.color"/>, in the order R, G, B, A.
        /// </summary>
        /// <param name="graphic">The graphic to read.</param>
        /// <param name="channels">The channels to choose from.</param>
        /// <returns>The channel value, or <c>0</c> when no channel is selected.</returns>
        public static float GetColorChannel(this Graphic graphic, ColorChannels channels)
        {
            if (channels.SelectsAny()) return graphic.color.Get(channels);

            ReportEmptyMask(channels, graphic, "Zero is returned.");
            return 0f;
        }

        private static void ReportEmptyMask(ColorChannels channels, Graphic graphic, string consequence) => BinderLogger.LogError(
            binderType: typeof(GraphicExtensions),
            problem: $"the channel mask {channels.Describe()} selects no declared {nameof(ColorChannels)}",
            consequence: consequence,
            context: graphic);
    }
}
