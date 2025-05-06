#if UNITY_2022_3_OR_NEWER
using System;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_2023_1_OR_NEWER
using System.Threading;
#endif // UNITY_2023_1_OR_NEWER

namespace Aspid.MVVM.Unity
{
    public abstract partial class MonoView
    {
        /// <summary>
        /// Asynchronously creates an instance of the View and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instance.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        /// <summary>
        /// Asynchronously creates an instance of the View with the specified parent
        /// and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="parent">The parent object for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instance.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, parent);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }
        
        /// <summary>
        /// Asynchronously creates an instance of the View with specified position and rotation,
        /// and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instance.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, position, rotation);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }

        /// <summary>
        /// Asynchronously creates an instance of the View with specified parent, position, and rotation,
        /// and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="parent">The parent object for the new View instance.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instance.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, parent, position, rotation);
            operation.completed += _ => operation.Result[0].Initialize(viewModel);
            return operation;
        }


        /// <summary>
        /// Asynchronously creates multiple instances of the View and initializes them with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            IViewModel viewModel) 
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }


        /// <summary>
        /// Asynchronously creates multiple instances of the View with the specified parent
        /// and initializes them with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent object for the new View instances.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Asynchronously creates multiple instances of the View with specified position and rotation,
        /// and initializes them with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="position">The position for the new View instances.</param>
        /// <param name="rotation">The rotation for the new View instances.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, position, rotation);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

        /// <summary>
        /// Asynchronously creates multiple View instances with specified positions and rotations,
        /// and initializes them with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="positions">An array of positions for the new View instances.</param>
        /// <param name="rotations">An array of rotations for the new View instances.</param>
        /// <param name="viewModel">The ViewModel for the View.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, positions, rotations);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }


        /// <summary>
        /// Asynchronously creates an instance of the View with specified parent, position, and rotation,
        /// and initializes it with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent object for the new View instance.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instance.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, position, rotation);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }


        /// <summary>
        /// Asynchronously creates multiple View instances with the specified parent
        /// and initializes them with the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <typeparam name="T">The type of View, which must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object to be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent object for the new View instances.</param>
        /// <param name="positions">An array of positions for the new View instances.</param>
        /// <param name="rotations">An array of rotations for the new View instances.</param>
        /// <param name="viewModel">The ViewModel for the View.</param>
        /// <returns>An operation representing the asynchronous process of creating the View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }

#if UNITY_2023_1_OR_NEWER
        /// <summary>
        /// Asynchronously creates multiple instances of View with the specified parent, position, and rotation, 
        /// and initializes them with the given <see cref="IViewModel"/>. Supports cancellation via <see cref="CancellationToken"/>.
        /// </summary>
        /// <typeparam name="T">The type of View that must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object that will be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent object for the new View instances.</param>
        /// <param name="position">The position for the new View instances.</param>
        /// <param name="rotation">The rotation for the new View instances.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <param name="cancellationToken">The cancellation token for stopping the creation process.</param>
        /// <returns>An operation representing the asynchronous process of creating View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel,
            CancellationToken cancellationToken)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, position, rotation, cancellationToken);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }
        
        /// <summary>
        /// Asynchronously creates multiple instances of View with the specified parent, positions, and rotations, 
        /// and initializes them with the given <see cref="IViewModel"/>. Supports cancellation via <see cref="CancellationToken"/>.
        /// </summary>
        /// <typeparam name="T">The type of View that must inherit from <see cref="Object"/> and implement <see cref="IView"/>.</typeparam>
        /// <param name="original">The original View object that will be instantiated.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent object for the new View instances.</param>
        /// <param name="positions">An array of positions for the new View instances.</param>
        /// <param name="rotations">An array of rotations for the new View instances.</param>
        /// <param name="viewModel">The ViewModel for initialization.</param>
        /// <param name="cancellationToken">The cancellation token for stopping the creation process.</param>
        /// <returns>An operation representing the asynchronous process of creating View instances.</returns>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel,
            CancellationToken cancellationToken)
            where T : Object, IView
        {
            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations, cancellationToken);
            
            operation.completed += _ =>
            {
                foreach (var result in operation.Result)
                    result.Initialize(viewModel);
            };
            
            return operation;
        }
#endif // UNITY_2023_1_OR_NEWER
    }
}
#endif // UNITY_2022_3_OR_NEWER