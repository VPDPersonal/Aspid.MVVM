using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Folds a number back into a range instead of clamping it.
    /// </summary>
    /// <remarks>
    /// For values that cycle rather than stop: a rotation past 360°, a carousel index past the last
    /// page, a progress bar that fills repeatedly.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Wrap Number", Tooltip = "Folds a number back into a range instead of clamping it")]
    public sealed class WrapNumberConverter : IConverterFloat
    {
        [Tooltip("How to fold a value that leaves the range.")]
        [SerializeField] private WrapMode _mode;

        [Tooltip("The low end of the range.")]
        [SerializeField] private float _min;

        [Tooltip("The high end of the range.")]
        [SerializeField] private float _max = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public WrapNumberConverter() { }

        /// <param name="mode">How to fold a value that leaves the range.</param>
        /// <param name="min">The low end of the range.</param>
        /// <param name="max">The high end of the range.</param>
        public WrapNumberConverter(WrapMode mode, float min, float max)
        {
            _mode = mode;
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Folds the specified value into the range.
        /// </summary>
        /// <param name="value">The value to fold.</param>
        /// <returns>The folded value. A degenerate range yields its low end.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public float Convert(float value)
        {
            var span = _max - _min;
            if (span <= 0f) return _min;

            return _mode switch
            {
                WrapMode.Repeat => _min + Mathf.Repeat(value - _min, span),
                WrapMode.PingPong => _min + Mathf.PingPong(value - _min, span),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };
        }
    }
}
