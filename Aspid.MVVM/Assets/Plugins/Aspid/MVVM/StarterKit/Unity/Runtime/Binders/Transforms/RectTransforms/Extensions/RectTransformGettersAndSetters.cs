#nullable enable
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for getting and setting anchored position and size delta on a <see cref="RectTransform"/>.
    /// </summary>
    public static class RectTransformGettersAndSetters
    {
        /// <summary>
        /// Sets the <see cref="RectTransform.sizeDelta"/> of the <paramref name="transform"/> according to the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="transform">The <see cref="RectTransform"/> to modify.</param>
        /// <param name="value">The value to apply. <c>x</c> is used as the width and <c>y</c> as the height.</param>
        /// <param name="mode">Determines whether to set width only, height only, or both axes.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeDelta(this RectTransform transform, Vector3 value, SizeDeltaMode mode)
        {
            var width = mode is not SizeDeltaMode.Height ? value.x : transform.sizeDelta.x;
            var height = mode is not SizeDeltaMode.Width ? value.y : transform.sizeDelta.y;
            
            value = new Vector2(width, height);
            transform.sizeDelta = value;
        }
        
        /// <summary>
        /// Returns the anchored position of the <paramref name="transform"/> in the specified <paramref name="space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="RectTransform"/> to read from.</param>
        /// <param name="space">
        /// <see cref="Space.Self"/> returns <see cref="RectTransform.anchoredPosition"/>;
        /// <see cref="Space.World"/> returns <see cref="RectTransform.anchoredPosition3D"/>.
        /// </param>
        /// <returns>The anchored position as a <see cref="Vector3"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetAnchoredPosition(this RectTransform transform, Space space) => space switch
        {
            Space.Self => transform.anchoredPosition,
            Space.World => transform.anchoredPosition3D,
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// Sets the anchored position of the <paramref name="transform"/> in the specified <paramref name="space"/>.
        /// </summary>
        /// <param name="transform">The <see cref="RectTransform"/> to modify.</param>
        /// <param name="value">The anchored position to apply.</param>
        /// <param name="space">
        /// <see cref="Space.Self"/> sets <see cref="RectTransform.anchoredPosition"/>;
        /// <see cref="Space.World"/> sets <see cref="RectTransform.anchoredPosition3D"/>.
        /// </param>
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