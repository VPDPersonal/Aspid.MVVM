using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts string values by applying a format string with optional handling of empty values.
    /// </summary>
    [Serializable]
    public class StringFormatConverter : GenericToString<string>, IConverterString
    {
        [Tooltip("When enabled, an empty or null value is formatted as well; otherwise it is passed through untouched.")]
        [SerializeField] private bool _formatEmptyValues;

        /// <summary>
        /// Initializes a new instance of <see cref="StringFormatConverter"/> with no format, leaving values untouched.
        /// </summary>
        public StringFormatConverter() { }

        /// <summary>
        /// Initializes a new instance of <see cref="StringFormatConverter"/> with the specified format.
        /// </summary>
        /// <param name="format">The format string to apply using <see cref="string.Format(string, object)"/>.</param>
        /// <param name="formatEmptyValues">When <see langword="true"/>, applies the format even to a <see langword="null"/>, empty or whitespace-only value. Default is <see langword="false"/>.</param>
        public StringFormatConverter(string format, bool formatEmptyValues = false)
            : base(format)
        {
            _formatEmptyValues = formatEmptyValues;
        }

        /// <summary>
        /// Accepts blank input only when formatting empty values is enabled, so a blank value otherwise passes through untouched.
        /// </summary>
        /// <param name="value">The value about to be formatted.</param>
        /// <returns>
        /// <see langword="true"/> if the value is neither <see langword="null"/>, empty nor whitespace-only,
        /// or if formatting empty values is enabled; otherwise, <see langword="false"/>.
        /// </returns>
        protected override bool ShouldFormat(string? value) =>
            _formatEmptyValues || !string.IsNullOrWhiteSpace(value);
    }
}