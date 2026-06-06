using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the three palette colors of an <see cref="AspidAnimatedTitle"/>. Each color can be
    /// inherited from its USS custom property or set explicitly in code; once set explicitly it is
    /// no longer overridden by USS resolution.
    /// </summary>
    internal readonly struct AspidAnimatedTitleColorsStyle
    {
        /// <summary>
        /// Custom USS property for the first palette color.
        /// </summary>
        public static readonly CustomStyleProperty<Color> Color1Property =
            new("--aspid-fasttools-colors-animated_title-color_1");

        /// <summary>
        /// Custom USS property for the second palette color.
        /// </summary>
        public static readonly CustomStyleProperty<Color> Color2Property =
            new("--aspid-fasttools-colors-animated_title-color_2");

        /// <summary>
        /// Custom USS property for the third palette color.
        /// </summary>
        public static readonly CustomStyleProperty<Color> Color3Property =
            new("--aspid-fasttools-colors-animated_title-color_3");

        private readonly InlineStyle<Color> _color1;
        private readonly InlineStyle<Color> _color2;
        private readonly InlineStyle<Color> _color3;

        /// <summary>
        /// The first palette color.
        /// </summary>
        public Color Color1 => _color1.Value;

        /// <summary>
        /// The second palette color.
        /// </summary>
        public Color Color2 => _color2.Value;

        /// <summary>
        /// The third palette color.
        /// </summary>
        public Color Color3 => _color3.Value;

        /// <summary>
        /// Returns the palette color at <paramref name="index"/>.
        /// Indices 0 and 1 return <see cref="Color1"/> and <see cref="Color2"/>;
        /// any other value returns <see cref="Color3"/>.
        /// </summary>
        /// <param name="index">The palette index (0, 1, or any other value for the third color).</param>
        public Color this[int index] => index switch
        {
            0 => _color1.Value,
            1 => _color2.Value,
            _ => _color3.Value,
        };

        /// <summary>
        /// Creates a colors binding for <paramref name="element"/> with initial palette values.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven
        /// values are applied as defaults until <see cref="SetColor1"/>/<see cref="SetColor2"/>/<see cref="SetColor3"/>
        /// is called.
        /// </summary>
        /// <param name="element">The element whose USS resolution feeds the palette.</param>
        /// <param name="color1">The initial first palette color.</param>
        /// <param name="color2">The initial second palette color.</param>
        /// <param name="color3">The initial third palette color.</param>
        /// <param name="onChanged">Optional callback fired whenever any palette color changes.</param>
        public AspidAnimatedTitleColorsStyle(
            VisualElement element,
            Color color1,
            Color color2,
            Color color3,
            Action onChanged)
        {
            _color1 = new InlineStyle<Color>(color1, (_, _) => onChanged?.Invoke());
            _color2 = new InlineStyle<Color>(color2, (_, _) => onChanged?.Invoke());
            _color3 = new InlineStyle<Color>(color3, (_, _) => onChanged?.Invoke());

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets <see cref="Color1"/>. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetColor1(Color value) => _color1.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets <see cref="Color2"/>. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetColor2(Color value) => _color2.SetInlineValue(value);

        /// <summary>
        /// Explicitly sets <see cref="Color3"/>. Subsequent USS resolutions will not override this value.
        /// </summary>
        public void SetColor3(Color value) => _color3.SetInlineValue(value);

        /// <summary>
        /// Sets <see cref="Color1"/> only if it has not already been overridden via <see cref="SetColor1"/>.
        /// </summary>
        public void SetDefaultColor1(Color value) => _color1.SetDefaultValue(value);

        /// <summary>
        /// Sets <see cref="Color2"/> only if it has not already been overridden via <see cref="SetColor2"/>.
        /// </summary>
        public void SetDefaultColor2(Color value) => _color2.SetDefaultValue(value);

        /// <summary>
        /// Sets <see cref="Color3"/> only if it has not already been overridden via <see cref="SetColor3"/>.
        /// </summary>
        public void SetDefaultColor3(Color value) => _color3.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(Color1Property, out var c1)) SetDefaultColor1(c1);
            if (evt.customStyle.TryGetValue(Color2Property, out var c2)) SetDefaultColor2(c2);
            if (evt.customStyle.TryGetValue(Color3Property, out var c3)) SetDefaultColor3(c3);
        }
    }
}
