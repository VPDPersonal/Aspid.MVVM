#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a number as colored text, the color chosen by how large it is.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Threshold Rich Text Color",
        Tooltip = "Writes a number as colored text, the color chosen by how large it is")]
    public sealed class ThresholdRichTextColorConverter :
        IConverter<float, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<double, string>
    {
        [Tooltip("Colors by threshold. The highest threshold at or below the value wins.")]
        [SerializeField] private ColorStop[] _stops = Array.Empty<ColorStop>();

        [Tooltip("Writes the number itself. Empty writes it in the general format.")]
        [TypeSelector]
        [SerializeReference] private IConverter<float, string?>? _number = new NumberFormatConverter("0.##");

        [Tooltip("Used when the value is below every threshold.")]
        [SerializeField] private Color _fallback = Color.white;

        private ThresholdRichTextColorConverter() { }

        /// <param name="stops">
        /// Colors by threshold. With none the converter has nothing to pick from, which is reported
        /// as an error.
        /// </param>
        /// <param name="fallback">Used when the value is below every threshold.</param>
        /// <param name="number">
        /// Writes the number itself. When omitted, writes it as <c>0.##</c> in the device locale.
        /// </param>
        public ThresholdRichTextColorConverter(
            ColorStop[]? stops,
            Color fallback,
            IConverter<float, string?>? number = null)
        {
            _stops = stops ?? Array.Empty<ColorStop>();
            _fallback = fallback;
            _number = number ?? _number;
        }

        /// <summary>
        /// Writes the specified number as colored text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The tagged number.</returns>
        public string Convert(float value) =>
            RichTextColorConverter.Wrap(Format(value), ColorFor(value), includeAlpha: false);

        private string Format(float value) =>
            _number?.Convert(value) ?? value.ToString(CultureInfo.CurrentCulture);

        private Color ColorFor(float value)
        {
            if (_stops.Length == 0)
            {
                this.LogError(
                    problem: "no stops are authored",
                    consequence: "Returning the fallback color.");

                return _fallback;
            }

            var color = _fallback;
            var best = float.NegativeInfinity;

            foreach (var stop in _stops)
            {
                if (value < stop.Threshold || stop.Threshold <= best) continue;

                best = stop.Threshold;
                color = stop.Color;
            }

            return color;
        }

        string IConverter<int, string>.Convert(int value) =>
            Convert(value);

        string IConverter<long, string>.Convert(long value) =>
            Convert(value);

        string IConverter<double, string>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
