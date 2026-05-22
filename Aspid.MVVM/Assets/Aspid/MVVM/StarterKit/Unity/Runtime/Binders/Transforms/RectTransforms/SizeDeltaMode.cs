// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Determines which axes of <see cref="UnityEngine.RectTransform.sizeDelta"/> are modified when setting the size.
    /// </summary>
    public enum SizeDeltaMode
    {
        /// <summary>
        /// Sets only the width (x axis) of <see cref="UnityEngine.RectTransform.sizeDelta"/>.
        /// </summary>
        Width,
        
        /// <summary>
        /// Sets only the height (y axis) of <see cref="UnityEngine.RectTransform.sizeDelta"/>.
        /// </summary>
        Height,
        
        /// <summary>
        /// Sets both the width and height of <see cref="UnityEngine.RectTransform.sizeDelta"/>.
        /// </summary>
        SizeDelta,
    }
}