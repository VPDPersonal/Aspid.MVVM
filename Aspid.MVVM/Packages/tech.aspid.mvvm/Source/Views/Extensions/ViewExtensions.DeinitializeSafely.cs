using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Unbinds the View from its current ViewModel.
        /// </summary>
        /// <param name="view">The View to deinitialize. Skipped if <see langword="null"/> or destroyed.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View was skipped or had none.</returns>
        public static IViewModel? DeinitializeViewSafely(this IView? view) => view.IsMissing()
            ? null
            : view.DeinitializeViewInternal();

        /// <summary>
        /// Deinitializes each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DeinitializeViewsSafely<T>(this T[]? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DeinitializeViewSafelyInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Deinitializes each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DeinitializeViewsSafely<T>(this Span<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DeinitializeViewSafelyInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DeinitializeViewsSafely{T}(Span{T})"/>
        public static void DeinitializeViewsSafely<T>(this ReadOnlySpan<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DeinitializeViewSafelyInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DeinitializeViewsSafely{T}(T[])"/>
        public static void DeinitializeViewsSafely<T>(this List<T>? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    view.DeinitializeViewSafelyInternal(ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DeinitializeViewsSafely{T}(T[])"/>
        public static void DeinitializeViewsSafely<T>(this IEnumerable<T>? views)
            where T : IView?
        {
            switch (views)
            {
                case null: return;
                case T[] array: array.DeinitializeViewsSafely(); return;
                case List<T> list: list.DeinitializeViewsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        enumerator.Current!.DeinitializeViewSafelyInternal(ref failures);
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

        private static void DeinitializeViewSafelyInternal(this IView? view, ref CleanupExceptions failures)
        {
            try
            {
                if (!view.IsMissing())
                    view.Deinitialize();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
