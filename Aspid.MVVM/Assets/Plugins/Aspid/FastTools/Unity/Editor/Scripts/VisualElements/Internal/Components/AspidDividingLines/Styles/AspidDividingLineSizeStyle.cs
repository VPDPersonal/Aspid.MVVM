using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the thickness of an <see cref="AspidDividingLine"/>. The size can be inherited from
    /// the <see cref="StyleProperty"/> USS custom property or set explicitly in code; once set explicitly
    /// it is no longer overridden by USS resolution.
    /// </summary>
    public readonly struct AspidDividingLineSizeStyle
    {
        /// <summary>
        /// Custom USS property for overriding the line size via USS.
        /// </summary>
        public static readonly CustomStyleProperty<string> StyleProperty = new("--aspid-fasttools-metrics-line_size");

        /// <summary>
        /// USS class for the <see cref="Type.Thin"/> variant.
        /// </summary>
        public const string ThinClass = "aspid-fasttools-dividing-line--thin";

        /// <summary>
        /// USS class for the <see cref="Type.Medium"/> variant.
        /// </summary>
        public const string MediumClass = "aspid-fasttools-dividing-line--medium";

        /// <summary>
        /// USS class for the <see cref="Type.Bold"/> variant.
        /// </summary>
        public const string BoldClass = "aspid-fasttools-dividing-line--bold";

        /// <summary>
        /// USS class applied when the editor runs at low DPI (<c>pixelsPerPoint &lt; 2</c>),
        /// so thin strokes stay visible.
        /// </summary>
        public const string LowDpiClass = "aspid-fasttools-dpi--low";

        private readonly InlineStyle<Type> _value;

        /// <summary>
        /// The current size value.
        /// </summary>
        public Type Value => _value;

        /// <summary>
        /// Creates a size binding for <paramref name="element"/> with an initial value.
        /// Adds <see cref="LowDpiClass"/> when running at low DPI and registers a
        /// <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values are
        /// applied as defaults until <see cref="SetValue"/> is called.
        /// </summary>
        /// <param name="element">The element whose USS classes track the line size.</param>
        /// <param name="value">The initial size value.</param>
        public AspidDividingLineSizeStyle(AspidDividingLine element, Type value)
        {
            if (EditorGUIUtility.pixelsPerPoint < 2f)
                element.AddClass(LowDpiClass);

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
            Type.Thin => ThinClass,
            Type.Medium => MediumClass,
            Type.Bold => BoldClass,
            _ => string.Empty,
        };

        /// <summary>
        /// Defines the thickness of an <see cref="AspidDividingLine"/>.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// No line is drawn.
            /// </summary>
            None,

            /// <summary>
            /// A thin line.
            /// </summary>
            Thin,

            /// <summary>
            /// A medium-thickness line.
            /// </summary>
            Medium,

            /// <summary>
            /// A bold (thick) line.
            /// </summary>
            Bold,
        }
    }
}
