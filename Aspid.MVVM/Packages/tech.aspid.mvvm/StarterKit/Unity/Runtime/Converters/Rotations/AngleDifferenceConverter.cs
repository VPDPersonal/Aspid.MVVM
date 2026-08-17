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
    /// Measures how far an angle is from a fixed one.
    /// </summary>
    /// <remarks>
    /// Compass deviation, or how far a turret has swung off its rest heading. Subtracting the two
    /// looks trivial until they straddle zero, where the plain difference reads 358° for what is
    /// really two degrees the other way.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Angle Difference", Tooltip = "Measures how far an angle is from a fixed one")]
    public sealed class AngleDifferenceConverter : IConverterFloat
    {
        [Tooltip("The angle the bound one is measured against, in degrees.")]
        [SerializeField] private float _reference;

        [Tooltip("Keep the sign. Clear it to report how far off the angle is whichever way it went.")]
        [SerializeField] private bool _signed = true;

        /// <remarks>Default: measuring from zero.</remarks>
        public AngleDifferenceConverter() { }

        /// <param name="reference">The angle the bound one is measured against, in degrees.</param>
        /// <param name="signed">Whether to keep the sign of the difference.</param>
        public AngleDifferenceConverter(float reference, bool signed = true)
        {
            _reference = reference;
            _signed = signed;
        }

        /// <summary>
        /// Measures the specified angle against the reference.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>The shortest way round from the reference to it, in degrees.</returns>
        public float Convert(float value)
        {
            var difference = Mathf.DeltaAngle(_reference, value);
            return _signed ? difference : Mathf.Abs(difference);
        }
    }
}
