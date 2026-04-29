using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the visual and animation metrics of an <see cref="AspidHoverGradientOverlay"/>:
    /// the number of vertical strips, the lerp rate of the fade animation, and the peak alpha
    /// scale of the overlay. Each value can be inherited from its USS custom property or set
    /// explicitly in code; once set explicitly it is no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidHoverGradientOverlayMetricsStyle
    {
        /// <summary>
        /// Custom USS property for overriding the strip count via USS.
        /// </summary>
        public static readonly CustomStyleProperty<int> StepsProperty = new("--aspid-fasttools-metrics-hover_overlay_steps");

        /// <summary>
        /// Custom USS property for overriding the fade lerp rate via USS.
        /// </summary>
        public static readonly CustomStyleProperty<float> LerpRateProperty = new("--aspid-fasttools-metrics-hover_overlay_lerp_rate");

        /// <summary>
        /// Custom USS property for overriding the peak alpha scale via USS.
        /// </summary>
        public static readonly CustomStyleProperty<float> AlphaScaleProperty = new("--aspid-fasttools-metrics-hover_overlay_alpha_scale");

        private readonly InlineStyle<int> _steps;
        private readonly InlineStyle<float> _lerpRate;
        private readonly InlineStyle<float> _alphaScale;

        /// <summary>
        /// The current number of vertical strips painted across the overlay width.
        /// </summary>
        public int Steps => _steps.Value;

        /// <summary>
        /// The current per-tick lerp rate driving the fade-in/fade-out animation.
        /// </summary>
        public float LerpRate => _lerpRate.Value;

        /// <summary>
        /// The current peak alpha scale at progress = 1 (multiplied by the per-strip falloff).
        /// </summary>
        public float AlphaScale => _alphaScale.Value;

        /// <summary>
        /// Creates a metrics binding for <paramref name="element"/> with initial values.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values are
        /// applied as defaults until the corresponding <c>Set*</c> method is called.
        /// </summary>
        /// <param name="element">The element whose USS custom properties drive the metrics.</param>
        /// <param name="steps">The initial number of vertical strips.</param>
        /// <param name="lerpRate">The initial per-tick lerp rate.</param>
        /// <param name="alphaScale">The initial peak alpha scale.</param>
        /// <param name="onChanged">Optional callback invoked whenever any metric value changes.</param>
        public AspidHoverGradientOverlayMetricsStyle(
            VisualElement element,
            int steps,
            float lerpRate,
            float alphaScale,
            Action onChanged = null)
        {
            _steps = new InlineStyle<int>(steps, (_, _) => onChanged?.Invoke());
            _lerpRate = new InlineStyle<float>(lerpRate, (_, _) => onChanged?.Invoke());
            _alphaScale = new InlineStyle<float>(alphaScale, (_, _) => onChanged?.Invoke());

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the strip count. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetSteps(int value) =>
            _steps.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the lerp rate. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetLerpRate(float value) =>
            _lerpRate.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the peak alpha scale. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetAlphaScale(float value) =>
            _alphaScale.SetInlineValue(value);

        /// <summary>
        /// Sets the strip count only if it has not already been overridden via <see cref="SetSteps"/>.
        /// </summary>
        public void SetDefaultSteps(int value) =>
            _steps.SetDefaultValue(value);

        /// <summary>
        /// Sets the lerp rate only if it has not already been overridden via <see cref="SetLerpRate"/>.
        /// </summary>
        public void SetDefaultLerpRate(float value) =>
            _lerpRate.SetDefaultValue(value);

        /// <summary>
        /// Sets the peak alpha scale only if it has not already been overridden via <see cref="SetAlphaScale"/>.
        /// </summary>
        public void SetDefaultAlphaScale(float value) =>
            _alphaScale.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StepsProperty, out var steps))
                SetDefaultSteps(steps);

            if (evt.customStyle.TryGetValue(LerpRateProperty, out var lerpRate))
                SetDefaultLerpRate(lerpRate);

            if (evt.customStyle.TryGetValue(AlphaScaleProperty, out var alphaScale))
                SetDefaultAlphaScale(alphaScale);
        }
    }
}
