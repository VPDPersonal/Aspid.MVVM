#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes a slice out of a string.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Substring",
        Tooltip = "Takes a slice out of a string")]
    public sealed class SubstringConverter : IConverter<string?, string?>
    {
        [Tooltip("Where the slice starts.")]
        [SerializeField] [Min(0)] private int _start;

        [Tooltip("How many characters to take. Zero takes everything from the start.")]
        [SerializeField] [Min(0)] private int _length = 1;

        /// <remarks>Default: taking the first character.</remarks>
        public SubstringConverter() { }

        /// <param name="start">Where the slice starts.</param>
        /// <param name="length">How many characters to take. Zero takes everything from the start.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="start"/> or <paramref name="length"/> is negative.</exception>
        public SubstringConverter(
            int start,
            int length)
        {
            _start = start >= 0 ? start : throw new ArgumentOutOfRangeException(nameof(start));
            _length = length >= 0 ? length : throw new ArgumentOutOfRangeException(nameof(length));
        }

        /// <summary>
        /// Takes the configured slice.
        /// </summary>
        /// <param name="value">The string to slice.</param>
        /// <returns>The slice, clamped to what the string holds. A blank string comes back unchanged.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var start = Math.Min(_start, value.Length);
            var available = value.Length - start;

            var length = _length is 0
                ? available
                : Math.Min(_length, available);

            return value.Substring(start, length);
        }
    }
}
