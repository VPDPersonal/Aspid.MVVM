using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 position to a value in a range.
    /// </summary>
    /// <remarks>The other direction of <see cref="InverseLerpConverter"/>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Lerp Number", Tooltip = "Converts a 0..1 position to a value in a range")]
    public sealed class LerpNumberConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The value 0 maps to.")]
        [SerializeField] private float _from;

        [Tooltip("The value 1 maps to.")]
        [SerializeField] private float _to = 1f;

        [Tooltip("Hold the incoming position inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: over 0..1.</remarks>
        public LerpNumberConverter() { }

        /// <param name="from">The value 0 maps to.</param>
        /// <param name="to">The value 1 maps to.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the incoming position inside 0..1.</param>
        public LerpNumberConverter(float from, float to, bool clamp = true)
        {
            _from = from;
            _to = to;
            _clamp = clamp;
        }

        /// <summary>
        /// Converts the specified position to a value in the range.
        /// </summary>
        /// <param name="value">The 0..1 position.</param>
        /// <returns>The value at that position.</returns>
        public float Convert(float value) => RemapNumberConverter.Map(value, 0f, 1f, _from, _to, _clamp);

        /// <summary>
        /// Converts a value in the range back to its position.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>Its 0..1 position.</returns>
        public float ConvertBack(float value) => RemapNumberConverter.Map(value, _from, _to, 0f, 1f, _clamp);
    }
}
