using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One word for every count — Chinese, Japanese, Korean, Thai, Vietnamese, Turkish, Indonesian.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "Single Form",
        Tooltip = "One word for every count — Chinese, Japanese, Korean, Thai, Vietnamese, Turkish")]
    public sealed class SingleFormPluralRule : PluralRule
    {
        [Tooltip("The word for every count.")]
        [SerializeField] private string _word = string.Empty;

        private SingleFormPluralRule() { }

        /// <param name="word">The word for every count.</param>
        /// <param name="zero">Written for a count of none, or <see langword="null"/> to word it like any other count.</param>
        public SingleFormPluralRule(string word, string? zero = null)
            : base(zero) => _word = word;

        /// <inheritdoc/>
        protected override string Word(long value) => _word;
    }
}
