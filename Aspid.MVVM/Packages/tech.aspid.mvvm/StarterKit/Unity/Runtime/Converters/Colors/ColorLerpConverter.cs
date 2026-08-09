#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Moves between two colours by a 0..1 amount.
    /// </summary>
    /// <remarks>A two-stop gradient without a <see cref="Gradient"/> to author.</remarks>
    [Serializable]
    public sealed class ColorLerpConverter : IConverter<float, Color>
    {
        [Tooltip("The colour at 0.")]
        [SerializeField] private Color _from = Color.red;

        [Tooltip("The colour at 1.")]
        [SerializeField] private Color _to = Color.green;

        [Tooltip("Hold the incoming amount inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorLerpConverter"/> class going red to green.
        /// </summary>
        public ColorLerpConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorLerpConverter"/> class.
        /// </summary>
        /// <param name="from">The colour at 0.</param>
        /// <param name="to">The colour at 1.</param>
        public ColorLerpConverter(Color from, Color to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Reads the colour at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>The colour there.</returns>
        public Color Convert(float value) =>
            _clamp ? Color.Lerp(_from, _to, value) : Color.LerpUnclamped(_from, _to, value);
    }
}
