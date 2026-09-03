using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods that read and write the anchored position of a <see cref="RectTransform"/> by <see cref="Space"/>.
    /// </summary>
    public static class RectTransformGettersAndSetters
    {
        /// <summary>
        /// Gets the anchored position in the specified space.
        /// </summary>
        /// <param name="transform">The rect transform to read.</param>
        /// <param name="space">
        /// <see cref="Space.Self"/> reads <see cref="RectTransform.anchoredPosition"/>,
        /// <see cref="Space.World"/> reads <see cref="RectTransform.anchoredPosition3D"/>.
        /// </param>
        /// <returns>The anchored position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetAnchoredPosition(this RectTransform transform, Space space) => space switch
        {
            Space.Self => transform.anchoredPosition,
            Space.World => transform.anchoredPosition3D,
            _ => throw new ArgumentOutOfRangeException(nameof(space), space, null)
        };

        /// <summary>
        /// Sets the anchored position in the specified space.
        /// </summary>
        /// <param name="transform">The rect transform to write.</param>
        /// <param name="value">The anchored position to apply.</param>
        /// <param name="space">
        /// <see cref="Space.Self"/> writes <see cref="RectTransform.anchoredPosition"/>,
        /// <see cref="Space.World"/> writes <see cref="RectTransform.anchoredPosition3D"/>.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnchoredPosition(this RectTransform transform, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.Self: transform.anchoredPosition = value; break;
                case Space.World: transform.anchoredPosition3D = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(space), space, null);
            }
        }
    }
}
