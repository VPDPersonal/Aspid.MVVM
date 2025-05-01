#nullable enable
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.StarterKit.Unity
{
    public static class RectTransformSetters
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeDelta(this RectTransform transform, Vector3 value, SizeDeltaMode mode)
        {
            var width = mode is not SizeDeltaMode.Height ? value.x : transform.sizeDelta.x;
            var height = mode is not SizeDeltaMode.Width ? value.y : transform.sizeDelta.y;
            
            value = new Vector2(width, height);
            transform.anchoredPosition = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnchoredPosition(this RectTransform transform, Vector3 value, Space space, Vector3CombineConverter? converter = null)
        {
            var currentValue = space switch
            {
                Space.Self => (Vector3)transform.anchoredPosition,
                Space.World => transform.anchoredPosition3D,
                _ => throw new ArgumentOutOfRangeException()
            };

            value = converter?.Convert(value, currentValue) ?? value;
            
            switch (space)
            {
                case Space.Self: transform.anchoredPosition = value; break;
                case Space.World: transform.anchoredPosition3D = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}