#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which pair of axes a bounding box is flattened onto.
    /// </summary>
    public enum BoundsPlane
    {
        /// <summary>
        /// The X and Y axes — the plane a 2D game and a canvas live on.
        /// </summary>
        XY,

        /// <summary>
        /// The X and Z axes — the ground plane of a 3D game.
        /// </summary>
        XZ,

        /// <summary>
        /// The Y and Z axes.
        /// </summary>
        YZ,
    }
}
