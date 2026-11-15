using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public partial class MonoView
    {
        /// <summary>
        /// Creates an instance of the View and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null 
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original), viewModel);
        }

        /// <summary>
        /// Creates an instance of the View with the specified parent and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="parent">The parent for the clone; <see langword="null"/> creates it at the root.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Transform parent, IViewModel viewModel)
            where T : Object, IView
        {
            return Instantiate(original, parent, false, viewModel);
        }

        /// <summary>
        /// Creates an instance of the View with the specified parent and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="parent">The parent for the clone; <see langword="null"/> creates it at the root.</param>
        /// <param name="worldPositionStays">When <see langword="true"/>, retains the original world position.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, parent, worldPositionStays), viewModel);
        }

        /// <summary>
        /// Creates an instance of the View with the specified position and rotation, and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel is null 
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeInstance(Object.Instantiate(original, position, rotation), viewModel);
        }

        /// <summary>
        /// Creates an instance of the View with the specified position, rotation, and parent, and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
        /// Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.
        /// </remarks>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="parent">The parent for the clone; <see langword="null"/> creates it at the root.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
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
