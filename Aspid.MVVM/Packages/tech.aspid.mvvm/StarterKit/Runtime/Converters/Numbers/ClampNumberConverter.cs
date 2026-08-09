using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps a number inside a range.
    /// </summary>
    /// <remarks>
    /// A View property with a legal range — <c>Image.fillAmount</c>, an alpha, a slider — will take
    /// whatever the ViewModel sends and render it wrong rather than complain. Clamping at the
    /// boundary keeps a bad number from becoming a bad frame.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Clamp Number", Tooltip = "Keeps a number inside a range")]
    public sealed class ClampNumberConverter : IConverterFloat
    {
        [Tooltip("The lowest value allowed through.")]
        [SerializeField] private float _min;

        [Tooltip("The highest value allowed through.")]
        [SerializeField] private float _max = 1f;

        [Tooltip("Which bound to apply.")]
        [SerializeField] private ClampMode _mode = ClampMode.Both;

        /// <remarks>Default: clamping to 0..1.</remarks>
        public ClampNumberConverter() { }

        /// <param name="min">The lowest value allowed through.</param>
        /// <param name="max">The highest value allowed through.</param>
        /// <param name="mode">Which bound to apply.</param>
        public ClampNumberConverter(float min, float max, ClampMode mode = ClampMode.Both)
        {
            _min = min;
            _max = max;
            _mode = mode;
        }

        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The value, held inside the configured bounds.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public float Convert(float value) => _mode switch
        {
            ClampMode.Both => Mathf.Clamp(value, _min, _max),
            ClampMode.Min => Mathf.Max(value, _min),
            ClampMode.Max => Mathf.Min(value, _max),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };
    }
}
