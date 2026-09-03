#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a 3D vector out of text.
    /// </summary>
    /// <inheritdoc cref="StringToVector2Converter" path="/remarks"/>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Vector",
        Name = "Parse Vector3",
        Tooltip = "Reads a 3D vector out of text")]
    public sealed class StringToVector3Converter :
        ITwoWayConverter<string?, Vector3>,
        ISerializationCallbackReceiver
    {
        [Tooltip("Placed between the components. Empty stands for a comma.")]
        [SerializeField] private string _separator = ",";

        [Tooltip("The culture the components are read and written with. Falls back to invariant when its decimal separator is the separator.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [Tooltip("Returned when the text is not a vector.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private Vector3 _fallback;

        [NonSerialized] private string? _parsedText;
        [NonSerialized] private Vector3 _parsed;

        /// <remarks>Default: reading comma-separated text.</remarks>
        public StringToVector3Converter() { }

        /// <param name="separator">Placed between the components. Empty stands for a comma.</param>
        /// <param name="fallback">Returned when the text is not a vector. When omitted, a zero vector.</param>
        /// <param name="culture">The culture the components are read and written with. Falls back to invariant when its decimal separator is the separator.</param>
        public StringToVector3Converter(
            string separator,
            Vector3? fallback = null,
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
        public Vector3 Convert(string? value)
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
        /// <returns>The three components with the separator between them, without brackets.</returns>
        public string ConvertBack(Vector3 value)
        {
            var separator = VectorText.Separator(_separator);
            var culture = VectorText.ComponentCulture(_culture.ToCultureInfo(), separator);

            return value.x.ToString(culture) +
                separator + value.y.ToString(culture) +
                separator + value.z.ToString(culture);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _parsedText = null;

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

        private string ExpectedText() =>
            $"three numbers separated by \"{VectorText.Separator(_separator)}\"";
    }
}
