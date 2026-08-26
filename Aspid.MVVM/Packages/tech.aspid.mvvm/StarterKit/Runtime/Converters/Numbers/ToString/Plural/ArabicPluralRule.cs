using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Six words, the widest grammar CLDR declares — Arabic.
    /// </summary>
    /// <remarks>
    /// The zero word of <see cref="PluralRule"/> is one of the six here rather than an override, since
    /// this grammar words a count of none apart in its own right.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "Arabic",
        Tooltip = "Six words, the widest grammar CLDR declares — Arabic")]
    public sealed class ArabicPluralRule : PluralRule
    {
        [Tooltip("The word for exactly one.")]
        [SerializeField] private string _one = string.Empty;

        [Tooltip("The word for exactly two.")]
        [SerializeField] private string _two = string.Empty;

        [Tooltip("The word for a count whose last two digits are three to ten.")]
        [SerializeField] private string _few = string.Empty;

        [Tooltip("The word for a count whose last two digits are 11 to 99.")]
        [SerializeField] private string _many = string.Empty;

        [Tooltip("The word for the round hundreds and the two counts after each.")]
        [SerializeField] private string _other = string.Empty;

        private ArabicPluralRule() { }

        /// <param name="one">The word for exactly one.</param>
        /// <param name="two">The word for exactly two.</param>
        /// <param name="few">The word for a count whose last two digits are three to ten.</param>
        /// <param name="many">The word for a count whose last two digits are 11 to 99.</param>
        /// <param name="other">The word for the round hundreds and the two counts after each.</param>
        /// <param name="zero">
        /// The word for a count of none. When <see langword="null"/>, zero takes
        /// <paramref name="other"/>, which this grammar does not call for.
        /// </param>
        public ArabicPluralRule(
            string one,
            string two,
            string few,
            string many,
            string other,
            string? zero = null)
            : base(zero)
        {
            _one = one;
            _two = two;
            _few = few;
            _many = many;
            _other = other;
        }

        /// <inheritdoc/>
        protected override string Word(long value)
        {
            switch (value)
            {
                case 1: return _one;
                case 2: return _two;
            }

            var lastTwo = value % 100;
            if (lastTwo is >= 3 and <= 10) return _few;

            return lastTwo is >= 11 and <= 99
                ? _many
                : _other;
        }
    }
}
