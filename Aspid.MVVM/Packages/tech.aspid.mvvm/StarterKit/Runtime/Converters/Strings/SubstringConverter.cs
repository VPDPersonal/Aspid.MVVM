using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes a slice out of a string.
    /// </summary>
    /// <remarks>An avatar initial, a prefix, a fixed-position field of a code.</remarks>
    [Serializable]
    public sealed class SubstringConverter : IConverterString
    {
        [Tooltip("Where the slice starts.")]
        [SerializeField] private int _start;

        [Tooltip("How many characters to take. Zero or less takes everything from the start.")]
        [SerializeField] private int _length = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstringConverter"/> class taking the first character.
        /// </summary>
        public SubstringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstringConverter"/> class.
        /// </summary>
        /// <param name="start">Where the slice starts.</param>
        /// <param name="length">How many characters to take.</param>
        public SubstringConverter(int start, int length)
        {
            _start = start;
            _length = length;
        }

        /// <summary>
        /// Takes the configured slice.
        /// </summary>
        /// <param name="value">The string to slice.</param>
        /// <returns>The slice, clamped to what the string actually holds.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var start = Math.Clamp(_start, 0, value!.Length);
            var available = value.Length - start;
            var length = _length <= 0 ? available : Math.Min(_length, available);

            return value.Substring(start, length);
        }
    }
}
