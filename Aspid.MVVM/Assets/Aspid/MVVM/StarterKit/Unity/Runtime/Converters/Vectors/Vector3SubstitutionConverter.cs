#nullable enable
using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts <see cref="Vector3"/> values by substituting and rearranging their components.
    /// </summary>
    [Serializable]
    public sealed class Vector3SubstitutionConverter : IConverterVector3
    {
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
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Specifies how to rearrange vector components.
        /// </summary>
        public enum Mode
        {
            XYZ,
            XZY,

            YXZ,
            YZX,
            
            ZXY,
            ZYX,
            
            XXY,
            XYX,
            YXX,
            
            XXZ,
            XZX,
            ZXX,
            
            YYX,
            YXY,
            XYY,
            
            YYZ,
            YZY,
            ZYY,
            
            ZZX,
            ZXZ,
            XZZ,
            
            ZZY,
            ZYZ,
            YZZ,
            
            XXX,
            YYY,
            ZZZ,
        }
    }
}