#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Words a count in one language: the grammar and the words it picks between.
    /// </summary>
    /// <remarks>A subclass declares only the words its grammar uses.</remarks>
    [Serializable]
    public abstract class PluralRule : IConverter<long, string>
    {
        [Tooltip("Written for a count of none. When empty, the grammar words zero like any other count.")]
        [SerializeField] private string? _zero;

        /// <param name="zero">Written for a count of none, or <see langword="null"/> to word it like any other count.</param>
        protected PluralRule(string? zero = null)
        {
            _zero = zero ?? string.Empty;
        }

        /// <summary>
        /// Words the specified count.
        /// </summary>
        /// <param name="value">The count, as a magnitude.</param>
        /// <returns>The zero word for a count of none when authored, otherwise the grammar's word. A blank word is reported.</returns>
        public string Convert(long value)
        {
            if (value is 0 && !string.IsNullOrWhiteSpace(_zero)) return _zero;

            var word = Word(value);
            if (!string.IsNullOrWhiteSpace(word)) return word;

            this.LogError(
                problem: $"no word is authored for a count of {value}",
                consequence: "Leaving the word out of the phrase.");

            return string.Empty;
        }

        /// <summary>
        /// Picks the word the grammar calls for.
        /// </summary>
        /// <param name="value">The count, as a magnitude.</param>
        /// <returns>The word for the count, or an empty string when it is not authored.</returns>
        protected abstract string Word(long value);
    }
}
