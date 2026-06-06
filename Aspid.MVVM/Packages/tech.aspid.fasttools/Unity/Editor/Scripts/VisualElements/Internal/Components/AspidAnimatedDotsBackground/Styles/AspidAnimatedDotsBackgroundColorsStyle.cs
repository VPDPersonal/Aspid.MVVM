using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the three blob colors of an <see cref="AspidAnimatedDotsBackground"/>. Each color can
    /// be inherited from its <c>--aspid-fasttools-colors-dot_blob-color_{1..3}</c> USS custom property
    /// or set explicitly in code; once set explicitly it is no longer overridden by USS resolution.
    /// </summary>
    internal readonly struct AspidAnimatedDotsBackgroundColorsStyle
    {
        /// <summary>
        /// Custom USS property for overriding the first blob color via USS.
        /// </summary>
        public static readonly CustomStyleProperty<Color> Color1Property = new("--aspid-fasttools-colors-dot_blob-color_1");

        /// <summary>
        /// Custom USS property for overriding the second blob color via USS.
        /// </summary>
        public static readonly CustomStyleProperty<Color> Color2Property = new("--aspid-fasttools-colors-dot_blob-color_2");

        /// <summary>
        /// Custom USS property for overriding the third blob color via USS.
        /// </summary>
        public static readonly CustomStyleProperty<Color> Color3Property = new("--aspid-fasttools-colors-dot_blob-color_3");

        private readonly InlineStyle<Color> _color1;
        private readonly InlineStyle<Color> _color2;
        private readonly InlineStyle<Color> _color3;

        /// <summary>
        /// The current first blob color.
        /// </summary>
        public Color Color1 => _color1.Value;

        /// <summary>
        /// The current second blob color.
        /// </summary>
        public Color Color2 => _color2.Value;

        /// <summary>
        /// The current third blob color.
        /// </summary>
        public Color Color3 => _color3.Value;

        /// <summary>
        /// Returns the current blob color at <paramref name="index"/> (0..2).
        /// </summary>
        /// <param name="index">The blob index in the inclusive range 0..2.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is outside 0..2.</exception>
        public Color this[int index] => index switch
        {
            0 => _color1.Value,
            1 => _color2.Value,
            2 => _color3.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(index)),
        };

        /// <summary>
        /// Creates a colors binding for <paramref name="element"/> with initial blob colors.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values are
        /// applied as defaults until <see cref="SetColor1"/>, <see cref="SetColor2"/> or
        /// <see cref="SetColor3"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS custom properties drive the colors.</param>
        /// <param name="color1">The initial first blob color.</param>
        /// <param name="color2">The initial second blob color.</param>
        /// <param name="color3">The initial third blob color.</param>
        /// <param name="onChanged">Optional callback invoked whenever any color value changes.</param>
        public AspidAnimatedDotsBackgroundColorsStyle(
            VisualElement element,
            Color color1,
            Color color2,
            Color color3,
            Action onChanged = null)
        {
            _color1 = new InlineStyle<Color>(color1, (_, _) => onChanged?.Invoke());
            _color2 = new InlineStyle<Color>(color2, (_, _) => onChanged?.Invoke());
            _color3 = new InlineStyle<Color>(color3, (_, _) => onChanged?.Invoke());

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the first blob color. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetColor1(Color value) =>
            _color1.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the second blob color. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetColor2(Color value) =>
            _color2.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets the third blob color. Subsequent USS resolutions will not override it.
        /// </summary>
        public void SetColor3(Color value) =>
            _color3.SetInlineValue(value);

        /// <summary>
        /// Sets the first blob color only if it has not already been overridden via <see cref="SetColor1"/>.
        /// </summary>
        public void SetDefaultColor1(Color value) =>
            _color1.SetDefaultValue(value);

        /// <summary>
        /// Sets the second blob color only if it has not already been overridden via <see cref="SetColor2"/>.
        /// </summary>
        public void SetDefaultColor2(Color value) =>
            _color2.SetDefaultValue(value);

        /// <summary>
        /// Sets the third blob color only if it has not already been overridden via <see cref="SetColor3"/>.
        /// </summary>
        public void SetDefaultColor3(Color value) =>
            _color3.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(Color1Property, out var c1))
                SetDefaultColor1(c1);
            
            if (evt.customStyle.TryGetValue(Color2Property, out var c2))
                SetDefaultColor2(c2);
            
            if (evt.customStyle.TryGetValue(Color3Property, out var c3))
                SetDefaultColor3(c3);
        }
    }
}
