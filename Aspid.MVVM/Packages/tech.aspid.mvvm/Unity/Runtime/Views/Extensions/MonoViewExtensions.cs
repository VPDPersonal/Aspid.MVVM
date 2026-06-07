#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for the <see cref="IView"/> interface.
    /// </summary>
    public static class MonoViewExtensions
    {
        /// <summary>
        /// Destroys the View component and deinitializes it if it does not implement the <see cref="IDisposable"/> interface.
        /// If the View implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <typeparam name="T">The type of View that inherits from <see cref="Component"/> and implements the <see cref="IView"/> interface.</typeparam>
        /// <param name="view">The instance of the View component to be destroyed.</param>
        /// <returns>The ViewModel that was bound to the View, or <c>null</c> if there was no ViewModel.</returns>
        public static IViewModel? DestroyView<T>(this T? view)
            where T : Component, IView
        {
            if (!view) return null;
            var viewModel = view.DisposeView();

            if (view)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    Object.DestroyImmediate(view);
                }
                else
#endif
                Object.Destroy(view);
            }

            return viewModel;
        }
        
        /// <summary>
        /// Destroys the View component's GameObject and deinitializes it if it does not implement the <see cref="IDisposable"/> interface.
        /// If the View implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <typeparam name="T">The type of View that inherits from <see cref="Component"/> and implements the <see cref="IView"/> interface.</typeparam>
        /// <param name="view">The instance of the View component to be destroyed.</param>
        /// <returns>The ViewModel that was bound to the View, or <c>null</c> if there was no ViewModel.</returns>
        public static IViewModel? DestroyViewAndGameObject<T>(this T? view)
            where T : Component, IView
        {
            if (!view) return null;

            var gameObject = view.gameObject;
            var viewModel = view.DisposeView();
            
            if (gameObject)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) Object.DestroyImmediate(gameObject);
                else
#endif
                Object.Destroy(gameObject);
            }

            return viewModel;
        }
        
        /// <summary>
        /// Destroys the View component and deinitializes it if it does not implement the <see cref="IDisposable"/> interface.
        /// If the View implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <param name="view">The instance of the View component to be destroyed.</param>
        /// <returns>The ViewModel that was bound to the View, or <c>null</c> if there was no ViewModel.</returns>
        public static IViewModel? DestroyView(this IView? view)
        {
            if (view is Component viewComponent)
            {
                if (!viewComponent) return null;
                
                var viewModel = view.DisposeView();
                
                if (viewComponent)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying) Object.DestroyImmediate(viewComponent);
                    else
#endif
                    Object.Destroy(viewComponent);
                }
                
                return viewModel;
            }
            
            return view.DisposeView();
        }
        
        /// <summary>
        /// Destroys the View component's GameObject and deinitializes it if it does not implement the <see cref="IDisposable"/> interface.
        /// If the View implements <see cref="IDisposable"/>, it calls the <see cref="IDisposable.Dispose"/> method.
        /// Returns the <see cref="IViewModel"/> instance that was bound to the View before its destruction.
        /// </summary>
        /// <param name="view">The instance of the View component to be destroyed.</param>
        /// <returns>The ViewModel that was bound to the View, or <c>null</c> if there was no ViewModel.</returns>
        public static IViewModel? DestroyViewAndGameObject(this IView? view)
        {
            if (view is Component viewComponent)
            {
                if (!viewComponent) return null;
                
                var gameObject = viewComponent.gameObject;
                var viewModel = view.DisposeView();
                
                if (gameObject)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying) Object.DestroyImmediate(gameObject);
                    else
#endif
                    Object.Destroy(gameObject);
                }
                
                return viewModel;
            }
            
            return view.DisposeView();
        }
    }
}