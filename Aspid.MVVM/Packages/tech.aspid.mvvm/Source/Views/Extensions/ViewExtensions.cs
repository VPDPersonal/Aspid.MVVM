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
        public static readonly Unity.Profiling.ProfilerMarker DisposeViewMarker = new(name: "DisposeView");
        
        private static class Markers<T>
            where T : IView, IDisposable
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewMarker = new(name: $"DisposeView<{typeof(T)}>");
        }
#endif
        
        /// <summary>
        /// Deinitializes <paramref name="view"/> and initializes it with <paramref name="newViewModel"/>.
        /// </summary>
        /// <param name="view">The view, or <see langword="null"/>.</param>
        /// <param name="newViewModel">The ViewModel to initialize the view with; <see langword="null"/> leaves it deinitialized.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none or <paramref name="view"/> is <see langword="null"/>.</returns>
        public static IViewModel? Reinitialize(this IView? view, IViewModel? newViewModel)
        {
            if (view is null) return null;
            var oldViewModel = view.DeinitializeView();

            if (newViewModel is not null)
                view.Initialize(newViewModel);

            return oldViewModel;
        }
        
        /// <summary>
        /// Deinitializes <paramref name="view"/>.
        /// </summary>
        /// <typeparam name="T">The view type.</typeparam>
        /// <param name="view">The view, or <see langword="null"/>.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none or <paramref name="view"/> is <see langword="null"/>.</returns>
        public static IViewModel? DeinitializeView<T>(this T? view)
            where T : IView
        {
            if (view is null) return null;
            
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }
        
        /// <summary>
        /// Disposes <paramref name="view"/>.
        /// </summary>
        /// <typeparam name="T">The view type.</typeparam>
        /// <param name="view">The view, or <see langword="null"/>.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none or <paramref name="view"/> is <see langword="null"/>.</returns>
        public static IViewModel? DisposeView<T>(this T? view)
            where T : IView, IDisposable
        {
#if ASPID_MVVM_PROFILER
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
        /// Disposes <paramref name="view"/> if it implements <see cref="IDisposable"/>; otherwise, deinitializes it.
        /// </summary>
        /// <param name="view">The view, or <see langword="null"/>.</param>
        /// <returns>The ViewModel the view had; <see langword="null"/> when it had none or <paramref name="view"/> is <see langword="null"/>.</returns>
        public static IViewModel? DisposeView(this IView? view)
        {
#if ASPID_MVVM_PROFILER
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
