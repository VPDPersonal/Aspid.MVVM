#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One word for one, another for everything else: English, German, Dutch, Spanish, Italian,
    /// Swedish, Greek.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Plural Rule",
        Name = "English",
        Tooltip = "One word for one, another for everything else: English, German, Spanish, Italian")]
    public sealed class EnglishPluralRule : PluralRule
    {
        [Tooltip("The word for one item.")]
        [SerializeField] private string _one = string.Empty;

        [Tooltip("The word for every other count, zero included.")]
        [SerializeField] private string _other = string.Empty;

        private EnglishPluralRule() { }

        /// <param name="one">The word for one item.</param>
        /// <param name="other">The word for every other count, zero included.</param>
        /// <param name="zero">Written for a count of none, or <see langword="null"/> to word it like any other count.</param>
        public EnglishPluralRule(
            string one,
            string other,
            string? zero = null)
            : base(zero)
        {
            _one = one;
            _other = other;
        }

        /// <inheritdoc/>
        protected override string Word(long value) => value == 1
            ? _one
            : _other;
    }
}
