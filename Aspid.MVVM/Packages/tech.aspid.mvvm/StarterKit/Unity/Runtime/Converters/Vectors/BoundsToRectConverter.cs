#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Flattens a bounding box onto a plane.
    /// </summary>
    /// <remarks>
    /// A minimap footprint or a world-space panel sized to the object behind it: the third axis is
    /// the one the map is looking down, so dropping it is the whole conversion.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Bounds To Rect", Tooltip = "Flattens a bounding box onto a plane")]
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
        /// The rectangle, positioned at the box's lower corner on the chosen plane rather than at
        /// its middle — that is the corner <see cref="Rect"/> measures its width and height from.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the plane is not a declared value.</exception>
        public Rect Convert(Bounds value)
        {
            var min = value.min;
            var size = value.size;

            return _plane switch
            {
                BoundsPlane.XY => new Rect(min.x, min.y, size.x, size.y),
                BoundsPlane.XZ => new Rect(min.x, min.z, size.x, size.z),
                BoundsPlane.YZ => new Rect(min.y, min.z, size.y, size.z),
                _ => throw new ArgumentOutOfRangeException(nameof(_plane), _plane, null)
            };
        }
    }
}
