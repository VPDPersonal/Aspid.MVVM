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
    /// Removes surrounding characters from a string.
    /// </summary>
    /// <remarks>Sanitising what came back from an input field before it is shown again.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Trim String", Tooltip = "Removes surrounding characters from a string")]
    public sealed class TrimStringConverter : IConverterString
    {
        [Tooltip("Which ends to trim.")]
        [SerializeField] private TrimSide _side = TrimSide.Both;

        [Tooltip("The characters to remove. When empty, whitespace is removed.")]
        [SerializeField] private string _trimChars = string.Empty;

        /// <remarks>Default: trimming whitespace from both ends.</remarks>
        public TrimStringConverter() { }

        /// <param name="side">Which ends to trim.</param>
        /// <param name="trimChars">The characters to remove. When empty, whitespace is removed.</param>
        public TrimStringConverter(TrimSide side, string trimChars = "")
        {
            _side = side;
            _trimChars = trimChars;
        }

        /// <summary>
        /// Trims the specified string.
        /// </summary>
        /// <param name="value">The string to trim.</param>
        /// <returns>The trimmed string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the side is not a declared value.</exception>
        public string? Convert(string? value)
        {
            if (value is null) return null;

            var chars = string.IsNullOrEmpty(_trimChars) ? null : _trimChars.ToCharArray();

            return _side switch
            {
                TrimSide.Both => chars is null ? value.Trim() : value.Trim(chars),
                TrimSide.Start => chars is null ? value.TrimStart() : value.TrimStart(chars),
                TrimSide.End => chars is null ? value.TrimEnd() : value.TrimEnd(chars),
                _ => throw new ArgumentOutOfRangeException(nameof(_side), _side, null)
            };
        }
    }
}
