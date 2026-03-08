// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which sides of a layout group's padding are affected when setting padding values.
    /// </summary>
    public enum PaddingMode
    {
        /// <summary>
        /// All four padding sides (top, right, bottom, left) are set simultaneously.
        /// </summary>
        All,
        
        /// <summary>
        /// Only the left padding side is set.
        /// </summary>
        Left,
        
        /// <summary>
        /// Only the right padding side is set.
        /// </summary>
        Right,
        
        /// <summary>
        /// Only the top padding side is set.
        /// </summary>
        Top,
        
        /// <summary>
        /// Only the bottom padding side is set.
        /// </summary>
        Bottom,
    }
}