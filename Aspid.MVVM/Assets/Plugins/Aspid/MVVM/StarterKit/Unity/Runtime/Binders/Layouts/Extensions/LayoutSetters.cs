using System;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class LayoutSetters
    {
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