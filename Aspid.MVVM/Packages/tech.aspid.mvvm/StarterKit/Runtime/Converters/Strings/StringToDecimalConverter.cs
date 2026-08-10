using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads an exact decimal number out of text.
    /// </summary>
    /// <remarks>
    /// Prices, balances and anything else where a tenth has to stay a tenth: binary floating point
    /// cannot hold 0.1, so a shop total accumulated in <see langword="float"/> drifts by a fraction of
    /// a cent and then shows it.
    /// <para>
    /// Unity cannot serialize a <see langword="decimal"/> field, so the fallback is authored as text
    /// and read with the invariant culture — write <c>1.5</c>, never <c>1,5</c>, whatever the machine
    /// reads player input with. A comma there is refused rather than read as a group separator, so an
    /// author who types the number their own machine shows them is told about it instead of getting
    /// ten times the value they meant. Bounds are not offered: they would have to be authored as text
    /// for the same reason, and nothing this converter is for — a price, a balance, an id — has
    /// wanted a range yet.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Decimal", Tooltip = "Reads an exact decimal number out of text")]
    public sealed class StringToDecimalConverter : ITwoWayConverter<string?, decimal>
    {
        [Tooltip("Returned when the text is not a number. Written in the invariant culture — 1.5, "
            + "never 1,5 — because it is authored once rather than typed by a player.")]
        [SerializeField] private string _fallback = "0";

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToDecimalConverter"/> class falling back to zero.
        /// </summary>
        public StringToDecimalConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToDecimalConverter"/> class.
        /// </summary>
        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToDecimalConverter(decimal fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback.ToString(CultureInfo.InvariantCulture);
            _culture = culture;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public decimal Convert(string? value)
        {
            // Blank text is an unfilled field, not a malformed number. Reporting it would fire on
            // every scene with an empty input, which is the noise that gets error logs ignored.
            if (string.IsNullOrWhiteSpace(value)) return Fallback();

            // AllowExponent on top of Number: the reason to reach for this converter over the double
            // one is a value that arrived as text from somewhere exact, and a backend that serializes
            // 1E5 would otherwise be readable by the converter with less precision and not by this one.
            const NumberStyles styles = NumberStyles.Number | NumberStyles.AllowExponent;

            return decimal.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed)
                ? parsed
                : OnUnparsed(value);
        }

        // The fallback arrives as text and a binder pushes on every notification, so the parse is
        // kept against the string it came from rather than repeated per call. Unity hands back a
        // fresh string on deserialization, which is precisely when the cache should miss.
        private decimal Fallback()
        {
            if (ReferenceEquals(_fallbackSource, _fallback)) return _fallbackValue;

            _fallbackSource = _fallback;
            _fallbackValue = decimal.Zero;

            // An unfilled field is zero rather than a malformed number, the same reading blank input
            // gets a few lines above.
            if (string.IsNullOrWhiteSpace(_fallback)) return _fallbackValue;

            // Float rather than Number: the fallback is read as invariant, where the comma is the
            // GROUP separator, so Number would read the "1,5" the field's own tooltip warns against
            // as fifteen instead of refusing it — silently, and authored once for every player.
            const NumberStyles styles = NumberStyles.Float;

            if (decimal.TryParse(_fallback, styles, CultureInfo.InvariantCulture, out var parsed))
            {
                _fallbackValue = parsed;
                return _fallbackValue;
            }

            // Reported rather than thrown whatever the failure mode says: this is the value the
            // converter falls back TO, so throwing from it would leave the failure path with nothing
            // to return, and a fallback that reads as zero is a scene bug nobody would otherwise see.
            ConverterFailure.Report(
                ref _loggedFallbackFailure,
                nameof(StringToDecimalConverter),
                _fallback,
                "a fallback written as an exact decimal number in the invariant culture — 1.5, never 1,5",
                "zero");

            return _fallbackValue;
        }

        private decimal OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToDecimalConverter), value, "an exact decimal number");

            ConverterFailure.Report(
                ref _loggedFailure,
                nameof(StringToDecimalConverter),
                value,
                "an exact decimal number",
                "the fallback");
            return Fallback();
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(decimal value) => value.ToString(_culture.ToCultureInfo());

        [NonSerialized] private string? _fallbackSource;
        [NonSerialized] private decimal _fallbackValue;
        [NonSerialized] private bool _loggedFailure;
        [NonSerialized] private bool _loggedFallbackFailure;
    }
}
