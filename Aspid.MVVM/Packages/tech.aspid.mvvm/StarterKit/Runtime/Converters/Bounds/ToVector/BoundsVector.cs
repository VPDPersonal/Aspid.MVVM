// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which vector of a bounding box <see cref="BoundsToVectorConverter"/> reads.
    /// </summary>
    public enum BoundsVector
    {
        /// <summary>
        /// The middle of the box.
        /// </summary>
        Center,

        /// <summary>
        /// The full size of the box.
        /// </summary>
        Size,

        /// <summary>
        /// The half-size, which is what a radius or an offset from the middle wants.
        /// </summary>
        Extents,
    }
}
