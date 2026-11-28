using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Calls <see cref="IDisposable.Dispose"/> on the View.
        /// </summary>
        /// <typeparam name="T">The disposable View type.</typeparam>
        /// <param name="view">The View to clean up. Skipped if <see langword="null"/> or destroyed.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View was skipped or had none.</returns>
        public static IViewModel? DisposeViewSafely<T>(this T? view)
            where T : class, IView, IDisposable
        {
            return view.IsMissing()
                ? null
                : view.DisposeViewInternal();
        }

        /// <summary>
        /// Disposes the View if it implements <see cref="IDisposable"/>, or deinitializes it otherwise.
        /// </summary>
        /// <param name="view">The View to clean up. Skipped if <see langword="null"/> or destroyed.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View was skipped or had none.</returns>
        public static IViewModel? DisposeViewSafely(this IView? view)
        {
            return view.IsMissing()
                ? null
                : view.DisposeViewInternal();
        }

        /// <summary>
        /// Calls <see cref="DisposeViewSafely(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DisposeViewsSafely<T>(this T[]? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            foreach (var view in views)
                DisposeSafelyInternal(view, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DisposeViewSafely(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DisposeViewsSafely<T>(this Span<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DisposeSafelyInternal(view, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewsSafely{T}(Span{T})"/>
        public static void DisposeViewsSafely<T>(this ReadOnlySpan<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DisposeSafelyInternal(view, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewsSafely{T}(T[])"/>
        public static void DisposeViewsSafely<T>(this List<T>? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    DisposeSafelyInternal(view, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewsSafely{T}(T[])"/>
        public static void DisposeViewsSafely<T>(this IEnumerable<T>? views)
            where T : IView?
        {
            switch (views)
            {
                case null: return;
                case T[] array: array.DisposeViewsSafely(); return;
                case List<T> list: list.DisposeViewsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        DisposeSafelyInternal(enumerator.Current, ref failures);
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

        private static void DisposeSafelyInternal(IView? view, ref CleanupExceptions failures)
        {
            try
            {
                view.DisposeViewSafely();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
