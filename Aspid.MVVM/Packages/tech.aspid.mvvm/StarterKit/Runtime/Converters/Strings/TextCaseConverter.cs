using System;
using System.Text;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the casing of a string.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Text Case",
        Tooltip = "Changes the casing of a string")]
    public sealed class TextCaseConverter : IConverter<string?, string?>
    {
        [Tooltip("Which casing to apply.")]
        [SerializeField] private TextCase _case;

        [Tooltip("The culture whose casing rules apply. Turkish and Azeri differ from the rest.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: upper-casing.</remarks>
        public TextCaseConverter() { }

        /// <param name="textCase">Which casing to apply.</param>
        /// <param name="culture">
        /// The culture whose casing rules apply. Turkish and Azeri differ from the rest.
        /// </param>
        public TextCaseConverter(TextCase textCase, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _case = textCase;
            _culture = culture;
        }

        /// <summary>
        /// Applies the configured casing.
        /// </summary>
        /// <param name="value">The string to recase.</param>
        /// <returns>
        /// The recased string — or the string unchanged when the casing is not a declared value.
        /// </returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var culture = _culture.ToCultureInfo();

            return _case switch
            {
                TextCase.Upper => value.ToUpper(culture),
                TextCase.Lower => value.ToLower(culture),
                TextCase.FirstUpper => char.ToUpper(value[0], culture) + value[1..],
                TextCase.Title => culture.TextInfo.ToTitleCase(value.ToLower(culture)),
                TextCase.Sentence => Sentence(value, culture),
                TextCase.Invert => Invert(value, culture),
                _ => Undeclared(value)
            };
        }

        private string? Undeclared(string? value)
        {
            this.LogError($"the case {_case.Describe()} is not a declared {nameof(TextCase)}",
                "Returning the value unchanged.");

            return value;
        }

        // One pass instead of lowering the whole string first: that would allocate a second string.
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

                // A quote or bracket between the stop and the next letter keeps the sentence open.
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
