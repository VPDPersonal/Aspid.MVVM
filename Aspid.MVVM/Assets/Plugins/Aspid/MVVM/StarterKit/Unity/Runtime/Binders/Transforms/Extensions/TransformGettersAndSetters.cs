using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class TransformGettersAndSetters
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetPosition(this Transform transform, Space space) => space switch
        {
            Space.Self => transform.localPosition,
            Space.World => transform.position,
            _ => throw new ArgumentOutOfRangeException()
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Transform transform, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.Self: transform.localPosition = value; break;
                case Space.World: transform.position = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion GetRotation(this Transform transform, Space space) => space switch
        {
            Space.Self => transform.localRotation,
            Space.World => transform.rotation,
            _ => throw new ArgumentOutOfRangeException()
        };

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
        public static Vector3 GetEulerAngles(this Transform transform, Space space) => space switch
        {
            Space.Self => transform.localEulerAngles,
            Space.World => transform.eulerAngles,
            _ => throw new ArgumentOutOfRangeException()
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerAngles(this Transform transform, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.Self: transform.localEulerAngles = value; break;
                case Space.World: transform.eulerAngles = value; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}