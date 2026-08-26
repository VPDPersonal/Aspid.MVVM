using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericToStringConverter{TFrom}"/> for strings, with optional handling of empty values.
    /// </summary>
    /// <remarks>
    /// By default a blank input passes through unformatted and <see langword="null"/> stays
    /// <see langword="null"/>; formatting empty values reads the two as the same empty string.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Format",
        Tooltip = "Formats a string into a template, skipping empty input by default")]
    public sealed class StringFormatConverter : GenericToStringConverter<string>
    {
        [Tooltip("Apply the format to a blank or null value too. A null is read as an empty string, " +
            "so it never comes out null.")]
        [SerializeField] private bool _formatEmptyValues;

        /// <remarks>Default: no format, passing the string through.</remarks>
        public StringFormatConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        /// <param name="formatEmptyValues">
        /// If <see langword="true"/>, applies the format to a blank value too, reading
        /// <see langword="null"/> as an empty string so it never comes out <see langword="null"/>.
        /// </param>
        public StringFormatConverter(string format, bool formatEmptyValues = false)
            : base(format)
        {
            _formatEmptyValues = formatEmptyValues;
        }

        /// <summary>
        /// Converts the specified string, reading <see langword="null"/> as an empty one when empty
        /// values are formatted.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The formatted string, or the value unchanged when the format does not apply; never
        /// <see langword="null"/> while empty values are formatted.
        /// </returns>
        // The base class short-circuits on null, so covering it takes substituting an empty string.
        public override string? Convert(string? value) =>
            base.Convert(_formatEmptyValues ? value ?? string.Empty : value);

        /// <summary>
        /// Applies the format unless the value is blank and blank values are not being formatted.
        /// </summary>
        /// <param name="value">The non-null value to format.</param>
        /// <returns>The formatted string, or the value unchanged.</returns>
        protected override string Format(string value) => _formatEmptyValues || !string.IsNullOrWhiteSpace(value)
            ? base.Format(value)
            : value;
    }
}
