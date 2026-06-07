using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the orientation of an <see cref="AspidDividingLine"/>. The direction can be inherited
    /// from the <see cref="StyleProperty"/> USS custom property or set explicitly in code; once set
    /// explicitly it is no longer overridden by USS resolution.
    /// </summary>
    internal readonly struct AspidDividingLineDirectionStyle
    {
        /// <summary>
        /// Custom USS property for overriding the line orientation via USS.
        /// </summary>
        public static readonly CustomStyleProperty<string> StyleProperty = new("--aspid-fasttools-prop-line_direction");

        /// <summary>
        /// USS class for the <see cref="Type.Horizontal"/> orientation.
        /// </summary>
        public const string HorizontalClass = "aspid-fasttools-dividing-line--horizontal";

        /// <summary>
        /// USS class for the <see cref="Type.Vertical"/> orientation.
        /// </summary>
        public const string VerticalClass = "aspid-fasttools-dividing-line--vertical";

        private readonly InlineStyle<Type> _value;

        /// <summary>
        /// The current direction value.
        /// </summary>
        public Type Value => _value;

        /// <summary>
        /// Creates a direction binding for <paramref name="element"/> with an initial value.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven
        /// values are applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS classes track the orientation.</param>
        /// <param name="value">The initial direction value.</param>
        public AspidDividingLineDirectionStyle(AspidDividingLine element, Type value)
        {
            _value = new InlineStyle<Type>(value, (oldValue, newValue) =>
            {
                element
                    .RemoveClass(GetClass(oldValue))
                    .AddClass(GetClass(newValue));
            });

            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        /// <summary>
        /// Explicitly sets the direction. Subsequent USS resolutions will not override this value.
        /// </summary>
        /// <param name="value">The new direction.</param>
        public void SetValue(Type value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the direction only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        /// <param name="value">The default direction to apply.</param>
        public void SetDefaultValue(Type value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<Type>(StyleProperty, out var value))
                SetDefaultValue(value);
        }

        /// <summary>
        /// Returns the USS class name corresponding to the given direction,
        /// or <see cref="string.Empty"/> for unknown values.
        /// </summary>
        /// <param name="type">The direction value to convert.</param>
        public static string GetClass(Type type) => type switch
        {
            Type.Horizontal => HorizontalClass,
            Type.Vertical => VerticalClass,
            _ => string.Empty,
        };

        /// <summary>
        /// Defines the orientation of an <see cref="AspidDividingLine"/>.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The line runs horizontally.
            /// </summary>
            Horizontal,

            /// <summary>
            /// The line runs vertically.
            /// </summary>
            Vertical,
        }
    }
}
