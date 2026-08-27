using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Clamping helpers that also reject non-finite values.
    /// </summary>
    /// <remarks>
    /// <see cref="Mathf.Clamp(float, float, float)"/> is two comparisons that are both false for
    /// <see cref="float.NaN"/>, so NaN passes through untouched.
    /// </remarks>
    public static class BinderMath
    {
        /// <summary>
        /// Clamps <paramref name="value"/> between <paramref name="min"/> and <paramref name="max"/>,
        /// mapping non-finite input to <paramref name="min"/>.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower bound, returned for <see cref="float.NaN"/> and infinities.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>The clamped value, or <paramref name="min"/> when <paramref name="value"/> is not finite.</returns>
        public static float SafeClamp(float value, float min, float max) =>
            IsFinite(value) ? Mathf.Clamp(value, min, max) : min;

        /// <summary>
        /// Clamps <paramref name="value"/> to the 0..1 range, mapping non-finite input to <c>0</c>.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The clamped value, or <c>0</c> when <paramref name="value"/> is not finite.</returns>
        public static float SafeClamp01(float value) =>
            IsFinite(value) ? Mathf.Clamp01(value) : 0f;

        /// <summary>
        /// Returns <paramref name="value"/> with anything below zero — and anything not finite — raised to <c>0</c>.
        /// </summary>
        /// <remarks>
        /// Unity rejects a non-finite extent with an error but accepts a negative one silently.
        /// </remarks>
        /// <param name="value">The extent to sanitise.</param>
        /// <returns><paramref name="value"/> when it is finite and positive; otherwise <c>0</c>.</returns>
        public static float NonNegative(float value) =>
            IsFinite(value) && value > 0f ? value : 0f;

        /// <inheritdoc cref="NonNegative(float)"/>
        public static Vector3 NonNegative(Vector3 value) =>
            new(NonNegative(value.x), NonNegative(value.y), NonNegative(value.z));

        /// <summary>
        /// Indicates whether <paramref name="value"/> is a finite number.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns><see langword="true"/> for a finite number; otherwise, <see langword="false"/>.</returns>
        public static bool IsFinite(float value) =>
            !float.IsNaN(value) && !float.IsInfinity(value);
    }
}
