using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the font size of an <see cref="AspidLabel"/>. The size can be inherited from the
    /// <see cref="StyleProperty"/> USS custom property or set explicitly in code; once set explicitly
    /// it is no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidLabelSizeStyle
    {
        /// <summary>
        /// Custom USS property for overriding the label font size via USS.
        /// </summary>
        public static readonly CustomStyleProperty<string> StyleProperty = new("--aspid-fasttools-metrics-label_size");

        /// <summary>
        /// USS class for the <see cref="Type.H1"/> size (36px).
        /// </summary>
        public const string H1Class = "aspid-fasttools-label-size--h1";

        /// <summary>
        /// USS class for the <see cref="Type.H2"/> size (24px).
        /// </summary>
        public const string H2Class = "aspid-fasttools-label-size--h2";

        /// <summary>
        /// USS class for the <see cref="Type.H3"/> size (18px).
        /// </summary>
        public const string H3Class = "aspid-fasttools-label-size--h3";

        /// <summary>
        /// USS class for the <see cref="Type.H4"/> size (16px).
        /// </summary>
        public const string H4Class = "aspid-fasttools-label-size--h4";

        /// <summary>
        /// USS class for the <see cref="Type.H5"/> size (14px).
        /// </summary>
        public const string H5Class = "aspid-fasttools-label-size--h5";

        /// <summary>
        /// USS class for the <see cref="Type.H6"/> size (13px).
        /// </summary>
        public const string H6Class = "aspid-fasttools-label-size--h6";

        /// <summary>
        /// USS class for the <see cref="Type.H7"/> size (12px).
        /// </summary>
        public const string H7Class = "aspid-fasttools-label-size--h7";

        private readonly InlineStyle<Type> _value;

        /// <summary>
        /// The current size value.
        /// </summary>
        public Type Value => _value;

        /// <summary>
        /// Creates a size binding for <paramref name="element"/> with an initial value.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven
        /// values are applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS classes track the label size.</param>
        /// <param name="value">The initial size value.</param>
        public AspidLabelSizeStyle(AspidLabel element, Type value)
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
        /// Explicitly sets the size. Subsequent USS resolutions will not override this value.
        /// </summary>
        /// <param name="value">The new size.</param>
        public void SetValue(Type value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the size only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        /// <param name="value">The default size to apply.</param>
        public void SetDefaultValue(Type value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<Type>(StyleProperty, out var value))
                SetDefaultValue(value);
        }

        /// <summary>
        /// Returns the USS class name corresponding to the given size,
        /// or <see cref="string.Empty"/> for <see cref="Type.None"/>.
        /// </summary>
        /// <param name="type">The size value to convert.</param>
        public static string GetClass(Type type) => type switch
        {
            Type.H1 => H1Class,
            Type.H2 => H2Class,
            Type.H3 => H3Class,
            Type.H4 => H4Class,
            Type.H5 => H5Class,
            Type.H6 => H6Class,
            Type.H7 => H7Class,
            _ => string.Empty,
        };

        /// <summary>
        /// Defines the font size of an <see cref="AspidLabel"/>.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// No size class is applied — the label inherits its font size from USS.
            /// Acts as the <c>default(AspidLabelSizeStyle.Type)</c> sentinel used by <see cref="InlineStyle{T}"/>
            /// before any explicit or USS-resolved value is applied.
            /// </summary>
            None = 0,

            /// <summary>
            /// FontSize 36px.
            /// </summary>
            H1 = 36,

            /// <summary>
            /// FontSize 24px.
            /// </summary>
            H2 = 24,

            /// <summary>
            /// FontSize 18px.
            /// </summary>
            H3 = 18,

            /// <summary>
            /// FontSize 16px.
            /// </summary>
            H4 = 16,

            /// <summary>
            /// FontSize 14px.
            /// </summary>
            H5 = 14,

            /// <summary>
            /// FontSize 13px.
            /// </summary>
            H6 = 13,

            /// <summary>
            /// FontSize 12px.
            /// </summary>
            H7 = 12,
        }
    }
}
