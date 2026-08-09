using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks the right word form for a count.
    /// </summary>
    /// <remarks>
    /// "1 предмет" / "2 предмета" / "5 предметов" cannot be reached by appending an "s", and a
    /// framework documented in Russian cannot treat the Slavic rule as an extra.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Pluralize", Tooltip = "Picks the right word form for a count")]
    public sealed class PluralizeConverter : IConverter<int, string>
    {
        [Tooltip("Which grammar to follow.")]
        [SerializeField] private PluralRule _rule = PluralRule.English;

        [Tooltip("Used for zero. When empty, the many form is used.")]
        [SerializeField] private string _zeroForm = string.Empty;

        [Tooltip("Used for one.")]
        [SerializeField] private string _oneForm = string.Empty;

        [Tooltip("Used for two to four under the Slavic rule. Ignored by the English rule.")]
        [SerializeField] private string _fewForm = string.Empty;

        [Tooltip("Used for everything else.")]
        [SerializeField] private string _manyForm = string.Empty;

        [Tooltip("A composite format for the result: {0} is the count, {1} the word.")]
        [SerializeField] private string _format = "{0} {1}";

        /// <remarks>Default: with English grammar.</remarks>
        public PluralizeConverter() { }

        /// <param name="rule">Which grammar to follow.</param>
        /// <param name="one">Used for one.</param>
        /// <param name="many">Used for everything else.</param>
        /// <param name="few">Used for two to four under the Slavic rule.</param>
        /// <param name="zero">Used for zero. When <see langword="null"/>, the many form is used.</param>
        public PluralizeConverter(PluralRule rule, string one, string many, string? few = null, string? zero = null)
        {
            _rule = rule;
            _oneForm = one;
            _manyForm = many;
            _fewForm = few ?? many;
            _zeroForm = zero ?? string.Empty;
        }

        /// <summary>
        /// Formats the specified count with the word form its grammar calls for.
        /// </summary>
        /// <param name="value">The count.</param>
        /// <returns>The formatted text.</returns>
        public string Convert(int value)
        {
            var word = Form(value);
            return string.IsNullOrEmpty(_format) ? word : string.Format(_format, value, word);
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rule is not a declared value.</exception>
        private string Form(int value)
        {
            if (value == 0 && !string.IsNullOrEmpty(_zeroForm)) return _zeroForm;

            var magnitude = Math.Abs(value);

            return _rule switch
            {
                PluralRule.English => magnitude == 1 ? _oneForm : _manyForm,
                PluralRule.Slavic => SlavicForm(magnitude),
                _ => throw new ArgumentOutOfRangeException(nameof(_rule), _rule, null)
            };
        }

        // The teens are the exception: 11 takes the many form even though it ends in 1, and 12-14
        // take it even though they end in 2-4.
        private string SlavicForm(int magnitude)
        {
            var lastTwo = magnitude % 100;
            if (lastTwo is >= 11 and <= 14) return _manyForm;

            return (magnitude % 10) switch
            {
                1 => _oneForm,
                2 or 3 or 4 => _fewForm,
                _ => _manyForm
            };
        }
    }
}
