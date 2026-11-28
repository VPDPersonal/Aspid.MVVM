using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Cleans up the View and destroys its Unity object, if any, preserving the owning GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="view">The View to destroy. Skipped if <see langword="null"/> or destroyed.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View was skipped or had none.</returns>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static IViewModel? DestroyViewSafely(this IView? view) => view.IsMissing()
            ? null
            : DestroyViewInternal(view, destroyGameObject: false);

        /// <summary>
        /// Cleans up the View and destroys its GameObject for components or the object itself for other Unity objects.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="view">The View to destroy. Skipped if <see langword="null"/> or destroyed.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View was skipped or had none.</returns>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static IViewModel? DestroyViewAndGameObjectSafely(this IView? view) => view.IsMissing()
            ? null
            : DestroyViewInternal(view, destroyGameObject: true);

        /// <summary>
        /// Calls <see cref="DestroyViewSafely(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DestroyViewsSafely<T>(this T[]? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroySafelyInternal(view, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewAndGameObjectSafely(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DestroyViewsAndGameObjectsSafely<T>(this T[]? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroySafelyInternal(view, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewSafely(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DestroyViewsSafely<T>(this Span<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroySafelyInternal(view, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewAndGameObjectSafely(IView)"/> on each View and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the Views.</exception>
        public static void DestroyViewsAndGameObjectsSafely<T>(this Span<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroySafelyInternal(view, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsSafely{T}(Span{T})"/>
        public static void DestroyViewsSafely<T>(this ReadOnlySpan<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroySafelyInternal(view, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsAndGameObjectsSafely{T}(Span{T})"/>
        public static void DestroyViewsAndGameObjectsSafely<T>(this ReadOnlySpan<T> views)
            where T : IView?
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroySafelyInternal(view, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsSafely{T}(T[])"/>
        public static void DestroyViewsSafely<T>(this List<T>? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    DestroySafelyInternal(view, destroyGameObject: false, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsAndGameObjectsSafely{T}(T[])"/>
        public static void DestroyViewsAndGameObjectsSafely<T>(this List<T>? views)
            where T : IView?
        {
            if (views is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    DestroySafelyInternal(view, destroyGameObject: true, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsSafely{T}(T[])"/>
        public static void DestroyViewsSafely<T>(this IEnumerable<T>? views)
            where T : IView?
        {
            switch (views)
            {
                case null: return;
                case T[] array: array.DestroyViewsSafely(); return;
                case List<T> list: list.DestroyViewsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        DestroySafelyInternal(enumerator.Current, destroyGameObject: false, ref failures);
                }
                catch (Exception exception)
                {
                    failures.Add(exception);
                }
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsAndGameObjectsSafely{T}(T[])"/>
        public static void DestroyViewsAndGameObjectsSafely<T>(this IEnumerable<T>? views)
            where T : IView?
        {
            switch (views)
            {
                case null: return;
                case T[] array: array.DestroyViewsAndGameObjectsSafely(); return;
                case List<T> list: list.DestroyViewsAndGameObjectsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        DestroySafelyInternal(enumerator.Current, destroyGameObject: true, ref failures);
                }
                catch (Exception exception)
                {
                    failures.Add(exception);
                }
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        private static void DestroySafelyInternal(IView? view, bool destroyGameObject, ref CleanupExceptions failures)
        {
            if (view.IsMissing()) return;

            try
            {
                DestroyViewInternal(view, destroyGameObject);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
