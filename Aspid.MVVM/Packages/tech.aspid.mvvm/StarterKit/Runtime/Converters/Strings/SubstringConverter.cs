using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes a slice out of a string.
    /// </summary>
    /// <remarks>An avatar initial, a prefix, a fixed-position field of a code.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Substring", Tooltip = "Takes a slice out of a string")]
    public sealed class SubstringConverter : IConverterString
    {
        [Tooltip("Where the slice starts.")]
        [SerializeField] private int _start;

        [Tooltip("How many characters to take. Zero or less takes everything from the start.")]
        [SerializeField] private int _length = 1;

        /// <remarks>Default: taking the first character.</remarks>
        public SubstringConverter() { }

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
