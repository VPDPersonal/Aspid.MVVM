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
        /// The Z axis, the one a 2D UI element spins on.
        /// </summary>
        Z,

        // Kept last: Unity stores the declaration index, so inserting before Z would repoint serialized fields.
        /// <summary>
        /// An arbitrary axis the converter carries itself.
        /// </summary>
        Custom,
    }
}
