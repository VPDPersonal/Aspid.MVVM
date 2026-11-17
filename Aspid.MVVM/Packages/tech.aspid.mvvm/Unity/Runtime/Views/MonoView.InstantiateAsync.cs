using System;
using UnityEngine;
using System.Threading;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public partial class MonoView
    {
        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent of the clone; <see langword="null"/> for the scene root.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, parent);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, position, rotation);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent of the clone; <see langword="null"/> for the scene root.</param>
        /// <param name="position">The position for the new View instance.</param>
        /// <param name="rotation">The rotation for the new View instance.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, parent, position, rotation);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones; <see langword="null"/> for the scene root.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, parent);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="position">The position for the new View instances.</param>
        /// <param name="rotation">The rotation for the new View instances.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, position, rotation);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="positions">A span of positions for the new View instances.</param>
        /// <param name="rotations">A span of rotations for the new View instances.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, positions, rotations);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones; <see langword="null"/> for the scene root.</param>
        /// <param name="position">The position for the new View instances.</param>
        /// <param name="rotation">The rotation for the new View instances.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, parent, position, rotation);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones; <see langword="null"/> for the scene root.</param>
        /// <param name="positions">A span of positions for the new View instances.</param>
        /// <param name="rotations">A span of rotations for the new View instances.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations);
            InitializeOnCompletion(operation, viewModel);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones; <see langword="null"/> for the scene root.</param>
        /// <param name="position">The position for the new View instances.</param>
        /// <param name="rotation">The rotation for the new View instances.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <param name="cancellationToken">The cancellation token for stopping the creation process.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
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
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, parent, position, rotation, cancellationToken);
            InitializeOnCompletion(operation, viewModel, cancellationToken);

            return operation;
        }

        /// <summary>
        /// Clones views asynchronously and initializes them on completion.
        /// </summary>
        /// <remarks>
        /// Call on the main thread. The returned Unity operation represents object creation, not initialization success.
        /// Initialization runs in the completion callback, which may run after the operation becomes done.
        /// Failures escape that callback after all clones are rolled back.
        /// Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones; <see langword="null"/> for the scene root.</param>
        /// <param name="positions">A span of positions for the new View instances.</param>
        /// <param name="rotations">A span of rotations for the new View instances.</param>
        /// <param name="viewModel">The shared ViewModel used to initialize every created view.</param>
        /// <param name="cancellationToken">The cancellation token for stopping the creation process.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
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
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            var operation = Object.InstantiateAsync(original, count, parent, positions, rotations, cancellationToken);
            InitializeOnCompletion(operation, viewModel, cancellationToken);

            return operation;
        }
    }
}
