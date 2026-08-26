#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns the four numbers of a <see cref="Vector4"/> into a padding.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/To Rect Offset",
        Name = "Vector4 To Rect Offset",
        Tooltip = "Turns the four numbers of a Vector4 into a padding")]
    public sealed class Vector4ToRectOffsetConverter : ITwoWayConverter<Vector4, RectOffset>
    {
        [Tooltip("Which way to drop the fraction.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private RoundMode _rounding;

        [NonSerialized] private RectOffset? _result;

        /// <remarks>Default: rounding to nearest.</remarks>
        public Vector4ToRectOffsetConverter() { }

        /// <param name="rounding">Which way to drop the fraction.</param>
        public Vector4ToRectOffsetConverter(RoundMode rounding)
        {
            _rounding = rounding;
        }

        /// <summary>
        /// Turns the specified vector into a padding, reading x, y, z and w as left, right, top and bottom.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>
        /// The padding. The same instance is returned every call, so copy it if it must outlive the
        /// next push. A component too large for a whole number is held at the nearest one, and a NaN
        /// reads as zero. A rounding that is not a declared <see cref="RoundMode"/> value reports an
        /// error and the fraction is truncated.
        /// </returns>
        public RectOffset Convert(Vector4 value)
        {
            var rounding = _rounding;
            if (rounding is not (RoundMode.Round or RoundMode.Floor or RoundMode.Ceil or RoundMode.Truncate))
                rounding = Undeclared();

            _result ??= new RectOffset();

            _result.left = Round(value.x, rounding);
            _result.right = Round(value.y, rounding);
            _result.top = Round(value.z, rounding);
            _result.bottom = Round(value.w, rounding);

            return _result;
        }

        /// <summary>
        /// Reads the specified padding back as four numbers.
        /// </summary>
        /// <param name="value">The padding to read, or <see langword="null"/> to read no padding at all.</param>
        /// <returns>
        /// The vector, reading left, right, top and bottom as x, y, z and w. The fraction dropped by
        /// <see cref="Convert"/> is not restored, so a TwoWay binding quantizes the source.
        /// </returns>
        public Vector4 ConvertBack(RectOffset? value) => value is null
            ? Vector4.zero
            : new Vector4(value.left, value.right, value.top, value.bottom);

        // Truncation is the fallback because it moves the number least. Screened once per push rather
        // than once per component, so a misconfigured rounding reports one error and not four.
        private RoundMode Undeclared()
        {
            this.LogError(
                problem: $"the rounding {_rounding.Describe()} is not a declared {nameof(RoundMode)}",
                consequence: "Truncating the fraction.");

            return RoundMode.Truncate;
        }

        private static int Round(float value, RoundMode rounding)
        {
            var rounded = rounding switch
            {
                RoundMode.Round => Mathf.Round(value),
                RoundMode.Floor => Mathf.Floor(value),
                RoundMode.Ceil => Mathf.Ceil(value),
                // Truncate, and an undeclared rounding the screen at the top of Convert replaced with it.
                _ => (float)Math.Truncate(value)
            };

            return NumericSaturation.ToInt(rounded);
        }
    }
}
