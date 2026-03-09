using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Safely unbinds a single binder.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binder">The binder instance to unbind.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T? binder)
            where T : IBinder
        {
            if (binder is null) return;
            binder.Unbind();
        }

        /// <summary>
        /// Safely unbinds an array of binders.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The array of binders to unbind.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <see langword="null"/>.
        /// In builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T[]? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (binder is null)
                {
#if UNITY_2020_3_OR_NEWER
#if DEBUG
                    UnityEngine.Debug.LogError("[UnbindSafely] Binder in array can't be null");
#endif // DEBUG
                    continue;
#endif // UNITY_2020_3_OR_NEWER
                    throw new UnbindSafelyNullReferenceException("Binder in array can't be null");
                }
                binder.Unbind();
            }
        }

        /// <summary>
        /// Safely unbinds a list of binders.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The list of binders to unbind.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <see langword="null"/>.
        /// In builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this List<T>? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null)
                {
#if UNITY_2020_3_OR_NEWER
#if DEBUG
                    UnityEngine.Debug.LogError("[UnbindSafely] Binder in list can't be null");
#endif // DEBUG
                    continue;
#endif // UNITY_2020_3_OR_NEWER
                    throw new UnbindSafelyNullReferenceException("Binder in list can't be null");
                }
                binder.Unbind();
            }
        }

        /// <summary>
        /// Safely unbinds a sequence of binders.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The enumerable of binders to unbind.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <see langword="null"/>.
        /// In builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this IEnumerable<T>? binders)
            where T : IBinder
        {
            if (binders is null) return;

            foreach (var binder in binders)
            {
                if (binder is null)
                {
#if UNITY_2020_3_OR_NEWER
#if DEBUG
                    UnityEngine.Debug.LogError("[UnbindSafely] Binder in enumerable can't be null");
#endif // DEBUG
                    continue;
#endif // UNITY_2020_3_OR_NEWER
                    throw new UnbindSafelyNullReferenceException("Binder in enumerable can't be null");
                }
                binder.Unbind();
            }
        }
    }
}