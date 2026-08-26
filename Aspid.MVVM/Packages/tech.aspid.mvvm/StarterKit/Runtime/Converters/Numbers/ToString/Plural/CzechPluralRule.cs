using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A word for one, a word for two to four, a word for the rest — Czech, Slovak.
    /// </summary>
    /// <remarks>
    /// The count itself decides, not its last digit, so 22 takes the same word as 5.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "Czech",
        Tooltip = "A word for one, a word for two to four, a word for the rest — Czech, Slovak")]
    public sealed class CzechPluralRule : PluralRule
    {
        [Tooltip("The word for exactly one.")]
        [SerializeField] private string _one = string.Empty;

        [Tooltip("The word for two, three and four.")]
        [SerializeField] private string _few = string.Empty;

        [Tooltip("The word for every other count, five and up included.")]
        [SerializeField] private string _other = string.Empty;

        private CzechPluralRule() { }

        /// <param name="one">The word for exactly one.</param>
        /// <param name="few">The word for two, three and four.</param>
        /// <param name="other">The word for every other count, five and up included.</param>
        /// <param name="zero">Written for a count of none, or <see langword="null"/> to word it like any other count.</param>
        public CzechPluralRule(string one, string few, string other, string? zero = null)
            : base(zero)
        {
            _one = one;
            _few = few;
            _other = other;
        }

        /// <inheritdoc/>
        protected override string Word(long value) => value switch
        {
            1 => _one,
            2 or 3 or 4 => _few,
            _ => _other,
        };
    }
}
