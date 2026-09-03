#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts seconds remaining to a 0..1 progress value.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Countdown Progress",
        Tooltip = "Converts seconds remaining to a 0..1 progress value")]
    public sealed class CountdownProgressConverter : IConverter<float, float>, IConverter<double, double>
    {
        [Tooltip("The full duration, in seconds.")]
        [SerializeField] [Min(0f)] private float _totalSeconds = 1f;

        [Tooltip("Return the elapsed fraction instead of the remaining one.")]
        [SerializeField] private bool _elapsed;

        /// <remarks>Default: over one second.</remarks>
        public CountdownProgressConverter() { }

        /// <param name="totalSeconds">The full duration, in seconds. Zero reads as a finished timer.</param>
        /// <param name="elapsed">If <see langword="true"/>, returns the elapsed fraction.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="totalSeconds"/> is negative.</exception>
        public CountdownProgressConverter(
            float totalSeconds,
            bool elapsed = false)
        {
            _elapsed = elapsed;
            _totalSeconds = totalSeconds >= 0f ? totalSeconds : throw new ArgumentOutOfRangeException(nameof(totalSeconds));
        }

        /// <summary>
        /// Converts the specified seconds remaining to a progress value.
        /// </summary>
        /// <param name="value">The seconds remaining.</param>
        /// <returns>The 0..1 progress. A duration of zero reads as a finished timer.</returns>
        public float Convert(float value) =>
            (float)Progress(value);

        double IConverter<double, double>.Convert(double value) =>
            Progress(value);

        private double Progress(double value)
        {
            if (_totalSeconds is 0f) return _elapsed ? 1f : 0f;

            var progress = value / _totalSeconds;
            var remaining = progress < 0d ? 0d : progress > 1d ? 1d : progress;

            return _elapsed ? 1d - remaining : remaining;
        }
    }
}
