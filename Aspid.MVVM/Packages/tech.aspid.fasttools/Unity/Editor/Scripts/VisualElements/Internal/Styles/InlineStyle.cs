using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Tracks the effective value of a USS-controlled property and remembers whether it was set inline from code.
    /// Inline values take precedence over USS values, mirroring the inline-vs-stylesheet rule in UIToolkit.
    /// </summary>
    /// <typeparam name="T">The value type of the style property.</typeparam>
    internal class InlineStyle<T>
    {
        private readonly Action<T, T> _onSet;

        /// <summary>
        /// Gets the current effective value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets whether the current value was set inline from code via <see cref="SetInlineValue"/>.
        /// While <c>true</c>, subsequent USS-driven calls to <see cref="SetDefaultValue"/> are ignored.
        /// </summary>
        public bool IsInline { get; private set; }

        /// <summary>
        /// Initialises a new instance with a starting value and an optional change callback.
        /// </summary>
        /// <remarks>
        /// The callback is invoked once during construction with <c>oldValue = default(T)</c> and
        /// <c>newValue = <paramref name="value"/></c>, so callers can apply the initial USS class
        /// without a separate setup step.
        /// </remarks>
        /// <param name="value">The initial value.</param>
        /// <param name="onSet">Optional callback invoked as <c>(oldValue, newValue)</c> whenever the value changes.</param>
        public InlineStyle(T value, Action<T, T> onSet = null)
        {
            Value = value;
            _onSet = onSet;
            IsInline = false;

            onSet?.Invoke(default, Value);
        }

        /// <summary>
        /// Sets the value from inline code and marks <see cref="IsInline"/> as <c>true</c>,
        /// preventing subsequent USS-driven defaults from overriding it.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void SetInlineValue(T value)
        {
            _onSet?.Invoke(Value, value);

            Value = value;
            IsInline = true;
        }

        /// <summary>
        /// Applies a default value (typically from a stylesheet or other external source).
        /// Has no effect once <see cref="IsInline"/> is <c>true</c>, so inline code always wins.
        /// </summary>
        /// <param name="value">The new default value.</param>
        public void SetDefaultValue(T value)
        {
            if (IsInline)  return;

            _onSet?.Invoke(Value, value);
            Value = value;
        }

        public static implicit operator T(InlineStyle<T> inlineStyle) => inlineStyle.Value;
    }
}
