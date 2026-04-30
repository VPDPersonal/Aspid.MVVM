using System;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for setting padding on Unity <see cref="LayoutGroup"/> components.
    /// </summary>
    public static class LayoutSetters
    {
        /// <summary>
        /// Sets the padding of a <see cref="LayoutGroup"/> according to the specified <see cref="PaddingMode"/>.
        /// </summary>
        /// <param name="layout">The layout group whose padding will be modified.</param>
        /// <param name="top">The top padding value.</param>
        /// <param name="right">The right padding value.</param>
        /// <param name="bottom">The bottom padding value.</param>
        /// <param name="left">The left padding value.</param>
        /// <param name="mode">
        /// Determines which sides of the padding are updated.
        /// Use <see cref="PaddingMode.All"/> to set all four sides, or a specific side value to update only that side.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPadding(this LayoutGroup layout, int top, int right, int bottom, int left, PaddingMode mode)
        {
            switch (mode)
            {
                case PaddingMode.All: 
                    layout.padding.top = top;
                    layout.padding.left = left;
                    layout.padding.right = right;
                    layout.padding.bottom = bottom;
                    break;
                
                case PaddingMode.Top: layout.padding.top = top; break;
                case PaddingMode.Left: layout.padding.left = left; break;
                case PaddingMode.Right: layout.padding.right = right; break;
                case PaddingMode.Bottom: layout.padding.bottom = bottom; break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}