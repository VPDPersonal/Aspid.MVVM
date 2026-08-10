using Aspid.FastTools.Types;
using System;
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
                _ => throw new ArgumentOutOfRangeException(nameof(_case), _case, null)
            };
        }
    }
}
