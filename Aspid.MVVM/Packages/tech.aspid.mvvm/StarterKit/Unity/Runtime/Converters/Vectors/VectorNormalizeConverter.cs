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
    /// Reduces a vector to its direction.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector Normalize", Tooltip = "Reduces a vector to its direction")]
    public sealed class VectorNormalizeConverter : IConverterVector3
    {
        /// <summary>
        /// Normalises the specified vector.
        /// </summary>
        /// <param name="value">The vector to normalise.</param>
        /// <returns>
        /// The unit vector pointing the same way, or zero for a zero-length input — Unity's own
        /// <c>normalized</c> does the same rather than producing a NaN.
        /// </returns>
        public Vector3 Convert(Vector3 value) => value.normalized;
    }
}
