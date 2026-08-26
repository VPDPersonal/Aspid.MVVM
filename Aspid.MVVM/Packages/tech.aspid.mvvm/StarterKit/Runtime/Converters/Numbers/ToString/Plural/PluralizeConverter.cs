using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks the right word form for a count.
    /// </summary>
    /// <remarks>
    /// The grammar and its words are a <see cref="PluralRule"/>; the converter itself holds only the
    /// phrase the count and the word are written into.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Pluralize",
        Tooltip = "Picks the right word form for a count")]
    public sealed class PluralizeConverter :
        IConverter<int, string>,
        IConverter<long, string>
    {
        private const string DefaultFormat = "{0} {1}";

        [Tooltip("The grammar and the words it picks between. Required; without it the phrase carries no word.")]
        [TypeSelector]
        [SerializeReference] private PluralRule? _rule;

        [Tooltip("Composite format for the result: {0} is the count, {1} the word. " +
            "When blank, the word is written alone; an invalid format is reported and does the same.")]
        [SerializeField] private string _format = DefaultFormat;

        private PluralizeConverter() { }

        /// <param name="rule">The grammar and the words it picks between.</param>
        /// <param name="format">
        /// A composite format for the result: <c>{0}</c> is the count, <c>{1}</c> the word.
        /// <see langword="null"/> writes the count and the word with a space between them; a blank
        /// one writes the word alone, as does an invalid format, which is reported.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        public PluralizeConverter(PluralRule rule, string? format = null)
        {
            _format = format ?? DefaultFormat;
            _rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }

        /// <summary>
        /// Formats the specified count with the word its grammar calls for.
        /// </summary>
        /// <param name="value">The count, which keeps its sign in the phrase while the grammar reads its magnitude.</param>
        /// <returns>
        /// The formatted text. A missing rule leaves the word out and an invalid format the count, and
        /// each is reported.
        /// </returns>
        public string Convert(int value) => Write(value);

        string IConverter<long, string>.Convert(long value) => Write(value);

        private string Write(long value)
        {
            var word = Word(value);
            if (string.IsNullOrWhiteSpace(_format)) return word;

            try
            {
                return string.Format(_format, value, word);
            }
            catch (FormatException exception)
            {
                // The format is typed in rather than picked from a list, so a typo has to show the
                // wrong text and say so, not stop the binder.
                this.LogError(
                    problem: $"{_format.Describe()} is not a composite format ({exception.Message})",
                    consequence: "Writing the word alone.");

                return word;
            }
        }

        private string Word(long value)
        {
            // long.MinValue has no positive counterpart of its own width, and its magnitude is the
            // one count Math.Abs cannot take.
            if (_rule is not null)
                return _rule.Convert(value is long.MinValue ? long.MaxValue : Math.Abs(value));

            this.LogError(
                problem: "no plural rule is set",
                consequence: "Leaving the word out of the phrase.");

            return string.Empty;
        }
    }
}
