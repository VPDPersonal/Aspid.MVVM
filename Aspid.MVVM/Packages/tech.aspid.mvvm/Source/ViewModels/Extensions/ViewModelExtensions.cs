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
        public static readonly Unity.Profiling.ProfilerMarker DisposeViewModelMarker = new(name: "DisposeViewModel");
        
        private static class Markers<T>
            where T : class, IViewModel, IDisposable
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly Unity.Profiling.ProfilerMarker DisposeViewModelMarker = new(name: $"DisposeViewModel<{typeof(T)}>");
        }
#endif
        
        /// <summary>
        /// Disposes <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose.</param>
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
        /// Disposes <paramref name="viewModel"/> if it implements <see cref="IDisposable"/>; otherwise, does nothing.
        /// </summary>
        /// <param name="viewModel">The ViewModel to dispose.</param>
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
