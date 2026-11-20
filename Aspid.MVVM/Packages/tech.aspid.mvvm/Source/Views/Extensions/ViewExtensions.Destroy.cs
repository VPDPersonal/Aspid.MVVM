using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Disposes or deinitializes the view and destroys its Unity object while preserving any GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
        /// Destruction is attempted even if cleanup fails. Cleanup failures propagate to the caller.
        /// Does not dispose the returned ViewModel. Managed views are disposed or deinitialized.
        /// </remarks>
        /// <param name="view">The view to destroy; <see langword="null"/> to do nothing.</param>
        /// <returns>The previous ViewModel; <see langword="null"/> if absent or the Unity object was already destroyed.</returns>
        public static IViewModel? DestroyView(this IView? view) =>
            DestroyViewInternal(view, destroyGameObject: false);

        /// <summary>
        /// Disposes or deinitializes the view and destroys its GameObject or the Unity object itself.
        /// </summary>
        /// <remarks>
        /// Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
        /// Destruction is attempted even if cleanup fails. Cleanup failures propagate to the caller.
        /// Does not dispose the returned ViewModel. Managed views are disposed or deinitialized.
        /// </remarks>
        /// <param name="view">The view to destroy; <see langword="null"/> to do nothing.</param>
        /// <returns>The previous ViewModel; <see langword="null"/> if absent or the Unity object was already destroyed.</returns>
        public static IViewModel? DestroyViewAndGameObject(this IView? view) =>
            DestroyViewInternal(view, destroyGameObject: true);

        /// <summary>
        /// Cleans up and destroys the View, preserving its GameObject when applicable, and logs failures.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.</remarks>
        /// <param name="view">The View to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The previous ViewModel on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static IViewModel? DestroyViewSafely(this IView? view) =>
            DestroyViewSafelyInternal(view, destroyGameObject: false);

        /// <summary>
        /// Cleans up and destroys the View, including its GameObject when applicable, and logs failures.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.</remarks>
        /// <param name="view">The View to destroy. Skips null and destroyed Unity objects.</param>
        /// <returns>The previous ViewModel on success, or <see langword="null"/> if absent, skipped, or failed.</returns>
        public static IViewModel? DestroyViewAndGameObjectSafely(this IView? view) =>
            DestroyViewSafelyInternal(view, destroyGameObject: true);

        private static IViewModel? DestroyViewSafelyInternal(IView? view, bool destroyGameObject)
        {
            try
            {
                return DestroyViewInternal(view, destroyGameObject);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }
        }

        private static IViewModel? DestroyViewInternal(IView? view, bool destroyGameObject)
        {
            if (view is not Object target)
                return view?.DisposeView();

            if (target == null) return null;

            if (destroyGameObject && target is Component component)
                target = component.gameObject;

            IViewModel? viewModel = null;
            var failures = new CleanupExceptions();

            try
            {
                viewModel = view.DisposeView();
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
            return viewModel;
        }
    }
}
