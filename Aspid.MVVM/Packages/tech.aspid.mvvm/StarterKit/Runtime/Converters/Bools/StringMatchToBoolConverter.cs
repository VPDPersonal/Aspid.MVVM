using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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
            _text = text;
            _match = match;
            _isInvert = isInvert;
            _ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Tests the specified string against the authored text.
        /// </summary>
        /// <param name="value">The string to test. <see langword="null"/> matches nothing.</param>
        /// <returns>The result of the comparison, inverted when configured.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the match mode is not a declared value.</exception>
        public bool Convert(string? value)
        {
            // A converter built in code can be handed a null text; Unity only ever writes an empty one.
            var text = _text ?? string.Empty;
            var comparison = _ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            var matched = value is not null && _match switch
            {
                StringMatch.Equals => value.Equals(text, comparison),
                StringMatch.Contains => value.IndexOf(text, comparison) >= 0,
                StringMatch.StartsWith => value.StartsWith(text, comparison),
                StringMatch.EndsWith => value.EndsWith(text, comparison),
                _ => throw new ArgumentOutOfRangeException(nameof(_match), _match, null)
            };

            return _isInvert ? !matched : matched;
        }
    }
}
