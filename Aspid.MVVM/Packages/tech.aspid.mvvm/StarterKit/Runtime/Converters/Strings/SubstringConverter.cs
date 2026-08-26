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

        [Tooltip("How many characters to take. Zero or less takes everything from the start.")]
        [SerializeField] private int _length = 1;

        /// <remarks>Default: taking the first character.</remarks>
        public SubstringConverter() { }

        /// <param name="start">Where the slice starts.</param>
        /// <param name="length">
        /// How many characters to take. Zero or less takes everything from the start.
        /// </param>
        public SubstringConverter(int start, int length)
        {
            _start = start;
            _length = length;
        }

        /// <summary>
        /// Takes the configured slice.
        /// </summary>
        /// <param name="value">The string to slice.</param>
        /// <returns>
        /// The slice, clamped to what the string actually holds. A blank string, spaces included,
        /// comes back unchanged.
        /// </returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var start = Math.Clamp(_start, 0, value.Length);
            var available = value.Length - start;
            var length = _length <= 0 ? available : Math.Min(_length, available);

            return value.Substring(start, length);
        }
    }
}
