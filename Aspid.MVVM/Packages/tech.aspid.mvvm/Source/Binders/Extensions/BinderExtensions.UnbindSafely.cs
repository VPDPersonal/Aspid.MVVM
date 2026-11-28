using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Releases the binding held by the specified owner.
        /// </summary>
        /// <param name="binder">The binder to unbind. Skipped if <see langword="null"/> or destroyed.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        public static void UnbindSafely(
            this IBinder? binder,
            in UnbindContext context)
        {
            if (binder.IsMissing()) return;
            binder.Unbind(in context);
        }

        /// <summary>
        /// Releases each binding held by the specified owner, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>Continues after binder failures and rethrows a single failure with its original stack trace.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind. Pass <see langword="null"/> to skip the operation.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="AggregateException">Multiple binders fail during cleanup.</exception>
        public static void UnbindSafely<T>(
            this T?[]? binders,
            in UnbindContext context)
            where T : IBinder
        {
            if (binders is null) return;
            
            context.EnsureValid();
            var failures = new CleanupExceptions();

            foreach (var binder in binders)
                binder.UnbindSafelyInternal(in context, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Releases each binding held by the specified owner, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>Continues after binder failures and rethrows a single failure with its original stack trace.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="AggregateException">Multiple binders fail during cleanup.</exception>
        public static void UnbindSafely<T>(
            this in Span<T?> binders,
            in UnbindContext context)
            where T : IBinder
        {
            context.EnsureValid();
            var failures = new CleanupExceptions();

            foreach (var binder in binders)
                binder.UnbindSafelyInternal(in context, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="UnbindSafely{T}(in Span{T}, in UnbindContext)"/>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        public static void UnbindSafely<T>(
            this in ReadOnlySpan<T?> binders,
            in UnbindContext context)
            where T : IBinder
        {
            context.EnsureValid();
            var failures = new CleanupExceptions();

            foreach (var binder in binders)
                binder.UnbindSafelyInternal(in context, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Releases each binding held by the specified owner, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>
        /// <para>Continues after binder failures and rethrows a single failure with its original stack trace.</para>
        /// <para>Modifying the list stops enumeration.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind. Pass <see langword="null"/> to skip the operation.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration failures occur.</exception>
        public static void UnbindSafely<T>(
            this List<T?>? binders,
            in UnbindContext context)
            where T : IBinder
        {
            if (binders is null) return;
            
            context.EnsureValid();
            var failures = new CleanupExceptions();

            try
            {
                foreach (var binder in binders)
                    binder.UnbindSafelyInternal(in context, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Releases each binding held by the specified owner, skipping <see langword="null"/> and destroyed Unity objects.
        /// </summary>
        /// <remarks>
        /// <para>Continues after binder failures and rethrows a single failure with its original stack trace.</para>
        /// <para>Enumeration errors can leave later binders unprocessed.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind. Pass <see langword="null"/> to skip the operation.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration failures occur.</exception>
        public static void UnbindSafely<T>(
            this IEnumerable<T?>? binders,
            in UnbindContext context)
            where T : IBinder
        {
            switch (binders)
            {
                case null: return;
                case T?[] array: array.UnbindSafely(in context); return;
                case List<T?> list: list.UnbindSafely(in context); return;
            }

            context.EnsureValid();

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = binders.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                    {
                        var binder = enumerator.Current;
                        binder.UnbindSafelyInternal(in context, ref failures);
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

        private static void UnbindSafelyInternal(
            this IBinder? binder,
            in UnbindContext context,
            ref CleanupExceptions failures)
        {
            if (!binder.IsMissing())
                UnbindInternal(binder, in context, ref failures);
        }
    }
}
