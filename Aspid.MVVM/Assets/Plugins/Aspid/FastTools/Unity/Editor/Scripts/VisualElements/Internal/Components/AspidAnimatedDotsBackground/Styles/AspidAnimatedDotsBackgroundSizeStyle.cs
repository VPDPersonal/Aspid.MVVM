using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the dot-grid metrics of an <see cref="AspidAnimatedDotsBackground"/>: base dot radius,
    /// base dot spacing and the reference window size used by the size-scaling curve. Each value can
    /// be inherited from its USS custom property or set explicitly in code; once set explicitly it is
    /// no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidAnimatedDotsBackgroundSizeStyle
    {
        /// <summary>
        /// Custom USS property for overriding the base dot radius via USS.
        /// </summary>
        public static readonly CustomStyleProperty<float> DotRadiusProperty = new("--aspid-fasttools-metrics-dot_radius");

        /// <summary>
        /// Custom USS property for overriding the base dot spacing via USS.
        /// </summary>
        public static readonly CustomStyleProperty<float> DotSpacingProperty = new("--aspid-fasttools-metrics-dot_spacing");

        /// <summary>
        /// Custom USS property for overriding the reference window size used by the scaling curve.
        /// </summary>
        public static readonly CustomStyleProperty<float> ScaleReferenceProperty = new("--aspid-fasttools-metrics-dot_scale_reference");

        private readonly InlineStyle<float> _dotRadius;
        private readonly InlineStyle<float> _dotSpacing;
        private readonly InlineStyle<float> _scaleReference;

        /// <summary>
        /// The current base dot radius (before window-size scaling).
        /// </summary>
        public float DotRadius => _dotRadius.Value;

        /// <summary>
        /// The current base dot spacing (before window-size scaling).
        /// </summary>
        public float DotSpacing => _dotSpacing.Value;

        /// <summary>
        /// The current reference window size — divisor in the <c>Sqrt(min(width, height) / reference)</c>
        /// scaling curve. Smaller values amplify the size response to window growth.
        /// </summary>
        public float ScaleReference => _scaleReference.Value;

        /// <summary>
        /// Creates a size binding for <paramref name="element"/> with initial metric values.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values are
        /// applied as defaults until <see cref="SetDotRadius"/>, <see cref="SetDotSpacing"/> or
        /// <see cref="SetScaleReference"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS custom properties drive the metrics.</param>
        /// <param name="dotRadius">The initial base dot radius.</param>
        /// <param name="dotSpacing">The initial base dot spacing.</param>
        /// <param name="scaleReference">The initial reference window size.</param>
        /// <param name="onChanged">Optional callback invoked whenever any metric value changes.</param>
        public AspidAnimatedDotsBackgroundSizeStyle(
            VisualElement element,
            float dotRadius,
            float dotSpacing,
            float scaleReference,
            Action onChanged = null)
        {
            _dotRadius = new InlineStyle<float>(dotRadius, (_, _) => onChanged?.Invoke());
            _dotSpacing = new InlineStyle<float>(dotSpacing, (_, _) => onChanged?.Invoke());
            _scaleReference = new InlineStyle<float>(scaleReference, (_, _) => onChanged?.Invoke());

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the base dot radius. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetDotRadius(float value) =>
            _dotRadius.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the base dot spacing. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetDotSpacing(float value) =>
            _dotSpacing.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the reference window size. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetScaleReference(float value) =>
            _scaleReference.SetInlineValue(value);

        /// <summary>
        /// Sets the dot radius only if it has not already been overridden via <see cref="SetDotRadius"/>.
        /// </summary>
        public void SetDefaultDotRadius(float value) =>
            _dotRadius.SetDefaultValue(value);

        /// <summary>
        /// Sets the dot spacing only if it has not already been overridden via <see cref="SetDotSpacing"/>.
        /// </summary>
        public void SetDefaultDotSpacing(float value) =>
            _dotSpacing.SetDefaultValue(value);

        /// <summary>
        /// Sets the reference window size only if it has not already been overridden via <see cref="SetScaleReference"/>.
        /// </summary>
        public void SetDefaultScaleReference(float value) =>
            _scaleReference.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(DotRadiusProperty, out var radius))
                SetDefaultDotRadius(radius);
            
            if (evt.customStyle.TryGetValue(DotSpacingProperty, out var spacing)) 
                SetDefaultDotSpacing(spacing);
            
            if (evt.customStyle.TryGetValue(ScaleReferenceProperty, out var reference))
                SetDefaultScaleReference(reference);
        }
    }
}
