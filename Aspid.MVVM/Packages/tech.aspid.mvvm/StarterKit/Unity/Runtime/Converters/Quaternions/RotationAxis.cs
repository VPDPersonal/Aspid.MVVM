// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which axis a rotation converter turns around.
    /// </summary>
    public enum RotationAxis
    {
        /// <summary>
        /// The X axis.
        /// </summary>
        X,

        /// <summary>
        /// The Y axis.
        /// </summary>
        Y,

        /// <summary>
        /// The Z axis — the one a 2D UI element spins on.
        /// </summary>
        Z,

        // Appended rather than filed next to the other axes: the declaration order is the value
        // Unity stores, so slotting a member in ahead of Z would repoint every field already
        // authored as Z without saying so.
        /// <summary>
        /// An arbitrary axis the converter carries itself.
        /// </summary>
        Custom,
    }
}
