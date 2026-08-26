using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Zero and one share a word — French, Brazilian Portuguese, Hindi.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "French",
        Tooltip = "Zero and one share a word — French, Brazilian Portuguese, Hindi")]
    public sealed class FrenchPluralRule : PluralRule
    {
        [Tooltip("The word for zero and for one.")]
        [SerializeField] private string _one = string.Empty;

        [Tooltip("The word for every count above one.")]
        [SerializeField] private string _other = string.Empty;

        private FrenchPluralRule() { }

        /// <param name="one">The word for zero and for one.</param>
        /// <param name="other">The word for every count above one.</param>
        /// <param name="zero">
        /// Written for a count of none, or <see langword="null"/> to word it as one, which is what this
        /// grammar calls for.
        /// </param>
        public FrenchPluralRule(string one, string other, string? zero = null)
            : base(zero)
        {
            _one = one;
            _other = other;
        }

        /// <inheritdoc/>
        protected override string Word(long value) => value is 0 or 1
            ? _one
            : _other;
    }
}
