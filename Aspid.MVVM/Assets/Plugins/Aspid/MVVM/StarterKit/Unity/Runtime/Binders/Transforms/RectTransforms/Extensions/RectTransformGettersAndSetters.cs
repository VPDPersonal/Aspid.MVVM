#nullable enable
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class RectTransformGettersAndSetters
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeDelta(this RectTransform transform, Vector3 value, SizeDeltaMode mode)
        {
            var width = mode is not SizeDeltaMode.Height ? value.x : transform.sizeDelta.x;
            var height = mode is not SizeDeltaMode.Width ? value.y : transform.sizeDelta.y;
            
            value = new Vector2(width, height);
            transform.sizeDelta = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetAnchoredPosition(this RectTransform transform, Space space) => space switch
        {
            Space.Self => transform.anchoredPosition,
            Space.World => transform.anchoredPosition3D,
            _ => throw new ArgumentOutOfRangeException()
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnchoredPosition(this RectTransform transform, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.Self: transform.anchoredPosition = value; break;
                case Space.World: transform.anchoredPosition3D = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}