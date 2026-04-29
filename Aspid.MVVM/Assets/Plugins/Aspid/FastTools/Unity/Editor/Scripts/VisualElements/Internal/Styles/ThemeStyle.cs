using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the theme (background brightness variant) of a <see cref="VisualElement"/>.
    /// The theme can be inherited from the <see cref="StyleProperty"/> USS custom property or set
    /// explicitly in code; once set explicitly it is no longer overridden by USS resolution.
    /// </summary>
    public readonly struct ThemeStyle
    {
        /// <summary>
        /// Custom USS property used to propagate the theme to child elements.
        /// </summary>
        public static readonly CustomStyleProperty<string> StyleProperty = new("--aspid-fasttools-prop-theme");

        /// <summary>
        /// USS class for the <see cref="Type.Darkness"/> variant.
        /// </summary>
        public const string DarknessClass = "aspid-fasttools-theme--darkness";

        /// <summary>
        /// USS class for the <see cref="Type.Dark"/> variant.
        /// </summary>
        public const string DarkClass = "aspid-fasttools-theme--dark";

        /// <summary>
        /// USS class for the <see cref="Type.Light"/> variant.
        /// </summary>
        public const string LightClass = "aspid-fasttools-theme--light";

        /// <summary>
        /// USS class for the <see cref="Type.Lightness"/> variant.
        /// </summary>
        public const string LightnessClass = "aspid-fasttools-theme--lightness";

        private readonly InlineStyle<Type> _value;

        /// <summary>
        /// The current theme value.
        /// </summary>
        public Type Value => _value;

        /// <summary>
        /// Creates a theme binding for <paramref name="element"/> with an initial value.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven
        /// values are applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS classes track the theme.</param>
        /// <param name="type">The initial theme value.</param>
        public ThemeStyle(VisualElement element, Type type = Type.Light)
        {
            _value = new InlineStyle<Type>(type, (oldValue, newValue) =>
            {
                element
                    .RemoveClass(GetClass(oldValue))
                    .AddClass(GetClass(newValue));
            });

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the theme. Subsequent USS resolutions will not override this value.
        /// </summary>
        /// <param name="value">The new theme.</param>
        public void SetValue(Type value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the theme only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        /// <param name="value">The default theme to apply.</param>
        public void SetDefaultValue(Type value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<Type>(StyleProperty, out var value))
                SetDefaultValue(value);
        }

        /// <summary>
        /// Returns the USS class name corresponding to the given theme,
        /// or <see cref="string.Empty"/> for unknown values.
        /// </summary>
        /// <param name="theme">The theme value to convert.</param>
        public static string GetClass(Type theme) => theme switch
        {
            Type.Darkness => DarknessClass,
            Type.Dark => DarkClass,
            Type.Light => LightClass,
            Type.Lightness => LightnessClass,
            _ => string.Empty,
        };

        /// <summary>
        /// Defines the visual theme (background brightness) of an Aspid UI element.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The darkest theme variant.
            /// </summary>
            Darkness,

            /// <summary>
            /// A dark theme variant.
            /// </summary>
            Dark,

            /// <summary>
            /// A light theme variant.
            /// </summary>
            Light,

            /// <summary>
            /// The lightest theme variant.
            /// </summary>
            Lightness,
        }
    }
}
