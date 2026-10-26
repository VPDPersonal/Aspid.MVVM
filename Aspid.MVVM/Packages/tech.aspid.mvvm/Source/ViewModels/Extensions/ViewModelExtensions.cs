#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IViewModel"/> providing lifecycle helpers such as disposal.
    /// </summary>
    public static class ViewModelExtensions
    {
#if ASPID_MVVM_PROFILER
        public static readonly Unity.Profiling.ProfilerMarker DisposeViewModelMarker = new(name: "DisposeViewModel");
        
        private static class Markers<T>
            where T : class, IViewModel, IDisposable
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewModelMarker = new(name: $"DisposeViewModel<{typeof(T)}>");
        }
#endif
        
        /// <summary>
        /// Disposes the ViewModel instance and returns the disposal marker.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        /// <typeparam name="T">The type of the ViewModel that implements <see cref="IViewModel"/> and <see cref="IDisposable"/>.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
#if ASPID_MVVM_PROFILER
            using (Markers<T>.DisposeViewModelMarker.Auto())
#endif
            {
                viewModel.Dispose();
            }
        }
        
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel(this IViewModel viewModel)
        {
#if ASPID_MVVM_PROFILER
            using (DisposeViewModelMarker.Auto())
#endif
            {
                if (viewModel is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
