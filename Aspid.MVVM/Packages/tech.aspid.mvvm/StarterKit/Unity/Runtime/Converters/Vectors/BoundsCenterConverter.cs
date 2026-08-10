#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the middle of a bounding box.
    /// </summary>
    /// <remarks>
    /// Where a world-space label, a health bar or a selection ring goes: the middle of what is being
    /// labelled rather than the origin of its transform, which on an imported model is often at its
    /// feet or somewhere off to one side.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Bounds Center", Tooltip = "Reads the middle of a bounding box")]
    public sealed class BoundsCenterConverter : IConverter<Bounds, Vector3>
    {
        /// <summary>
        /// Reads the middle of the specified box.
        /// </summary>
        /// <param name="value">The box to read.</param>
        /// <returns>
        /// The middle of the box, in whichever space it was measured in — a
        /// <see cref="Renderer.bounds"/> is world space and a <see cref="Mesh.bounds"/> is local,
        /// and the converter has no transform with which to move between them.
        /// </returns>
        public Vector3 Convert(Bounds value) => value.center;
    }
}
