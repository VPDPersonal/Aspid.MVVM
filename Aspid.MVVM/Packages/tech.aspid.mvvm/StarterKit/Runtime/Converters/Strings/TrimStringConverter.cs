#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Removes surrounding characters from a string.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Trim",
        Tooltip = "Removes surrounding characters from a string")]
    public sealed class TrimStringConverter : IConverter<string?, string?>, ISerializationCallbackReceiver
    {
        [Tooltip("Which ends to trim.")]
        [SerializeField] private TrimSide _side = TrimSide.Both;

        [Tooltip("The characters to remove. When empty, whitespace is removed.")]
        [SerializeField] private string _trimChars = string.Empty;

        [NonSerialized] private char[]? _trimCharsCache;

        /// <remarks>Default: trimming whitespace from both ends.</remarks>
        public TrimStringConverter() { }

        /// <param name="side">Which ends to trim.</param>
        /// <param name="trimChars">The characters to remove. When empty, whitespace is removed.</param>
        public TrimStringConverter(
            TrimSide side,
            string trimChars = "")
        {
            _side = side;
            _trimChars = trimChars;
        }

        /// <summary>
        /// Trims the specified string.
        /// </summary>
        /// <param name="value">The string to trim.</param>
        /// <returns>The trimmed string. An undeclared side reports an error and returns the value unchanged.</returns>
        public string? Convert(string? value)
        {
            if (value is null) return null;

            var chars = TrimChars();

            return _side switch
            {
                TrimSide.Both => chars is null ? value.Trim() : value.Trim(chars),
                TrimSide.Start => chars is null ? value.TrimStart() : value.TrimStart(chars),
                TrimSide.End => chars is null ? value.TrimEnd() : value.TrimEnd(chars),
                _ => Undeclared(value)
            };
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _trimCharsCache = null;

        private string Undeclared(string value)
        {
            this.LogError(
                problem: $"the side {_side.Describe()} is not a declared {nameof(TrimSide)}",
                consequence: "Returning the value unchanged.");

            return value;
        }

        private char[]? TrimChars()
        {
            _trimCharsCache ??= string.IsNullOrEmpty(_trimChars)
                ? Array.Empty<char>()
                : _trimChars.ToCharArray();

            return _trimCharsCache.Length is 0 ? null : _trimCharsCache;
        }
    }
}
