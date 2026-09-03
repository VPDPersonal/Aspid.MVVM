#nullable enable
using System;
using System.Text;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes rich-text markup out of a string, or shows it as text instead of obeying it.
    /// </summary>
    /// <remarks><see cref="RichTextSanitize.Escape"/> emits TextMeshPro's <c>&lt;noparse&gt;</c>; legacy uGUI <c>Text</c> needs <see cref="RichTextSanitize.Strip"/>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/Rich Text",
        Name = "Sanitize",
        Tooltip = "Takes rich-text markup out of a string, or shows it as text instead of obeying it")]
    public sealed class RichTextSanitizeConverter : IConverter<string?, string?>
    {
        [Tooltip("Whether markup is removed or shown as text.")]
        [SerializeField] private RichTextSanitize _mode = RichTextSanitize.Strip;

        [Tooltip("Tag names allowed through, without brackets: b, color. Closing tags match.")]
        [SerializeField] private string?[] _allowedTags = Array.Empty<string>();

        [Tooltip("Keep a bracket that does not open a tag, so \"a < b\" survives.")]
        [SerializeField] private bool _keepStrayBrackets = true;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: stripping every tag.</remarks>
        public RichTextSanitizeConverter() { }

        /// <param name="mode">Whether markup is removed or shown as text.</param>
        /// <param name="allowedTags">Tag names allowed through, without brackets. Closing tags match; <c>color</c> covers <c>&lt;#RRGGBB&gt;</c>.</param>
        /// <param name="keepStrayBrackets">If <see langword="true"/>, a bracket that does not open a tag is left as text.</param>
        public RichTextSanitizeConverter(
            RichTextSanitize mode,
            string[]? allowedTags = null,
            bool keepStrayBrackets = true)
        {
            _mode = mode;
            _keepStrayBrackets = keepStrayBrackets;
            _allowedTags = allowedTags ?? _allowedTags;
        }

        /// <summary>
        /// Removes or escapes the markup in the specified string, leaving the allowed tags untouched.
        /// </summary>
        /// <param name="value">The string to sanitize.</param>
        /// <returns>The sanitized string. An undeclared mode reports an error and strips.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var first = value.IndexOf('<');
            if (first < 0) return value;

            _builder ??= new StringBuilder();
            _builder.Clear();
            _builder.Append(value, 0, first);

            var index = first;

            while (index < value.Length)
            {
                if (value[index] != '<')
                {
                    _builder.Append(value[index]);
                    index++;
                    continue;
                }

                var close = value.IndexOf('>', index + 1);

                if (close < 0)
                {
                    _builder.Append(value, index, value.Length - index);
                    break;
                }

                // A second '<' before the close: the first is a stray bracket, the second may open a tag.
                var nested = value.IndexOf('<', index + 1);
                if (nested >= 0 && nested < close)
                {
                    _builder.Append(value, index, nested - index);
                    index = nested;
                    continue;
                }

                Write(_builder, value, index, close);
                index = close + 1;
            }

            return _builder.ToString();
        }

        private void Write(StringBuilder builder, string value, int open, int close)
        {
            var start = open + 1;
            var length = close - start;
            var span = close - open + 1;

            if (IsAllowed(value, start, length) || (_keepStrayBrackets && !IsTagLike(value, start, length)))
            {
                builder.Append(value, open, span);
                return;
            }

            if (_mode is RichTextSanitize.Strip) return;

            if (_mode is not RichTextSanitize.Escape)
            {
                this.LogError(
                    problem: $"the mode {_mode.Describe()} is not a declared {nameof(RichTextSanitize)}",
                    consequence: "Stripping the markup.");

                return;
            }

            // A <noparse> tag inside the wrapper would end it early.
            if (IsNamed(value, start, length, "noparse")) return;

            builder
                .Append("<noparse>")
                .Append(value, open, span)
                .Append("</noparse>");
        }

        // A tag is a name, optionally closing, optionally with a value: <b>, </b>, <size=40>, <#ffffff>.
        private static bool IsTagLike(string value, int start, int length)
        {
            if (length <= 0) return false;

            var index = start;
            var end = start + length;

            if (value[index] == '/') index++;
            if (index >= end) return false;

            if (value[index] == '#') return true;
            if (!char.IsLetter(value[index])) return false;

            for (index++; index < end; index++)
            {
                var character = value[index];
                if (char.IsLetterOrDigit(character) || character == '-') continue;

                return character is '=' or ' ';
            }

            return true;
        }

        private bool IsAllowed(string value, int start, int length)
        {
            if (_allowedTags is not { Length: > 0 } || length <= 0) return false;

            var name = value[start] == '/' ? start + 1 : start;

            if (name < start + length && value[name] == '#') return AllowsTag("color");

            foreach (var tag in _allowedTags)
            {
                if (tag is not null && IsNamed(value, start, length, tag))
                    return true;
            }

            return false;
        }

        private static bool IsNamed(string value, int start, int length, string name)
        {
            if (length <= 0 || name.Length == 0) return false;

            var index = value[start] == '/' ? start + 1 : start;
            var end = start + length;
            var nameStart = index;

            while (index < end && (char.IsLetterOrDigit(value[index]) || value[index] == '-')) index++;

            return index - nameStart == name.Length
                && string.Compare(value, nameStart, name, 0, name.Length, StringComparison.OrdinalIgnoreCase) == 0;
        }

        private bool AllowsTag(string name)
        {
            if (_allowedTags is not { Length: > 0 }) return false;

            foreach (var tag in _allowedTags)
            {
                if (string.Equals(tag, name, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
