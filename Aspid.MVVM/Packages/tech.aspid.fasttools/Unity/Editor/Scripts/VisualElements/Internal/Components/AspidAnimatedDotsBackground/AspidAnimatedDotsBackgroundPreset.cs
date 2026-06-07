using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidAnimatedDotsBackground"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    internal struct AspidAnimatedDotsBackgroundPreset
    {
        /// <summary>
        /// The default preset: zero colors (resolved from USS) and the canonical dot metrics.
        /// </summary>
        public static AspidAnimatedDotsBackgroundPreset Default => new AspidAnimatedDotsBackgroundPreset()
            .SetDotSpacing(18)
            .SetDotRadius(1.55f)
            .SetScaleReferenceSize(420);

        /// <summary>
        /// Color of the first blob. Falls back to USS when default.
        /// </summary>
        public Color Color1;

        /// <summary>
        /// Color of the second blob. Falls back to USS when default.
        /// </summary>
        public Color Color2;

        /// <summary>
        /// Color of the third blob. Falls back to USS when default.
        /// </summary>
        public Color Color3;

        /// <summary>
        /// Base dot radius before window-size scaling. Overridden by USS when default-initialized via
        /// <c>--aspid-fasttools-metrics-dot_radius</c>.
        /// </summary>
        public float DotRadius;

        /// <summary>
        /// Base dot spacing before window-size scaling. Overridden by USS when default-initialized via
        /// <c>--aspid-fasttools-metrics-dot_spacing</c>.
        /// </summary>
        public float DotSpacing;

        /// <summary>
        /// Reference window size for the size-scaling curve. Overridden by USS when default-initialized
        /// via <c>--aspid-fasttools-metrics-dot_scale_reference</c>.
        /// </summary>
        public float ScaleReferenceSize;

        /// <summary>
        /// Sets <see cref="Color1"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedDotsBackgroundPreset SetColor1(Color value)
        {
            Color1 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Color2"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedDotsBackgroundPreset SetColor2(Color value)
        {
            Color2 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Color3"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedDotsBackgroundPreset SetColor3(Color value)
        {
            Color3 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="DotRadius"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedDotsBackgroundPreset SetDotRadius(float value)
        {
            DotRadius = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="DotSpacing"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedDotsBackgroundPreset SetDotSpacing(float value)
        {
            DotSpacing = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="ScaleReferenceSize"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedDotsBackgroundPreset SetScaleReferenceSize(float value)
        {
            ScaleReferenceSize = value;
            return this;
        }
    }
}
