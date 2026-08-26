#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Scales a padding.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Rect Offset",
        Name = "Scale",
        Tooltip = "Scales a padding")]
    public sealed class RectOffsetScaleConverter : IConverter<RectOffset, RectOffset>
    {
        [Tooltip("What the padding is multiplied by.")]
        [SerializeField] private float _scale = 1f;

        [Tooltip("Which sides are scaled.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _rounding;

        [NonSerialized] private RectOffset? _result;

        /// <remarks>Default: leaving every side as it is.</remarks>
        public RectOffsetScaleConverter() { }

        /// <param name="scale">What the padding is multiplied by.</param>
        /// <param name="sides">Which sides are scaled.</param>
        /// <param name="rounding">Which way to drop the fraction.</param>
        public RectOffsetScaleConverter(
            float scale,
            RectSides sides = RectSides.All,
            RoundMode rounding = RoundMode.Round)
        {
            _scale = scale;
            _sides = sides;
            _rounding = rounding;
        }

        /// <summary>
        /// Scales the specified padding.
        /// </summary>
        /// <param name="value">The padding to scale, or <see langword="null"/> to read no padding at all.</param>
        /// <returns>
        /// The scaled padding. The same instance is returned every call, so copy it if it must
        /// outlive the next push. A side too large for a whole number is held at the nearest one. A
        /// rounding that is not a declared <see cref="RoundMode"/> value reports an error and the
        /// fraction is truncated.
        /// </returns>
        public RectOffset Convert(RectOffset? value)
        {
            _result ??= new RectOffset();

            // Reading four zeroes off a throwaway RectOffset would allocate on every push.
            _result.left = Scale(value?.left ?? 0, RectSides.Left);
            _result.right = Scale(value?.right ?? 0, RectSides.Right);
            _result.top = Scale(value?.top ?? 0, RectSides.Top);
            _result.bottom = Scale(value?.bottom ?? 0, RectSides.Bottom);

            return _result;
        }

        private int Scale(int value, RectSides side)
        {
            // Written as a mask test rather than HasFlag, which boxes both operands on every call.
            if ((_sides & side) == 0) return value;

            var scaled = value * _scale;
            var rounded = _rounding switch
            {
                RoundMode.Round => Mathf.Round(scaled),
                RoundMode.Floor => Mathf.Floor(scaled),
                RoundMode.Ceil => Mathf.Ceil(scaled),
                RoundMode.Truncate => (float)Math.Truncate(scaled),
                _ => Undeclared(scaled)
            };

            return NumericSaturation.ToInt(rounded);
        }

        // Truncation is the fallback because it moves the number least.
        private float Undeclared(float scaled)
        {
            this.LogError(
                problem: $"the rounding {_rounding.Describe()} is not a declared {nameof(RoundMode)}",
                consequence: "Truncating the fraction.");

            return (float)Math.Truncate(scaled);
        }
    }
}
