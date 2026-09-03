#nullable enable
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// An authored <see langword="decimal"/> kept as the text a Unity field can hold.
    /// </summary>
    /// <remarks>Unity cannot serialize a <see langword="decimal"/>, so exact values are authored as text. The reading is cached.</remarks>
    internal struct DecimalText
    {
        private string? _source;
        private decimal _value;

        /// <summary>
        /// Reads the number the specified text is written as.
        /// </summary>
        /// <param name="text">The authored text, written in the invariant culture.</param>
        /// <param name="blank">Read for blank text.</param>
        /// <param name="converter">The reporting converter.</param>
        /// <param name="field">What the text is, as it reads in the sentence: "the fallback".</param>
        /// <returns>The number, or <paramref name="blank"/> when the text is blank or unreadable.</returns>
        internal decimal Read(string? text, decimal blank, IConverter converter, string field)
        {
            if (ReferenceEquals(_source, text)) return _value;

            _source = text;
            _value = blank;

            if (string.IsNullOrWhiteSpace(text)) return _value;

            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed))
            {
                _value = parsed;
                return _value;
            }

            converter.LogError(
                problem: $"{field} \"{text}\" is not a decimal number in the invariant culture (1.5, never 1,5)",
                consequence: $"Using {_value.ToString(CultureInfo.InvariantCulture)}.");

            return _value;
        }
    }
}
