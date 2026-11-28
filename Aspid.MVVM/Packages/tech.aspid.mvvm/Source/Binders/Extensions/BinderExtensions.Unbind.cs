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
        /// Releases each binding held by the specified owner.
        /// </summary>
        /// <remarks>Continues after binder failures and rethrows a single failure with its original stack trace.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentNullException"><paramref name="binders"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="NullReferenceException">The only failure is a <see langword="null"/> entry.</exception>
        /// <exception cref="AggregateException">Multiple binders fail during cleanup.</exception>
        public static void Unbind<T>(
            this T[] binders,
            in UnbindContext context)
            where T : IBinder
        {
            if (binders is null)
                throw new ArgumentNullException(nameof(binders));

            context.EnsureValid();
            var failures = new CleanupExceptions();
            
            foreach (var binder in binders)
                UnbindInternal(binder, in context, ref failures);

            failures.ThrowIfAny();
        }
        
        /// <summary>
        /// Releases each binding held by the specified owner.
        /// </summary>
        /// <remarks>Continues after binder failures and rethrows a single failure with its original stack trace.</remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="NullReferenceException">The only failure is a <see langword="null"/> entry.</exception>
        /// <exception cref="AggregateException">Multiple binders fail during cleanup.</exception>
        public static void Unbind<T>(
            this Span<T> binders,
            in UnbindContext context)
            where T : IBinder
        {
            context.EnsureValid();
            var failures = new CleanupExceptions();
            
            foreach (var binder in binders)
                UnbindInternal(binder, in context, ref failures);

            failures.ThrowIfAny();
        }
        
        /// <inheritdoc cref="Unbind{T}(Span{T}, in UnbindContext)"/>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        public static void Unbind<T>(
            this ReadOnlySpan<T> binders,
            in UnbindContext context)
            where T : IBinder
        {
            context.EnsureValid();
            var failures = new CleanupExceptions();
            
            foreach (var binder in binders)
                UnbindInternal(binder, in context, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Releases each binding held by the specified owner.
        /// </summary>
        /// <remarks>
        /// <para>Continues after binder failures and rethrows a single failure with its original stack trace.</para>
        /// <para>Modifying the list stops enumeration.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentNullException"><paramref name="binders"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="NullReferenceException">The only failure is a <see langword="null"/> entry.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration failures occur.</exception>
        public static void Unbind<T>(
            this List<T> binders,
            in UnbindContext context)
            where T : IBinder
        {
            if (binders is null)
                throw new ArgumentNullException(nameof(binders));

            context.EnsureValid();
            var failures = new CleanupExceptions();

            try
            {
                foreach (var binder in binders)
                    UnbindInternal(binder, in context, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }
        
        /// <summary>
        /// Releases each binding held by the specified owner.
        /// </summary>
        /// <remarks>
        /// <para>Continues after binder failures and rethrows a single failure with its original stack trace.</para>
        /// <para>Enumeration errors can leave later binders unprocessed.</para>
        /// </remarks>
        /// <typeparam name="T">The binder type.</typeparam>
        /// <param name="binders">The binders to unbind.</param>
        /// <param name="context">The owner that acquired the binding.</param>
        /// <exception cref="ArgumentNullException"><paramref name="binders"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="context"/> has no owner.</exception>
        /// <exception cref="NullReferenceException">The only failure is a <see langword="null"/> entry.</exception>
        /// <exception cref="AggregateException">Multiple cleanup or enumeration failures occur.</exception>
        public static void Unbind<T>(
            this IEnumerable<T> binders,
            in UnbindContext context)
            where T : IBinder
        {
            switch (binders)
            {
                case T[] array: array.Unbind(in context); return;
                case List<T> list: list.Unbind(in context); return;
                case null: throw new ArgumentNullException(nameof(binders));
            }

            context.EnsureValid();
            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = binders.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        UnbindInternal(enumerator.Current!, in context, ref failures);
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

        private static void UnbindInternal(
            IBinder binder,
            in UnbindContext context,
            ref CleanupExceptions failures)
        {
            try
            {
                binder.Unbind(in context);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
