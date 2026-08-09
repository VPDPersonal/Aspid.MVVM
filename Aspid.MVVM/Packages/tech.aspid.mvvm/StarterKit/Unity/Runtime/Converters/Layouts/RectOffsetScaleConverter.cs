#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Scales a padding.
    /// </summary>
    /// <remarks>DPI or safe-area scaling applied to an authored padding.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Layout", Name = "Rect Offset Scale", Tooltip = "Scales a padding")]
    public sealed class RectOffsetScaleConverter : IConverterRectOffset
    {
        [Tooltip("What the padding is multiplied by.")]
        [SerializeField] private float _scale = 1f;

        [Tooltip("Which sides are scaled.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _rounding;

        [NonSerialized] private RectOffset? _result;

        public RectOffsetScaleConverter() { }

        /// <param name="scale">What the padding is multiplied by.</param>
        /// <param name="sides">Which sides are scaled.</param>
        public RectOffsetScaleConverter(float scale, RectSides sides = RectSides.All)
        {
            _scale = scale;
            _sides = sides;
        }

        /// <summary>
        /// Scales the specified padding.
        /// </summary>
        /// <param name="value">The padding to scale.</param>
        /// <returns>
        /// The scaled padding. The same instance is returned every call, so copy it if it must
        /// outlive the next push.
        /// </returns>
        public RectOffset Convert(RectOffset value)
        {
            _result ??= new RectOffset();
            var source = value ?? new RectOffset();

            _result.left = Scale(source.left, RectSides.Left);
            _result.right = Scale(source.right, RectSides.Right);
            _result.top = Scale(source.top, RectSides.Top);
            _result.bottom = Scale(source.bottom, RectSides.Bottom);

            return _result;
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rounding is not a declared value.</exception>
        private int Scale(int value, RectSides side)
        {
            if (!_sides.HasFlag(side)) return value;

            var scaled = value * _scale;

            return _rounding switch
            {
                RoundMode.Round => Mathf.RoundToInt(scaled),
                RoundMode.Floor => Mathf.FloorToInt(scaled),
                RoundMode.Ceil => Mathf.CeilToInt(scaled),
                RoundMode.Truncate => (int)scaled,
                _ => throw new ArgumentOutOfRangeException(nameof(_rounding), _rounding, null)
            };
        }
    }
}
