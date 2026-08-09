using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a boolean out of text.
    /// </summary>
    /// <remarks>
    /// Configuration and backend payloads say "on", "1" and "yes" as often as they say "true", so the
    /// accepted spellings are authored rather than fixed.
    /// </remarks>
    [Serializable]
    public sealed class StringToBoolParseConverter : IConverterStringToBool
    {
        [Tooltip("The spellings read as true. Matched without regard to case.")]
        [SerializeField] private string[] _trueTokens = { "true", "1", "yes", "on" };

        [Tooltip("Returned when the text matches nothing.")]
        [SerializeField] private bool _fallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToBoolParseConverter"/> class with the usual spellings.
        /// </summary>
        public StringToBoolParseConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToBoolParseConverter"/> class.
        /// </summary>
        /// <param name="trueTokens">The spellings read as <see langword="true"/>.</param>
        /// <param name="fallback">Returned when the text matches nothing.</param>
        public StringToBoolParseConverter(string[]? trueTokens, bool fallback = false)
        {
            if (trueTokens is { Length: > 0 }) _trueTokens = trueTokens;
            _fallback = fallback;
        }

        /// <summary>
        /// Reads a boolean out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>Whether it matches one of the accepted spellings, or the fallback.</returns>
        public bool Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || _trueTokens is not { Length: > 0 }) return _fallback;

            var trimmed = value!.Trim();

            for (var i = 0; i < _trueTokens.Length; i++)
                if (string.Equals(trimmed, _trueTokens[i], StringComparison.OrdinalIgnoreCase))
                    return true;

            return _fallback;
        }
    }
}
