using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a string to a boolean based on whether it is absent, with optional inversion.
    /// </summary>
    /// <remarks>
    /// <see cref="StringEmptiness.NullOrWhiteSpace"/> is what "did the user type anything?" usually
    /// means, since a string of spaces is not empty but reads as one.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Bool", Name = "String Empty To Bool", Tooltip = "Converts string values to boolean based on empty check, with optional inversion")]
    public class StringEmptyToBoolConverter : IConverterStringToBool
    {
        [Tooltip("What counts as an absent string.")]
        [SerializeField] private StringEmptiness _emptiness = StringEmptiness.NullOrEmpty;

        [Tooltip("Invert the result — true when the string has content.")]
        [SerializeField] private bool _isInvert;

        public StringEmptyToBoolConverter() { }

        /// <param name="isInvert">When <see langword="true"/>, inverts the result.</param>
        public StringEmptyToBoolConverter(bool isInvert)
            : this(StringEmptiness.NullOrEmpty, isInvert) { }

        /// <param name="emptiness">What counts as an absent string.</param>
        /// <param name="isInvert">When <see langword="true"/>, inverts the result.</param>
        public StringEmptyToBoolConverter(StringEmptiness emptiness, bool isInvert = false)
        {
            _emptiness = emptiness;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Tests whether the specified string is absent under the configured <see cref="StringEmptiness"/>.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns><see langword="true"/> when the string is absent, inverted when configured.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the emptiness mode is not a declared value.</exception>
        public bool Convert(string? value)
        {
            var isEmpty = _emptiness switch
            {
                StringEmptiness.Null => value is null,
                StringEmptiness.NullOrEmpty => string.IsNullOrEmpty(value),
                StringEmptiness.NullOrWhiteSpace => string.IsNullOrWhiteSpace(value),
                _ => throw new ArgumentOutOfRangeException(nameof(_emptiness), _emptiness, null)
            };

            return _isInvert ? !isEmpty : isEmpty;
        }
    }
}
