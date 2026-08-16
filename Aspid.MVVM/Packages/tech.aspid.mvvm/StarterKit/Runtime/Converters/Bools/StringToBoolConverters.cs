using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="StringMatchToBoolConverter"/> compares a bound string with the authored one.
    /// </summary>
    public enum StringMatch
    {
        /// <summary>The whole string must match.</summary>
        Equals,

        /// <summary>The string must contain the authored text.</summary>
        Contains,

        /// <summary>The string must begin with the authored text.</summary>
        StartsWith,

        /// <summary>The string must end with the authored text.</summary>
        EndsWith,
    }

    /// <summary>
    /// Tests a bound string against an authored one.
    /// </summary>
    /// <remarks>
    /// The only question the shipped string converters could answer was "is it empty?", so anything
    /// finer — a state name, a tag, a prefix — needed a boolean added to the ViewModel for the View's
    /// benefit.
    /// </remarks>
    [Serializable]
    public sealed class StringMatchToBoolConverter : IConverterStringToBool
    {
        [Tooltip("How the bound string is compared with the text below.")]
        [SerializeField] private StringMatch _match;

        [Tooltip("The text the bound string is compared against.")]
        [SerializeField] private string _text = string.Empty;

        [Tooltip("Compare without regard to case.")]
        [SerializeField] private bool _ignoreCase = true;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        public StringMatchToBoolConverter() { }

        /// <param name="match">How the bound string is compared with <paramref name="text"/>.</param>
        /// <param name="text">The text the bound string is compared against.</param>
        /// <param name="ignoreCase">If <see langword="true"/>, compares without regard to case.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public StringMatchToBoolConverter(
            StringMatch match,
            string text,
            bool ignoreCase = true,
            bool isInvert = false)
        {
            _match = match;
            _text = text;
            _ignoreCase = ignoreCase;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Tests the specified string against the authored text.
        /// </summary>
        /// <param name="value">The string to test. <see langword="null"/> matches nothing.</param>
        /// <returns>The result of the comparison, inverted when configured.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the match mode is not a declared value.</exception>
        public bool Convert(string? value)
        {
            var comparison = _ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            var matched = value is not null && _match switch
            {
                StringMatch.Equals => value.Equals(_text, comparison),
                StringMatch.Contains => value.IndexOf(_text ?? string.Empty, comparison) >= 0,
                StringMatch.StartsWith => value.StartsWith(_text ?? string.Empty, comparison),
                StringMatch.EndsWith => value.EndsWith(_text ?? string.Empty, comparison),
                _ => throw new ArgumentOutOfRangeException(nameof(_match), _match, null)
            };

            return _isInvert ? !matched : matched;
        }
    }

    /// <summary>
    /// Converts a string to a boolean based on whether it is null, empty or whitespace.
    /// </summary>
    /// <remarks>
    /// <see cref="StringEmptyToBoolConverter"/> asks whether a string is empty; this one also counts
    /// a string of spaces as empty, which is what "did the user type anything?" usually means.
    /// </remarks>
    [Serializable]
    public sealed class StringWhiteSpaceToBoolConverter : IConverterStringToBool
    {
        [Tooltip("Invert the result — true when the string has content.")]
        [SerializeField] private bool _isInvert;

        public StringWhiteSpaceToBoolConverter() { }

        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public StringWhiteSpaceToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Tests whether the specified string is null, empty or whitespace.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns><see langword="true"/> when the string has no content, inverted when configured.</returns>
        public bool Convert(string? value)
        {
            var isBlank = string.IsNullOrWhiteSpace(value);
            return _isInvert ? !isBlank : isBlank;
        }
    }
}
