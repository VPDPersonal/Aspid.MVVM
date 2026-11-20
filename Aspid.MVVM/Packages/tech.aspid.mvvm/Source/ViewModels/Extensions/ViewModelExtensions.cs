#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IViewModel"/>.
    /// </summary>
    public static class ViewModelExtensions
    {
#if ASPID_MVVM_PROFILER
        private static readonly Unity.Profiling.ProfilerMarker _disposeViewModelMarker = new(name: "DisposeViewModel");
#endif

        /// <summary>
        /// Initializes <paramref name="view"/> with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="view">The View to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> or <paramref name="view"/> is <see langword="null"/>.
        /// </exception>
        public static void InitializeView(this IViewModel viewModel, IView view)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));
            if (view is null)
                throw new ArgumentNullException(nameof(view));

            view.Initialize(viewModel);
        }

        /// <summary>
        /// Initializes the View and logs any exception.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="view">The View to initialize. Skips a missing or destroyed View.</param>
        public static void InitializeViewSafely(this IViewModel? viewModel, IView? view)
        {
            if (viewModel is null || IsDestroyed(viewModel)) return;

            InitializeViewSafelyCore(viewModel, view);
        }

        /// <summary>
        /// Initializes each View with <paramref name="viewModel"/> in collection order.
        /// </summary>
        /// <remarks>Stops at the first exception; previously initialized Views remain initialized.</remarks>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="views">The Views to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/>, <paramref name="views"/>, or an element is <see langword="null"/>.
        /// </exception>
        public static void InitializeViews(this IViewModel viewModel, params IView[] views)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
            {
                if (view is null)
                    throw new ArgumentNullException(nameof(view));

                view.Initialize(viewModel);
            }
        }

        /// <summary>
        /// Initializes each View with <paramref name="viewModel"/> in collection order.
        /// </summary>
        /// <remarks>Stops at the first exception; previously initialized Views remain initialized.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="views">The Views to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/>, <paramref name="views"/>, or an element is <see langword="null"/>.
        /// </exception>
        public static void InitializeViews<T>(this IViewModel viewModel, List<T> views)
            where T : IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
            {
                if (view is null)
                    throw new ArgumentNullException(nameof(view));

                view.Initialize(viewModel);
            }
        }

        /// <summary>
        /// Initializes each View with <paramref name="viewModel"/> in collection order.
        /// </summary>
        /// <remarks>Stops at the first exception; previously initialized Views remain initialized.</remarks>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="views">The Views to initialize.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/>, <paramref name="views"/>, or an element is <see langword="null"/>.
        /// </exception>
        public static void InitializeViews(this IViewModel viewModel, IEnumerable<IView> views)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            foreach (var view in views)
            {
                if (view is null)
                    throw new ArgumentNullException(nameof(view));

                view.Initialize(viewModel);
            }
        }

        /// <summary>
        /// Initializes the Views in order, logging failures and continuing with the next View.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="views">The Views to initialize. Skips a null collection and missing or destroyed Views.</param>
        public static void InitializeViewsSafely(this IViewModel? viewModel, params IView?[]? views) =>
            InitializeViewsSafelyCore(viewModel, views);

        /// <summary>
        /// Initializes the Views in order, logging failures and continuing with the next View.
        /// </summary>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="views">The Views to initialize. Skips a null collection and missing or destroyed Views.</param>
        public static void InitializeViewsSafely<T>(this IViewModel? viewModel, List<T>? views)
            where T : IView? =>
            InitializeViewsSafelyCore(viewModel, views);

        /// <summary>
        /// Initializes the Views in order, logging failures and continuing with the next View.
        /// </summary>
        /// <remarks>An enumeration failure is logged and ends the batch.</remarks>
        /// <param name="viewModel">The ViewModel to bind. Skips a missing or destroyed ViewModel.</param>
        /// <param name="views">The Views to initialize. Skips a null collection and missing or destroyed Views.</param>
        public static void InitializeViewsSafely(this IViewModel? viewModel, IEnumerable<IView?>? views) =>
            InitializeViewsSafelyCore(viewModel, views);

        /// <summary>
        /// Disposes <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose.</param>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

#if ASPID_MVVM_PROFILER
            using (Markers<T>.DisposeViewModelMarker.Auto())
#endif
            {
                viewModel.Dispose();
            }
        }

        /// <summary>
        /// Disposes <paramref name="viewModel"/> if it implements <see cref="IDisposable"/>; otherwise, does nothing.
        /// </summary>
        /// <param name="viewModel">The ViewModel to dispose.</param>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

#if ASPID_MVVM_PROFILER
            using (_disposeViewModelMarker.Auto())
#endif
            {
                if (viewModel is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        /// <summary>
        /// Disposes the ViewModel and logs any exception.
        /// </summary>
        /// <typeparam name="T">The disposable ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose. Skips <see langword="null"/> and destroyed Unity objects.</param>
        public static void DisposeViewModelSafely<T>(this T? viewModel)
            where T : class, IViewModel, IDisposable
        {
            if (viewModel is null || IsDestroyed(viewModel)) return;

            try
            {
                viewModel.DisposeViewModel();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        /// <summary>
        /// Disposes a disposable ViewModel and logs any exception.
        /// </summary>
        /// <param name="viewModel">The ViewModel to dispose. Skips <see langword="null"/> and destroyed Unity objects.</param>
        public static void DisposeViewModelSafely(this IViewModel? viewModel)
        {
            if (viewModel is null || IsDestroyed(viewModel)) return;

            try
            {
                viewModel.DisposeViewModel();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private static void InitializeViewsSafelyCore<T>(IViewModel? viewModel, IEnumerable<T>? views)
            where T : IView?
        {
            if (views is null || viewModel is null || IsDestroyed(viewModel)) return;

            try
            {
                foreach (var view in views)
                    InitializeViewSafelyCore(viewModel, view);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private static void InitializeViewSafelyCore<T>(IViewModel viewModel, T view)
            where T : IView?
        {
            if (view is null || view is Object unityObject && !unityObject) return;

            try
            {
                view.Initialize(viewModel);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private static bool IsDestroyed(IViewModel viewModel) =>
            viewModel is Object unityObject && !unityObject;

#if ASPID_MVVM_PROFILER
        private static class Markers<T>
            where T : class, IViewModel, IDisposable
        {
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewModelMarker = new(name: $"DisposeViewModel<{typeof(T)}>");
        }
#endif
    }
}
