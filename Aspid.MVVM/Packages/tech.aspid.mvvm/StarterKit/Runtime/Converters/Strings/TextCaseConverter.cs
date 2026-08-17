using Aspid.FastTools.Types;
using System;
using System.Text;
using UnityEngine;
using System.Globalization;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the casing of a string.
    /// </summary>
    /// <remarks>
    /// Casing is presentation, so it belongs on this side of the binding. It is also culture-bound —
    /// Turkish has a dotless lower-case I, so a naive upper-casing of "i" produces the wrong letter
    /// there — which is why the culture is a setting rather than an assumption.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Text Case", Tooltip = "Changes the casing of a string")]
    public sealed class TextCaseConverter : IConverterString
    {
        [Tooltip("Which casing to apply.")]
        [SerializeField] private TextCase _case;

        [Tooltip("The culture whose casing rules apply. Turkish and Azeri differ from the rest.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: upper-casing.</remarks>
        public TextCaseConverter() { }

        /// <param name="textCase">Which casing to apply.</param>
        /// <param name="culture">The culture whose casing rules apply.</param>
        public TextCaseConverter(TextCase textCase, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _case = textCase;
            _culture = culture;
        }

        /// <summary>
        /// Applies the configured casing.
        /// </summary>
        /// <param name="value">The string to recase.</param>
        /// <returns>The recased string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the casing is not a declared value.</exception>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var culture = _culture.ToCultureInfo();

            return _case switch
            {
                TextCase.Upper => value!.ToUpper(culture),
                TextCase.Lower => value!.ToLower(culture),
                TextCase.FirstUpper => char.ToUpper(value![0], culture) + value[1..],
                TextCase.Title => culture.TextInfo.ToTitleCase(value!.ToLower(culture)),
                TextCase.Sentence => Sentence(value!, culture),
                TextCase.Invert => Invert(value!, culture),
                _ => throw new ArgumentOutOfRangeException(nameof(_case), _case, null)
            };
        }

        // Lowering the whole string first and then raising the sentence openings would read better,
        // but it means a second string nobody sees. One pass over the characters produces the same
        // text and only the one the caller gets.
        private string Sentence(string value, CultureInfo culture)
        {
            var builder = Builder();
            var opening = true;

            foreach (var character in value)
            {
                if (opening && char.IsLetter(character))
                {
                    builder.Append(char.ToUpper(character, culture));
                    opening = false;
                    continue;
                }

                builder.Append(char.ToLower(character, culture));

                // Anything between the stop and the next letter — a quote, a bracket, the space —
                // stays where it is and keeps the sentence open.
                if (character is '.' or '!' or '?') opening = true;
            }

            return builder.ToString();
        }

        private string Invert(string value, CultureInfo culture)
        {
            var builder = Builder();

            foreach (var character in value)
                builder.Append(char.IsUpper(character)
                    ? char.ToLower(character, culture)
                    : char.ToUpper(character, culture));

            return builder.ToString();
        }

        private StringBuilder Builder()
        {
            _builder ??= new StringBuilder();
            _builder.Clear();

            return _builder;
        }
    }
}
