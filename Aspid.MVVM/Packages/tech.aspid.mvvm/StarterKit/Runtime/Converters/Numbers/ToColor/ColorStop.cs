#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One color of a threshold color scale.
    /// </summary>
    [Serializable]
    public struct ColorStop
    {
        /// <summary>
        /// Gets the value at or above which this color applies.
        /// </summary>
        [field: Tooltip("The value at or above which this color applies.")]
        [field: SerializeField]
        public float Threshold { get; private set; }

        /// <summary>
        /// Gets the color used from <see cref="Threshold"/> up.
        /// </summary>
        [field: Tooltip("The color used from this threshold up.")]
        [field: SerializeField]
        public Color Color { get; private set; }

        /// <param name="threshold">The value at or above which this color applies.</param>
        /// <param name="color">The color used from the threshold up.</param>
        public ColorStop(
            float threshold,
            Color color)
        {
            Color = color;
            Threshold = threshold;
        }
    }
}
