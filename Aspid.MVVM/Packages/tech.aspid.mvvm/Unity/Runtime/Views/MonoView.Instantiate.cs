using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public partial class MonoView
    {
        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeInstance(Object.Instantiate(original), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view using its local transform under the parent.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent for the clone, or <see langword="null"/> for the scene root.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Transform parent, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, parent), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent for the clone, or <see langword="null"/> for the scene root.</param>
        /// <param name="worldPositionStays">When <see langword="true"/>, keeps the original world position and rotation.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, parent, worldPositionStays), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>.</exception>
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeInstance(Object.Instantiate(original, position, rotation), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="parent">The parent for the clone, or <see langword="null"/> for the scene root.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/>, or it is a <see cref="ScriptableObject"/> and <paramref name="parent"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeInstance(Object.Instantiate(original, position, rotation, parent), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="parameters">The Unity cloning options.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(
            T original,
            IViewModel viewModel,
            InstantiateParameters parameters)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeInstance(Object.Instantiate(original, parameters), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>Failed initialization destroys the clone without disposing its ViewModel.</remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The position in the configured coordinate space.</param>
        /// <param name="rotation">The rotation in the configured coordinate space.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <param name="parameters">The Unity cloning options.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel,
            InstantiateParameters parameters)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeInstance(Object.Instantiate(original, position, rotation, parameters), viewModel);
        }
    }
}
