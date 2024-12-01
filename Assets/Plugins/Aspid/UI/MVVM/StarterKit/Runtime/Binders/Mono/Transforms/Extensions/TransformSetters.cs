using System;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    public static class TransformSetters
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Transform transform, Vector3 value, VectorMode mode, Space space)
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
                case Space.Self: transform.localPosition = value; break;
                case Space.World: transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotation(this Transform transform, Quaternion value, Space space)
        {
            switch (space)
            {
                case Space.Self: transform.localRotation = value; break;
                case Space.World: transform.rotation = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScale(this Transform transform, Vector3 value, VectorMode mode)
        {
            var currentValue = transform.localScale;

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

            transform.localScale = value;
        }
    }
}