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
    /// Converts <see cref="Vector3"/> values by substituting and rearranging their components.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 Substitution", Tooltip = "Converts Vector3 values by substituting and rearranging their components")]
    public sealed class Vector3SubstitutionConverter : IConverterVector3
    {
        [Tooltip("How the components are rearranged.")]
        [SerializeField] private Mode _mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3SubstitutionConverter"/> class with XYZ mode.
        /// </summary>
        public Vector3SubstitutionConverter()
            : this(Mode.XYZ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3SubstitutionConverter"/> class.
        /// </summary>
        /// <param name="mode">The substitution mode.</param>
        public Vector3SubstitutionConverter(Mode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/> by applying the configured substitution mode.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The converted vector with components rearranged according to the mode.</returns>
        public Vector3 Convert(Vector3 value) => _mode switch
        {
            Mode.XYZ => new Vector3(value.x, value.y, value.z),
            Mode.XZY => new Vector3(value.x, value.z, value.y),
            
            Mode.YXZ => new Vector3(value.y, value.x, value.z),
            Mode.YZX => new Vector3(value.y, value.z, value.x),
            
            Mode.ZXY => new Vector3(value.z, value.x, value.y),
            Mode.ZYX => new Vector3(value.z, value.y, value.x),
            
            Mode.XXY => new Vector3(value.x, value.x, value.y),
            Mode.XYX => new Vector3(value.x, value.y, value.x),
            Mode.YXX => new Vector3(value.y, value.x, value.x),
            
            Mode.XXZ => new Vector3(value.x, value.x, value.z),
            Mode.XZX => new Vector3(value.x, value.z, value.x),
            Mode.ZXX => new Vector3(value.z, value.x, value.x),
            
            Mode.YYX => new Vector3(value.y, value.y, value.x),
            Mode.YXY => new Vector3(value.y, value.x, value.y),
            Mode.XYY => new Vector3(value.x, value.y, value.y),
            
            Mode.YYZ => new Vector3(value.y, value.y, value.z),
            Mode.YZY => new Vector3(value.y, value.z, value.y),
            Mode.ZYY => new Vector3(value.z, value.y, value.y),
            
            Mode.ZZX => new Vector3(value.z, value.z, value.x),
            Mode.ZXZ => new Vector3(value.z, value.x, value.z),
            Mode.XZZ => new Vector3(value.x, value.z, value.z),
            
            Mode.ZZY => new Vector3(value.z, value.z, value.y),
            Mode.ZYZ => new Vector3(value.z, value.y, value.z),
            Mode.YZZ => new Vector3(value.y, value.z, value.z),
            
            Mode.XXX => new Vector3(value.x, value.x, value.x),
            Mode.YYY => new Vector3(value.y, value.y, value.y),
            Mode.ZZZ => new Vector3(value.z, value.z, value.z),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <summary>
        /// Specifies how to rearrange vector components. The letters name the source components in
        /// the order they are written into the result, so a repeated letter is a component copied
        /// and a missing one is a component dropped.
        /// </summary>
        public enum Mode
        {
            /// <summary>The vector unchanged — <c>(x, y, z)</c>, the mode a new converter starts in.</summary>
            XYZ,

            /// <summary><c>(x, z, y)</c> — Y and Z swapped.</summary>
            XZY,

            /// <summary><c>(y, x, z)</c> — X and Y swapped.</summary>
            YXZ,

            /// <summary><c>(y, z, x)</c> — the components cycled left, X ending up last.</summary>
            YZX,

            /// <summary><c>(z, x, y)</c> — the components cycled right, Z ending up first.</summary>
            ZXY,

            /// <summary><c>(z, y, x)</c> — the order reversed, X and Z swapped.</summary>
            ZYX,

            /// <summary><c>(x, x, y)</c> — X duplicated, Z dropped.</summary>
            XXY,

            /// <summary><c>(x, y, x)</c> — X duplicated, Z dropped.</summary>
            XYX,

            /// <summary><c>(y, x, x)</c> — X duplicated, Z dropped.</summary>
            YXX,

            /// <summary><c>(x, x, z)</c> — X duplicated, Y dropped.</summary>
            XXZ,

            /// <summary><c>(x, z, x)</c> — X duplicated, Y dropped.</summary>
            XZX,

            /// <summary><c>(z, x, x)</c> — X duplicated, Y dropped.</summary>
            ZXX,

            /// <summary><c>(y, y, x)</c> — Y duplicated, Z dropped.</summary>
            YYX,

            /// <summary><c>(y, x, y)</c> — Y duplicated, Z dropped.</summary>
            YXY,

            /// <summary><c>(x, y, y)</c> — Y duplicated, Z dropped.</summary>
            XYY,

            /// <summary><c>(y, y, z)</c> — Y duplicated, X dropped.</summary>
            YYZ,

            /// <summary><c>(y, z, y)</c> — Y duplicated, X dropped.</summary>
            YZY,

            /// <summary><c>(z, y, y)</c> — Y duplicated, X dropped.</summary>
            ZYY,

            /// <summary><c>(z, z, x)</c> — Z duplicated, Y dropped.</summary>
            ZZX,

            /// <summary><c>(z, x, z)</c> — Z duplicated, Y dropped.</summary>
            ZXZ,

            /// <summary><c>(x, z, z)</c> — Z duplicated, Y dropped.</summary>
            XZZ,

            /// <summary><c>(z, z, y)</c> — Z duplicated, X dropped.</summary>
            ZZY,

            /// <summary><c>(z, y, z)</c> — Z duplicated, X dropped.</summary>
            ZYZ,

            /// <summary><c>(y, z, z)</c> — Z duplicated, X dropped.</summary>
            YZZ,

            /// <summary><c>(x, x, x)</c> — X broadcast to every axis, Y and Z dropped.</summary>
            XXX,

            /// <summary><c>(y, y, y)</c> — Y broadcast to every axis, X and Z dropped.</summary>
            YYY,

            /// <summary><c>(z, z, z)</c> — Z broadcast to every axis, X and Y dropped.</summary>
            ZZZ,
        }
    }
}