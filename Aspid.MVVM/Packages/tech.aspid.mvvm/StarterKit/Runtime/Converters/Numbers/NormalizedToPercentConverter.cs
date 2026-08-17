using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 fraction to a percentage.
    /// </summary>
    [Serializable]
    public sealed class NormalizedToPercentConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("Round the percentage to a whole number.")]
        [SerializeField] private bool _round;

        public NormalizedToPercentConverter() { }

        /// <param name="round">If <see langword="true"/>, rounds the percentage to a whole number.</param>
        public NormalizedToPercentConverter(bool round)
        {
            _round = round;
        }

        /// <summary>
        /// Converts the specified fraction to a percentage.
        /// </summary>
        /// <param name="value">The 0..1 fraction.</param>
        /// <returns>The percentage.</returns>
        public float Convert(float value)
        {
            var percent = value * 100f;
            return _round ? Mathf.Round(percent) : percent;
        }

        /// <summary>
        /// Converts a percentage back to a fraction.
        /// </summary>
        /// <param name="value">The percentage.</param>
        /// <returns>The 0..1 fraction.</returns>
        public float ConvertBack(float value) => value / 100f;
    }
}
