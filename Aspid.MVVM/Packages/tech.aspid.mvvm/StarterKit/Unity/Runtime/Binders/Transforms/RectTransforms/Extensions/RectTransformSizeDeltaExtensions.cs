#nullable enable
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods that write the size delta of a <see cref="RectTransform"/>.
    /// </summary>
    public static class RectTransformSizeDeltaExtensions
    {
        /// <summary>
        /// Sets the <see cref="RectTransform.sizeDelta"/> of the <paramref name="transform"/> according to the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="transform">The <see cref="RectTransform"/> to modify.</param>
        /// <param name="value">The value to apply. <c>x</c> is used as the width and <c>y</c> as the height.</param>
        /// <param name="mode">Determines whether to set width only, height only, or both axes.</param>
        /// <remarks>
        /// A non-finite axis is skipped entirely: the rect is computed from these numbers and one <c>NaN</c>
        /// takes the element off the screen.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSizeDelta(this RectTransform transform, Vector3 value, SizeDeltaMode mode)
        {
            var current = transform.sizeDelta;
            var width = mode is not SizeDeltaMode.Height ? value.x : current.x;
            var height = mode is not SizeDeltaMode.Width ? value.y : current.y;

            var size = new Vector2(width, height);
            if (!BinderMath.RequireFinite(typeof(RectTransformSizeDeltaExtensions), size, transform)) return;

            transform.sizeDelta = size;
        }
    }
}
