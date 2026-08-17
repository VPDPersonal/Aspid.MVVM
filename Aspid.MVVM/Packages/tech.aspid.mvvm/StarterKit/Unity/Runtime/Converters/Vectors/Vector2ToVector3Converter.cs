#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

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
        // The field keeps the name _values although its type is now Mode: renaming a
        // serialized field orphans the prefab-instance overrides authored against the old
        // path, and [FormerlySerializedAs] does not reach them.
        [SerializeField] private Mode _values;
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
            _values = mode;
            _thirdValue = thirdValue;
        }

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector3"/> using the configured component mapping.
        /// </summary>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>The converted 3D vector.</returns>
        public Vector3 Convert(Vector2 value) => _values switch
        {
            Mode.XY => new Vector3(value.x, value.y, _thirdValue),
            Mode.XZ => new Vector3(value.x, _thirdValue, value.y),
            Mode.YZ => new Vector3(_thirdValue, value.x, value.y),
            Mode.YX => new Vector3(value.y, value.x, _thirdValue),
            Mode.ZX => new Vector3(value.y, _thirdValue, value.x),
            Mode.ZY => new Vector3(_thirdValue, value.y, value.x),
            _ => throw new ArgumentOutOfRangeException(nameof(_values), _values, null)
        };

        /// <summary>
        /// Specifies which components of the 2D vector to map to the 3D vector. The letters name the
        /// destination axes, in the order the 2D components are read.
        /// </summary>
        /// <remarks>
        /// The last three complete the set <see cref="Vector3ToVector2Converter"/> has always
        /// offered, so the pair round-trips whichever order was picked there. They are appended
        /// rather than filed in alphabetical order because the declaration order is the value Unity
        /// stores — inserting YX between XZ and YZ would silently repoint every field authored as
        /// YZ.
        /// </remarks>
        public enum Mode
        {
            /// <summary>
            /// The 2D X goes to X and the 2D Y to Y; the constant fills Z. The mode a new converter
            /// starts in.
            /// </summary>
            XY,

            /// <summary>
            /// The 2D X goes to X and the 2D Y to Z; the constant fills Y, laying a flat value on the
            /// ground plane.
            /// </summary>
            XZ,

            /// <summary>
            /// The 2D X goes to Y and the 2D Y to Z; the constant fills X.
            /// </summary>
            YZ,

            /// <summary>
            /// The 2D X goes to Y and the 2D Y to X — the pair swapped; the constant fills Z.
            /// </summary>
            YX,

            /// <summary>
            /// The 2D X goes to Z and the 2D Y to X; the constant fills Y.
            /// </summary>
            ZX,

            /// <summary>
            /// The 2D X goes to Z and the 2D Y to Y; the constant fills X.
            /// </summary>
            ZY,
        }
    }
}