using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the base color of an <see cref="AspidHoverGradientOverlay"/>. The color can be inherited
    /// from the <see cref="StyleProperty"/> USS custom property or set explicitly in code; once set
    /// explicitly it is no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidHoverGradientOverlayColorStyle
    {
        /// <summary>
        /// Custom USS property for overriding the overlay color via USS.
        /// </summary>
        public static readonly CustomStyleProperty<Color> StyleProperty = new("--aspid-fasttools-colors-hover_overlay");

        private readonly InlineStyle<Color> _value;

        /// <summary>
        /// The current overlay color.
        /// </summary>
        public Color Value => _value.Value;

        /// <summary>
        /// Creates a color binding for <paramref name="element"/> with an initial color.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values are
        /// applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS custom property drives the color.</param>
        /// <param name="value">The initial color.</param>
        /// <param name="onChanged">Optional callback invoked when the color value changes to a different value.</param>
        public AspidHoverGradientOverlayColorStyle(VisualElement element, Color value, Action onChanged = null)
        {
            _value = new InlineStyle<Color>(value, (oldValue, newValue) =>
            {
                if (oldValue != newValue) onChanged?.Invoke();
            });

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the color. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetValue(Color value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the color only if it has not already been overridden via <see cref="SetValue"/>.
        /// </summary>
        public void SetDefaultValue(Color value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(StyleProperty, out var value))
                SetDefaultValue(value);
        }
    }
}
