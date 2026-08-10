#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using UnityEngine.Serialization;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts <see cref="Vector2"/> values to <see cref="Vector3"/> by specifying which components to use and a constant value for the third component.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 To Vector3", Tooltip = "Converts Vector2 values to Vector3 by specifying which components to use and a constant value for the third component")]
    public sealed class Vector2ToVector3Converter : IConverterVector2ToVector3
    {
        [Tooltip("Which axes of the 3D vector the 2D components are written into.")]
        [FormerlySerializedAs("_values")]
        [SerializeField] private Mode _mode;
        [Tooltip("The constant written into the axis the mode leaves out.")]
        [SerializeField] private float _thirdValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ToVector3Converter"/> class with XY mode.
        /// </summary>
        public Vector2ToVector3Converter()
            : this(Mode.XY) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ToVector3Converter"/> class.
        /// </summary>
        /// <param name="mode">Which vector components to use.</param>
        /// <param name="thirdValue">The constant value for the third component. Default is 0.</param>
        public Vector2ToVector3Converter(Mode mode, float thirdValue = 0)
        {
            _mode = mode;
            _thirdValue = thirdValue;
        }

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector3"/> using the configured component mapping.
        /// </summary>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>The converted 3D vector.</returns>
        public Vector3 Convert(Vector2 value) => _mode switch
        {
            Mode.XY => new Vector3(value.x, value.y, _thirdValue),
            Mode.XZ => new Vector3(value.x, _thirdValue, value.y),
            Mode.YZ => new Vector3(_thirdValue, value.x, value.y),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <summary>
        /// Specifies which components of the 2D vector to map to the 3D vector.
        /// </summary>
        public enum Mode
        {
            XY,
            XZ,
            YZ,
        }
    }
}