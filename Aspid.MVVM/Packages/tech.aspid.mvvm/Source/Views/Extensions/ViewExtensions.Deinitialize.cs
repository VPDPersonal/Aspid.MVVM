using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Unbinds the View from its current ViewModel.
        /// </summary>
        /// <param name="view">The View to deinitialize.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or destroyed.
        /// </exception>
        public static IViewModel? DeinitializeView(this IView view) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : view.DeinitializeViewInternal();

        /// <summary>
        /// Deinitializes each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to deinitialize.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="views"/> is <see langword="null"/>, or the only failure is a destroyed View.
        /// </exception>
        /// <exception cref="NullReferenceException">The only failure is a <see langword="null"/> View.</exception>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DeinitializeViews<T>(this T[] views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DeinitializeViewInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Deinitializes each View and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to deinitialize.</param>
        /// <exception cref="ArgumentNullException">The only failure is a destroyed View.</exception>
        /// <exception cref="NullReferenceException">The only failure is a <see langword="null"/> View.</exception>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DeinitializeViews<T>(this Span<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DeinitializeViewInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DeinitializeViews{T}(Span{T})"/>
        public static void DeinitializeViews<T>(this ReadOnlySpan<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                view.DeinitializeViewInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DeinitializeViews{T}(T[])"/>
        public static void DeinitializeViews<T>(this List<T> views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    view.DeinitializeViewInternal(ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DeinitializeViews{T}(T[])"/>
        public static void DeinitializeViews<T>(this IEnumerable<T> views)
            where T : IView
        {
            switch (views)
            {
                case T[] array: array.DeinitializeViews(); return;
                case List<T> list: list.DeinitializeViews(); return;
                case null: throw new ArgumentNullException(nameof(views));
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        enumerator.Current!.DeinitializeViewInternal(ref failures);
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

        private static IViewModel? DeinitializeViewInternal(this IView view)
        {
            var viewModel = view.ViewModel;
            view.Deinitialize();

            return viewModel;
        }

        private static void DeinitializeViewInternal(this IView view, ref CleanupExceptions failures)
        {
            try
            {
                if (view.IsMissing())
                    throw new ArgumentNullException(nameof(view));

                view.Deinitialize();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
