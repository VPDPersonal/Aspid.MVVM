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
    /// Reduces a 2D vector to its direction.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="VectorNormalizeConverter"/>: taking the direction of a
    /// joystick or a drag with its throw dropped, so a stick pushed halfway aims a marker exactly
    /// where one pushed to the rim does.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 Normalize", Tooltip = "Reduces a 2D vector to its direction")]
    public sealed class Vector2NormalizeConverter : IConverterVector2
    {
        /// <summary>
        /// Normalises the specified vector.
        /// </summary>
        /// <param name="value">The vector to normalise.</param>
        /// <returns>
        /// The unit vector pointing the same way, or zero for a zero-length input — Unity's own
        /// <c>normalized</c> does the same rather than producing a NaN.
        /// </returns>
        public Vector2 Convert(Vector2 value) => value.normalized;
    }
}
