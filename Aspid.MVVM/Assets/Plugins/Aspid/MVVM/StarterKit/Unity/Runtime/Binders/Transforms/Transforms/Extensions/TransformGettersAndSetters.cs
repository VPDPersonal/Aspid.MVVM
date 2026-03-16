using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for getting and setting position, rotation, and euler angles on a <see cref="Transform"/>
    /// in either world or local space.
    /// </summary>
    public static class TransformGettersAndSetters
    {
        /// <summary>
        /// Gets the position of the <see cref="Transform"/> in the specified <see cref="Space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to read from.</param>
        /// <param name="space">The coordinate space: <see cref="Space.World"/> returns <see cref="Transform.position"/>, <see cref="Space.Self"/> returns <see cref="Transform.localPosition"/>.</param>
        /// <returns>The position in the specified space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetPosition(this Transform transform, Space space) => space switch
        {
            Space.Self => transform.localPosition,
            Space.World => transform.position,
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Sets the position of the <see cref="Transform"/> in the specified <see cref="Space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to update.</param>
        /// <param name="value">The position to apply.</param>
        /// <param name="space">The coordinate space: <see cref="Space.World"/> sets <see cref="Transform.position"/>, <see cref="Space.Self"/> sets <see cref="Transform.localPosition"/>.</param>
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
        
        /// <summary>
        /// Gets the rotation of the <see cref="Transform"/> in the specified <see cref="Space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to read from.</param>
        /// <param name="space">The coordinate space: <see cref="Space.World"/> returns <see cref="Transform.rotation"/>, <see cref="Space.Self"/> returns <see cref="Transform.localRotation"/>.</param>
        /// <returns>The rotation in the specified space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion GetRotation(this Transform transform, Space space) => space switch
        {
            Space.Self => transform.localRotation,
            Space.World => transform.rotation,
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Sets the rotation of the <see cref="Transform"/> in the specified <see cref="Space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to update.</param>
        /// <param name="value">The rotation to apply.</param>
        /// <param name="space">The coordinate space: <see cref="Space.World"/> sets <see cref="Transform.rotation"/>, <see cref="Space.Self"/> sets <see cref="Transform.localRotation"/>.</param>
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
        
        /// <summary>
        /// Gets the euler angles of the <see cref="Transform"/> in the specified <see cref="Space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to read from.</param>
        /// <param name="space">The coordinate space: <see cref="Space.World"/> returns <see cref="Transform.eulerAngles"/>, <see cref="Space.Self"/> returns <see cref="Transform.localEulerAngles"/>.</param>
        /// <returns>The euler angles in the specified space.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetEulerAngles(this Transform transform, Space space) => space switch
        {
            Space.Self => transform.localEulerAngles,
            Space.World => transform.eulerAngles,
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Sets the euler angles of the <see cref="Transform"/> in the specified <see cref="Space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> to update.</param>
        /// <param name="value">The euler angles to apply.</param>
        /// <param name="space">The coordinate space: <see cref="Space.World"/> sets <see cref="Transform.eulerAngles"/>, <see cref="Space.Self"/> sets <see cref="Transform.localEulerAngles"/>.</param>
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