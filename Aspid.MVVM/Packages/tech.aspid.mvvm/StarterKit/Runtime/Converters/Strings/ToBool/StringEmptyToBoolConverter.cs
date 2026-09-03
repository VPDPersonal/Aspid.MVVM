#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tests whether a string is absent.
    /// </summary>
    /// <remarks>Returns <see langword="true"/> for an absent string; invert it to drive <c>SetActive</c>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Bool",
        Name = "Is Empty",
        Tooltip = "Tests whether a string is absent")]
    public sealed class StringEmptyToBoolConverter : IConverter<string?, bool>
    {
        [Tooltip("What counts as an absent string.")]
        [SerializeField] private StringEmptiness _emptiness = StringEmptiness.NullOrEmpty;

        [Tooltip("Invert the result: true when the string has content.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>Default: <see langword="true"/> for a <see langword="null"/> or empty string.</remarks>
        public StringEmptyToBoolConverter() { }

        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public StringEmptyToBoolConverter(bool isInvert)
            : this(StringEmptiness.NullOrEmpty, isInvert) { }

        /// <param name="emptiness">What counts as an absent string.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public StringEmptyToBoolConverter(
            StringEmptiness emptiness,
            bool isInvert = false)
        {
            _isInvert = isInvert;
            _emptiness = emptiness;
        }

        /// <summary>
        /// Tests whether the specified string is absent under the configured <see cref="StringEmptiness"/>.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns><see langword="true"/> when the string is absent, inverted when configured. An undeclared emptiness reports an error and returns <see langword="false"/>.</returns>
        public bool Convert(string? value) => _emptiness switch
        {
            StringEmptiness.Null => value is null != _isInvert,
            StringEmptiness.NullOrEmpty => string.IsNullOrEmpty(value) != _isInvert,
            StringEmptiness.NullOrWhiteSpace => string.IsNullOrWhiteSpace(value) != _isInvert,
            _ => Undeclared()
        };

        private bool Undeclared()
        {
            this.LogError(
                problem: $"the emptiness {_emptiness.Describe()} is not a declared {nameof(StringEmptiness)}",
                consequence: "Reporting false.");

            return false;
        }
    }
}
