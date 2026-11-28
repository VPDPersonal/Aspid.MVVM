using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IViewModel"/>.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Calls <see cref="IDisposable.Dispose"/> on the ViewModel.
        /// </summary>
        /// <typeparam name="T">The disposable ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to clean up.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> is <see langword="null"/> or destroyed.
        /// </exception>
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            if (viewModel.IsMissing())
                throw new ArgumentNullException(nameof(viewModel));

            viewModel.Dispose();
        }

        /// <summary>
        /// Disposes the ViewModel if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to clean up.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> is <see langword="null"/> or destroyed.
        /// </exception>
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel.IsMissing())
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }

        /// <summary>
        /// Calls <see cref="DisposeViewModel(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels to clean up.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModels"/> is <see langword="null"/>, or the only failure is a missing or destroyed ViewModel.
        /// </exception>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DisposeViewModels<T>(this T[] viewModels)
            where T : IViewModel
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                viewModel.DisposeViewModelInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DisposeViewModel(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels to clean up.</param>
        /// <exception cref="ArgumentNullException">The only failure is a <see langword="null"/> or destroyed ViewModel.</exception>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DisposeViewModels<T>(this Span<T> viewModels)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                viewModel.DisposeViewModelInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewModels{T}(Span{T})"/>
        public static void DisposeViewModels<T>(this ReadOnlySpan<T> viewModels)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                viewModel.DisposeViewModelInternal(ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewModels{T}(T[])"/>
        public static void DisposeViewModels<T>(this List<T> viewModels)
            where T : IViewModel
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            var failures = new CleanupExceptions();

            try
            {
                foreach (var viewModel in viewModels)
                    viewModel.DisposeViewModelInternal(ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DisposeViewModels{T}(T[])"/>
        public static void DisposeViewModels<T>(this IEnumerable<T> viewModels)
            where T : IViewModel
        {
            switch (viewModels)
            {
                case T[] array: array.DisposeViewModels(); return;
                case List<T> list: list.DisposeViewModels(); return;
                case null: throw new ArgumentNullException(nameof(viewModels));
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = viewModels.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        enumerator.Current!.DisposeViewModelInternal(ref failures);
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

        private static void DisposeViewModelInternal(this IViewModel viewModel, ref CleanupExceptions failures)
        {
            try
            {
                viewModel.DisposeViewModel();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
