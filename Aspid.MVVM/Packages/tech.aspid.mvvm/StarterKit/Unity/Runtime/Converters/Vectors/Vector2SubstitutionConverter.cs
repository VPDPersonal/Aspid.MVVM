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
    /// Converts <see cref="Vector2"/> values by substituting and rearranging their components.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 Substitution", Tooltip = "Converts Vector2 values by substituting and rearranging their components")]
    public sealed class Vector2SubstitutionConverter : IConverterVector2
    {
        [Tooltip("How the components are rearranged.")]
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
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <summary>
        /// Specifies how to rearrange vector components. The letters name the source components in
        /// the order they are written into the result, so a repeated letter is a component copied
        /// and a missing one is a component dropped.
        /// </summary>
        public enum Mode
        {
            /// <summary>The vector unchanged — <c>(x, y)</c>, the mode a new converter starts in.</summary>
            XY,

            /// <summary><c>(y, x)</c> — the two components swapped.</summary>
            YX,

            /// <summary><c>(y, y)</c> — Y in both components, X dropped.</summary>
            YY,

            /// <summary><c>(x, x)</c> — X in both components, Y dropped.</summary>
            XX,
        }
    }
}