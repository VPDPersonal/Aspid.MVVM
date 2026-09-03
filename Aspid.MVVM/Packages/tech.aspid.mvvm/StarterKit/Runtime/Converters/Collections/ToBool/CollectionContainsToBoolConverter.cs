#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reports whether a collection holds a matching item.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Bool",
        Name = "Contains",
        Tooltip = "Reports whether a collection holds a matching item")]
    public class CollectionContainsToBoolConverter<T> : IConverter<IEnumerable<T?>?, bool>
    {
        [Tooltip("Decides whether an item counts as a match. Required.")]
        [TypeSelector]
        [SerializeReference] private IConverter<T?, bool>? _match = new EqualityToBoolConverter<T?>();

        [Tooltip("Invert the result: true when no item matches.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>Default: looking for the type default, without inverting.</remarks>
        public CollectionContainsToBoolConverter() { }

        /// <param name="value">The item looked for, by equality.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public CollectionContainsToBoolConverter(
            T? value,
            bool isInvert = false)
            : this(new EqualityToBoolConverter<T?>(value), isInvert) { }

        /// <param name="match">Decides whether an item counts as a match.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="match"/> is <see langword="null"/>.</exception>
        public CollectionContainsToBoolConverter(
            IConverter<T?, bool> match,
            bool isInvert = false)
        {
            _match = match ?? throw new ArgumentNullException(nameof(match));
            _isInvert = isInvert;
        }

        /// <summary>
        /// Looks for a matching item.
        /// </summary>
        /// <param name="value">The collection to search.</param>
        /// <returns>Whether any item matches, inverted when configured. A missing match converter counts as no match.</returns>
        public bool Convert(IEnumerable<T?>? value)
        {
            if (_match is null)
            {
                this.LogError(
                    problem: "the match converter is required, and it is missing",
                    consequence: "Counting it as no match.");

                return _isInvert;
            }

            if (value is not null)
            {
                foreach (var item in value)
                {
                    if (!_match.Convert(item)) continue;
                    return !_isInvert;
                }
            }

            return _isInvert;
        }
    }
}
