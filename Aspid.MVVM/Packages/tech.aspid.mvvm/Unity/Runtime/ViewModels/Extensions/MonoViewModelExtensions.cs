#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IViewModel"/>.
    /// </summary>
    public static class MonoViewModelExtensions
    {
        /// <summary>
        /// Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.
        /// </summary>
        /// <remarks>
        /// <para>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</para>
        /// <para>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller.</para>
        /// </remarks>
        /// <typeparam name="T">The Unity ViewModel object.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose and destroy.</param>
        /// <returns>The associated GameObject if still alive after the call; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static GameObject? DestroyViewModel<T>(this T viewModel)
            where T : Object, IViewModel =>
            DestroyViewModelInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.
        /// </summary>
        /// <remarks>
        /// <para>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</para>
        /// <para>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller. Managed ViewModels are only disposed.</para>
        /// </remarks>
        /// <param name="viewModel">The ViewModel to dispose and destroy.</param>
        /// <returns>The associated GameObject if still alive after the call; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static GameObject? DestroyViewModel(this IViewModel viewModel) =>
            DestroyViewModelInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.
        /// </summary>
        /// <remarks>
        /// <para>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</para>
        /// <para>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller.</para>
        /// </remarks>
        /// <typeparam name="T">The Unity ViewModel object.</typeparam>
        /// <param name="viewModel">The ViewModel to dispose and destroy.</param>
        /// <returns>The associated GameObject if still alive after the call; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static GameObject? DestroyViewModelAndGameObject<T>(this T viewModel)
            where T : Object, IViewModel =>
            DestroyViewModelInternal(viewModel, destroyGameObject: true);

        /// <summary>
        /// Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.
        /// </summary>
        /// <remarks>
        /// <para>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</para>
        /// <para>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller. Managed ViewModels are only disposed.</para>
        /// </remarks>
        /// <param name="viewModel">The ViewModel to dispose and destroy.</param>
        /// <returns>The associated GameObject if still alive after the call; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
        public static GameObject? DestroyViewModelAndGameObject(this IViewModel viewModel) =>
            DestroyViewModelInternal(viewModel, destroyGameObject: true);

        private static GameObject? DestroyViewModelInternal(IViewModel viewModel, bool destroyGameObject)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel is not Object target)
            {
                viewModel.DisposeViewModel();
                return null;
            }
            
            if (!target) return null;

            var gameObject = target is Component component
                ? component.gameObject
                : null;
            
            if (destroyGameObject && gameObject is not null)
                target = gameObject;

            var failures = new CleanupExceptions();

            try
            {
                viewModel.DisposeViewModel();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            try
            {
                UnityObjectDestruction.Destroy(target);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
            return gameObject ? gameObject : null;
        }
    }
}
