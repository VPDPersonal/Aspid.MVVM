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
    /// <remarks>
    /// <see cref="RichTextSanitize.Escape"/> emits TextMeshPro's <c>&lt;noparse&gt;</c>; use
    /// <see cref="RichTextSanitize.Strip"/> with a legacy uGUI <c>Text</c>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/Rich Text",
        Name = "Sanitize",
        Tooltip = "Takes rich-text markup out of a string, or shows it as text instead of obeying it")]
    public sealed class RichTextSanitizeConverter : IConverter<string?, string?>
    {
        [Tooltip("Whether markup is removed or shown as text.")]
        [SerializeField] private RichTextSanitize _mode = RichTextSanitize.Strip;

        [Tooltip("Tag names allowed through, without brackets: b, color. Closing tags match; color covers <#RRGGBB>.")]
        [SerializeField] private string[] _allowedTags = Array.Empty<string>();

        [Tooltip("A bracket that does not open a tag is text, so \"a < b\" survives.")]
        [SerializeField] private bool _keepStrayBrackets = true;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: stripping every tag.</remarks>
        public RichTextSanitizeConverter() { }

        /// <param name="mode">Whether markup is removed or shown as text.</param>
        /// <param name="allowedTags">
        /// Tag names allowed through, without angle brackets. Closing tags match the same name, and
        /// <c>color</c> also covers <c>&lt;#RRGGBB&gt;</c>.
        /// </param>
        /// <param name="keepStrayBrackets">
        /// If <see langword="true"/>, an angle bracket that does not open a tag is left as text.
        /// </param>
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
        /// Removes the markup from the specified string, or escapes it, leaving the allowed tags
        /// untouched.
        /// </summary>
        /// <param name="value">The string to sanitize.</param>
        /// <returns>
        /// The sanitized string — stripped when the mode is not a declared value.
        /// </returns>
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

                // Nothing closes this bracket, so nothing after it is a tag either.
                if (close < 0)
                {
                    _builder.Append(value, index, value.Length - index);
                    break;
                }

                // A second '<' inside the run: emit the stray bracket on its own and judge the
                // inner '<' as the tag it is, or "< <size=400%>" slips through as prose.
                var nested = value.IndexOf('<', index + 1);
                if (nested >= 0 && nested < close)
                {
                    _builder.Append(value, index, nested - index);
                    index = nested;
                    continue;
                }

                Write(value, index, close);
                index = close + 1;
            }

            return _builder.ToString();
        }

        // Decides what happens to the tag between the brackets at open and close, inclusive.
        private void Write(string value, int open, int close)
        {
            var start = open + 1;
            var length = close - start;
            var span = close - open + 1;

            if (IsAllowed(value, start, length) || (_keepStrayBrackets && !IsTagLike(value, start, length)))
            {
                _builder!.Append(value, open, span);
                return;
            }

            if (_mode is RichTextSanitize.Strip) return;

            // Stripping rather than handing the markup back: a broken setting must not be the way
            // live markup reaches a text component.
            if (_mode is not RichTextSanitize.Escape)
            {
                this.LogError(
                    problem: $"the mode {_mode.Describe()} is not a declared {nameof(RichTextSanitize)}",
                    consequence: "Stripping the markup.");

                return;
            }

            // <noparse> is the one tag that cannot be shown this way: its own closing tag, inside the
            // wrapper, would end the block early and swallow everything after it.
            if (IsNamed(value, start, length, "noparse")) return;

            _builder!
                .Append("<noparse>")
                .Append(value, open, span)
                .Append("</noparse>");
        }

        // A tag is a name, optionally closing, optionally carrying a value: <b>, </b>, <size=40>,
        // <color="#ffffff">, <#ffffff>. Anything else between two brackets — "a < b", "5<10", "<3" —
        // is text that happens to contain one.
        private static bool IsTagLike(string value, int start, int length)
        {
            if (length <= 0) return false;

            var index = start;
            var end = start + length;

            if (value[index] == '/') index++;
            if (index >= end) return false;

            // <#RRGGBB> is the color tag under a shorter name.
            if (value[index] == '#') return true;
            if (!char.IsLetter(value[index])) return false;

            for (index++; index < end; index++)
            {
                var character = value[index];
                if (char.IsLetterOrDigit(character) || character == '-') continue;

                // The name has ended. What follows is a value or an attribute, and only a tag has
                // either.
                return character is '=' or ' ';
            }

            return true;
        }

        private bool IsAllowed(string value, int start, int length)
        {
            if (_allowedTags is not { Length: > 0 } || length <= 0) return false;

            var name = value[start] == '/' ? start + 1 : start;

            // <#RRGGBB> has no name of its own, so it matches under color.
            if (name < start + length && value[name] == '#') return AllowsTag("color");

            foreach (var tag in _allowedTags)
            {
                if (tag is not null && IsNamed(value, start, length, tag))
                    return true;
            }

            return false;
        }

        // Compared in place: the name is a slice of the bound string, and cutting it out to compare
        // it would allocate on every push.
        private static bool IsNamed(string value, int start, int length, string name)
        {
            // An unfilled entry in the allowed list must not match the tag that has no name either.
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
