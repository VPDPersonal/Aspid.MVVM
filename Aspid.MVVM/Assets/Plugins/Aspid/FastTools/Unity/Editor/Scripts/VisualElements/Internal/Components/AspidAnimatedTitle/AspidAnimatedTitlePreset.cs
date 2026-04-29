using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Configuration preset for an <see cref="AspidAnimatedTitle"/>.
    /// Use the fluent builder methods to create a customized preset.
    /// </summary>
    public struct AspidAnimatedTitlePreset
    {
        /// <summary>
        /// The default preset matching the values resolved from USS.
        /// </summary>
        public static AspidAnimatedTitlePreset Default => new AspidAnimatedTitlePreset()
            .SetColorStride(0.12f)
            .SetColorSpeed(0.4f)
            .SetWaveStride(0.55f)
            .SetWaveSpeed(1.6f)
            .SetWaveAmplitude(3f);

        /// <summary>
        /// Per-character offset along the color palette.
        /// </summary>
        public float ColorStride;

        /// <summary>
        /// Speed at which characters cycle through the color palette.
        /// </summary>
        public float ColorSpeed;

        /// <summary>
        /// Per-character phase offset of the vertical wave animation.
        /// </summary>
        public float WaveStride;

        /// <summary>
        /// Speed of the vertical wave animation.
        /// </summary>
        public float WaveSpeed;

        /// <summary>
        /// Maximum vertical displacement (in pixels) of the wave animation.
        /// </summary>
        public float WaveAmplitude;

        /// <summary>
        /// First color in the cycling palette. Falls back to USS when default.
        /// </summary>
        public Color Color1;

        /// <summary>
        /// Second color in the cycling palette. Falls back to USS when default.
        /// </summary>
        public Color Color2;

        /// <summary>
        /// Third color in the cycling palette. Falls back to USS when default.
        /// </summary>
        public Color Color3;

        /// <summary>
        /// Sets <see cref="ColorStride"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetColorStride(float value)
        {
            ColorStride = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="ColorSpeed"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetColorSpeed(float value)
        {
            ColorSpeed = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="WaveStride"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetWaveStride(float value)
        {
            WaveStride = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="WaveSpeed"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetWaveSpeed(float value)
        {
            WaveSpeed = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="WaveAmplitude"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetWaveAmplitude(float value)
        {
            WaveAmplitude = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Color1"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetColor1(Color value)
        {
            Color1 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Color2"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetColor2(Color value)
        {
            Color2 = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Color3"/> and returns the modified preset.
        /// </summary>
        public AspidAnimatedTitlePreset SetColor3(Color value)
        {
            Color3 = value;
            return this;
        }
    }
}
