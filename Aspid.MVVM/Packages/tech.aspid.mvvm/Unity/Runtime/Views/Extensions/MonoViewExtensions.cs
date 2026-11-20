#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for <see cref="IView"/>.
    /// </summary>
    public static class MonoViewExtensions
    {
        /// <summary>
        /// Disposes or deinitializes the view and destroys its Unity object while preserving any GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
        /// Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
        /// Does not dispose the returned ViewModel.
        /// </remarks>
        /// <typeparam name="T">The Unity view object.</typeparam>
        /// <param name="view">The view to destroy; <see langword="null"/> to do nothing.</param>
        /// <returns>The previous ViewModel; <see langword="null"/> if absent or the Unity object was already destroyed.</returns>
        public static IViewModel? DestroyView<T>(this T? view)
            where T : Object, IView =>
            DestroyViewInternal(view, destroyGameObject: false);

        /// <summary>
        /// Disposes or deinitializes the view and destroys its Unity object while preserving any GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
        /// Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
        /// Does not dispose the returned ViewModel. Managed views are only disposed or deinitialized.
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
        /// Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
        /// Does not dispose the returned ViewModel.
        /// </remarks>
        /// <typeparam name="T">The Unity view object.</typeparam>
        /// <param name="view">The view to destroy; <see langword="null"/> to do nothing.</param>
        /// <returns>The previous ViewModel; <see langword="null"/> if absent or the Unity object was already destroyed.</returns>
        public static IViewModel? DestroyViewAndGameObject<T>(this T? view)
            where T : Object, IView =>
            DestroyViewInternal(view, destroyGameObject: true);

        /// <summary>
        /// Disposes or deinitializes the view and destroys its GameObject or the Unity object itself.
        /// </summary>
        /// <remarks>
        /// Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
        /// Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
        /// Does not dispose the returned ViewModel. Managed views are only disposed or deinitialized.
        /// </remarks>
        /// <param name="view">The view to destroy; <see langword="null"/> to do nothing.</param>
        /// <returns>The previous ViewModel; <see langword="null"/> if absent or the Unity object was already destroyed.</returns>
        public static IViewModel? DestroyViewAndGameObject(this IView? view) =>
            DestroyViewInternal(view, destroyGameObject: true);

        private static IViewModel? DestroyViewInternal(IView? view, bool destroyGameObject)
        {
            if (view is not Object target)
                return view?.DisposeView();

            if (!target) return null;

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
