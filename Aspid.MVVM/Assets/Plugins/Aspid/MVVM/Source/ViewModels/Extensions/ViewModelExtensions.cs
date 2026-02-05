using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        /// <typeparam name="T">The type of the ViewModel that implements <see cref="IViewModel"/> and <see cref="IDisposable"/>.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            viewModel.Dispose();
        }
        
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }
    }
}