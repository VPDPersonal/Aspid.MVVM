using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.
        /// </summary>
        /// <remarks>
        /// <para>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</para>
        /// <para>Destruction is attempted even if disposal throws. Cleanup failures propagate to the caller.</para>
        /// </remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The associated GameObject if still alive after the call; otherwise, <see langword="null"/>.</returns>
        public static GameObject? DestroyViewModel(this IViewModel? viewModel) =>
            DestroyViewModelInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.
        /// </summary>
        /// <remarks>
        /// <para>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</para>
        /// <para>Destruction is attempted even if disposal throws. Cleanup failures propagate to the caller.</para>
        /// </remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        public static void DestroyViewModelAndGameObject(this IViewModel? viewModel) =>
            DestroyViewModelInternal(viewModel, destroyGameObject: true);

        /// <summary>
        /// Cleans up and destroys the ViewModel, preserving its GameObject when applicable, and logs failures.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.</remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The surviving GameObject on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static GameObject? DestroyViewModelSafely(this IViewModel? viewModel) =>
            DestroyViewModelSafelyInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Cleans up and destroys the ViewModel, including its GameObject when applicable, and logs failures.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.</remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skips null and destroyed Unity objects.</param>
        public static void DestroyViewModelAndGameObjectSafely(this IViewModel? viewModel) =>
            DestroyViewModelSafelyInternal(viewModel, destroyGameObject: true);

        private static GameObject? DestroyViewModelSafelyInternal(IViewModel? viewModel, bool destroyGameObject)
        {
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

        private static GameObject? DestroyViewModelInternal(IViewModel? viewModel, bool destroyGameObject)
        {
            if (viewModel is not Object target)
            {
                viewModel?.DisposeViewModel();
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
            return gameObject != null ? gameObject : null;
        }
    }
}
