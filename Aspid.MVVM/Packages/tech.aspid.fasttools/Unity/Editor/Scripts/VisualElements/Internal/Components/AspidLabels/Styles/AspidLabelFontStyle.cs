using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the font style and weight of an <see cref="AspidLabel"/>. The value can be inherited
    /// from the <see cref="StyleProperty"/> USS custom property or set explicitly in code; once set
    /// explicitly it is no longer overridden by USS resolution.
    /// </summary>
    internal readonly struct AspidLabelFontStyle
    {
        /// <summary>
        /// Custom USS property for overriding the label font style via USS.
        /// </summary>
        public static readonly CustomStyleProperty<string> StyleProperty = new("--aspid-fasttools-metrics-label_font_style");

        private readonly InlineStyle<StyleEnum<FontStyle>> _value;

        /// <summary>
        /// The current font style and weight.
        /// </summary>
        public StyleEnum<FontStyle> Value => _value;

        /// <summary>
        /// Creates a font-style binding for <paramref name="element"/> with an initial value.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven
        /// values are applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The label whose inline style is updated when the value changes.</param>
        /// <param name="value">The initial font style and weight.</param>
        public AspidLabelFontStyle(AspidLabel element, StyleEnum<FontStyle> value)
        {
            _value = new InlineStyle<StyleEnum<FontStyle>>(value, (_, newValue) =>
            {
                element.style.SetUnityFontStyleAndWeight(newValue);
            });

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the font style. Subsequent USS resolutions will not override this value.
        /// </summary>
        /// <param name="value">The new font style and weight.</param>
        public void SetValue(StyleEnum<FontStyle> value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the font style only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        /// <param name="value">The default font style and weight to apply.</param>
        public void SetDefaultValue(StyleEnum<FontStyle> value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<FontStyle>(StyleProperty, out var value))
                SetDefaultValue(value);
        }
    }
}
