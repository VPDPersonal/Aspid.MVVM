using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Replaces occurrences of one piece of text with another.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Replace",
        Tooltip = "Replaces occurrences of one piece of text with another")]
    public sealed class ReplaceStringConverter : IConverter<string?, string?>
    {
        [Tooltip("The text to look for. When empty, the string passes through.")]
        [SerializeField] private string _search = string.Empty;

        [Tooltip("The text put in its place.")]
        [SerializeField] private string _replacement = string.Empty;

        [Tooltip("Match without regard to case.")]
        [SerializeField] private bool _ignoreCase;

        /// <remarks>Default: with nothing to look for, which leaves the string as it is.</remarks>
        public ReplaceStringConverter() { }

        /// <param name="search">The text to look for. When empty, the string passes through.</param>
        /// <param name="replacement">The text put in its place.</param>
        /// <param name="ignoreCase">If <see langword="true"/>, matches without regard to case.</param>
        public ReplaceStringConverter(string search, string replacement, bool ignoreCase = false)
        {
            _search = search;
            _replacement = replacement;
            _ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Replaces every occurrence in the specified string.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <returns>The string with replacements made.</returns>
        public string? Convert(string? value)
        {
            if (value is null || string.IsNullOrEmpty(_search)) return value;

            var comparison = _ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return value.Replace(_search, _replacement ?? string.Empty, comparison);
        }
    }
}
