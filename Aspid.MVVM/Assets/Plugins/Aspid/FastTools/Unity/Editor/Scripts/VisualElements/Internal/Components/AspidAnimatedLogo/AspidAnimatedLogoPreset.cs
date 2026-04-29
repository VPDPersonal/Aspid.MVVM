using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidAnimatedLogo"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    public struct AspidAnimatedLogoPreset
    {
        /// <summary>
        /// The default preset matching the values resolved from USS.
        /// </summary>
        public static AspidAnimatedLogoPreset Default => new AspidAnimatedLogoPreset()
            .SetColorCycleIntervalMs(2600)
            .SetPulseSpeed(5f)
            .SetPulseHoverAmplitude(0.04f);

        /// <summary>
        /// Interval (in milliseconds) between color-layer transitions while hovered.
        /// </summary>
        public long ColorCycleIntervalMs;

        /// <summary>
        /// Angular speed of the hover pulse animation.
        /// </summary>
        public float PulseSpeed;

        /// <summary>
        /// Maximum scale amplitude of the hover pulse, expressed as a fraction (e.g. 0.04 = ±4%).
        /// </summary>
        public float PulseHoverAmplitude;

        /// <summary>
        /// Texture for the first logo layer (always visible by default).
        /// </summary>
        public Texture2D Image1;

        /// <summary>
        /// Texture for the second logo layer.
        /// </summary>
        public Texture2D Image2;

        /// <summary>
        /// Texture for the third logo layer.
        /// </summary>
        public Texture2D Image3;

        /// <summary>
        /// Sets <see cref="ColorCycleIntervalMs"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedLogoPreset SetColorCycleIntervalMs(long value)
        {
            ColorCycleIntervalMs = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="PulseSpeed"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedLogoPreset SetPulseSpeed(float value)
        {
            PulseSpeed = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="PulseHoverAmplitude"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedLogoPreset SetPulseHoverAmplitude(float value)
        {
            PulseHoverAmplitude = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Image1"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedLogoPreset SetImage1(Texture2D value)
        {
            Image1 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Image2"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedLogoPreset SetImage2(Texture2D value)
        {
            Image2 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Image3"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedLogoPreset SetImage3(Texture2D value)
        {
            Image3 = value;
            return this;
        }
    }
}
