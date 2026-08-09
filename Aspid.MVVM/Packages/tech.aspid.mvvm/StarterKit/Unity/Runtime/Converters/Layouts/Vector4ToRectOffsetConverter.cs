#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns the four numbers of a <see cref="Vector4"/> into a padding.
    /// </summary>
    /// <remarks>
    /// Lets the ViewModel keep a struct rather than a <see cref="RectOffset"/>, which is a class and
    /// would have to be allocated on its side.
    /// </remarks>
    [Serializable]
    public sealed class Vector4ToRectOffsetConverter : IConverter<Vector4, RectOffset>
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _rounding;

        [NonSerialized] private RectOffset? _result;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4ToRectOffsetConverter"/> class rounding to nearest.
        /// </summary>
        public Vector4ToRectOffsetConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4ToRectOffsetConverter"/> class.
        /// </summary>
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
        /// next push.
        /// </returns>
        public RectOffset Convert(Vector4 value)
        {
            _result ??= new RectOffset();

            _result.left = Round(value.x);
            _result.right = Round(value.y);
            _result.top = Round(value.z);
            _result.bottom = Round(value.w);

            return _result;
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rounding is not a declared value.</exception>
        private int Round(float value) => _rounding switch
        {
            RoundMode.Round => Mathf.RoundToInt(value),
            RoundMode.Floor => Mathf.FloorToInt(value),
            RoundMode.Ceil => Mathf.CeilToInt(value),
            RoundMode.Truncate => (int)value,
            _ => throw new ArgumentOutOfRangeException(nameof(_rounding), _rounding, null)
        };
    }
}
