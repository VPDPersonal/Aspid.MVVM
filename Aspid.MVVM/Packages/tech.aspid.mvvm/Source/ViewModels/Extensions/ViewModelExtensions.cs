#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using System.Runtime.CompilerServices;

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
       
#if ASPID_MVVM_PROFILER
        private static class Markers<T>
            where T : class, IViewModel, IDisposable
        {
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewModelMarker = new(name: $"DisposeViewModel<{typeof(T)}>");
        }
#endif
    }
}
