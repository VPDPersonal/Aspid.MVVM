using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IViewModel"/>.
    /// </summary>
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Initializes <paramref name="view"/> with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="view">The View to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> or <paramref name="view"/> is null or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Both arguments are null or destroyed.</exception>
        public static void InitializeView(this IViewModel viewModel, IView view)
        {
            ValidateInitializationArguments(viewModel, view, nameof(view));
            view.Initialize(viewModel);
        }

        /// <summary>
        /// Initializes the View and logs any exception.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="view">The View to initialize. Skips a missing or destroyed View.</param>
        public static void InitializeViewSafely(this IViewModel? viewModel, IView? view)
        {
            if (viewModel.IsMissing()) return;
            InitializeViewSafelyInternal(viewModel, view);
        }

        /// <summary>
        /// Initializes each View with <paramref name="viewModel"/> in collection order.
        /// </summary>
        /// <remarks>Stops at the first exception; previously initialized Views remain initialized.</remarks>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="views">The Views to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// The ViewModel, collection, or a View is null or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Both arguments are null or destroyed.</exception>
        public static void InitializeViews(this IViewModel viewModel, params IView[] views)
        {
            ValidateInitializationArguments(viewModel, views, nameof(views));

            foreach (var view in views)
            {
                if (view.IsMissing())
                    throw new ArgumentNullException(nameof(view));

                view.Initialize(viewModel);
            }
        }

        /// <summary>
        /// Initializes the Views in order, logging failures and continuing with the next View.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="views">The Views to initialize. Skips a null collection and missing or destroyed Views.</param>
        public static void InitializeViewsSafely(this IViewModel? viewModel, params IView?[]? views)
        {
            if (views is null || viewModel.IsMissing()) return;

            foreach (var view in views)
                InitializeViewSafelyInternal(viewModel, view);
        }

        /// <summary>
        /// Initializes each View with <paramref name="viewModel"/> in collection order.
        /// </summary>
        /// <remarks>Stops at the first exception; previously initialized Views remain initialized.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="views">The Views to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// The ViewModel, collection, or a View is null or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Both arguments are null or destroyed.</exception>
        public static void InitializeViews<T>(this IViewModel viewModel, List<T> views)
            where T : IView
        {
            ValidateInitializationArguments(viewModel, views, nameof(views));

            foreach (var view in views)
            {
                if (view.IsMissing())
                    throw new ArgumentNullException(nameof(view));

                view.Initialize(viewModel);
            }
        }

        /// <summary>
        /// Initializes the Views in order, logging failures and continuing with the next View.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="views">The Views to initialize. Skips a null collection and missing or destroyed Views.</param>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, List<T>? views)
            where T : IView?
        {
            if (views is null || viewModel.IsMissing()) return;

            try
            {
                foreach (var view in views)
                    InitializeViewSafelyInternal(viewModel, view);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        /// <summary>
        /// Initializes each View with <paramref name="viewModel"/> in collection order.
        /// </summary>
        /// <remarks>Stops at the first exception; previously initialized Views remain initialized.</remarks>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="views">The Views to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// The ViewModel, collection, or a View is null or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Both arguments are null or destroyed.</exception>
        public static void InitializeViews(this IViewModel viewModel, IEnumerable<IView> views)
        {
            ValidateInitializationArguments(viewModel, views, nameof(views));

            foreach (var view in views)
            {
                if (view.IsMissing())
                    throw new ArgumentNullException(nameof(view));

                view.Initialize(viewModel);
            }
        }

        /// <summary>
        /// Initializes the Views in order, logging failures and continuing with the next View.
        /// </summary>
        /// <remarks>An enumeration failure is logged and ends the batch.</remarks>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="views">The Views to initialize. Skips a null collection and missing or destroyed Views.</param>
        public static void InitializeViewsSafely(this IViewModel? viewModel, IEnumerable<IView?>? views)
        {
            if (views is null || viewModel.IsMissing()) return;

            try
            {
                foreach (var view in views)
                    InitializeViewSafelyInternal(viewModel, view);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        /// <summary>
        /// Disposes <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> is <see langword="null"/> or a destroyed Unity object.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            if (viewModel.IsMissing())
                throw new ArgumentNullException(nameof(viewModel));

            viewModel.Dispose();
        }

        /// <summary>
        /// Disposes the ViewModel and logs any exception.
        /// </summary>
        /// <typeparam name="T">The disposable ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose. Skips <see langword="null"/> and destroyed Unity objects.</param>
        public static void DisposeViewModelSafely<T>(this T? viewModel)
            where T : class, IViewModel, IDisposable
        {
            if (viewModel.IsMissing()) return;

            try
            {
                viewModel.Dispose();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        /// <summary>
        /// Disposes <paramref name="viewModel"/> if it implements <see cref="IDisposable"/>; otherwise, does nothing.
        /// </summary>
        /// <param name="viewModel">The ViewModel to dispose.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> is <see langword="null"/> or a destroyed Unity object.
        /// </exception>
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel.IsMissing())
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }

        /// <summary>
        /// Disposes a disposable ViewModel and logs any exception.
        /// </summary>
        /// <param name="viewModel">The ViewModel to dispose. Skips null and destroyed Unity objects.</param>
        public static void DisposeViewModelSafely(this IViewModel? viewModel)
        {
            if (viewModel.IsMissing()) return;

            try
            {
                if (viewModel is IDisposable disposable)
                    disposable.Dispose();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private static void InitializeViewSafelyInternal<T>(IViewModel viewModel, T view)
            where T : IView?
        {
            if (view.IsMissing()) return;

            try
            {
                view.Initialize(viewModel);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private static void ValidateInitializationArguments(IViewModel? viewModel, object? view, string viewName)
        {
            var missingTarget = view.IsMissing();
            var missingViewModel = viewModel.IsMissing();

            if (missingViewModel && missingTarget)
            {
                throw new AggregateException(
                    new ArgumentNullException(nameof(viewModel)), new ArgumentNullException(viewName));
            };

            if (missingViewModel)
                throw new ArgumentNullException(nameof(viewModel));
            if (missingTarget)
                throw new ArgumentNullException(viewName);
        }
    }
}
