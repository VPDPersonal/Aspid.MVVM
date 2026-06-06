#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
#if PROFILER
        private static partial class Markers<T>
            where T : IBinder
        {
            public static readonly Unity.Profiling.ProfilerMarker UnbindSafelyMarker = new(name: $"UnbindSafely<{typeof(T)}>");
        }
#endif
        
        /// <summary>
        /// Safely unbinds a single binder.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binder">The binder instance to unbind.</param>
        /// <param name="owner">Unused; accepted for signature parity with the collection overloads.</param>
        /// <param name="memberName">Unused; accepted for signature parity with the collection overloads.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T? binder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binder is null) return;
                binder.Unbind();
            }
        }

        /// <summary>
        /// Safely unbinds an array of binders.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The array of binders to unbind.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <see langword="null"/>.
        /// In Unity builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T[]? binders, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Length; i++)
                {
                    var binder = binders[i];
                    
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                    if (binder is null)
                    {
                        BuildUnbindSafelyBinderNullMessage(i, owner, memberName);
                    }
                    else
                    {
                        binder.Unbind();
                    }
                }
            }
        }

        /// <summary>
        /// Safely unbinds a list of binders.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The list of binders to unbind.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <see langword="null"/>.
        /// In Unity builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this List<T>? binders, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Count; i++)
                {
                    var binder = binders[i];
                    
                    if (binder is null)
                    {
                        BuildUnbindSafelyBinderNullMessage(i, owner, memberName);
                    }
                    else
                    {
                        binder.Unbind();
                    }
                }
            }
        }

        /// <summary>
        /// Safely unbinds a sequence of binders.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The enumerable of binders to unbind.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="UnbindSafelyNullReferenceException">
        /// Thrown if any element in the sequence is <see langword="null"/>.
        /// In Unity builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this IEnumerable<T>? binders, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                var index = 0;
                foreach (var binder in binders)
                {
                    if (binder is null)
                    {
                        BuildUnbindSafelyBinderNullMessage(index, owner, memberName);
                    }
                    else
                    {
                        binder.Unbind();
                    }
                    
                    index++;
                }
            }
        }
        
        [Conditional(conditionString: "DEBUG")]
        private static void BuildUnbindSafelyBinderNullMessage(int index, object? owner, string? memberName)
        {
            var message = BuildBinderNullMessage(operation: "UnbindSafely", index, owner, memberName);
            
#if UNITY_2020_3_OR_NEWER
            UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
#else
            throw new UnbindSafelyNullReferenceException(message);
#endif
        }
    }
}
