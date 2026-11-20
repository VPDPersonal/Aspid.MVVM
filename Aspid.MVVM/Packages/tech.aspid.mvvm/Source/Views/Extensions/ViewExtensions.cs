using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IView"/>.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Deinitializes <paramref name="view"/> and initializes it with <paramref name="newViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Does not dispose either ViewModel. If initialization fails, recovery follows the View implementation's contract.
        /// </remarks>
        /// <param name="view">The view to operate on.</param>
        /// <param name="newViewModel">The ViewModel to initialize the view with; A missing or destroyed ViewModel leaves it deinitialized.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or a destroyed Unity object.
        /// </exception>
        public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : view.ReinitializeInternal(newViewModel);

        /// <summary>
        /// Deinitializes the View and initializes it with <paramref name="newViewModel"/>, logging any exception.
        /// </summary>
        /// <param name="view">The View to reinitialize. Skips <see langword="null"/> and destroyed Unity objects.</param>
        /// <param name="newViewModel">The replacement ViewModel, A missing or destroyed ViewModel only deinitializes the View.</param>
        /// <returns>The previous ViewModel on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static IViewModel? ReinitializeSafely(this IView? view, IViewModel? newViewModel)
        {
            if (view.IsMissing()) return null;

            try
            {
                return view.ReinitializeInternal(newViewModel);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }
        }

        /// <summary>
        /// Deinitializes <paramref name="view"/>.
        /// </summary>
        /// <param name="view">The view to operate on.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or a destroyed Unity object.
        /// </exception>
        public static IViewModel? DeinitializeView(this IView view) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : view.DeinitializeViewInternal();

        /// <summary>
        /// Deinitializes the View, logging any exception.
        /// </summary>
        /// <param name="view">The View to deinitialize. Skips <see langword="null"/> and destroyed Unity objects.</param>
        /// <returns>The previous ViewModel on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static IViewModel? DeinitializeViewSafely(this IView? view)
        {
            if (view.IsMissing()) return null;

            try
            {
                return view.DeinitializeViewInternal();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }
        }

        /// <summary>
        /// Disposes <paramref name="view"/>.
        /// </summary>
        /// <remarks>Does not dispose the returned ViewModel.</remarks>
        /// <typeparam name="T">The view type.</typeparam>
        /// <param name="view">The view to operate on.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or a destroyed Unity object.
        /// </exception>
        public static IViewModel? DisposeView<T>(this T view)
            where T : class, IView, IDisposable
        {
            return view.IsMissing()
                ? throw new ArgumentNullException(nameof(view))
                : view.DisposeViewInternal();
        }

        /// <summary>
        /// Disposes the View, logging any exception.
        /// </summary>
        /// <typeparam name="T">The disposable View type.</typeparam>
        /// <param name="view">The View to dispose. Skips <see langword="null"/> and destroyed Unity objects.</param>
        /// <returns>The previous ViewModel on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static IViewModel? DisposeViewSafely<T>(this T? view)
            where T : class, IView, IDisposable
        {
            if (view.IsMissing()) return null;

            try
            {
                return view.DisposeViewInternal();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }
        }

        /// <summary>
        /// Disposes <paramref name="view"/> if it implements <see cref="IDisposable"/>; otherwise, deinitializes it.
        /// </summary>
        /// <remarks>Does not dispose the returned ViewModel.</remarks>
        /// <param name="view">The view to operate on.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or a destroyed Unity object.
        /// </exception>
        public static IViewModel? DisposeView(this IView view) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : view.DisposeViewInternal();

        /// <summary>
        /// Disposes a disposable View or deinitializes it otherwise, logging any exception.
        /// </summary>
        /// <param name="view">The View to clean up. Skips <see langword="null"/> and destroyed Unity objects.</param>
        /// <returns>The previous ViewModel on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static IViewModel? DisposeViewSafely(this IView? view)
        {
            if (view.IsMissing()) return null;

            try
            {
                return view.DisposeViewInternal();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }
        }

        private static IViewModel? ReinitializeInternal(this IView view, IViewModel? newViewModel)
        {
            var oldViewModel = view.DeinitializeViewInternal();

            if (!newViewModel.IsMissing())
                view.Initialize(newViewModel);

            return oldViewModel;
        }

        private static IViewModel? DeinitializeViewInternal(this IView view)
        {
            var viewModel = view.ViewModel;
            view.Deinitialize();

            return viewModel;
        }

        private static IViewModel? DisposeViewInternal(this IView view)
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable)
                disposable.Dispose();
            else view.Deinitialize();

            return viewModel;
        }

        private static IViewModel? DisposeViewInternal<T>(this T view)
            where T : class, IView, IDisposable
        {
            var viewModel = view.ViewModel;
            view.Dispose();

            return viewModel;
        }
    }
}
