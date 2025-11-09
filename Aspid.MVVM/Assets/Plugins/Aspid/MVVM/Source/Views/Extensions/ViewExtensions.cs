using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class ViewExtensions
    {
        public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel)
        { 
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
        /// <returns>The associated <see cref="IViewModel"/>, or <c>null</c> if none is present.</returns>
        public static IViewModel? DeinitializeView<T>(this T view)
            where T : IView
        {
            var viewModel = view.ViewModel;
            view.Deinitialize();
            
            return viewModel;
        }
        
        /// <summary>
        /// Disposes the view if it implements <see cref="IDisposable"/> and returns the associated <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="view">The view to be disposed.</param>
        /// <typeparam name="T">The type of the view that implements <see cref="IView"/> and <see cref="IDisposable"/>.</typeparam>
        /// <returns>The associated <see cref="IViewModel"/>, or <c>null</c> if none is present.</returns>
        public static IViewModel? DisposeView<T>(this T view)
            where T : IView, IDisposable
        {
            var viewModel = view.ViewModel;
            view.Dispose();

            return viewModel;
        }
        
        /// <summary>
        /// Disposes the view if it implements <see cref="IDisposable"/> and returns the associated <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="view">The view to be disposed.</param>
        /// <returns>The associated <see cref="IViewModel"/>, or <c>null</c> if none is present.</returns>
        public static IViewModel? DisposeView(this IView view)
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable) disposable.Dispose();
            else view.Deinitialize();

            return viewModel;
        }
    }
}