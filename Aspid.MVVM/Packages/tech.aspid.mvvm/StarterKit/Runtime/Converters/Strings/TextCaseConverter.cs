using System;
using UnityEngine;
using System.Globalization;

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
    public sealed class TextCaseConverter : IConverterString
    {
        [Tooltip("Which casing to apply.")]
        [SerializeField] private TextCase _case;

        [Tooltip("The culture whose casing rules apply. Turkish and Azeri differ from the rest.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCaseConverter"/> class upper-casing.
        /// </summary>
        public TextCaseConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCaseConverter"/> class.
        /// </summary>
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
