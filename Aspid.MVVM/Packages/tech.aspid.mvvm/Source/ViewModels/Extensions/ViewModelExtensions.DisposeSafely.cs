using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Calls <see cref="IDisposable.Dispose"/> on the ViewModel.
        /// </summary>
        /// <typeparam name="T">The disposable ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to clean up. Skipped if <see langword="null"/> or destroyed.</param>
        public static void DisposeViewModelSafely<T>(this T? viewModel)
            where T : class, IViewModel, IDisposable
        {
            if (viewModel.IsMissing()) return;
            viewModel.Dispose();
        }

        /// <summary>
        /// Disposes the ViewModel if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to clean up. Skipped if <see langword="null"/> or destroyed.</param>
        public static void DisposeViewModelSafely(this IViewModel? viewModel)
        {
            if (viewModel.IsMissing()) return;

            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }

        /// <summary>
        /// Calls <see cref="DisposeViewModelSafely(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DisposeViewModelsSafely<T>(this T[]? viewModels)
            where T : IViewModel?
        {
            if (viewModels is null) return;

            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DisposeSafelyInternal(viewModel, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DisposeViewModelSafely(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DisposeViewModelsSafely<T>(this Span<T> viewModels)
            where T : IViewModel?
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DisposeSafelyInternal(viewModel, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewModelsSafely{T}(Span{T})"/>
        public static void DisposeViewModelsSafely<T>(this ReadOnlySpan<T> viewModels)
            where T : IViewModel?
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DisposeSafelyInternal(viewModel, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewModelsSafely{T}(T[])"/>
        public static void DisposeViewModelsSafely<T>(this List<T>? viewModels)
            where T : IViewModel?
        {
            if (viewModels is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var viewModel in viewModels)
                    DisposeSafelyInternal(viewModel, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewModelsSafely{T}(T[])"/>
        public static void DisposeViewModelsSafely<T>(this IEnumerable<T>? viewModels)
            where T : IViewModel?
        {
            switch (viewModels)
            {
                case null: return;
                case T[] array: array.DisposeViewModelsSafely(); return;
                case List<T> list: list.DisposeViewModelsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = viewModels.GetEnumerator();

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

        private static void DisposeSafelyInternal(IViewModel? viewModel, ref CleanupExceptions failures)
        {
            try
            {
                viewModel.DisposeViewModelSafely();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
