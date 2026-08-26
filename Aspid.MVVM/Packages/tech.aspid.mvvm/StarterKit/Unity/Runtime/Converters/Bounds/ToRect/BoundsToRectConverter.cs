#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Flattens a bounding box onto a plane.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Bounds/To Rect",
        Name = "Bounds To Rect",
        Tooltip = "Flattens a bounding box onto a plane")]
    public sealed class BoundsToRectConverter : IConverter<Bounds, Rect>
    {
        [Tooltip("Which two axes the box is flattened onto.")]
        [SerializeField] private BoundsPlane _plane = BoundsPlane.XY;

        /// <remarks>Default: flattening onto XY.</remarks>
        public BoundsToRectConverter() { }

        /// <param name="plane">Which two axes the box is flattened onto.</param>
        public BoundsToRectConverter(BoundsPlane plane)
        {
            _plane = plane;
        }

        /// <summary>
        /// Flattens the specified box.
        /// </summary>
        /// <param name="value">The box to flatten.</param>
        /// <returns>
        /// The rectangle, positioned at the box's lower corner on the chosen plane. A plane that is
        /// not a declared <see cref="BoundsPlane"/> is reported and read as
        /// <see cref="BoundsPlane.XY"/>.
        /// </returns>
        public Rect Convert(Bounds value)
        {
            var min = value.min;
            var size = value.size;

            return _plane switch
            {
                BoundsPlane.XY => Xy(min, size),
                BoundsPlane.XZ => new Rect(x: min.x, y: min.z, width: size.x, height: size.z),
                BoundsPlane.YZ => new Rect(x: min.y, y: min.z, width: size.y, height: size.z),
                _ => Undeclared(min, size)
            };
        }

        private static Rect Xy(Vector3 min, Vector3 size) =>
            new(x: min.x, y: min.y, width: size.x, height: size.y);

        // Shares the XY arm rather than repeating it, so the fallback cannot drift from the plane
        // it claims to flatten onto.
        private Rect Undeclared(Vector3 min, Vector3 size)
        {
            this.LogError(
                problem: $"the plane {_plane.Describe()} is not a declared {nameof(BoundsPlane)}",
                consequence: "Flattening onto XY.");

            return Xy(min, size);
        }
    }
}
