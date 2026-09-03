#nullable enable
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
    /// <remarks>Wording is delegated to <see cref="PluralizeConverter"/>; a sequence with no count of its own is walked on every push.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To String",
        Name = "Count To String",
        Tooltip = "Writes how many items a collection holds, in words")]
    public class CollectionCountToStringConverter<T> :
        IConverter<IEnumerable<T?>?, string>,
        IConverter<IReadOnlyCollection<T?>?, string>
    {
        [Tooltip("Written for an empty collection instead of the count. Blank words zero like any count.")]
        [SerializeField] private string _zeroText = "Empty";

        [Tooltip("Words the count into the phrase.")]
        [SerializeField] private PluralizeConverter _pluralize = new(new EnglishPluralRule(one: "item", other: "items"));

        /// <remarks>Default: writing English item counts.</remarks>
        public CollectionCountToStringConverter() { }

        /// <param name="pluralize">Words the count into the phrase.</param>
        /// <param name="zeroText">
        /// Written for an empty collection instead of <paramref name="pluralize"/>. Blank words zero like any count.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pluralize"/> is <see langword="null"/>.</exception>
        public CollectionCountToStringConverter(
            PluralizeConverter pluralize,
            string? zeroText = null)
        {
            _zeroText = zeroText ?? string.Empty;
            _pluralize = pluralize ?? throw new ArgumentNullException(nameof(pluralize));
        }

        /// <summary>
        /// Writes the size of the specified collection.
        /// </summary>
        /// <param name="value">The collection to describe.</param>
        /// <returns>The worded phrase, or the empty text for a <see langword="null"/> or empty collection.</returns>
        public string Convert(IReadOnlyCollection<T?>? value) =>
            Write(value?.Count ?? 0);

        string IConverter<IEnumerable<T?>?, string>.Convert(IEnumerable<T?>? value) =>
            Write(value.CountItems());

        private string Write(int count) => count is 0 && !string.IsNullOrWhiteSpace(_zeroText)
            ? _zeroText
            : _pluralize.Convert(count);
    }
}
