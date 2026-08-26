using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a string to a boolean based on whether it is absent, with optional inversion.
    /// </summary>
    /// <remarks>
    /// Returns <see langword="true"/> for an absent string — invert it to drive <c>SetActive</c>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Bool",
        Name = "Is Empty",
        Tooltip = "Converts a string to a boolean based on whether it is absent, with optional inversion")]
    public sealed class StringEmptyToBoolConverter : IConverter<string?, bool>
    {
        [Tooltip("What counts as an absent string.")]
        [SerializeField] private StringEmptiness _emptiness = StringEmptiness.NullOrEmpty;

        [Tooltip("Invert the result — true when the string has content.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>
        /// Default: <see langword="true"/> for a <see langword="null"/> or empty string.
        /// </remarks>
        public StringEmptyToBoolConverter() { }

        /// <param name="isInvert">When <see langword="true"/>, inverts the result.</param>
        public StringEmptyToBoolConverter(bool isInvert)
            : this(StringEmptiness.NullOrEmpty, isInvert) { }

        /// <param name="emptiness">What counts as an absent string.</param>
        /// <param name="isInvert">When <see langword="true"/>, inverts the result.</param>
        public StringEmptyToBoolConverter(StringEmptiness emptiness, bool isInvert = false)
        {
            _isInvert = isInvert;
            _emptiness = emptiness;
        }

        /// <summary>
        /// Tests whether the specified string is absent under the configured <see cref="StringEmptiness"/>.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="true"/> when the string is absent, inverted when configured. Reports an error
        /// and answers <see langword="false"/> when the emptiness mode is not a declared value.
        /// </returns>
        public bool Convert(string? value)
        {
            return _emptiness switch
            {
                StringEmptiness.Null => value is null != _isInvert,
                StringEmptiness.NullOrEmpty => string.IsNullOrEmpty(value) != _isInvert,
                StringEmptiness.NullOrWhiteSpace => string.IsNullOrWhiteSpace(value) != _isInvert,
                _ => Undeclared()
            };
        }

        private bool Undeclared()
        {
            this.LogError(
                problem: $"the emptiness {_emptiness.Describe()} is not a declared {nameof(StringEmptiness)}",
                consequence: "Reporting false.");

            return false;
        }
    }
}
