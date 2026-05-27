#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IView"/> providing helpers for reinitialization and safe disposal.
    /// </summary>
    public static class ViewExtensions
    {
#if PROFILER
        public static readonly Unity.Profiling.ProfilerMarker DisposeViewMarker = new(name: "DisposeView");
        
        private static class Markers<T>
            where T : IView, IDisposable
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewMarker = new(name: $"DisposeView<{typeof(T)}>");
        }
#endif
        
        /// <summary>
        /// Deinitializes the view from its current ViewModel and optionally reinitializes it with a new one.
        /// </summary>
        /// <param name="view">The view to reinitialize.</param>
        /// <param name="newViewModel">The new ViewModel to initialize the view with, or <see langword="null"/> to only deinitialize.</param>
        /// <returns>The previously associated <see cref="IViewModel"/>, or <see langword="null"/> if none was present.</returns>
        public static IViewModel? Reinitialize(this IView? view, IViewModel? newViewModel)
        {
            if (view is null) return null;
            var oldViewModel = view.DeinitializeView();

            if (newViewModel is not null)
                view.Initialize(newViewModel);

            return oldViewModel;
        }
        
        /// <summary>
        /// Deinitializes the view and returns the associated <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of the view that implements <see cref="IView"/>.</typeparam>
        /// <param name="view">The view to be deinitialized.</param>
        /// <returns>The associated <see cref="IViewModel"/>, or <see langword="null"/> if none is present.</returns>
        public static IViewModel? DeinitializeView<T>(this T? view)
            where T : IView
        {
            if (view is null) return null;
            
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }
        
        /// <summary>
        /// Disposes the view and returns the associated <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="view">The view to be disposed.</param>
        /// <typeparam name="T">The type of the view that implements <see cref="IView"/> and <see cref="IDisposable"/>.</typeparam>
        /// <returns>The associated <see cref="IViewModel"/>, or <see langword="null"/> if none is present.</returns>
        public static IViewModel? DisposeView<T>(this T? view)
            where T : IView, IDisposable
        {
#if PROFILER
            using (Markers<T>.DisposeViewMarker.Auto())
#endif
            {
                if (view is null) return null;

                var viewModel = view.ViewModel;
                view.Dispose();

                return viewModel;
            }
        }
        
        /// <summary>
        /// Disposes the view if it implements <see cref="IDisposable"/> and returns the associated <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="view">The view to be disposed.</param>
        /// <returns>The associated <see cref="IViewModel"/>, or <see langword="null"/> if none is present.</returns>
        public static IViewModel? DisposeView(this IView? view)
        {
#if PROFILER
            using (DisposeViewMarker.Auto())
#endif
            {
                if (view is null) return null;
                var viewModel = view.ViewModel;

                if (view is IDisposable disposable) disposable.Dispose();
                else view.Deinitialize();

                return viewModel;
            }
        }
    }
}
