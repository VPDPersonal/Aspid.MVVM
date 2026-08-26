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
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    /// <remarks>
    /// The format is a <b>composite</b> format string: <c>"{0:F2}"</c> formats the value, <c>"F2"</c> is a literal.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Object/To String",
        Name = "To String (Typed)",
        Tooltip = "Writes a value as text, with optional formatting")]
    public class GenericToStringConverter<TFrom> : IConverter<TFrom?, string?>
    {
        [Tooltip("A composite format string such as \"{0:F2}\". Note the braces: a bare \"F2\" is a literal.")]
        [SerializeField] private string? _format;

        [Tooltip("The culture numbers and dates are formatted with. Defaults to the device locale.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: <see cref="object.ToString"/> in the device locale.</remarks>
        public GenericToStringConverter()
        {
            _format = string.Empty;
        }

        /// <param name="format">
        /// A composite format string such as <c>"{0:F2}"</c>. Note the braces: a bare <c>"F2"</c> is a literal.
        /// </param>
        public GenericToStringConverter(string format)
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
        /// The formatted value, <see cref="object.ToString"/> when the format is blank or invalid,
        /// or <see langword="null"/> for a <see langword="null"/> value.
        /// </returns>
        public virtual string? Convert(TFrom? value)
        {
            if (value is null) return null;

            if (string.IsNullOrWhiteSpace(_format))
            {
                if (_culture is CultureInfoMode.CurrentCulture)
                    return value.ToString();

                if (value is IFormattable formattable)
                    return formattable.ToString(format: null, Culture);

                return value.ToString();
            }

            try
            {
                return Format(value);
            }
            // FormatException alone: that is what a bad format string raises, and it is the authoring
            // mistake the fallback exists for. Anything else out of an overridden Format is a bug in
            // the override, and swallowing it would present as a value rather than as a problem.
            catch (FormatException exception)
            {
                return HandleFormatError(value, exception);
            }
        }

        /// <summary>
        /// Called when <see cref="Format"/> raises a <see cref="FormatException"/>. Override to
        /// change the fallback.
        /// </summary>
        /// <param name="value">The non-null value that failed to format.</param>
        /// <param name="exception">The <see cref="FormatException"/> thrown by <see cref="Format"/>.</param>
        /// <returns>
        /// The fallback string — <see cref="object.ToString"/>, or the type name when that throws too.
        /// </returns>
        protected virtual string? HandleFormatError(TFrom value, Exception exception)
        {
            this.LogError(
                problem: $"format string \"{_format}\" is invalid ({exception.Message})",
                consequence: "Falling back to ToString().");

            try
            {
                return value?.ToString();
            }
            catch (Exception toStringException)
            {
                this.LogError(
                    problem: $"the value's ToString() also threw ({toStringException.Message})",
                    consequence: "Returning the type name.");

                return ConverterMessageText.GetTypeName(typeof(TFrom));
            }
        }

        /// <summary>
        /// Applies the configured format. Called only when the format is not blank; override to
        /// change how it is applied.
        /// </summary>
        /// <param name="value">The non-null value to format.</param>
        /// <returns>The formatted string.</returns>
        /// <remarks>
        /// A <see cref="FormatException"/> thrown here is routed to <see cref="HandleFormatError"/>;
        /// every other exception is left to propagate.
        /// </remarks>
        protected virtual string Format(TFrom value) =>
            string.Format(Culture, _format, value);
    }
}
