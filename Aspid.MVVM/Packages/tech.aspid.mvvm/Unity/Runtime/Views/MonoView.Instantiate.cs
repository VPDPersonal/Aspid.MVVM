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
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent for the clone; <see langword="null"/> creates it at the root.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Transform parent, IViewModel viewModel)
            where T : Object, IView
        {
            return Instantiate(original, parent, false, viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent for the clone; <see langword="null"/> creates it at the root.</param>
        /// <param name="worldPositionStays">When <see langword="true"/>, retains the original world position.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, parent, worldPositionStays), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, position, rotation), viewModel);
        }

        /// <summary>
        /// Clones and initializes the view.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="parent">The parent for the clone; <see langword="null"/> creates it at the root.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The initialized clone.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, position, rotation, parent), viewModel);
        }
    }
}
