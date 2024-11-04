using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.Mono.Views.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IView"/> interface.
    /// </summary>
    public static class MonoViewExtensions
    {
        /// <summary>
        /// Destroys the View and deinitializes it if it does not implement the <see cref="IDisposable"/> interface.
        /// If the View implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <typeparam name="T">The type of View that inherits from <see cref="Component"/> and implements the <see cref="IView"/> interface.</typeparam>
        /// <param name="view">The instance of the View to be destroyed.</param>
        /// <returns>The ViewModel that was bound to the View, or <c>null</c> if there was no ViewModel.</returns>
        public static IViewModel? DestroyView<T>(this T view)
            where T : Component, IView
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable)
            {
                disposable.Dispose();
            }
            else
            {
                view.Deinitialize();
                Object.Destroy(view.gameObject);
            }

            return viewModel;
        }
        
        /// <summary>
        /// Destroys the View and deinitializes it if it does not implement the <see cref="IDisposable"/> interface.
        /// If the View implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <param name="view">The instance of the View to be destroyed.</param>
        /// <returns>The ViewModel that was bound to the View, or <c>null</c> if there was no ViewModel.</returns>
        public static IViewModel? DestroyView(this IView view)
        {
            var viewModel = view.ViewModel;

            if (view is IDisposable disposable)
            {
                disposable.Dispose();
            }
            else
            {
                view.Deinitialize();
                if (view is Component component) Object.Destroy(component.gameObject);
            }

            return viewModel;
        }
    }
}