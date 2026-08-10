using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a 3D vector out of text.
    /// </summary>
    /// <inheritdoc cref="StringToVector2Converter" path="/remarks"/>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Vector3", Tooltip = "Reads a 3D vector out of text")]
    public sealed class StringToVector3Converter : ITwoWayConverter<string?, Vector3>
    {
        [Tooltip("Placed between the components. The Inspector has no field for a single character, "
            + "so the whole text is the separator — \"; \" works as well as \",\". Left empty, a "
            + "comma stands in.")]
        [SerializeField] private string _separator = ",";

        [Tooltip("Returned when the text is not a vector.")]
        [SerializeField] private Vector3 _fallback;

        [Tooltip("The culture the components are read and written with. Invariant unless the "
            + "separator is something other than a comma: a culture whose own decimal separator is "
            + "the separator cannot write a triple this converter reads back, so it is read as "
            + "invariant anyway.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToVector3Converter"/> class reading comma-separated text.
        /// </summary>
        public StringToVector3Converter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToVector3Converter"/> class.
        /// </summary>
        /// <param name="separator">Placed between the components.</param>
        /// <param name="fallback">Returned when the text is not a vector.</param>
        /// <param name="culture">The culture the components are read with.</param>
        public StringToVector3Converter(
            string separator,
            Vector3 fallback = default,
            CultureInfoMode culture = CultureInfoMode.InvariantCulture)
        {
            _separator = separator;
            _fallback = fallback;
            _culture = culture;
        }

        /// <summary>
        /// Reads a vector out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The vector, or the fallback when the text is not one.</returns>
        public Vector3 Convert(string? value)
        {
            // Blank text is an unfilled field, not a malformed vector.
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            // Splitting allocates and a binder pushes on every notification, not on every change, so
            // the last parse is kept. The separator and the culture are part of the key: both are
            // editable while the game runs, and a hit that ignored them would freeze the old reading in.
            if (string.Equals(_parsedText, value, StringComparison.Ordinal)
                && ReferenceEquals(_parsedSeparator, _separator)
                && _parsedCulture == _culture)
                return _parsed;

            var parsed = Parse(value!);
            if (parsed is null) return OnUnparsed(value);

            _parsedText = value;
            _parsedSeparator = _separator;
            _parsedCulture = _culture;
            _parsed = parsed.Value;

            return _parsed;
        }

        private Vector3? Parse(string value)
        {
            var separator = VectorText.Separator(_separator);
            var (start, end) = VectorText.Unwrap(value);

            var first = value.IndexOf(separator, start, end - start, StringComparison.Ordinal);
            if (first < 0) return null;

            var second = first + separator.Length;
            second = value.IndexOf(separator, second, end - second, StringComparison.Ordinal);
            if (second < 0) return null;

            var culture = VectorText.ComponentCulture(_culture.ToCultureInfo(), separator);
            var middle = first + separator.Length;
            var last = second + separator.Length;

            if (!VectorText.TryReadAxis(value, start, first - start, culture, out var x)) return null;
            if (!VectorText.TryReadAxis(value, middle, second - middle, culture, out var y)) return null;
            if (!VectorText.TryReadAxis(value, last, end - last, culture, out var z)) return null;

            return new Vector3(x, y, z);
        }

        private Vector3 OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToVector3Converter), value, Expected());

            // Report keeps the first message and drops every one after it, and text that will not
            // parse usually fails on every push: composing the message past that point allocates a
            // string per notification for the guard inside Report to throw away.
            if (_loggedFailure) return _fallback;

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToVector3Converter), value, Expected(), "the fallback vector");
            return _fallback;
        }

        private string Expected() => $"three numbers separated by \"{VectorText.Separator(_separator)}\"";

        /// <summary>
        /// Writes the specified vector as text.
        /// </summary>
        /// <param name="value">The vector to write.</param>
        /// <returns>
        /// The three components with the separator between them, without brackets. The components are
        /// written as invariant when the chosen culture's decimal separator is the separator itself,
        /// so that what is written here is what <see cref="Convert"/> reads.
        /// </returns>
        public string ConvertBack(Vector3 value)
        {
            var separator = VectorText.Separator(_separator);
            var culture = VectorText.ComponentCulture(_culture.ToCultureInfo(), separator);

            return value.x.ToString(culture)
                + separator + value.y.ToString(culture)
                + separator + value.z.ToString(culture);
        }

        [NonSerialized] private string? _parsedText;
        [NonSerialized] private string? _parsedSeparator;
        [NonSerialized] private CultureInfoMode _parsedCulture;
        [NonSerialized] private Vector3 _parsed;
        [NonSerialized] private bool _loggedFailure;
    }
}
