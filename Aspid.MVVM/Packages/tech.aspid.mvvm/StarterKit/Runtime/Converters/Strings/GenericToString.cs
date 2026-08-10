using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

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
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Generic To String", Tooltip = "Generic converter that transforms values to strings with optional formatting")]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
        [Tooltip("A composite format string such as \"{0:F2}\". Note the braces: a bare \"F2\" is a literal.")]
        [SerializeField] private string? _format;

        [Tooltip("The culture numbers and dates are formatted with. Defaults to the device locale.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

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
        /// Gets the culture the value is formatted with.
        /// </summary>
        protected CultureInfo Culture => _culture.ToCultureInfo();

        /// <summary>
        /// Converts the specified value to a string using the configured format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The string representation of the value, or <see cref="object.ToString"/> if the format is
        /// blank or invalid; <see langword="null"/> only when <typeparamref name="TFrom"/> can hold
        /// <see langword="null"/> — a reference type, or a <see cref="Nullable{T}"/> — and the value is
        /// <see langword="null"/>.
        /// </returns>
        /// <remarks>
        /// <typeparamref name="TFrom"/> is unconstrained, so the <c>?</c> on the parameter is an
        /// annotation rather than a <see cref="Nullable{T}"/>: a plain value-type instantiation such as
        /// <see cref="TimeSpanToStringConverter"/> has no null to be handed and never returns one. Each
        /// instantiation therefore has a single behaviour, and a binder fed by a value-type one never
        /// sees the <see langword="null"/> the contract above allows.
        /// </remarks>
        public virtual string? Convert(TFrom? value)
        {
            if (value is null) return null;
            if (string.IsNullOrWhiteSpace(_format)) return ToStringValue(value);

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
            string.Format(Culture, _format, value);

        // The default is the device locale, which is what ToString() already uses — so the common
        // path keeps the plain call and takes no boxing for the IFormattable test.
        private string? ToStringValue(TFrom value) => _culture is CultureInfoMode.CurrentCulture
            ? value?.ToString()
            : value is IFormattable formattable
                ? formattable.ToString(format: null, Culture)
                : value?.ToString();
    }
}
