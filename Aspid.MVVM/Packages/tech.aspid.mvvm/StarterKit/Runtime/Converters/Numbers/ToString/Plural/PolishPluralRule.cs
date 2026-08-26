using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Three words where only a bare one is singular — Polish.
    /// </summary>
    /// <remarks>
    /// The singular is claimed by the count 1 alone rather than by any count ending in 1, so 21 takes
    /// the many word.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "Polish",
        Tooltip = "Three words where only a bare one is singular — Polish")]
    public sealed class PolishPluralRule : PluralRule
    {
        [Tooltip("The word for exactly one.")]
        [SerializeField] private string _one = string.Empty;

        [Tooltip("The word for a count ending in two to four, except in the 12-14 window.")]
        [SerializeField] private string _few = string.Empty;

        [Tooltip("The word for every other count, 21 and 101 included.")]
        [SerializeField] private string _many = string.Empty;

        private PolishPluralRule() { }

        /// <param name="one">The word for exactly one.</param>
        /// <param name="few">The word for a count ending in two to four, except in the 12-14 window.</param>
        /// <param name="many">The word for every other count, 21 and 101 included.</param>
        /// <param name="zero">Written for a count of none, or <see langword="null"/> to word it like any other count.</param>
        public PolishPluralRule(string one, string few, string many, string? zero = null)
            : base(zero)
        {
            _one = one;
            _few = few;
            _many = many;
        }

        /// <inheritdoc/>
        protected override string Word(long value)
        {
            if (value == 1) return _one;
            if (value % 100 is >= 12 and <= 14) return _many;
            return value % 10 is >= 2 and <= 4 ? _few : _many;
        }
    }
}
