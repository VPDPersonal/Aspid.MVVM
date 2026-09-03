#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Three words picked by the last digit, with the teens excepted: Russian, Ukrainian, Belarusian.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "East Slavic",
        Tooltip = "Three words picked by the last digit, with the teens excepted: Russian, Ukrainian")]
    public sealed class EastSlavicPluralRule : PluralRule
    {
        [Tooltip("The word for a count ending in one, except in the teens.")]
        [SerializeField] private string _one = string.Empty;

        [Tooltip("The word for a count ending in two to four, except in the teens.")]
        [SerializeField] private string _few = string.Empty;

        [Tooltip("The word for every other count, the whole 11-14 window included.")]
        [SerializeField] private string _many = string.Empty;

        private EastSlavicPluralRule() { }

        /// <param name="one">The word for a count ending in one, except in the teens.</param>
        /// <param name="few">The word for a count ending in two to four, except in the teens.</param>
        /// <param name="many">The word for every other count, the whole 11-14 window included.</param>
        /// <param name="zero">Written for a count of none, or <see langword="null"/> to word it like any other count.</param>
        public EastSlavicPluralRule(
            string one,
            string few,
            string many,
            string? zero = null)
            : base(zero)
        {
            _one = one;
            _few = few;
            _many = many;
        }

        /// <inheritdoc/>
        protected override string Word(long value)
        {
            if (value % 100 is >= 11 and <= 14) return _many;

            return (value % 10) switch
            {
                1 => _one,
                2 or 3 or 4 => _few,
                _ => _many,
            };
        }
    }
}
