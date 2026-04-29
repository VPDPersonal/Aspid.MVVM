using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the per-character color cycling parameters of an <see cref="AspidAnimatedTitle"/>.
    /// Both stride and speed can be inherited from their USS custom properties or set explicitly
    /// in code; once set explicitly they are no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidAnimatedTitleColorAnimationStyle
    {
        /// <summary>
        /// Custom USS property for the per-character color-palette stride.
        /// </summary>
        public static readonly CustomStyleProperty<float> StrideProperty =
            new("--aspid-fasttools-prop-animated-title-color_stride");

        /// <summary>
        /// Custom USS property for the color-palette cycling speed.
        /// </summary>
        public static readonly CustomStyleProperty<float> SpeedProperty =
            new("--aspid-fasttools-prop-animated-title-color_speed");

        private readonly InlineStyle<float> _stride;
        private readonly InlineStyle<float> _speed;

        /// <summary>
        /// The per-character offset along the color palette.
        /// </summary>
        public float Stride => _stride.Value;

        /// <summary>
        /// The speed at which characters cycle through the color palette.
        /// </summary>
        public float Speed => _speed.Value;

        /// <summary>
        /// Creates a color-animation binding for <paramref name="element"/> with initial values.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values
        /// are applied as defaults until <see cref="SetStride"/>/<see cref="SetSpeed"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS resolution feeds the parameters.</param>
        /// <param name="stride">The initial per-character stride.</param>
        /// <param name="speed">The initial cycling speed.</param>
        /// <param name="onChanged">Optional callback fired whenever any parameter changes.</param>
        public AspidAnimatedTitleColorAnimationStyle(
            VisualElement element,
            float stride,
            float speed,
            Action onChanged)
        {
            _stride = new InlineStyle<float>(stride, (_, _) => onChanged?.Invoke());
            _speed = new InlineStyle<float>(speed, (_, _) => onChanged?.Invoke());

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
        /// Sets <see cref="Stride"/> only if it has not already been overridden via <see cref="SetStride"/>.
        /// </summary>
        public void SetDefaultStride(float value) => _stride.SetDefaultValue(value);

        /// <summary>
        /// Sets <see cref="Speed"/> only if it has not already been overridden via <see cref="SetSpeed"/>.
        /// </summary>
        public void SetDefaultSpeed(float value) => _speed.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StrideProperty, out var stride)) SetDefaultStride(stride);
            if (evt.customStyle.TryGetValue(SpeedProperty, out var speed)) SetDefaultSpeed(speed);
        }
    }
}
