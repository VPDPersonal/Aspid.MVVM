using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericToString{TFrom}"/> for strings, with optional handling of empty values.
    /// </summary>
    /// <remarks>
    /// By default a blank input passes through unformatted, so a label bound to an empty field stays
    /// empty rather than showing the surrounding text with a hole in it. Set
    /// <c>_formatEmptyValues</c> to format blank and <see langword="null"/> input as well.
    /// </remarks>
    [Serializable]
    public class StringFormatConverter : GenericToString<string>, IConverterString
    {
        [SerializeField] private bool _formatEmptyValues;

        public StringFormatConverter() { }

        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        /// <param name="formatEmptyValues">If <see langword="true"/>, applies the format even when the input value is null, empty or whitespace-only. Default is <see langword="false"/>.</param>
        public StringFormatConverter(string format, bool formatEmptyValues = false)
            : base(format)
        {
            _formatEmptyValues = formatEmptyValues;
        }

        /// <summary>
        /// Converts the specified string, formatting <see langword="null"/> as empty when
        /// <c>_formatEmptyValues</c> is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The formatted string, or the value unchanged when the format does not apply.</returns>
        /// <remarks>
        /// The base class short-circuits on <see langword="null"/> before <see cref="Format"/> is
        /// reached, so covering null takes substituting an empty string ahead of it.
        /// </remarks>
        public override string? Convert(string? value) => value is null && _formatEmptyValues && !string.IsNullOrWhiteSpace(FormatString)
                ? base.Convert(string.Empty)
                : base.Convert(value);

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
