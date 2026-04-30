// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which color endpoints of a <see cref="UnityEngine.LineRenderer"/> are affected when setting a color value.
    /// </summary>
    public enum LineRendererColorMode
    {
        /// <summary>
        /// Only the start color of the line renderer is set.
        /// </summary>
        Start,
        
        /// <summary>
        /// Only the end color of the line renderer is set.
        /// </summary>
        End,
        
        /// <summary>
        /// Both the start and end colors of the line renderer are set to the same value.
        /// </summary>
        StartAndEnd,
    }
}