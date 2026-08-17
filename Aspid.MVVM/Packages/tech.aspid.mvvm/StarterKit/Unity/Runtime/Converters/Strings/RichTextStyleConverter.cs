#nullable enable
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
    /// Wraps a string in rich-text style tags.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Rich Text Style", Tooltip = "Wraps a string in rich-text style tags")]
    public sealed class RichTextStyleConverter : IConverterString
    {
        [Tooltip("Wrap in <b>.")]
        [SerializeField] private bool _bold;

        [Tooltip("Wrap in <i>.")]
        [SerializeField] private bool _italic;

        [Tooltip("Wrap in <u>.")]
        [SerializeField] private bool _underline;

        [Tooltip("Wrap in <s>.")]
        [SerializeField] private bool _strikethrough;

        public RichTextStyleConverter() { }

        /// <param name="bold">Whether to wrap in <c>&lt;b&gt;</c>.</param>
        /// <param name="italic">Whether to wrap in <c>&lt;i&gt;</c>.</param>
        /// <param name="underline">Whether to wrap in <c>&lt;u&gt;</c>.</param>
        /// <param name="strikethrough">Whether to wrap in <c>&lt;s&gt;</c>.</param>
        public RichTextStyleConverter(
            bool bold = false,
            bool italic = false,
            bool underline = false,
            bool strikethrough = false)
        {
            _bold = bold;
            _italic = italic;
            _underline = underline;
            _strikethrough = strikethrough;
        }

        /// <summary>
        /// Wraps the specified string in the configured tags.
        /// </summary>
        /// <param name="value">The string to style.</param>
        /// <returns>The tagged string.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var text = value!;
            if (_bold) text = "<b>" + text + "</b>";
            if (_italic) text = "<i>" + text + "</i>";
            if (_underline) text = "<u>" + text + "</u>";
            if (_strikethrough) text = "<s>" + text + "</s>";

            return text;
        }
    }
}
