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
        /// Clones a view asynchronously and initializes it on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes it on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent of the clone, or <see langword="null"/> for the scene root.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, parent), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes it on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, position, rotation), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes it on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="parent">The parent of the clone, or <see langword="null"/> for the scene root.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, parent, position, rotation), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel))
                : InitializeOnCompletion(Object.InstantiateAsync(original, count), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones, or <see langword="null"/> for the scene root.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion( Object.InstantiateAsync(original, count, parent), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, position, rotation), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="positions">World-space positions, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="rotations">World-space rotations, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, positions, rotations), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones, or <see langword="null"/> for the scene root.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, parent, position, rotation), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones, or <see langword="null"/> for the scene root.</param>
        /// <param name="positions">World-space positions, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="rotations">World-space rotations, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Transform parent,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, parent, positions, rotations), viewModel);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones, or <see langword="null"/> for the scene root.</param>
        /// <param name="position">The world-space position.</param>
        /// <param name="rotation">The world-space rotation.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <param name="cancellationToken">The token that cancels cloning or destroys clones if initialization is canceled.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
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
            return viewModel.IsMissing() 
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, parent, position, rotation, cancellationToken), viewModel, cancellationToken);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="parent">The parent of the clones, or <see langword="null"/> for the scene root.</param>
        /// <param name="positions">World-space positions, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="rotations">World-space rotations, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <param name="cancellationToken">The token that cancels cloning or destroys clones if initialization is canceled.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
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
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, parent, positions, rotations, cancellationToken), viewModel, cancellationToken);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <param name="parameters">The Unity cloning options.</param>
        /// <param name="cancellationToken">The token that cancels cloning or destroys clones if initialization is canceled.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            IViewModel viewModel,
            InstantiateParameters parameters,
            CancellationToken cancellationToken = default)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, parameters, cancellationToken), viewModel, cancellationToken);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="position">The position in the configured coordinate space.</param>
        /// <param name="rotation">The rotation in the configured coordinate space.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <param name="parameters">The Unity cloning options.</param>
        /// <param name="cancellationToken">The token that cancels cloning or destroys clones if initialization is canceled.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            Vector3 position,
            Quaternion rotation,
            IViewModel viewModel,
            InstantiateParameters parameters,
            CancellationToken cancellationToken = default)
            where T : Object, IView
        {
            return viewModel.IsMissing() 
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, position, rotation, parameters, cancellationToken), viewModel, cancellationToken);
        }

        /// <summary>
        /// Clones a view asynchronously and initializes each clone on completion.
        /// </summary>
        /// <remarks>
        /// If initialization fails or the ViewModel is destroyed before initialization, the completion callback
        /// destroys all clones and rethrows the exception.
        /// </remarks>
        /// <typeparam name="T">The Unity view type.</typeparam>
        /// <param name="original">The view to clone.</param>
        /// <param name="count">The number of instances to create.</param>
        /// <param name="positions">Positions, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="rotations">Rotations, repeated when shorter than <paramref name="count"/>.</param>
        /// <param name="viewModel">The ViewModel shared by all clones.</param>
        /// <param name="parameters">The Unity cloning options.</param>
        /// <param name="cancellationToken">The token that cancels cloning or destroys clones if initialization is canceled.</param>
        /// <returns>The Unity cloning operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/> or destroyed.</exception>
        /// <exception cref="ArgumentException"><paramref name="original"/> is <see langword="null"/> or a <see cref="ScriptableObject"/>, or <paramref name="count"/> is less than one.</exception>
        public static AsyncInstantiateOperation<T> InstantiateAsync<T>(
            T original,
            int count,
            ReadOnlySpan<Vector3> positions,
            ReadOnlySpan<Quaternion> rotations,
            IViewModel viewModel,
            InstantiateParameters parameters,
            CancellationToken cancellationToken = default)
            where T : Object, IView
        {
            return viewModel.IsMissing()
                ? throw new ArgumentNullException(nameof(viewModel)) 
                : InitializeOnCompletion(Object.InstantiateAsync(original, count, positions, rotations, parameters, cancellationToken), viewModel, cancellationToken);
        }
    }
}
