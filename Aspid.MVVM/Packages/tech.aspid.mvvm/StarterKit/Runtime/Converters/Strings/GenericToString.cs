using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Generic converter that transforms values to strings with optional formatting.
    /// </summary>
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private string? _format;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericToString{TFrom}"/> class with no formatting.
        /// </summary>
        public GenericToString() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericToString{TFrom}"/> class.
        /// </summary>
        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>, or <c>null</c> to use the default string representation.</param>
        public GenericToString(string? format)
        {
            _format = format;
        }

        /// <summary>
        /// Converts the specified value to a string using the configured format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The string representation of the value, or <c>null</c> if the value is <c>null</c>.</returns>
        public string? Convert(TFrom? value)
        {
            if (value is null) return null;
            return string.IsNullOrEmpty(_format) ? ToStringValue(value) : string.Format(_format, value);
        }

        /// <summary>
        /// Gets the string representation of the value. Can be overridden in derived classes to provide custom conversion logic.
        /// </summary>
        /// <param name="value">The non-null value to convert.</param>
        /// <returns>The string representation of the value.</returns>
        protected virtual string ToStringValue(TFrom value) =>
            value!.ToString();
    }
}