using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Mono
{
    /// <summary>
    /// Provides extension methods for the <see cref="IViewModel"/> interface.
    /// </summary>
    public static class MonoViewModelExtensions
    {
        /// <summary>
        /// Destroys the ViewModel component if it does not implement the <see cref="IDisposable"/> interface.
        /// If the ViewModel implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <typeparam name="T">The type of ViewModel that inherits from <see cref="Component"/> and implements the <see cref="IViewModel"/> interface.</typeparam>
        /// <param name="viewModel">The instance of the ViewModel to be destroyed.</param>
        /// <returns>The ViewModel <see cref="GameObject"/>, or <c>null</c> if it is destroyed or ViewModel is not a <see cref="Component"/>.</returns>
        public static GameObject? DestroyViewModel<T>(this T viewModel)
            where T : Component, IViewModel
        {
            return DestroyViewModel(viewModel as Component);
        }

        /// <summary>
        /// Destroys the ViewModel component if it does not implement the <see cref="IDisposable"/> interface.
        /// If the ViewModel implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <param name="viewModel">The instance of the ViewModel to be destroyed.</param>
        /// <returns>The ViewModel <see cref="GameObject"/>, or <c>null</c> if it is destroyed or ViewModel is not a <see cref="Component"/>.</returns>
        public static GameObject? DestroyViewModel(this IViewModel viewModel)
        {
            switch (viewModel)
            {
                case IDisposable disposable: disposable.Dispose(); break;
                case Component component: return DestroyViewModel(component);
            }

            return null;
        }

        private static GameObject? DestroyViewModel(Component component)
        {
            // There is a possibility that ViewModel is on the same object as MonoView.
            // And if MonoView is destroyed, ViewModel is also destroyed.
            if (!component) return null;
            
            var gameObject = component.gameObject;
            
            if (component is IDisposable disposable)
            {
                disposable.Dispose();
                
                // There is no guarantee that the GameObject is not destroyed by Dispose
                return gameObject ? gameObject : null;
            }

            Object.Destroy(component);
            return gameObject;
        }
    }
}