#nullable enable
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The range <see cref="AngleWrapConverter"/> reports angles in.
    /// </summary>
    public enum AngleRange
    {
        /// <summary>
        /// 0 to 360.
        /// </summary>
        Zero360,

        /// <summary>
        /// -180 to 180.
        /// </summary>
        Signed180,
    }
}
