#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
#if ASPID_MVVM_PROFILER
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
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (IsMissing(binder)) return;
                binder.Unbind();
            }
        }

        /// <summary>
        /// Safely unbinds an array of binders.
        /// </summary>
        /// <remarks>
        /// Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.
        /// </remarks>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The array of binders to unbind.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this T[]? binders, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Length; i++)
                {
                    var binder = binders[i];
                    
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                    if (IsMissing(binder))
                    {
                        BuildUnbindSafelyBinderNullMessage(i, owner, memberName);
                    }
                    else
                    {
                        try
                        {
                            binder.Unbind();
                        }
                        catch (System.Exception exception)
                        {
                            // One binder must not take the rest of the collection with it: on Bind that would leave
                            // the View half-initialised, and on Unbind it would leave the remaining binders
                            // subscribed to a ViewModel that is going away.
                            ReportBinderFailure("UnbindSafely", i, owner, memberName, exception);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Safely unbinds a list of binders.
        /// </summary>
        /// <remarks>
        /// Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.
        /// </remarks>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The list of binders to unbind.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this List<T>? binders, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Count; i++)
                {
                    var binder = binders[i];
                    
                    if (IsMissing(binder))
                    {
                        BuildUnbindSafelyBinderNullMessage(i, owner, memberName);
                    }
                    else
                    {
                        try
                        {
                            binder.Unbind();
                        }
                        catch (System.Exception exception)
                        {
                            // One binder must not take the rest of the collection with it: on Bind that would leave
                            // the View half-initialised, and on Unbind it would leave the remaining binders
                            // subscribed to a ViewModel that is going away.
                            ReportBinderFailure("UnbindSafely", i, owner, memberName, exception);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Safely unbinds a sequence of binders.
        /// </summary>
        /// <remarks>
        /// Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.
        /// </remarks>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The enumerable of binders to unbind.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnbindSafely<T>(this IEnumerable<T>? binders, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                var index = 0;
                foreach (var binder in binders)
                {
                    if (IsMissing(binder))
                    {
                        BuildUnbindSafelyBinderNullMessage(index, owner, memberName);
                    }
                    else
                    {
                        try
                        {
                            binder.Unbind();
                        }
                        catch (System.Exception exception)
                        {
                            // One binder must not take the rest of the collection with it: on Bind that would leave
                            // the View half-initialised, and on Unbind it would leave the remaining binders
                            // subscribed to a ViewModel that is going away.
                            ReportBinderFailure("UnbindSafely", index, owner, memberName, exception);
                        }
                    }
                    
                    index++;
                }
            }
        }
        
        [Conditional(conditionString: "DEBUG")]
        private static void BuildUnbindSafelyBinderNullMessage(int index, object? owner, string? memberName)
        {
            var message = BuildBinderNullMessage(operation: "UnbindSafely", index, owner, memberName);
            UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
        }
    }
}
