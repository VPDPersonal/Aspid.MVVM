// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which end colors of a <see cref="UnityEngine.LineRenderer"/> a bound color writes.
    /// </summary>
    public enum LineRendererColorMode
    {
        /// <summary>
        /// Only <see cref="UnityEngine.LineRenderer.startColor"/>.
        /// </summary>
        Start,

        /// <summary>
        /// Only <see cref="UnityEngine.LineRenderer.endColor"/>.
        /// </summary>
        End,

        /// <summary>
        /// Both end colors.
        /// </summary>
        StartAndEnd,
    }
}
