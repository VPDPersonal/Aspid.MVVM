using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a 2D vector out of text.
    /// </summary>
    /// <remarks>
    /// Reads back what a vector's own <see cref="object.ToString"/> writes, brackets and all, so a value
    /// copied out of a log goes straight back in.
    /// <para>
    /// The culture defaults to invariant rather than to the machine's, unlike the rest of this family: a
    /// culture that writes <c>1,5</c> cannot also use a comma between components, and thousands
    /// separators are refused for the same reason. A culture chosen anyway whose decimal separator
    /// <i>is</i> the component separator reads and writes its components as invariant instead.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Vector2", Tooltip = "Reads a 2D vector out of text")]
    public sealed class StringToVector2Converter : ITwoWayConverter<string?, Vector2>
    {
        [Tooltip("Placed between the components. The Inspector has no field for a single character, "
            + "so the whole text is the separator — \"; \" works as well as \",\". Left empty, a "
            + "comma stands in.")]
        [SerializeField] private string _separator = ",";

        [Tooltip("Returned when the text is not a vector.")]
        [SerializeField] private Vector2 _fallback;

        [Tooltip("The culture the components are read and written with. Invariant unless the "
            + "separator is something other than a comma: a culture whose own decimal separator is "
            + "the separator cannot write a pair this converter reads back, so it is read as "
            + "invariant anyway.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToVector2Converter"/> class reading comma-separated text.
        /// </summary>
        public StringToVector2Converter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToVector2Converter"/> class.
        /// </summary>
        /// <param name="separator">Placed between the components.</param>
        /// <param name="fallback">Returned when the text is not a vector.</param>
        /// <param name="culture">The culture the components are read with.</param>
        public StringToVector2Converter(
            string separator,
            Vector2 fallback = default,
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
        public Vector2 Convert(string? value)
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

        private Vector2? Parse(string value)
        {
            var separator = VectorText.Separator(_separator);
            var (start, end) = VectorText.Unwrap(value);

            var split = value.IndexOf(separator, start, end - start, StringComparison.Ordinal);
            if (split < 0) return null;

            var culture = VectorText.ComponentCulture(_culture.ToCultureInfo(), separator);
            var rest = split + separator.Length;

            if (!VectorText.TryReadAxis(value, start, split - start, culture, out var x)) return null;
            if (!VectorText.TryReadAxis(value, rest, end - rest, culture, out var y)) return null;

            return new Vector2(x, y);
        }

        private Vector2 OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToVector2Converter), value, Expected());

            // Report keeps the first message and drops every one after it, and text that will not
            // parse usually fails on every push: composing the message past that point allocates a
            // string per notification for the guard inside Report to throw away.
            if (_loggedFailure) return _fallback;

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToVector2Converter), value, Expected(), "the fallback vector");
            return _fallback;
        }

        private string Expected() => $"two numbers separated by \"{VectorText.Separator(_separator)}\"";

        /// <summary>
        /// Writes the specified vector as text.
        /// </summary>
        /// <param name="value">The vector to write.</param>
        /// <returns>
        /// The two components with the separator between them, without brackets. The components are
        /// written as invariant when the chosen culture's decimal separator is the separator itself,
        /// so that what is written here is what <see cref="Convert"/> reads.
        /// </returns>
        public string ConvertBack(Vector2 value)
        {
            var separator = VectorText.Separator(_separator);
            var culture = VectorText.ComponentCulture(_culture.ToCultureInfo(), separator);

            return value.x.ToString(culture) + separator + value.y.ToString(culture);
        }

        [NonSerialized] private string? _parsedText;
        [NonSerialized] private string? _parsedSeparator;
        [NonSerialized] private CultureInfoMode _parsedCulture;
        [NonSerialized] private Vector2 _parsed;
        [NonSerialized] private bool _loggedFailure;
    }
}
