using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IView"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Calls <see cref="IDisposable.Dispose"/> on the View.
        /// </summary>
        /// <typeparam name="T">The disposable View type.</typeparam>
        /// <param name="view">The View to clean up.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or destroyed.
        /// </exception>
        public static IViewModel? DisposeView<T>(this T view)
            where T : class, IView, IDisposable
        {
            return view.IsMissing()
                ? throw new ArgumentNullException(nameof(view))
                : view.DisposeViewInternal();
        }

        /// <summary>
        /// Disposes the View if it implements <see cref="IDisposable"/>, or deinitializes it otherwise.
        /// </summary>
        /// <param name="view">The View to clean up.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or destroyed.
        /// </exception>
        public static IViewModel? DisposeView(this IView view) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : view.DisposeViewInternal();

        /// <summary>
        /// Calls <see cref="DisposeView(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to clean up.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="views"/> is <see langword="null"/>, or the only failure is a missing or destroyed View.
        /// </exception>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DisposeViews<T>(this T[] views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DisposeViewInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DisposeView(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to clean up.</param>
        /// <exception cref="ArgumentNullException">The only failure is a <see langword="null"/> or destroyed View.</exception>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DisposeViews<T>(this Span<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DisposeViewInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViews{T}(Span{T})"/>
        public static void DisposeViews<T>(this ReadOnlySpan<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DisposeViewInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViews{T}(T[])"/>
        public static void DisposeViews<T>(this List<T> views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    view.DisposeViewInternal(ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViews{T}(T[])"/>
        public static void DisposeViews<T>(this IEnumerable<T> views)
            where T : IView
        {
            switch (views)
            {
                case T[] array: array.DisposeViews(); return;
                case List<T> list: list.DisposeViews(); return;
                case null: throw new ArgumentNullException(nameof(views));
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        enumerator.Current!.DisposeViewInternal(ref failures);
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

        private static IViewModel? DisposeViewInternal<T>(this T view)
            where T : class, IView, IDisposable
        {
            var viewModel = view.ViewModel;
            view.Dispose();

            return viewModel;
        }

        private static IViewModel? DisposeViewInternal(this IView view)
        {
            if (view is not IDisposable disposable)
                return view.DeinitializeView();

            var viewModel = view.ViewModel;
            disposable.Dispose();

            return viewModel;
        }

        private static void DisposeViewInternal(this IView view, ref CleanupExceptions failures)
        {
            try
            {
                view.DisposeView();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
