using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes how many items a collection holds, in words.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// The wording is delegated to <see cref="PluralizeConverter"/>, so a grammar added there is
    /// available here. What belongs to this converter is the empty caption, which is written on its
    /// own with no count in front of it.
    /// <para>A sequence carrying no count of its own is walked on every push.</para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To String",
        Name = "Count To String",
        Tooltip = "Writes how many items a collection holds, in words")]
    public class CollectionCountToStringConverter<T> :
        IConverter<IEnumerable<T?>?, string>,
        IConverter<IReadOnlyCollection<T?>?, string>
    {
        [Tooltip("Written on its own for an empty collection, instead of the wording below. " +
            "When blank, an empty collection is worded like any other count.")]
        [SerializeField] private string _zeroText = "Empty";

        [Tooltip("Words the count into the phrase.")]
        [SerializeField] private PluralizeConverter _pluralize = new(new EnglishPluralRule(one: "item", other: "items"));

        /// <remarks>Default: writing English item counts.</remarks>
        public CollectionCountToStringConverter() { }

        /// <param name="pluralize">Words the count into the phrase.</param>
        /// <param name="zeroText">
        /// Written on its own for an empty collection, instead of <paramref name="pluralize"/>. When
        /// <see langword="null"/> or blank, an empty collection is worded like any other count.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="pluralize"/> is <see langword="null"/>.
        /// </exception>
        public CollectionCountToStringConverter(PluralizeConverter pluralize, string? zeroText = null)
        {
            _zeroText = zeroText ?? string.Empty;
            _pluralize = pluralize ?? throw new ArgumentNullException(nameof(pluralize));
        }

        /// <summary>
        /// Writes the size of the specified collection.
        /// </summary>
        /// <param name="value">The collection to describe.</param>
        /// <returns>
        /// The worded phrase, or the authored empty text for a <see langword="null"/> or empty
        /// collection.
        /// </returns>
        public string Convert(IReadOnlyCollection<T?>? value) =>
            Write(value?.Count ?? 0);

        string IConverter<IEnumerable<T?>?, string>.Convert(IEnumerable<T?>? value) =>
            Write(Count(value));

        private string Write(int count) => count is 0 && !string.IsNullOrWhiteSpace(_zeroText)
            ? _zeroText
            : _pluralize.Convert(count);

        private static int Count(IEnumerable<T?>? value)
        {
            switch (value)
            {
                case null: return 0;
                case IReadOnlyCollection<T> collection: return collection.Count;
                case ICollection<T> collection: return collection.Count;
            }

            var count = 0;

            foreach (var _ in value)
                count++;

            return count;
        }
    }
}
