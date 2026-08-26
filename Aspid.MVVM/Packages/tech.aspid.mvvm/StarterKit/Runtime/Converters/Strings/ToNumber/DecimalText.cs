using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// An authored <see langword="decimal"/> kept as the text a Unity field can hold.
    /// </summary>
    /// <remarks>
    /// Unity cannot serialize a <see langword="decimal"/> field, so exact values are authored as text.
    /// The reading is cached against the string it came from.
    /// </remarks>
    internal struct DecimalText
    {
        private string? _source;
        private decimal _value;

        /// <summary>
        /// Reads the number the specified text is written as.
        /// </summary>
        /// <param name="text">The authored text, written in the invariant culture.</param>
        /// <param name="blank">Read for blank text.</param>
        /// <param name="converter">The reporting converter — pass <see langword="this"/>.</param>
        /// <param name="field">What the text is, as it reads in the sentence — "the fallback".</param>
        /// <returns>The number, or <paramref name="blank"/> when the text is blank or unreadable.</returns>
        /// <remarks>
        /// Unreadable text is logged rather than passed to the failure mode: this is authored state,
        /// not a bound value.
        /// </remarks>
        internal decimal Read(string? text, decimal blank, IConverter converter, string field)
        {
            if (ReferenceEquals(_source, text)) return _value;

            _source = text;
            _value = blank;

            if (string.IsNullOrWhiteSpace(text)) return _value;

            // Float rather than Number: the text is read as invariant, where the comma is the group
            // separator, so Number would read "1,5" as fifteen instead of refusing it.
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed))
            {
                _value = parsed;
                return _value;
            }

            converter.LogError(
                $"{field} \"{text}\" is not an exact decimal number written in the invariant culture — " +
                "1.5, never 1,5",
                $"Using {_value.ToString(CultureInfo.InvariantCulture)}.");

            return _value;
        }
    }
}
