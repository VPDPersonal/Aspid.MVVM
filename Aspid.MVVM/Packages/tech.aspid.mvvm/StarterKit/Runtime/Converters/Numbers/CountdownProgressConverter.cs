using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts seconds remaining to a 0..1 progress value.
    /// </summary>
    /// <remarks>
    /// A timer ring driven by the same number the label shows, rather than a second property that
    /// has to be kept in step with it.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Countdown Progress", Tooltip = "Converts seconds remaining to a 0..1 progress value")]
    public sealed class CountdownProgressConverter : IConverterFloat
    {
        [Tooltip("The full duration, in seconds.")]
        [SerializeField] private float _totalSeconds = 1f;

        [Tooltip("Return the elapsed fraction instead of the remaining one.")]
        [SerializeField] private bool _elapsed;

        /// <remarks>Default: over one second.</remarks>
        public CountdownProgressConverter() { }

        /// <param name="totalSeconds">The full duration, in seconds.</param>
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
        /// <returns>The 0..1 progress. A duration of zero yields a finished timer.</returns>
        public float Convert(float value)
        {
            if (_totalSeconds <= 0f) return _elapsed ? 1f : 0f;

            var remaining = Mathf.Clamp01(value / _totalSeconds);
            return _elapsed ? 1f - remaining : remaining;
        }
    }
}
