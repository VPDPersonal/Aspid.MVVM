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
        /// Unbinds <paramref name="binder"/>; does nothing when it is <see langword="null"/> or a destroyed Unity object.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder to unbind, or <see langword="null"/>.</param>
        /// <param name="owner">Unused; matches the collection overloads.</param>
        /// <param name="memberName">Unused; matches the collection overloads.</param>
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
        /// Unbinds each binder in <paramref name="binders"/>.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to unbind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind, or <see langword="null"/>.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
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
        /// Unbinds each binder in <paramref name="binders"/>.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to unbind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind, or <see langword="null"/>.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
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
        /// Unbinds each binder in <paramref name="binders"/>.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to unbind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind, or <see langword="null"/>.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
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
