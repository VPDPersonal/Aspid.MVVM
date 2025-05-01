using UnityEngine;

namespace Aspid.MVVM.Unity
{
    public abstract partial class MonoView
    {
        /// <summary>
        /// Creates an instance of the View and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        public static T Instantiate<T>(
            T original, 
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original); 
            view.Initialize(viewModel);

            return view;
        }

        /// <summary>
        /// Creates an instance of the View with the specified parent and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="parent">The parent object for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        public static T Instantiate<T>(
            T original, 
            Transform parent,
            IViewModel viewModel) 
            where T : Object, IView
        {
            return Instantiate(original, parent, false, viewModel);
        }
        
        /// <summary>
        /// Creates an instance of the View with the specified parent and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="parent">The parent object for the new View instance.</param>
        /// <param name="worldPositionStays">Indicates whether the new instance should retain its world position.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        public static T Instantiate<T>(
            T original,
            Transform parent,
            bool worldPositionStays,
            IViewModel viewModel) 
            where T : Object, IView
        {
            var view = Object.Instantiate(original, parent, worldPositionStays);
            view.Initialize(viewModel);

            return view;
        }

        /// <summary>
        /// Creates an instance of the View with the specified position and rotation, and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        public static T Instantiate<T>(
            T original,
            Vector3 position,
            Quaternion rotation, 
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original, position, rotation);
            view.Initialize(viewModel);

            return view;
        }
        
        /// <summary>
        /// Creates an instance of the View with the specified position, rotation, and parent, and initializes it with the given <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of the View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="parent">The parent object for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>The created instance of the View.</returns>
        public static T Instantiate<T>(
            T original,
            Vector3 position, 
            Quaternion rotation,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var view = Object.Instantiate(original, position, rotation, parent);
            view.Initialize(viewModel);

            return view;
        }
    }
}