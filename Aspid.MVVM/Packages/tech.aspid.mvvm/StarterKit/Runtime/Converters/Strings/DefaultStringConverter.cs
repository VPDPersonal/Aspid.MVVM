#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Substitutes a placeholder for a blank string.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Default",
        Tooltip = "Substitutes a placeholder for a blank string")]
    public sealed class DefaultStringConverter : IConverter<string?, string?>
    {
        [Tooltip("Shown when the bound string is blank. A string of spaces counts as blank.")]
        [SerializeField] private string? _fallback = "—";

        /// <remarks>Default: with an em dash.</remarks>
        public DefaultStringConverter() { }

        /// <param name="fallback">Shown when the bound string is blank. A string of spaces counts as blank.</param>
        public DefaultStringConverter(string? fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Returns the specified string, or the placeholder when it is blank.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>The string, or the placeholder when the string is blank, spaces included.</returns>
        public string? Convert(string? value) => string.IsNullOrWhiteSpace(value)
            ? _fallback
            : value;
    }
}
