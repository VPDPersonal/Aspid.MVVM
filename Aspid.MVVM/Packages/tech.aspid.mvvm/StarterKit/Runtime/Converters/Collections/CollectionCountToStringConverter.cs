#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes how many items a collection holds, in words.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// The two shipped halves do not compose into this: chaining
    /// <see cref="CollectionCountConverter{T}"/> into <see cref="PluralizeConverter"/> puts the count in
    /// front of whichever form it picked, so a zero form reads "0 Empty". The empty caption has to be a
    /// phrase of its own.
    /// <para>
    /// The last phrase is returned again while the count is unchanged, because a binder pushes on every
    /// notification and <c>string.Format</c> allocates on each — so a field edited in the Inspector during
    /// play shows up on the next count change rather than immediately.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection Count To String", Tooltip = "Writes how many items a collection holds, in words")]
    public sealed class CollectionCountToStringConverter<T> : IConverter<IReadOnlyCollection<T>?, string>
    {
        [Tooltip("Which grammar to follow when picking the word form.")]
        [SerializeField] private PluralRule _rule = PluralRule.English;

        [Tooltip("Written on its own for an empty collection, without the count in front of it. When empty, an empty collection is formatted like any other count.")]
        [SerializeField] private string _zeroText = "Empty";

        [Tooltip("The word used for one item.")]
        [SerializeField] private string _oneForm = "item";

        [Tooltip("The word used for two to four items under the Slavic rule. When empty, the many form is used. Ignored by the English rule.")]
        [SerializeField] private string _fewForm = string.Empty;

        [Tooltip("The word used for every other count.")]
        [SerializeField] private string _manyForm = "items";

        [Tooltip("A composite format for the result: {0} is the count, {1} the word. When empty, only the word is written.")]
        [SerializeField] private string _format = "{0} {1}";

        [NonSerialized] private int _cachedCount;
        [NonSerialized] private string? _cached;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionCountToStringConverter{T}"/> class
        /// writing English item counts.
        /// </summary>
        public CollectionCountToStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionCountToStringConverter{T}"/> class.
        /// </summary>
        /// <param name="oneForm">The word used for one item.</param>
        /// <param name="manyForm">The word used for every other count.</param>
        /// <param name="zeroText">
        /// Written on its own for an empty collection. When <see langword="null"/>, an empty
        /// collection is formatted like any other count.
        /// </param>
        /// <param name="rule">Which grammar to follow when picking the word form.</param>
        /// <param name="fewForm">
        /// The word used for two to four items under <see cref="PluralRule.Slavic"/>. When
        /// <see langword="null"/>, the many form is used.
        /// </param>
        public CollectionCountToStringConverter(
            string oneForm,
            string manyForm,
            string? zeroText = null,
            PluralRule rule = PluralRule.English,
            string? fewForm = null)
        {
            _oneForm = oneForm;
            _manyForm = manyForm;
            _zeroText = zeroText ?? string.Empty;
            _rule = rule;
            _fewForm = fewForm ?? manyForm;
        }

        /// <summary>
        /// Writes the size of the specified collection.
        /// </summary>
        /// <param name="value">The collection to describe.</param>
        /// <returns>
        /// The formatted phrase, or the empty text when the collection is <see langword="null"/> or
        /// empty and an empty text is authored.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rule is not a declared value.</exception>
        public string Convert(IReadOnlyCollection<T>? value)
        {
            var count = value?.Count ?? 0;

            // The null check is the has-a-cache flag: a count of zero is a real answer, so it cannot
            // double as one.
            if (_cached is not null && _cachedCount == count) return _cached;

            _cachedCount = count;
            _cached = Build(count);

            return _cached;
        }

        private string Build(int count)
        {
            if (count == 0 && !string.IsNullOrEmpty(_zeroText)) return _zeroText;

            var word = Form(count);
            return string.IsNullOrEmpty(_format) ? word : string.Format(_format, count, word);
        }

        private string Form(int count) => _rule switch
        {
            // A count is never negative, so the sign handling PluralizeConverter needs is absent here.
            PluralRule.English => count == 1 ? _oneForm : _manyForm,
            PluralRule.Slavic => SlavicForm(count),
            _ => throw new ArgumentOutOfRangeException(nameof(_rule), _rule, null)
        };

        // The teens are the exception: 11 takes the many form even though it ends in 1, and 12-14
        // take it even though they end in 2-4.
        private string SlavicForm(int count)
        {
            if (count % 100 is >= 11 and <= 14) return _manyForm;

            return (count % 10) switch
            {
                1 => _oneForm,

                // The constructor falls back to the many form for a null few form; the field starts
                // empty, so a converter authored in the Inspector needs the same fallback or the
                // word is simply missing from the phrase.
                2 or 3 or 4 => string.IsNullOrEmpty(_fewForm) ? _manyForm : _fewForm,

                _ => _manyForm
            };
        }
    }
}
