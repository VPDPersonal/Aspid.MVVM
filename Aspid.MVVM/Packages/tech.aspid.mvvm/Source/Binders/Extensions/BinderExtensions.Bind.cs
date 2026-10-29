#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for binding and unbinding <see cref="IBinder"/> instances and their collections.
    /// </summary>
    public static partial class BinderExtensions
    {
#if ASPID_MVVM_PROFILER
        private static partial class Markers<T>
            where T : IBinder
        {
            public static readonly Unity.Profiling.ProfilerMarker BindSafelyMarker = new(name: $"BindSafely<{typeof(T)}>");
        }
#endif
        
        #region Single BindSafely
        /// <summary>
        /// Binds <paramref name="binder"/> to the found bindable member; does nothing when <paramref name="binder"/> is
        /// <see langword="null"/> or no member was found.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder to bind, or <see langword="null"/>.</param>
        /// <param name="result">The result of <see cref="IViewModel.FindBindableMember"/>.</param>
        /// <param name="owner">Unused; matches the collection overloads.</param>
        /// <param name="memberName">Unused; matches the collection overloads.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                // ReSharper disable once NullableWarningSuppressionIsUsed
                if (result.IsFound)
                    binder?.Bind(result.Adder!);
            }
        }

        /// <summary>
        /// Binds <paramref name="binder"/> to <paramref name="binderAdder"/>; does nothing when <paramref name="binder"/> is
        /// <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder to bind, or <see langword="null"/>.</param>
        /// <param name="binderAdder">The bindable member to bind to.</param>
        /// <param name="owner">Unused; matches the collection overloads.</param>
        /// <param name="memberName">Unused; matches the collection overloads.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                binder?.Bind(binderAdder);
            }
        }
        #endregion

        #region Array BindSafely
        /// <summary>
        /// Binds each binder in <paramref name="binders"/> to the found bindable member; does nothing when no member was found.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to bind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="result">The result of <see cref="IViewModel.FindBindableMember"/>.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                // ReSharper disable once NullableWarningSuppressionIsUsed
                if (result.IsFound)
                    binders.BindSafely(result.Adder!, owner, memberName);
            }
        }

        /// <summary>
        /// Binds each binder in <paramref name="binders"/> to <paramref name="binderAdder"/>.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to bind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="binderAdder">The bindable member to bind to.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Length; i++)
                {
                    var binder = binders[i];
                    
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                    if (IsMissing(binder))
                    {
                        BuildBindSafelyBinderNullMessage(i, owner, memberName);
                    }
                    else
                    {
                        try
                        {
                            binder.Bind(binderAdder);
                        }
                        catch (System.Exception exception)
                        {
                            // One binder must not take the rest of the collection with it: on Bind that would leave
                            // the View half-initialised, and on Unbind it would leave the remaining binders
                            // subscribed to a ViewModel that is going away.
                            ReportBinderFailure("BindSafely", i, owner, memberName, exception);
                        }
                    }
                }
            }
        }
        #endregion

        #region List BindSafely
        /// <summary>
        /// Binds each binder in <paramref name="binders"/> to the found bindable member; does nothing when no member was found.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to bind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="result">The result of <see cref="IViewModel.FindBindableMember"/>.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                // ReSharper disable once NullableWarningSuppressionIsUsed
                if (result.IsFound)
                    binders.BindSafely(result.Adder!, owner, memberName);
            }
        }

        /// <summary>
        /// Binds each binder in <paramref name="binders"/> to <paramref name="binderAdder"/>.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to bind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="binderAdder">The bindable member to bind to.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Count; i++)
                {
                    var binder = binders[i];
                    
                    if (IsMissing(binder))
                    {
                        BuildBindSafelyBinderNullMessage(i, owner, memberName);
                    }
                    else
                    {
                        try
                        {
                            binder.Bind(binderAdder);
                        }
                        catch (System.Exception exception)
                        {
                            // One binder must not take the rest of the collection with it: on Bind that would leave
                            // the View half-initialised, and on Unbind it would leave the remaining binders
                            // subscribed to a ViewModel that is going away.
                            ReportBinderFailure("BindSafely", i, owner, memberName, exception);
                        }
                    }
                }
            }
        }
        #endregion

        #region IEnumerable BindSafely
        /// <summary>
        /// Binds each binder in <paramref name="binders"/> to the found bindable member; does nothing when no member was found.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to bind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="result">The result of <see cref="IViewModel.FindBindableMember"/>.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                // ReSharper disable once NullableWarningSuppressionIsUsed
                if (result.IsFound)
                    binders.BindSafely(result.Adder!, owner, memberName);
            }
        }

        /// <summary>
        /// Binds each binder in <paramref name="binders"/> to <paramref name="binderAdder"/>.
        /// </summary>
        /// <remarks>
        /// <para>Skips a <see langword="null"/> or destroyed binder; under <c>DEBUG</c>, also logs an error.</para>
        /// <para>Logs an exception thrown by a binder and goes on to bind the rest.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind, or <see langword="null"/>.</param>
        /// <param name="binderAdder">The bindable member to bind to.</param>
        /// <param name="owner">The View that holds <paramref name="binders"/>, named in log messages and used as their context when it is a Unity object; <see langword="null"/> for none.</param>
        /// <param name="memberName">The name of the member that holds <paramref name="binders"/>, named in log messages; <see langword="null"/> for none.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                var index = 0;
                foreach (var binder in binders)
                {
                    if (IsMissing(binder))
                    {
                        BuildBindSafelyBinderNullMessage(index, owner, memberName);
                    }
                    else
                    {
                        try
                        {
                            binder.Bind(binderAdder);
                        }
                        catch (System.Exception exception)
                        {
                            // One binder must not take the rest of the collection with it: on Bind that would leave
                            // the View half-initialised, and on Unbind it would leave the remaining binders
                            // subscribed to a ViewModel that is going away.
                            ReportBinderFailure("BindSafely", index, owner, memberName, exception);
                        }
                    }
                    
                    index++;
                }
            }
        }
        #endregion

        [Conditional(conditionString: "DEBUG")]
        private static void BuildBindSafelyBinderNullMessage(int index, object? owner, string? memberName)
        {
            var message = BuildBinderNullMessage(operation: "BindSafely", index, owner, memberName);
            UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
        }

        // Not [Conditional("DEBUG")]: a binder that fails in a player build needs the report most.
        private static void ReportBinderFailure(string operation, int index, object? owner, string? memberName, System.Exception exception)
        {
            var message = $"{BuildBinderContext(operation, index, owner, memberName)} threw {exception.GetType().Name}: {exception.Message}";

            UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
            UnityEngine.Debug.LogException(exception, owner as UnityEngine.Object);
        }

        // A destroyed MonoBinder is not null to C#; only Unity's own bool conversion sees it.
        private static bool IsMissing<T>(T binder)
        {
            if (binder is null) return true;

            if (binder is UnityEngine.Object unityObject) return !unityObject;
            return false;
        }

        private static string BuildBinderNullMessage(string operation, int index, object? owner, string? memberName) =>
            $"{BuildBinderContext(operation, index, owner, memberName)} can't be null";

        private static string BuildBinderContext(string operation, int index, object? owner, string? memberName)
        {
            var memberPart = memberName ?? "<unknown>";

            if (owner is null)
                return $"[{operation}] Binder at index {index} '{memberPart}'";

            var typeName = owner.GetType().Name;

            if (owner is UnityEngine.Object uo)
                return $"[{typeName}] [{operation}] Binder at index {index} '{memberPart}' of view '{typeName}' (GameObject '{uo.name}')";
            return $"[{typeName}] [{operation}] Binder at index {index} '{memberPart}' of view '{typeName}'";
        }
    }
}
