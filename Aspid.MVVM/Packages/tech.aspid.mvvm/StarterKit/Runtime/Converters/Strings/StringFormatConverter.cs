#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ValueToStringConverter{T}"/> for strings, with optional handling of empty values.
    /// </summary>
    /// <remarks>By default, a blank input passes through and <see langword="null"/> stays <see langword="null"/>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Format",
        Tooltip = "Formats a string into a template, skipping empty input by default")]
    public sealed class StringFormatConverter : ValueToStringConverter<string>
    {
        [Tooltip("Apply the format to a blank or null value too, reading null as an empty string.")]
        [SerializeField] private bool _formatEmptyValues;

        /// <remarks>Default: no format, passing the string through.</remarks>
        public StringFormatConverter() { }

        /// <param name="format">Composite format string such as <c>"HP: {0}"</c>.</param>
        /// <param name="formatEmptyValues">If <see langword="true"/>, applies the format to a blank value too, reading <see langword="null"/> as an empty string.</param>
        /// <param name="culture">The culture the format is applied with.</param>
        public StringFormatConverter(
            string format,
            bool formatEmptyValues = false,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
            : base(format, culture)
        {
            _formatEmptyValues = formatEmptyValues;
        }

        /// <summary>
        /// Converts the specified string, reading <see langword="null"/> as an empty one when empty values are formatted.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The formatted string, or the value unchanged when the format does not apply.</returns>
        public override string? Convert(string? value) =>
            base.Convert(_formatEmptyValues ? value ?? string.Empty : value);

        /// <summary>
        /// Applies the format unless the value is blank and blank values are not being formatted.
        /// </summary>
        /// <param name="value">The non-null value to format.</param>
        /// <param name="format">The composite format string, never blank.</param>
        /// <returns>The formatted string, or the value unchanged.</returns>
        protected override string Format(string value, string format) => _formatEmptyValues || !string.IsNullOrWhiteSpace(value)
            ? base.Format(value, format)
            : value;
    }
}
