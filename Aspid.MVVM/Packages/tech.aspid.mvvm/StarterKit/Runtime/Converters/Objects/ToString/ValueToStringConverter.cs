#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a value as text, with optional formatting.
    /// </summary>
    /// <typeparam name="T">The type of the value to convert.</typeparam>
    /// <remarks>
    /// The format is a composite format string: <c>"{0:F2}"</c> formats the value, a bare <c>"F2"</c> is a literal.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Object/To String",
        Name = "Value To String",
        Tooltip = "Writes a value as text, with optional formatting")]
    public class ValueToStringConverter<T> : IConverter<T?, string?>
    {
        [Tooltip("Composite format, e.g. \"{0:F2}\". A bare \"F2\" is printed as is.")]
        [SerializeField] private string? _format;

        [Tooltip("Culture for numbers and dates.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: <see cref="object.ToString"/> in the device locale.</remarks>
        public ValueToStringConverter() { }

        /// <param name="format">
        /// Composite format string such as <c>"{0:F2}"</c>. A bare <c>"F2"</c> is a literal.
        /// </param>
        /// <param name="culture">The culture numbers and dates are formatted with.</param>
        public ValueToStringConverter(
            string? format,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <summary>
        /// Gets the composite format string, or <see langword="null"/> when none is set.
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
        /// The formatted value, <see cref="object.ToString"/> when the format is blank or invalid,
        /// or <see langword="null"/> for a <see langword="null"/> value.
        /// </returns>
        public virtual string? Convert(T? value)
        {
            if (value is null) return null;

            if (string.IsNullOrWhiteSpace(_format))
            {
                return value is IFormattable formattable
                    ? formattable.ToString(format: null, Culture)
                    : value.ToString();
            }

            try
            {
                return Format(value, _format);
            }
            catch (FormatException exception)
            {
                return HandleFormatError(value, exception);
            }
        }

        /// <summary>
        /// Called when <see cref="Format"/> throws a <see cref="FormatException"/>. Override to change the fallback.
        /// </summary>
        /// <param name="value">The non-null value that failed to format.</param>
        /// <param name="exception">The exception thrown by <see cref="Format"/>.</param>
        /// <returns><see cref="object.ToString"/>, or the type name when that throws too.</returns>
        protected virtual string? HandleFormatError(T value, Exception exception)
        {
            this.LogError(
                problem: $"format string \"{_format}\" is invalid ({exception.Message})",
                consequence: "Falling back to ToString().");
            
            try
            {
                return value!.ToString();
            }
            catch (Exception toStringException)
            {
                this.LogError(
                    problem: $"the value's ToString() also threw ({toStringException.Message})",
                    consequence: "Returning the type name.");

                return typeof(T).GetTypeName();
            }
        }

        /// <summary>
        /// Applies the format. Called only when the format is not blank; override to change how it is applied.
        /// </summary>
        /// <param name="value">The non-null value to format.</param>
        /// <param name="format">The composite format string, never blank.</param>
        /// <returns>The formatted string.</returns>
        /// <remarks>
        /// A <see cref="FormatException"/> thrown here is routed to <see cref="HandleFormatError"/>;
        /// every other exception propagates.
        /// </remarks>
        protected virtual string Format(T value, string format) =>
            string.Format(Culture, format, value);
    }
}
