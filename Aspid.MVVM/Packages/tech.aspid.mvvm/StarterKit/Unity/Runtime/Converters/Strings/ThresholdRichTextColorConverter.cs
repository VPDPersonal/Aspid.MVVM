#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a number as coloured text, the colour chosen by how large it is.
    /// </summary>
    /// <remarks>
    /// Health below a quarter in red, above three quarters in green, and the number itself in the
    /// same label — one converter instead of a number binder plus a colour binder that have to be
    /// kept in step.
    /// </remarks>
    [Serializable]
    public sealed class ThresholdRichTextColorConverter : IConverter<float, string>
    {
        [Tooltip("Colours by threshold. The highest threshold at or below the value wins.")]
        [SerializeField] private ColorStop[] _stops = Array.Empty<ColorStop>();

        [Tooltip("Used when the value is below every threshold.")]
        [SerializeField] private Color _fallback = Color.white;

        [Tooltip("A standard numeric format string for the number itself.")]
        [SerializeField] private string _numberFormat = "0.##";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdRichTextColorConverter"/> class with no stops.
        /// </summary>
        public ThresholdRichTextColorConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdRichTextColorConverter"/> class.
        /// </summary>
        /// <param name="stops">Colours by threshold.</param>
        /// <param name="fallback">Used when the value is below every threshold.</param>
        public ThresholdRichTextColorConverter(ColorStop[]? stops, Color fallback)
        {
            _stops = stops ?? Array.Empty<ColorStop>();
            _fallback = fallback;
        }

        /// <summary>
        /// Writes the specified number as coloured text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The tagged number.</returns>
        public string Convert(float value)
        {
            var text = value.ToString(_numberFormat, _culture.ToCultureInfo());
            return RichTextColorConverter.Wrap(text, ColorFor(value), includeAlpha: false);
        }

        private Color ColorFor(float value)
        {
            if (_stops is not { Length: > 0 }) return _fallback;

            var color = _fallback;
            var best = float.NegativeInfinity;

            // The stops are authored in whatever order the Inspector left them, so the highest
            // qualifying threshold has to be found rather than assumed to be last.
            for (var i = 0; i < _stops.Length; i++)
                if (value >= _stops[i].Threshold && _stops[i].Threshold > best)
                {
                    best = _stops[i].Threshold;
                    color = _stops[i].Color;
                }

            return color;
        }
    }
}
