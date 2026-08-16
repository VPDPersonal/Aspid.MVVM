#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Folds an angle into a standard range.
    /// </summary>
    /// <remarks>
    /// Removes the 359°-to-1° discontinuity that makes an angle look like it jumped most of the way
    /// round when it moved two degrees.
    /// </remarks>
    [Serializable]
    public sealed class AngleWrapConverter : IConverterFloat
    {
        [Tooltip("Which range to report in.")]
        [SerializeField] private AngleRange _range = AngleRange.Zero360;

        [Tooltip("Added before wrapping.")]
        [SerializeField] private float _offset;

        /// <remarks>Default: reporting 0..360.</remarks>
        public AngleWrapConverter() { }

        /// <param name="range">Which range to report in.</param>
        /// <param name="offset">Added before wrapping.</param>
        public AngleWrapConverter(AngleRange range, float offset = 0f)
        {
            _range = range;
            _offset = offset;
        }

        /// <summary>
        /// Folds the specified angle into the configured range.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>The folded angle.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the range is not a declared value.</exception>
        public float Convert(float value)
        {
            var wrapped = Mathf.Repeat(value + _offset, 360f);

            return _range switch
            {
                AngleRange.Zero360 => wrapped,
                AngleRange.Signed180 => wrapped > 180f ? wrapped - 360f : wrapped,
                _ => throw new ArgumentOutOfRangeException(nameof(_range), _range, null)
            };
        }
    }
}
