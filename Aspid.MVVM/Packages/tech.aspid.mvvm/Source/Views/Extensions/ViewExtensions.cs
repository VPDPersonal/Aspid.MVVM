#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IView"/>.
    /// </summary>
    public static class ViewExtensions
    {
#if ASPID_MVVM_PROFILER
        private static readonly Unity.Profiling.ProfilerMarker _disposeViewMarker = new(name: "DisposeView");
#endif
        
        /// <summary>
        /// Deinitializes <paramref name="view"/> and initializes it with <paramref name="newViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Does not dispose either ViewModel. If initialization fails, recovery follows the View implementation's contract.
        /// </remarks>
        /// <param name="view">The view to operate on.</param>
        /// <param name="newViewModel">The ViewModel to initialize the view with; <see langword="null"/> leaves it deinitialized.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="view"/> is <see langword="null"/>.</exception>
        public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel)
        {
            if (view is null)
                throw new ArgumentNullException(nameof(view));

            var oldViewModel = view.DeinitializeView();

            if (newViewModel is not null)
                view.Initialize(newViewModel);

            return oldViewModel;
        }
        
        /// <summary>
        /// Deinitializes <paramref name="view"/>.
        /// </summary>
        /// <typeparam name="T">The view type.</typeparam>
        /// <param name="view">The view to operate on.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="view"/> is <see langword="null"/>.</exception>
        public static IViewModel? DeinitializeView<T>(this T view)
            where T : class, IView
        {
            if (view is null)
                throw new ArgumentNullException(nameof(view));
            
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }
        
        /// <summary>
        /// Disposes <paramref name="view"/>.
        /// </summary>
        /// <remarks>Does not dispose the returned ViewModel.</remarks>
        /// <typeparam name="T">The view type.</typeparam>
        /// <param name="view">The view to operate on.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="view"/> is <see langword="null"/>.</exception>
        public static IViewModel? DisposeView<T>(this T view)
            where T : class, IView, IDisposable
        {
            if (view is null)
                throw new ArgumentNullException(nameof(view));

#if ASPID_MVVM_PROFILER
            using (Markers<T>.DisposeViewMarker.Auto())
#endif
            {
                var viewModel = view.ViewModel;
                view.Dispose();

                return viewModel;
            }
        }
        
        /// <summary>
        /// Disposes <paramref name="view"/> if it implements <see cref="IDisposable"/>; otherwise, deinitializes it.
        /// </summary>
        /// <remarks>Does not dispose the returned ViewModel.</remarks>
        /// <param name="view">The view to operate on.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="view"/> is <see langword="null"/>.</exception>
        public static IViewModel? DisposeView(this IView view)
        {
            if (view is null)
                throw new ArgumentNullException(nameof(view));

#if ASPID_MVVM_PROFILER
            using (_disposeViewMarker.Auto())
#endif
            {
                var viewModel = view.ViewModel;

                if (view is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                else
                {
                    view.Deinitialize();
                }

                return viewModel;
            }
        }

#if ASPID_MVVM_PROFILER
        private static class Markers<T>
            where T : class, IView, IDisposable
        {
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewMarker = new(name: $"DisposeView<{typeof(T)}>");
        }
#endif
    }
}
