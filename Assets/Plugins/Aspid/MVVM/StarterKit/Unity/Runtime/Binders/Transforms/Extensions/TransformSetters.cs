using System;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.StarterKit.Unity
{
    public static class TransformSetters
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Transform transform, Vector3 value, Space space, Vector3CombineConverter converter = null)
        {
            var currentValue = space switch
            {
                Space.Self => transform.localPosition,
                Space.World => transform.position,
                _ => throw new ArgumentOutOfRangeException()
            };

            value = converter?.Convert(value, currentValue) ?? value;
            
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
        public static void SetScale(this Transform transform, Vector3 value, Vector3CombineConverter converter = null) =>
            transform.localScale = converter?.Convert(value, transform.localScale) ?? value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerAngles(this Transform transform, Vector3 value, Space space, Vector3CombineConverter converter = null)
        {
            var currentValue = space switch
            {
                Space.Self => transform.localEulerAngles,
                Space.World => transform.eulerAngles,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            value = converter?.Convert(value, currentValue) ?? value;
            
            switch (space)
            {
                case Space.Self: transform.localEulerAngles = value; break;
                case Space.World: transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}