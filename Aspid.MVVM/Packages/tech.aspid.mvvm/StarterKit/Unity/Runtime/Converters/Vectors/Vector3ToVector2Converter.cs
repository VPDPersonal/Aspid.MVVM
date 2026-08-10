#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts <see cref="Vector3"/> values to <see cref="Vector2"/> by selecting which components to use.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 To Vector2", Tooltip = "Converts Vector3 values to Vector2 by selecting which components to use")]
    public sealed class Vector3ToVector2Converter : IConverterVector3ToVector2
    {
        [Tooltip("Which components of the 3D vector are kept, and in what order.")]
        // The field keeps the name _values although its type is now Mode: renaming a
        // serialized field orphans the prefab-instance overrides authored against the old
        // path, and [FormerlySerializedAs] does not reach them.
        [SerializeField] private Mode _values = Mode.XY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToVector2Converter"/> class.
        /// </summary>
        public Vector3ToVector2Converter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToVector2Converter"/> class.
        /// </summary>
        /// <param name="mode">Which vector components to use.</param>
        public Vector3ToVector2Converter(Mode mode)
        {
            _values = mode;
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/> to a <see cref="Vector2"/> by selecting the specified components.
        /// </summary>
        /// <param name="value">The 3D vector to convert.</param>
        /// <returns>The converted 2D vector.</returns>
        public Vector2 Convert(Vector3 value) => _values switch
        {
            Mode.XY => new Vector2(value.x, value.y),
            Mode.XZ => new Vector2(value.x, value.z),
            Mode.YX => new Vector2(value.y, value.x),
            Mode.YZ => new Vector2(value.y, value.z),
            Mode.ZX => new Vector2(value.z, value.x),
            Mode.ZY => new Vector2(value.z, value.y),
            _ => throw new ArgumentOutOfRangeException(nameof(_values), _values, null)
        };

        /// <summary>
        /// Specifies which components of the 3D vector to map to the 2D vector.
        /// </summary>
        public enum Mode
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