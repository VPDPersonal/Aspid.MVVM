using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tests a bound string against an authored one.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Bool",
        Name = "Match",
        Tooltip = "Tests a bound string against an authored one")]
    public sealed class StringMatchToBoolConverter : IConverter<string?, bool>
    {
        [Tooltip("How the bound string is compared with the text below.")]
        [SerializeField] private StringMatch _match;

        [Tooltip("The text the bound string is compared against. " +
            "Blank text is reported and answers false — use Is Empty to test for a blank string.")]
        [SerializeField] private string? _text = string.Empty;

        [Tooltip("Compare without regard to case.")]
        [SerializeField] private bool _ignoreCase = true;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        private StringMatchToBoolConverter() { }

        /// <param name="match">How the bound string is compared with <paramref name="text"/>.</param>
        /// <param name="text">
        /// The text the bound string is compared against. Blank text is reported and answers
        /// <see langword="false"/> — use <see cref="StringEmptyToBoolConverter"/> to test for a blank
        /// string.
        /// </param>
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
        /// <returns>
        /// The comparison result, inverted when configured. Reports an error and answers
        /// <see langword="false"/> when the authored text is blank or the match mode is not a
        /// declared value.
        /// </returns>
        public bool Convert(string? value)
        {
            // Blank text is not a comparison anyone authored on purpose: three of the four modes
            // answer true for it, so the converter would read as always-on rather than as unfilled.
            if (string.IsNullOrEmpty(_text))
            {
                this.LogError(
                    problem: "the text to compare against is blank, which every mode but Equals matches",
                    consequence: "Reporting false.");

                return false;
            }

            var comparison = _ignoreCase
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

            var matched = Matches(value, _text, comparison);
            if (matched is null) return Undeclared();

            return matched.Value != _isInvert;
        }

        private bool? Matches(string? value, string text, StringComparison comparison) => _match switch
        {
            StringMatch.Equals => value is not null && value.Equals(text, comparison),
            StringMatch.Contains => value is not null && value.IndexOf(text, comparison) >= 0,
            StringMatch.StartsWith => value is not null && value.StartsWith(text, comparison),
            StringMatch.EndsWith => value is not null && value.EndsWith(text, comparison),
            _ => null
        };

        private bool Undeclared()
        {
            this.LogError(
                problem: $"the match {_match.Describe()} is not a declared {nameof(StringMatch)}",
                consequence: "Reporting false.");

            return false;
        }
    }
}
