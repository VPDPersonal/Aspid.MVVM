using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages the status accent (success / warning / error / info) of a <see cref="VisualElement"/>.
    /// The status can be inherited from the <see cref="StyleProperty"/> USS custom property or set
    /// explicitly in code; once set explicitly it is no longer overridden by the
    /// <see cref="CustomStyleResolvedEvent"/>.
    /// </summary>
    internal readonly struct StatusStyle
    {
        /// <summary>
        /// Custom USS property used to propagate the status to child elements.
        /// </summary>
        public static readonly CustomStyleProperty<string> StyleProperty = new("--aspid-fasttools-prop-status");

        /// <summary>
        /// USS class for the <see cref="Type.Success"/> status.
        /// </summary>
        public const string SuccessClass = "aspid-fasttools-status--success";

        /// <summary>
        /// USS class for the <see cref="Type.Warning"/> status.
        /// </summary>
        public const string WarningClass = "aspid-fasttools-status--warning";

        /// <summary>
        /// USS class for the <see cref="Type.Error"/> status.
        /// </summary>
        public const string ErrorClass = "aspid-fasttools-status--error";

        /// <summary>
        /// USS class for the <see cref="Type.Info"/> status.
        /// </summary>
        public const string InfoClass = "aspid-fasttools-status--info";

        private readonly InlineStyle<Type> _value;

        /// <summary>
        /// The current status value.
        /// </summary>
        public Type Value => _value;

        /// <summary>
        /// Creates a status binding for <paramref name="element"/> with an initial value.
        /// Registers a <see cref="CustomStyleResolvedEvent"/> handler so that USS-driven values
        /// apply as defaults; once <see cref="SetValue"/> is called the inline value takes precedence.
        /// </summary>
        /// <param name="element">The element whose USS classes track the status.</param>
        /// <param name="type">The initial status value.</param>
        public StatusStyle(VisualElement element, Type type = Type.None)
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
        /// Explicitly sets the status. Subsequent USS resolutions will not override this value.
        /// </summary>
        /// <param name="value">The new status.</param>
        public void SetValue(Type value) =>
            _value.SetInlineValue(value);

        /// <summary>
        /// Sets the status only if it has not already been overridden via <see cref="SetValue"/>.
        /// Used when applying USS-resolved values.
        /// </summary>
        /// <param name="value">The default status to apply.</param>
        public void SetDefaultValue(Type value) =>
            _value.SetDefaultValue(value);

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<Type>(StyleProperty, out var value))
                SetDefaultValue(value);
        }

        /// <summary>
        /// Returns the USS class name corresponding to the given status,
        /// or <see cref="string.Empty"/> if no class is mapped (e.g. <see cref="Type.None"/>).
        /// </summary>
        /// <param name="status">The status value to convert.</param>
        /// <returns>The USS class name, or <see cref="string.Empty"/> when no class is mapped.</returns>
        public static string GetClass(Type status) => status switch
        {
            Type.Success => SuccessClass,
            Type.Warning => WarningClass,
            Type.Error => ErrorClass,
            Type.Info => InfoClass,
            _ => string.Empty,
        };

        /// <summary>
        /// Defines the visual status of an Aspid UI element, controlling its color accent.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// No status applied.
            /// </summary>
            None,

            /// <summary>
            /// Indicates an informational state.
            /// </summary>
            Info,

            /// <summary>
            /// Indicates a warning or cautionary state.
            /// </summary>
            Warning,

            /// <summary>
            /// Indicates an error or critical state.
            /// </summary>
            Error,

            /// <summary>
            /// Indicates a successful or positive state.
            /// </summary>
            Success,
        }
    }
}
