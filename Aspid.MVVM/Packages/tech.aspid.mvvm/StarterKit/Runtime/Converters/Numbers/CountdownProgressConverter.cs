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

        /// <param name="totalSeconds">
        /// The full duration, in seconds. A negative duration reports an error and reads as a
        /// finished timer.
        /// </param>
        /// <param name="elapsed">If <see langword="true"/>, returns the elapsed fraction.</param>
        public CountdownProgressConverter(float totalSeconds, bool elapsed = false)
        {
            _totalSeconds = totalSeconds;
            _elapsed = elapsed;
        }

        /// <summary>
        /// Converts the specified seconds remaining to a progress value.
        /// </summary>
        /// <param name="value">The seconds remaining.</param>
        /// <returns>
        /// The 0..1 progress. A duration of zero reads as a finished timer; a negative one does the
        /// same and reports an error.
        /// </returns>
        public float Convert(float value) => (float)Progress(value);

        double IConverter<double, double>.Convert(double value) => Progress(value);

        private double Progress(double value)
        {
            if (_totalSeconds < 0f)
            {
                this.LogError($"the duration {_totalSeconds} is negative",
                    "Treating the timer as finished.");
                return _elapsed ? 1f : 0f;
            }

            if (_totalSeconds == 0f) return _elapsed ? 1f : 0f;

            // Math.Clamp is not in .NET Standard 2.0; this matches Mathf.Clamp01, NaN passing through.
            var progress = value / _totalSeconds;
            var remaining = progress < 0d ? 0d : progress > 1d ? 1d : progress;

            return _elapsed ? 1d - remaining : remaining;
        }
    }
}
