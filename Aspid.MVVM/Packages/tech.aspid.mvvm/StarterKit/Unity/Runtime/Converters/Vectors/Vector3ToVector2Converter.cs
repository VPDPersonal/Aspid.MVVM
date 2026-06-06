#nullable enable
using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts <see cref="Vector3"/> values to <see cref="Vector2"/> by selecting which components to use.
    /// </summary>
    [Serializable]
    public sealed class Vector3ToVector2Converter : IConverterVector3ToVector2
    {
        [SerializeField] private Values _values = Values.XY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToVector2Converter"/> class.
        /// </summary>
        public Vector3ToVector2Converter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToVector2Converter"/> class.
        /// </summary>
        /// <param name="values">Which vector components to use.</param>
        public Vector3ToVector2Converter(Values values)
        {
            _values = values;
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/> to a <see cref="Vector2"/> by selecting the specified components.
        /// </summary>
        /// <param name="value">The 3D vector to convert.</param>
        /// <returns>The converted 2D vector.</returns>
        public Vector2 Convert(Vector3 value) => _values switch
        {
            Values.XY => new Vector2(value.x, value.y),
            Values.XZ => new Vector2(value.x, value.z),
            Values.YX => new Vector2(value.y, value.x),
            Values.YZ => new Vector2(value.y, value.z),
            Values.ZX => new Vector2(value.z, value.x),
            Values.ZY => new Vector2(value.z, value.y),
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Specifies which components of the 3D vector to map to the 2D vector.
        /// </summary>
        public enum Values
        {
            XY,
            XZ,
            YX,
            YZ,
            ZX,
            ZY,
        }
    }
}