#nullable enable
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
        [SerializeField] private StringMatchMode _mode;

        [Tooltip("The text the bound string is compared against. Blank is reported and answers false.")]
        [SerializeField] private string? _text = string.Empty;

        [Tooltip("Compare without regard to case.")]
        [SerializeField] private bool _ignoreCase = true;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        private StringMatchToBoolConverter() { }

        /// <param name="mode">How the bound string is compared with <paramref name="text"/>.</param>
        /// <param name="text">The text the bound string is compared against. Blank is reported and answers <see langword="false"/>.</param>
        /// <param name="ignoreCase">If <see langword="true"/>, compares without regard to case.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public StringMatchToBoolConverter(
            StringMatchMode mode,
            string text,
            bool ignoreCase = true,
            bool isInvert = false)
        {
            _text = text;
            _mode = mode;
            _isInvert = isInvert;
            _ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Tests the specified string against the authored text.
        /// </summary>
        /// <param name="value">The string to test. <see langword="null"/> matches nothing.</param>
        /// <returns>The result, inverted when configured. Blank text or an undeclared mode reports an error and returns <see langword="false"/>.</returns>
        public bool Convert(string? value)
        {
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

        private bool? Matches(string? value, string text, StringComparison comparison) => _mode switch
        {
            StringMatchMode.Equals => value is not null && value.Equals(text, comparison),
            StringMatchMode.Contains => value is not null && value.IndexOf(text, comparison) >= 0,
            StringMatchMode.StartsWith => value is not null && value.StartsWith(text, comparison),
            StringMatchMode.EndsWith => value is not null && value.EndsWith(text, comparison),
            _ => null
        };

        private bool Undeclared()
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(StringMatchMode)}",
                consequence: "Reporting false.");

            return false;
        }
    }
}
