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
    /// The format is a <b>composite</b> format string, so it needs a placeholder: <c>"{0:F2}"</c>
    /// formats the value, while <c>"F2"</c> is a literal that comes back unchanged. A format the
    /// Inspector cannot validate is treated as a configuration mistake, not a failure: the converter
    /// reports it once and falls back to <see cref="object.ToString"/> rather than throwing into the
    /// binder that pushed the value.
    /// </remarks>
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
        [SerializeField] private string? _format;

        [NonSerialized] private bool _loggedFormatFailure;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericToString{TFrom}"/> class with no formatting.
        /// </summary>
        public GenericToString()
        {
            _format = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericToString{TFrom}"/> class.
        /// </summary>
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
            catch (FormatException exception)
            {
                LogFormatFailure(exception);
                return value.ToString();
            }
        }

        /// <summary>
        /// Applies the configured format to a non-null value. Called only when the format is not blank;
        /// override to change how the format is applied.
        /// </summary>
        /// <param name="value">The non-null value to format.</param>
        /// <returns>The formatted string.</returns>
        protected virtual string Format(TFrom value) =>
            string.Format(_format, value);

        private void LogFormatFailure(FormatException exception)
        {
            if (_loggedFormatFailure) return;
            _loggedFormatFailure = true;

            Debug.LogError(
                $"{GetType().Name}: format string \"{_format}\" is invalid ({exception.Message}). "
                + "Falling back to ToString().");
        }
    }
}
