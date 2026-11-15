#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Releases each binder whose owner matches the context.
        /// </summary>
        /// <remarks>
        /// Skips <see langword="null"/> entries and finishes cleanup before reporting failures; a single failure retains its original stack trace.
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to release; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The owner releasing the bindings.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> is uninitialized.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration operations fail.</exception>
        public static void Unbind<T>(
            this T[]? binders,
            in UnbindContext context)
            where T : IBinder
        {
            context.EnsureValid();
            if (binders is null) return;
            
            var failures = new CleanupExceptions();
            
            foreach (var binder in binders)
                UnbindInternal(binder, in context, ref failures);
            
            failures.ThrowIfAny();
        }

        /// <summary>
        /// Releases each binder whose owner matches the context.
        /// </summary>
        /// <remarks>
        /// Skips <see langword="null"/> entries and finishes cleanup before reporting failures; a single failure retains its original stack trace.
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to release; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The owner releasing the bindings.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> is uninitialized.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration operations fail.</exception>
        public static void Unbind<T>(
            this List<T>? binders,
            in UnbindContext context)
            where T : IBinder
        {
            context.EnsureValid();
            if (binders is null) return;
            
            var failures = new CleanupExceptions();
            
            foreach (var binder in binders)
                UnbindInternal(binder, in context, ref failures);
            
            failures.ThrowIfAny();
        }

        /// <summary>
        /// Releases each binder whose owner matches the context.
        /// </summary>
        /// <remarks>Skips <see langword="null"/> entries and reports cleanup and enumeration failures after processing reachable entries.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to release; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The owner releasing the bindings.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> is uninitialized.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration operations fail.</exception>
        public static void Unbind<T>(
            this IEnumerable<T>? binders,
            in UnbindContext context)
            where T : IBinder
        {
            switch (binders)
            {
                case T[] array:
                    array.Unbind(in context);
                    return;
                
                case List<T> list:
                    list.Unbind(in context);
                    return;
            }

            context.EnsureValid();
            if (binders is null) return;
            
            var failures = new CleanupExceptions();
            
            try
            {
                using var enumerator = binders.GetEnumerator();
                
                try
                {
                    while (enumerator.MoveNext())
                        UnbindInternal(enumerator.Current, in context, ref failures);
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

        /// <summary>
        /// Unbinds <paramref name="binder"/>.
        /// </summary>
        /// <remarks>Skips <see langword="null"/> or destroyed binders and logs binder exceptions.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binder">The binder to unbind; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The owner releasing the binding, also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        public static void UnbindSafely<T>(
            this T? binder,
            in UnbindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                UnbindSafelyInternal(binder, in context, memberName);
            }
        }

        /// <summary>
        /// Unbinds each binder.
        /// </summary>
        /// <remarks>
        /// <para>Skips <see langword="null"/> or destroyed entries, reporting them under <c>DEBUG</c>.</para>
        /// <para>Logs binder exceptions and continues with the remaining entries.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The owner releasing the bindings, also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        public static void UnbindSafely<T>(
            this T[]? binders,
            in UnbindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Length; i++)
                    UnbindSafelyInternal(binders[i], in context, memberName, i);
            }
        }

        /// <inheritdoc cref="UnbindSafely{T}(T[], in UnbindContext, string)"/>
        public static void UnbindSafely<T>(
            this List<T>? binders,
            in UnbindContext context, 
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
#endif
            {
                if (binders is null) return;

                for (var i = 0; i < binders.Count; i++)
                    UnbindSafelyInternal(binders[i], in context, memberName, i);
            }
        }

        /// <summary>
        /// Unbinds each binder.
        /// </summary>
        /// <remarks>
        /// <para>Skips <see langword="null"/> or destroyed entries, reporting them under <c>DEBUG</c>.</para>
        /// <para>Logs binder exceptions and continues with the remaining entries.</para>
        /// <para>Enumeration and enumerator disposal failures propagate; a single failure retains its original stack trace.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind; <see langword="null"/> to do nothing.</param>
        /// <param name="context">The owner releasing the bindings, also used for diagnostics.</param>
        /// <param name="memberName">The binder field or property name; <see langword="null"/> for none.</param>
        /// <exception cref="AggregateException">Both enumeration and enumerator disposal fail.</exception>
        public static void UnbindSafely<T>(
            this IEnumerable<T>? binders,
            in UnbindContext context,
            string? memberName = null)
            where T : IBinder
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.UnbindSafelyMarker.Auto())
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
                            UnbindSafelyInternal(enumerator.Current, in context, memberName, index);
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

        private static void UnbindInternal<T>(
            T? binder,
            in UnbindContext context,
            ref CleanupExceptions failures)
            where T : IBinder
        {
            if (binder is null) return;

            try
            {
                binder.Unbind(in context);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }

        private static void UnbindSafelyInternal<T>(
            T? binder,
            in UnbindContext context,
            string? memberName,
            int? index = null)
            where T : IBinder
        {
            if (IsMissing(binder))
            {
                ReportMissingBinder(
                    operation: "UnbindSafely",
                    index: index,
                    owner: context.Owner,
                    memberName: memberName);
                
                return;
            }

            try
            {
                context.EnsureValid();
                binder!.Unbind(in context);
            }
            catch (Exception exception)
            {
                ReportBinderFailure(
                    operation: "UnbindSafely",
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
            public static readonly Unity.Profiling.ProfilerMarker UnbindSafelyMarker = new(name: $"UnbindSafely<{typeof(T)}>");
        }
#endif
    }
}
