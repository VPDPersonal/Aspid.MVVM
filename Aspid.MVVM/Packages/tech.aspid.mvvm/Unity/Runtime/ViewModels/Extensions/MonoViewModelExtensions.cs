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

        /// <summary>
        /// Disposes and destroys the Unity ViewModel, preserving its GameObject and logging failures.
        /// </summary>
        /// <remarks>Call on the main thread. Destruction is attempted even if disposal fails.</remarks>
        /// <typeparam name="T">The Unity ViewModel object.</typeparam>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The surviving GameObject on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static GameObject? DestroyViewModelSafely<T>(this T? viewModel)
            where T : Object, IViewModel =>
            DestroyViewModelSafelyInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Disposes the ViewModel if supported, destroys its Unity object if present, and logs failures.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects. Destruction is attempted even if disposal fails.</remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The surviving GameObject on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static GameObject? DestroyViewModelSafely(this IViewModel? viewModel) =>
            DestroyViewModelSafelyInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Disposes and destroys the Unity ViewModel and its GameObject, logging failures.
        /// </summary>
        /// <remarks>Call on the main thread. Destruction is attempted even if disposal fails.</remarks>
        /// <typeparam name="T">The Unity ViewModel object.</typeparam>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The surviving GameObject on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static GameObject? DestroyViewModelAndGameObjectSafely<T>(this T? viewModel)
            where T : Object, IViewModel =>
            DestroyViewModelSafelyInternal(viewModel, destroyGameObject: true);

        /// <summary>
        /// Disposes the ViewModel if supported, destroys its GameObject or Unity object if present, and logs failures.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects. Destruction is attempted even if disposal fails.</remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The surviving GameObject on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static GameObject? DestroyViewModelAndGameObjectSafely(this IViewModel? viewModel) =>
            DestroyViewModelSafelyInternal(viewModel, destroyGameObject: true);

        private static GameObject? DestroyViewModelSafelyInternal(IViewModel? viewModel, bool destroyGameObject)
        {
            if (viewModel is null) return null;

            try
            {
                return DestroyViewModelInternal(viewModel, destroyGameObject);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }
        }

        private static GameObject? DestroyViewModelInternal(IViewModel viewModel, bool destroyGameObject)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel is not Object target)
            {
                viewModel.DisposeViewModel();
                return null;
            }

            if (target == null) return null;

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
                UnityObjectHelper.Destroy(target);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
            return gameObject != null
                ? gameObject
                : null;
        }
    }
}
