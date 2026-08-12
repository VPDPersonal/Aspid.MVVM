using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

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
        [Tooltip("Optional format string applied to the value. Leave empty for the type's default formatting.")]
        [SerializeField] private string? _format;
        
        /// <summary>
        /// Initializes a new instance of <see cref="GenericToString{TFrom}"/> with no format, leaving values in their default string representation.
        /// </summary>
        public GenericToString()
        {
            _format = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GenericToString{TFrom}"/> with the specified format.
        /// </summary>
        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        public GenericToString(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Converts the specified value to a string using the configured format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The formatted value when a format is configured and <see cref="ShouldFormat"/> accepts it;
        /// otherwise, the value's default string representation, or <see langword="null"/> if the value is <see langword="null"/>.
        /// </returns>
        public string? Convert(TFrom? value)
        {
            if (string.IsNullOrWhiteSpace(_format)) return value?.ToString();

            return ShouldFormat(value) ? Format(value) : value?.ToString();
        }

        /// <summary>
        /// Called for every value once a non-empty format is configured. Override to narrow or widen which values the format applies to.
        /// </summary>
        /// <param name="value">The value about to be formatted.</param>
        /// <returns><see langword="true"/> if the format should be applied to <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
        /// <remarks>By default, <see langword="null"/> is rejected, so a null value is reported as <see langword="null"/> rather than as formatted text.</remarks>
        protected virtual bool ShouldFormat(TFrom? value) =>
            value is not null;

        /// <summary>
        /// Called for every value <see cref="ShouldFormat"/> accepts. Override to change how the format is applied.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The value rendered through the configured format string.</returns>
        protected virtual string? Format(TFrom? value) =>
            string.Format(_format, value);
    }
}