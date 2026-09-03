#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a 2D vector out of text.
    /// </summary>
    /// <remarks>Reads back what a vector's own <see cref="object.ToString"/> writes, brackets and all.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Vector",
        Name = "Parse Vector2",
        Tooltip = "Reads a 2D vector out of text")]
    public sealed class StringToVector2Converter :
        ITwoWayConverter<string?, Vector2>,
        ISerializationCallbackReceiver
    {
        [Tooltip("Placed between the components. Empty stands for a comma.")]
        [SerializeField] private string _separator = ",";

        [Tooltip("The culture the components are read and written with. Falls back to invariant when its decimal separator is the separator.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [Tooltip("Returned when the text is not a vector.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private Vector2 _fallback;

        [NonSerialized] private string? _parsedText;
        [NonSerialized] private Vector2 _parsed;

        /// <remarks>Default: reading comma-separated text.</remarks>
        public StringToVector2Converter() { }

        /// <param name="separator">Placed between the components. Empty stands for a comma.</param>
        /// <param name="fallback">Returned when the text is not a vector. When omitted, a zero vector.</param>
        /// <param name="culture">The culture the components are read and written with. Falls back to invariant when its decimal separator is the separator.</param>
        public StringToVector2Converter(
            string separator,
            Vector2? fallback = null,
            CultureInfoMode culture = CultureInfoMode.InvariantCulture)
        {
            _culture = culture;
            _separator = separator;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Reads a vector out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The vector, or the fallback when the text is not one.</returns>
        public Vector2 Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            if (string.Equals(_parsedText, value, StringComparison.Ordinal)) return _parsed;

            var parsed = Parse(value);

            if (parsed is null)
            {
                return this.UseFallback(
                    fallback: _fallback,
                    problem: value.Expected(ExpectedText()));
            }

            _parsedText = value;
            _parsed = parsed.Value;

            return _parsed;
        }

        /// <summary>
        /// Writes the specified vector as text.
        /// </summary>
        /// <param name="value">The vector to write.</param>
        /// <returns>The two components with the separator between them, without brackets.</returns>
        public string ConvertBack(Vector2 value)
        {
            var separator = VectorText.Separator(_separator);
            var culture = VectorText.ComponentCulture(_culture.ToCultureInfo(), separator);

            return value.x.ToString(culture) + separator + value.y.ToString(culture);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _parsedText = null;

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

        private string ExpectedText() =>
            $"two numbers separated by \"{VectorText.Separator(_separator)}\"";
    }
}
