#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads how big a bounding box is.
    /// </summary>
    /// <remarks>
    /// Sizing a frame, a footprint marker or a minimap icon to the thing it stands for, without the
    /// ViewModel holding a renderer to ask.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Bounds Size", Tooltip = "Reads how big a bounding box is")]
    public sealed class BoundsSizeConverter : IConverter<Bounds, Vector3>
    {
        [Tooltip("Report the half-size instead, which is what a radius or an offset from the middle wants.")]
        [SerializeField] private bool _extents;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundsSizeConverter"/> class reporting the full size.
        /// </summary>
        public BoundsSizeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundsSizeConverter"/> class.
        /// </summary>
        /// <param name="extents">Whether to report the half-size.</param>
        public BoundsSizeConverter(bool extents)
        {
            _extents = extents;
        }

        /// <summary>
        /// Reads the size of the specified box.
        /// </summary>
        /// <param name="value">The box to read.</param>
        /// <returns>The size of the box, or its half-size.</returns>
        public Vector3 Convert(Bounds value) => _extents ? value.extents : value.size;
    }
}
