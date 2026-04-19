using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Manages a style value that can be either inherited from USS custom properties or explicitly
    /// overridden in code. Once <see cref="Set"/> is called the value is locked and subsequent
    /// <see cref="SetDefault"/> calls (driven by USS resolution) are ignored.
    /// </summary>
    /// <typeparam name="T">The type of the style value.</typeparam>
    public struct StyleOverride<T>
    {
        private readonly Action<T, T> _onSet;

        /// <summary>
        /// The current style value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// <see langword="true"/> if the value has been explicitly set via <see cref="Set"/>;
        /// <see langword="false"/> if it still reflects the USS-derived default.
        /// </summary>
        public bool IsOverridden { get; private set; }

        /// <summary>
        /// Initialises the override with an initial value and an optional change callback.
        /// The callback is invoked immediately with <c>default</c> as the old value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="onSet">Optional callback invoked with (oldValue, newValue) whenever the value changes.</param>
        public StyleOverride(T value, Action<T, T> onSet = null)
        {
            Value = value;
            _onSet = onSet;
            IsOverridden = false;

            onSet?.Invoke(default, Value);
        }

        /// <summary>
        /// Explicitly sets the value and marks the override as active.
        /// Subsequent calls to <see cref="SetDefault"/> will have no effect.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void Set(T value)
        {
            _onSet?.Invoke(Value, value);

            Value = value;
            IsOverridden = true;
        }

        /// <summary>
        /// Sets the value only if the override has not been explicitly set via <see cref="Set"/>.
        /// Intended for applying USS-resolved values without overriding user code.
        /// </summary>
        /// <param name="value">The default value to apply if not already overridden.</param>
        public void SetDefault(T value)
        {
            if (IsOverridden)
            {
                return;
            }

            _onSet?.Invoke(Value, value);
            Value = value;
        }

        /// <summary>
        /// Implicitly converts the override to its underlying value.
        /// </summary>
        public static implicit operator T(StyleOverride<T> styleOverride) => styleOverride.Value;
    }
}
