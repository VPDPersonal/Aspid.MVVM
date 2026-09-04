using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="LayoutGroup"/> used by the layout group binders.
    /// </summary>
    public static class LayoutGroupExtensions
    {
        /// <summary>
        /// Copies the selected <paramref name="sides"/> of <paramref name="value"/> into
        /// <see cref="LayoutGroup.padding"/> and marks the layout for rebuild.
        /// </summary>
        /// <param name="layout">The layout group whose padding is set.</param>
        /// <param name="value">The padding to copy from.</param>
        /// <param name="sides">The sides to copy.</param>
        public static void SetPadding(this LayoutGroup layout, RectOffset value, RectSides sides)
        {
            var padding = layout.padding;

            if ((sides & RectSides.Left) != 0) padding.left = value.left;
            if ((sides & RectSides.Right) != 0) padding.right = value.right;
            if ((sides & RectSides.Top) != 0) padding.top = value.top;
            if ((sides & RectSides.Bottom) != 0) padding.bottom = value.bottom;

            LayoutRebuilder.MarkLayoutForRebuild(layout.transform as RectTransform);
        }
    }
}
