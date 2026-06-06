#nullable enable
using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts <see cref="Vector2"/> values by substituting and rearranging their components.
    /// </summary>
    [Serializable]
    public sealed class Vector2SubstitutionConverter : IConverterVector2
    {
        [SerializeField] private Mode _mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2SubstitutionConverter"/> class with XY mode.
        /// </summary>
        public Vector2SubstitutionConverter()
            : this(Mode.XY) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2SubstitutionConverter"/> class.
        /// </summary>
        /// <param name="mode">The substitution mode.</param>
        public Vector2SubstitutionConverter(Mode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Converts a <see cref="Vector2"/> by applying the configured substitution mode.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The converted vector with components rearranged according to the mode.</returns>
        public Vector2 Convert(Vector2 value) => _mode switch
        {
            Mode.XY => new Vector2(value.x, value.y),
            Mode.YX => new Vector2(value.y, value.x),

            Mode.YY => new Vector2(value.y, value.y),
            Mode.XX => new Vector2(value.x, value.x),
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Specifies how to rearrange vector components.
        /// </summary>
        public enum Mode
        {
            XY,
            YX,

            YY,
            XX,
        }
    }
}