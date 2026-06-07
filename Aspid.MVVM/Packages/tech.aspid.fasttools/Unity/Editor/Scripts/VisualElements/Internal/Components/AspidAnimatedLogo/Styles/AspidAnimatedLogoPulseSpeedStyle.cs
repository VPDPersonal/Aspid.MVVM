using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the angular speed of the hover pulse animation for an
    /// <see cref="AspidAnimatedLogo"/>. The value can be inherited from the
    /// <see cref="StyleProperty"/> USS custom property or set explicitly in code;
    /// once set explicitly it is no longer overridden by USS resolution.
    /// </summary>
    internal readonly struct AspidAnimatedLogoPulseSpeedStyle
    {
        /// <summary>
        /// Custom USS property used to drive the pulse speed.
        /// </summary>
        public static readonly CustomStyleProperty<float> StyleProperty =
            new("--aspid-fasttools-prop-animated_logo-pulse_speed");

        private readonly InlineStyle<float> _value;

        /// <summary>
        /// The current pulse speed value.
        /// </summary>
        public float Value => _value;

        /// <summary>
        /// Creates a pulse-speed binding for <paramref name="element"/> with an initial value.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven
        /// values are applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS resolution drives the value.</param>
        /// <param name="value">The initial pulse speed.</param>
        public AspidAnimatedLogoPulseSpeedStyle(VisualElement element, float value)
        {
            _value = new InlineStyle<float>(value);
            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the pulse speed. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetValue(float value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the pulse speed only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        public void SetDefaultValue(float value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StyleProperty, out var value))
                SetDefaultValue(value);
        }
    }
}
