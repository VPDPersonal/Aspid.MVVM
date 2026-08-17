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
    /// Substitutes a placeholder for a blank string.
    /// </summary>
    /// <remarks>
    /// A label bound to an unset name should read "—", not nothing. Without this the ViewModel has to
    /// know what the empty state looks like.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Default String", Tooltip = "Substitutes a placeholder for a blank string")]
    public sealed class DefaultStringConverter : IConverterString
    {
        [Tooltip("Shown when the bound string is blank.")]
        [SerializeField] private string _fallback = "—";

        [Tooltip("Count a string of spaces as blank.")]
        [SerializeField] private bool _treatWhiteSpaceAsEmpty = true;

        /// <remarks>Default: with an em dash.</remarks>
        public DefaultStringConverter() { }

        /// <param name="fallback">Shown when the bound string is blank.</param>
        /// <param name="treatWhiteSpaceAsEmpty">If <see langword="true"/>, counts a string of spaces as blank.</param>
        public DefaultStringConverter(string fallback, bool treatWhiteSpaceAsEmpty = true)
        {
            _fallback = fallback;
            _treatWhiteSpaceAsEmpty = treatWhiteSpaceAsEmpty;
        }

        /// <summary>
        /// Returns the specified string, or the placeholder when it is blank.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>The string, or the placeholder.</returns>
        public string? Convert(string? value)
        {
            var blank = _treatWhiteSpaceAsEmpty ? string.IsNullOrWhiteSpace(value) : string.IsNullOrEmpty(value);
            return blank ? _fallback : value;
        }
    }
}
