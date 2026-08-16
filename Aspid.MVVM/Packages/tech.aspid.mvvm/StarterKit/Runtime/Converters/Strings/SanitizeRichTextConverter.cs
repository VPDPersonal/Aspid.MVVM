#nullable enable
using Aspid.FastTools.Types;
using System;
using System.Text;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes rich-text markup out of a string, or shows it as text instead of obeying it.
    /// </summary>
    /// <remarks>
    /// <c>RichTextNoParseConverter</c> answers the same problem by wrapping the whole string, which also
    /// disables the markup the game itself put there; this works tag by tag.
    /// <para>
    /// <see cref="RichTextSanitize.Escape"/> emits <c>&lt;noparse&gt;</c>, which is TextMeshPro's tag — a
    /// legacy uGUI <c>Text</c> shows it as letters, so use <see cref="RichTextSanitize.Strip"/> there.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Sanitize Rich Text", Tooltip = "Takes rich-text markup out of a string, or shows it as text instead of obeying it")]
    public sealed class SanitizeRichTextConverter : IConverter<string?, string?>
    {
        [Tooltip("Whether markup is removed or shown as text.")]
        [SerializeField] private RichTextSanitize _mode = RichTextSanitize.Strip;

        [Tooltip("Tag names allowed through untouched, written without their angle brackets: b, "
            + "color. The closing tag goes through under the same name, and color also covers the "
            + "<#RRGGBB> spelling.")]
        [SerializeField] private string[] _allowedTags = Array.Empty<string>();

        [Tooltip("Treat an angle bracket that does not open a tag as ordinary text, so that \"a < b\" "
            + "and \"<3\" survive. Turn this off to treat every <…> as markup.")]
        [SerializeField] private bool _keepStrayBrackets = true;

        [NonSerialized] private StringBuilder? _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="SanitizeRichTextConverter"/> class stripping every tag.
        /// </summary>
        public SanitizeRichTextConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SanitizeRichTextConverter"/> class.
        /// </summary>
        /// <param name="mode">Whether markup is removed or shown as text.</param>
        /// <param name="allowedTags">Tag names allowed through untouched, without their angle brackets.</param>
        /// <param name="keepStrayBrackets">
        /// If <see langword="true"/>, an angle bracket that does not open a tag is left as text.
        /// </param>
        public SanitizeRichTextConverter(
            RichTextSanitize mode,
            string[]? allowedTags = null,
            bool keepStrayBrackets = true)
        {
            _mode = mode;
            _keepStrayBrackets = keepStrayBrackets;
            if (allowedTags is not null) _allowedTags = allowedTags;
        }

        /// <summary>
        /// Removes the markup from the specified string, or escapes it.
        /// </summary>
        /// <param name="value">The string to sanitize.</param>
        /// <returns>The string with the markup dealt with.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // Most strings carry no markup at all. Finding that out costs one scan and hands back the
            // string that came in, rather than a copy of it.
            var first = value!.IndexOf('<');
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

                // A second '<' inside the run means the run is not one thing. Judging it as a whole
                // is how "< <size=400%>" got through: the leading space made the span look like
                // prose, and keeping it verbatim carried the live tag along with it. Emit the stray
                // bracket on its own and let the inner '<' be judged as the tag it is.
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

        // Writes the span from the opening bracket at open to the closing one at close, inclusive.
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

            if (_mode is not RichTextSanitize.Escape)
                throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null);

            // <noparse> is the one tag that cannot be shown this way: its own closing tag, inside the
            // wrapper, would end the block early and swallow everything after it. Dropping it is all
            // that is left.
            if (IsNamed(value, start, length, "noparse")) return;

            _builder!.Append("<noparse>").Append(value, open, span).Append("</noparse>");
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

            // <#RRGGBB> is the colour tag under a shorter name.
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

            // <#RRGGBB> says what <color=#RRGGBB> says, so the name that allows the one allows the
            // other; on its own it has no name to match against.
            if (name < start + length && value[name] == '#') return AllowsTag("color");

            foreach (var tag in _allowedTags)
                if (tag is not null && IsNamed(value, start, length, tag))
                    return true;

            return false;
        }

        // Whether the tag between the brackets carries this name. Compared in place: the name is a
        // slice of the bound string, and cutting it out to compare it would allocate on every push.
        private static bool IsNamed(string value, int start, int length, string name)
        {
            // An empty entry in the allowed list — a designer adding an array element and not
            // filling it in — must not match the tag that has no name either.
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
                if (string.Equals(tag, name, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }
    }
}
