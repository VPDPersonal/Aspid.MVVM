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
    /// Wraps a string in authored text.
    /// </summary>
    /// <remarks>
    /// Friendlier than remembering where <c>{0}</c> goes, and it can leave a blank value alone rather
    /// than decorating nothing.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Concat String", Tooltip = "Wraps a string in authored text")]
    public sealed class ConcatStringConverter : IConverterString
    {
        [Tooltip("Placed before the value.")]
        [SerializeField] private string _prefix = string.Empty;

        [Tooltip("Placed after the value.")]
        [SerializeField] private string _suffix = string.Empty;

        [Tooltip("Leave a blank value undecorated.")]
        [SerializeField] private bool _skipWhenEmpty = true;

        public ConcatStringConverter() { }

        /// <param name="prefix">Placed before the value.</param>
        /// <param name="suffix">Placed after the value.</param>
        /// <param name="skipWhenEmpty">If <see langword="true"/>, leaves a blank value undecorated.</param>
        public ConcatStringConverter(string prefix, string suffix, bool skipWhenEmpty = true)
        {
            _prefix = prefix;
            _suffix = suffix;
            _skipWhenEmpty = skipWhenEmpty;
        }

        /// <summary>
        /// Wraps the specified string.
        /// </summary>
        /// <param name="value">The string to wrap.</param>
        /// <returns>The wrapped string, or the value unchanged when it is blank and that is configured.</returns>
        public string? Convert(string? value)
        {
            if (_skipWhenEmpty && string.IsNullOrWhiteSpace(value)) return value;
            return _prefix + value + _suffix;
        }
    }
}
