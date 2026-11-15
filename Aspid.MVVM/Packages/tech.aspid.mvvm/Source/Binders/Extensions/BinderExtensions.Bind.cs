#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for binding and unbinding <see cref="IBinder"/> instances and their collections.
    /// </summary>
    /// <remarks>
    /// Binder failures are logged once with context and the original exception details.
    /// A live Unity object owner is used as the Console context.
    /// </remarks>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Binds <paramref name="binder"/> to the found member.
        /// </summary>
        /// <remarks>Skips <see langword="null"/> or destroyed binders and logs binder exceptions.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder to bind; <see langword="null"/> to do nothing.</param>
        /// <param name="result">The member lookup result; an unsuccessful lookup does nothing.</param>
        /// <param name="owner">The owner acquiring the binding, also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        public static void BindSafely<T>(
            this T? binder,
            in FindBindableMemberResult result,
            object owner,
            string? memberName = null)
            where T : IBinder
        {
            if (result.IsFound)
                binder.BindSafely(new BindContext(result.Adder!, owner), memberName);
        }

        /// <summary>
        /// Binds <paramref name="binder"/> using the given context.
        /// </summary>
        /// <remarks>Skips <see langword="null"/> or destroyed binders and logs binder exceptions.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder to bind; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The bindable member and owner; the owner is also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        public static void BindSafely<T>(
            this T? binder,
            in BindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                BindSafelyInternal(binder, in context, memberName);
            }
        }

        /// <summary>
        /// Binds each binder to the found member.
        /// </summary>
        /// <remarks>
        /// <para>Skips <see langword="null"/> or destroyed entries, reporting them under <c>DEBUG</c>.</para>
        /// <para>Logs binder exceptions and continues with the remaining entries.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind; <see langword="null"/> to do nothing.</param>
        /// <param name="result">The member lookup result; an unsuccessful lookup does nothing.</param>
        /// <param name="owner">The owner acquiring the binding, also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        public static void BindSafely<T>(
            this T[]? binders,
            in FindBindableMemberResult result,
            object owner,
            string? memberName = null)
            where T : IBinder
        {
            if (result.IsFound)
                binders.BindSafely(new BindContext(result.Adder!, owner), memberName);
        }

        /// <summary>
        /// Binds each binder using the given context.
        /// </summary>
        /// <remarks>
        /// <para>Skips <see langword="null"/> or destroyed entries, reporting them under <c>DEBUG</c>.</para>
        /// <para>Logs binder exceptions and continues with the remaining entries.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The bindable member and owner; the owner is also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        public static void BindSafely<T>(
            this T[]? binders,
            in BindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Length; i++)
                    BindSafelyInternal(binders[i], in context, memberName, i);
            }
        }

        /// <inheritdoc cref="BindSafely{T}(T[], in FindBindableMemberResult, object, string)"/>
        public static void BindSafely<T>(
            this List<T>? binders,
            in FindBindableMemberResult result,
            object owner,
            string? memberName = null)
            where T : IBinder
        {
            if (result.IsFound)
                binders.BindSafely(new BindContext(result.Adder!, owner), memberName);
        }

        /// <inheritdoc cref="BindSafely{T}(T[], in BindContext, string)"/>
        public static void BindSafely<T>(
            this List<T>? binders,
            in BindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Count; i++)
                    BindSafelyInternal(binders[i], in context, memberName, i);
            }
        }

        /// <summary>
        /// Binds each binder to the found member.
        /// </summary>
        /// <remarks>
        /// <para>Skips <see langword="null"/> or destroyed entries, reporting them under <c>DEBUG</c>.</para>
        /// <para>Logs binder exceptions and continues with the remaining entries.</para>
        /// <para>Enumeration and enumerator disposal failures propagate; a single failure retains its original stack trace.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind; <see langword="null"/> to do nothing.</param>
        /// <param name="result">The member lookup result; an unsuccessful lookup does nothing.</param>
        /// <param name="owner">The owner acquiring the binding, also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        /// <exception cref="AggregateException">Both enumeration and enumerator disposal fail.</exception>
        public static void BindSafely<T>(
            this IEnumerable<T>? binders,
            in FindBindableMemberResult result,
            object owner,
            string? memberName = null)
            where T : IBinder
        {
            if (result.IsFound)
                binders.BindSafely(new BindContext(result.Adder!, owner), memberName);
        }

        /// <summary>
        /// Binds each binder using the given context.
        /// </summary>
        /// <remarks>
        /// <para>Skips <see langword="null"/> or destroyed entries, reporting them under <c>DEBUG</c>.</para>
        /// <para>Logs binder exceptions and continues with the remaining entries.</para>
        /// <para>Enumeration and enumerator disposal failures propagate; a single failure retains its original stack trace.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to bind; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The bindable member and owner; the owner is also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        /// <exception cref="AggregateException">Both enumeration and enumerator disposal fail.</exception>
        public static void BindSafely<T>(
            this IEnumerable<T>? binders,
            in BindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.BindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;
                var failures = new CleanupExceptions();

                try
                {
                    using var enumerator = binders.GetEnumerator();

                    try
                    {
                        var index = 0;
                        while (enumerator.MoveNext())
                        {
                            BindSafelyInternal(enumerator.Current, in context, memberName, index);
                            index++;
                        }
                    }
                    catch (Exception exception)
                    {
                        failures.Add(exception);
                    }
                }
                catch (Exception exception)
                {
                    failures.Add(exception);
                }

                failures.ThrowIfAny();
            }
        }

        private static void BindSafelyInternal<T>(
            T? binder,
            in BindContext context,
            string? memberName,
            int? index = null)
            where T : IBinder
        {
            if (IsMissing(binder))
            {
                ReportMissingBinder(
                    operation: "BindSafely",
                    index: index,
                    owner: context.Owner,
                    memberName: memberName);

                return;
            }

            try
            {
                context.EnsureValid();
                binder!.Bind(in context);
            }
            catch (Exception exception)
            {
                ReportBinderFailure(
                    operation: "BindSafely",
                    index: index,
                    owner: context.Owner,
                    memberName: memberName,
                    exception: exception);
            }
        }

#if ASPID_MVVM_PROFILER
        private static partial class Markers<T>
            where T : IBinder
        {
            public static readonly Unity.Profiling.ProfilerMarker BindSafelyMarker = new(name: $"BindSafely<{typeof(T)}>");
        }
#endif
    }
}
