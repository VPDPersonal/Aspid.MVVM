using System;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.StarterKit.Binders
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
        public static void SetAnchoredPosition(this RectTransform transform, Vector3 value, VectorMode mode, Space space)
        {
            var currentValue = space switch
            {
                Space.Self => transform.localPosition,
                Space.World => transform.position,
                _ => throw new ArgumentOutOfRangeException()
            };

            value = mode switch
            {
                VectorMode.X => new Vector3(value.x, currentValue.y, currentValue.z),
                VectorMode.Y => new Vector3(currentValue.x, value.y, currentValue.z),
                VectorMode.Z => new Vector3(currentValue.x, currentValue.y, value.z),
                VectorMode.XY => new Vector3(value.x, value.y, currentValue.z),
                VectorMode.XZ => new Vector3(value.x, currentValue.y, value.z),
                VectorMode.YZ => new Vector3(currentValue.x, value.y, value.z),
                VectorMode.XYZ => new Vector3(value.x, value.y, value.z),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            switch (space)
            {
                case Space.Self: transform.anchoredPosition = value; break;
                case Space.World: transform.anchoredPosition3D = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}