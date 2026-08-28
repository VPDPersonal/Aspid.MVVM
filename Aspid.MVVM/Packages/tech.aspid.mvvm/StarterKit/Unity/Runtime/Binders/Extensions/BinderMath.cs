using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Sanitising helpers that report the value they had to replace.
    /// </summary>
    /// <remarks>
    /// A value outside the target's range saturates at the bound silently — that is the documented contract.
    /// A non-finite one has no bound to saturate at, so it is replaced and reported through
    /// <see cref="BinderLogger"/>. The <see cref="Type"/> overloads are for helpers reporting on another
    /// binder's behalf.
    /// </remarks>
    public static class BinderMath
    {
        private const string NotApplied = "The value is not applied.";

        /// <summary>
        /// Clamps <paramref name="value"/> between <paramref name="min"/> and <paramref name="max"/>,
        /// reporting non-finite input and mapping it to <paramref name="min"/>.
        /// </summary>
        /// <param name="binder">The clamping binder; a scene or asset object is pinged by default.</param>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower bound, returned for <see cref="float.NaN"/> and infinities.</param>
        /// <param name="max">The upper bound.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        /// <returns>The clamped value, or <paramref name="min"/> when <paramref name="value"/> is not finite.</returns>
        public static float SafeClamp(this IBinder binder, float value, float min, float max, Object? context = null) =>
            SafeClamp(binder.GetType(), value, min, max, context ?? binder as Object);

        /// <summary>
        /// Clamps <paramref name="value"/> between <paramref name="min"/> and <paramref name="max"/>
        /// on behalf of the specified binder type.
        /// </summary>
        /// <param name="binderType">The clamping binder's type.</param>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower bound, returned for <see cref="float.NaN"/> and infinities.</param>
        /// <param name="max">The upper bound.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        /// <returns>The clamped value, or <paramref name="min"/> when <paramref name="value"/> is not finite.</returns>
        public static float SafeClamp(Type binderType, float value, float min, float max, Object? context = null)
        {
            if (IsFinite(value)) return Mathf.Clamp(value, min, max);

            LogNotFinite(binderType, value, $"Using {min}.", context);
            return min;
        }

        /// <summary>
        /// Clamps <paramref name="value"/> to the 0..1 range, reporting non-finite input and mapping it to <c>0</c>.
        /// </summary>
        /// <param name="binder">The clamping binder; a scene or asset object is pinged by default.</param>
        /// <param name="value">The value to clamp.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        /// <returns>The clamped value, or <c>0</c> when <paramref name="value"/> is not finite.</returns>
        public static float SafeClamp01(this IBinder binder, float value, Object? context = null) =>
            SafeClamp01(binder.GetType(), value, context ?? binder as Object);

        /// <summary>
        /// Clamps <paramref name="value"/> to the 0..1 range on behalf of the specified binder type.
        /// </summary>
        /// <param name="binderType">The clamping binder's type.</param>
        /// <param name="value">The value to clamp.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        /// <returns>The clamped value, or <c>0</c> when <paramref name="value"/> is not finite.</returns>
        public static float SafeClamp01(Type binderType, float value, Object? context = null)
        {
            if (IsFinite(value)) return Mathf.Clamp01(value);

            LogNotFinite(binderType, value, "Using 0.", context);
            return 0f;
        }

        /// <summary>
        /// Returns <paramref name="value"/> with anything below zero raised to <c>0</c>,
        /// reporting non-finite input and mapping it to <c>0</c> as well.
        /// </summary>
        /// <remarks>
        /// Unity rejects a non-finite extent with an error but accepts a negative one silently.
        /// </remarks>
        /// <param name="binder">The sanitising binder; a scene or asset object is pinged by default.</param>
        /// <param name="value">The extent to sanitise.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        /// <returns><paramref name="value"/> when it is finite and positive; otherwise, <c>0</c>.</returns>
        public static float NonNegative(this IBinder binder, float value, Object? context = null) =>
            NonNegative(binder.GetType(), value, context ?? binder as Object);

        /// <summary>
        /// Raises anything below zero — and anything not finite — to <c>0</c>
        /// on behalf of the specified binder type.
        /// </summary>
        /// <param name="binderType">The sanitising binder's type.</param>
        /// <param name="value">The extent to sanitise.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        /// <returns><paramref name="value"/> when it is finite and positive; otherwise, <c>0</c>.</returns>
        public static float NonNegative(Type binderType, float value, Object? context = null)
        {
            if (IsFinite(value)) return value > 0f ? value : 0f;

            LogNotFinite(binderType, value, "Using 0.", context);
            return 0f;
        }

        /// <inheritdoc cref="NonNegative(IBinder, float, UnityEngine.Object)"/>
        public static Vector2 NonNegative(this IBinder binder, Vector2 value, Object? context = null) =>
            NonNegative(binder.GetType(), value, context ?? binder as Object);

        /// <inheritdoc cref="NonNegative(Type, float, UnityEngine.Object)"/>
        public static Vector2 NonNegative(Type binderType, Vector2 value, Object? context = null)
        {
            if (!IsFinite(value.x) || !IsFinite(value.y))
                LogNotFinite(binderType, value, "Non-finite components become 0.", context);

            return new Vector2(NonNegative(value.x), NonNegative(value.y));
        }

        /// <inheritdoc cref="NonNegative(IBinder, float, UnityEngine.Object)"/>
        public static Vector3 NonNegative(this IBinder binder, Vector3 value, Object? context = null) =>
            NonNegative(binder.GetType(), value, context ?? binder as Object);

        /// <inheritdoc cref="NonNegative(Type, float, UnityEngine.Object)"/>
        public static Vector3 NonNegative(Type binderType, Vector3 value, Object? context = null)
        {
            if (!IsFinite(value.x) || !IsFinite(value.y) || !IsFinite(value.z))
                LogNotFinite(binderType, value, "Non-finite components become 0.", context);

            return new Vector3(NonNegative(value.x), NonNegative(value.y), NonNegative(value.z));
        }

        /// <summary>
        /// Reports <paramref name="value"/> when it is not finite, so the caller can skip the write.
        /// </summary>
        /// <param name="binder">The checking binder; a scene or asset object is pinged by default.</param>
        /// <param name="value">The value to test.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        /// <returns><see langword="true"/> for a finite value; otherwise, <see langword="false"/>.</returns>
        public static bool RequireFinite(this IBinder binder, float value, Object? context = null) =>
            RequireFinite(binder.GetType(), value, context ?? binder as Object);

        /// <summary>
        /// Reports <paramref name="value"/> when it is not finite, on behalf of the specified binder type.
        /// </summary>
        /// <param name="binderType">The checking binder's type.</param>
        /// <param name="value">The value to test.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        /// <returns><see langword="true"/> for a finite value; otherwise, <see langword="false"/>.</returns>
        public static bool RequireFinite(Type binderType, float value, Object? context = null)
        {
            if (IsFinite(value)) return true;

            LogNotFinite(binderType, value, NotApplied, context);
            return false;
        }

        /// <inheritdoc cref="RequireFinite(IBinder, float, UnityEngine.Object)"/>
        public static bool RequireFinite(this IBinder binder, Vector2 value, Object? context = null) =>
            RequireFinite(binder.GetType(), value, context ?? binder as Object);

        /// <inheritdoc cref="RequireFinite(Type, float, UnityEngine.Object)"/>
        public static bool RequireFinite(Type binderType, Vector2 value, Object? context = null)
        {
            if (IsFinite(value.x) && IsFinite(value.y)) return true;

            LogNotFinite(binderType, value, NotApplied, context);
            return false;
        }

        /// <inheritdoc cref="RequireFinite(IBinder, float, UnityEngine.Object)"/>
        public static bool RequireFinite(this IBinder binder, Vector3 value, Object? context = null) =>
            RequireFinite(binder.GetType(), value, context ?? binder as Object);

        /// <inheritdoc cref="RequireFinite(Type, float, UnityEngine.Object)"/>
        public static bool RequireFinite(Type binderType, Vector3 value, Object? context = null)
        {
            if (IsFinite(value.x) && IsFinite(value.y) && IsFinite(value.z)) return true;

            LogNotFinite(binderType, value, NotApplied, context);
            return false;
        }

        /// <inheritdoc cref="RequireFinite(IBinder, float, UnityEngine.Object)"/>
        public static bool RequireFinite(this IBinder binder, Vector4 value, Object? context = null) =>
            RequireFinite(binder.GetType(), value, context ?? binder as Object);

        /// <inheritdoc cref="RequireFinite(Type, float, UnityEngine.Object)"/>
        public static bool RequireFinite(Type binderType, Vector4 value, Object? context = null)
        {
            if (IsFinite(value.x) && IsFinite(value.y) && IsFinite(value.z) && IsFinite(value.w)) return true;

            LogNotFinite(binderType, value, NotApplied, context);
            return false;
        }

        /// <inheritdoc cref="RequireFinite(IBinder, float, UnityEngine.Object)"/>
        public static bool RequireFinite(this IBinder binder, Rect value, Object? context = null) =>
            RequireFinite(binder.GetType(), value, context ?? binder as Object);

        /// <inheritdoc cref="RequireFinite(Type, float, UnityEngine.Object)"/>
        public static bool RequireFinite(Type binderType, Rect value, Object? context = null)
        {
            if (IsFinite(value.x) && IsFinite(value.y) && IsFinite(value.width) && IsFinite(value.height)) return true;

            LogNotFinite(binderType, value, NotApplied, context);
            return false;
        }

        /// <summary>
        /// Indicates whether <paramref name="value"/> is a finite number.
        /// </summary>
        /// <remarks>
        /// <see cref="Mathf.Clamp(float, float, float)"/> is two comparisons that are both false for
        /// <see cref="float.NaN"/>, so NaN passes through untouched.
        /// </remarks>
        /// <param name="value">The value to test.</param>
        /// <returns><see langword="true"/> for a finite number; otherwise, <see langword="false"/>.</returns>
        public static bool IsFinite(float value) =>
            !float.IsNaN(value) && !float.IsInfinity(value);

        /// <summary>
        /// Raises anything below zero to <c>0</c> without reporting — the vector overloads report once
        /// for the whole vector and then sanitise each component through this.
        /// </summary>
        private static float NonNegative(float value) =>
            IsFinite(value) && value > 0f ? value : 0f;

        [HideInCallstack]
        private static void LogNotFinite(Type binderType, object value, string consequence, Object? context) =>
            BinderLogger.LogError(binderType, $"the value {value} is not finite", consequence, context);
    }
}
