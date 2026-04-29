using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the vertical wave parameters of an <see cref="AspidAnimatedTitle"/>. Stride, speed
    /// and amplitude can be inherited from their USS custom properties or set explicitly in code;
    /// once set explicitly they are no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidAnimatedTitleWaveAnimationStyle
    {
        /// <summary>
        /// Custom USS property for the per-character wave phase offset.
        /// </summary>
        public static readonly CustomStyleProperty<float> StrideProperty =
            new("--aspid-fasttools-prop-animated-title-wave_stride");

        /// <summary>
        /// Custom USS property for the wave animation speed.
        /// </summary>
        public static readonly CustomStyleProperty<float> SpeedProperty =
            new("--aspid-fasttools-prop-animated-title-wave_speed");

        /// <summary>
        /// Custom USS property for the maximum vertical wave displacement (in pixels).
        /// </summary>
        public static readonly CustomStyleProperty<float> AmplitudeProperty =
            new("--aspid-fasttools-prop-animated-title-wave_amplitude");

        private readonly InlineStyle<float> _stride;
        private readonly InlineStyle<float> _speed;
        private readonly InlineStyle<float> _amplitude;

        /// <summary>
        /// The per-character phase offset of the vertical wave.
        /// </summary>
        public float Stride => _stride.Value;

        /// <summary>
        /// The speed of the vertical wave.
        /// </summary>
        public float Speed => _speed.Value;

        /// <summary>
        /// The maximum vertical displacement of the wave, in pixels.
        /// </summary>
        public float Amplitude => _amplitude.Value;

        /// <summary>
        /// Creates a wave-animation binding for <paramref name="element"/> with initial values.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values
        /// are applied as defaults until <see cref="SetStride"/>/<see cref="SetSpeed"/>/<see cref="SetAmplitude"/>
        /// is called.
        /// </summary>
        /// <param name="element">The element whose USS resolution feeds the parameters.</param>
        /// <param name="stride">The initial per-character stride.</param>
        /// <param name="speed">The initial wave speed.</param>
        /// <param name="amplitude">The initial wave amplitude.</param>
        /// <param name="onChanged">Optional callback fired whenever any parameter changes.</param>
        public AspidAnimatedTitleWaveAnimationStyle(
            VisualElement element,
            float stride,
            float speed,
            float amplitude,
            Action onChanged)
        {
            _stride = new InlineStyle<float>(stride, (_, _) => onChanged?.Invoke());
            _speed = new InlineStyle<float>(speed, (_, _) => onChanged?.Invoke());
            _amplitude = new InlineStyle<float>(amplitude, (_, _) => onChanged?.Invoke());

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets <see cref="Stride"/>. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetStride(float value) => _stride.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets <see cref="Speed"/>. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetSpeed(float value) => _speed.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets <see cref="Amplitude"/>. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetAmplitude(float value) => _amplitude.SetInlineValue(value);

        /// <summary>
        /// Sets <see cref="Stride"/> only if it has not already been overridden via <see cref="SetStride"/>.
        /// </summary>
        public void SetDefaultStride(float value) => _stride.SetDefaultValue(value);

        /// <summary>
        /// Sets <see cref="Speed"/> only if it has not already been overridden via <see cref="SetSpeed"/>.
        /// </summary>
        public void SetDefaultSpeed(float value) => _speed.SetDefaultValue(value);

        /// <summary>
        /// Sets <see cref="Amplitude"/> only if it has not already been overridden via <see cref="SetAmplitude"/>.
        /// </summary>
        public void SetDefaultAmplitude(float value) => _amplitude.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StrideProperty, out var stride)) SetDefaultStride(stride);
            if (evt.customStyle.TryGetValue(SpeedProperty, out var speed)) SetDefaultSpeed(speed);
            if (evt.customStyle.TryGetValue(AmplitudeProperty, out var amplitude)) SetDefaultAmplitude(amplitude);
        }
    }
}
