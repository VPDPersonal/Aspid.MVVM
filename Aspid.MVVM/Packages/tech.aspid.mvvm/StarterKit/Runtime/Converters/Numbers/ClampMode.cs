// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which bound <see cref="ClampNumberConverter"/> applies.
    /// </summary>
    public enum ClampMode
    {
        /// <summary>
        /// Keep the value between both bounds.
        /// </summary>
        Both,

        /// <summary>
        /// Only raise the value to the minimum.
        /// </summary>
        Min,

        /// <summary>
        /// Only lower the value to the maximum.
        /// </summary>
        Max,
    }
}
