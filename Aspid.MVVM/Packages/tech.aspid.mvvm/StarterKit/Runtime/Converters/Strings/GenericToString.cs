using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Generic converter that transforms values to strings with optional formatting.
    /// </summary>
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    /// <remarks>
    /// The format is a <b>composite</b> format string: <c>"{0:F2}"</c> formats the value, while
    /// <c>"F2"</c> is a literal. Exceptions from <see cref="Format"/> are routed to
    /// <see cref="HandleFormatError"/>, which by default logs the error and falls back to
    /// <see cref="object.ToString"/> instead of throwing into the binder.
    /// </remarks>
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
        [SerializeField] private string? _format;

        public GenericToString()
        {
            _format = string.Empty;
        }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public GenericToString(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Gets the configured composite format string, or <see langword="null"/> when none is set.
        /// </summary>
        protected string? FormatString => _format;

        /// <summary>
        /// Converts the specified value to a string using the configured format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The string representation of the value; <see langword="null"/> if the value is
        /// <see langword="null"/>, and <see cref="object.ToString"/> if the format is blank or invalid.
        /// </returns>
        public virtual string? Convert(TFrom? value)
        {
            if (value is null) return null;
            if (string.IsNullOrWhiteSpace(_format)) return value.ToString();

            try
            {
                return Format(value);
            }
            catch (Exception exception)
            {
                return HandleFormatError(value, exception);
            }
        }

        /// <summary>
        /// Called when <see cref="Format"/> throws. Override to substitute a different fallback
        /// value or rethrow when the failure should not be swallowed.
        /// </summary>
        /// <param name="value">The non-null value that failed to format.</param>
        /// <param name="exception">The exception thrown by <see cref="Format"/>.</param>
        /// <returns>The fallback string, which is <see cref="object.ToString"/> by default.</returns>
        protected virtual string? HandleFormatError(TFrom value, Exception exception)
        {
            Debug.LogError($"{GetType().Name}: format string \"{_format}\" is invalid or threw ({exception.Message}). Falling back to ToString().");
            return value.ToString();
        }

        /// <summary>
        /// Applies the configured format to a non-null value. Called only when the format is not blank;
        /// override to change how the format is applied.
        /// </summary>
        /// <param name="value">The non-null value to format.</param>
        /// <returns>The formatted string.</returns>
        protected virtual string Format(TFrom value) =>
            string.Format(_format, value);
    }
}
