using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static partial class ViewExtensions
    {
        /// <summary>
        /// Cleans up the View and destroys its Unity object, if any, preserving the owning GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="view">The View to destroy.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static IViewModel? DestroyView(this IView view) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : DestroyViewInternal(view, destroyGameObject: false);

        /// <summary>
        /// Cleans up the View and destroys its GameObject for components or the object itself for other Unity objects.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="view">The View to destroy.</param>
        /// <returns>The previous ViewModel, or <see langword="null"/> if the View had none.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <see langword="null"/> or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static IViewModel? DestroyViewAndGameObject(this IView view) => view.IsMissing()
            ? throw new ArgumentNullException(nameof(view))
            : DestroyViewInternal(view, destroyGameObject: true);

        /// <summary>
        /// Calls <see cref="DestroyView(IView)"/> on each View and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to destroy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="views"/> is <see langword="null"/>, or the only failure is a missing or destroyed View.
        /// </exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViews<T>(this T[] views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroyViewInternal(view, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewAndGameObject(IView)"/> on each View and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to destroy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="views"/> is <see langword="null"/>, or the only failure is a missing or destroyed View.
        /// </exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViewsAndGameObjects<T>(this T[] views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroyViewInternal(view, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyView(IView)"/> on each View and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to destroy.</param>
        /// <exception cref="ArgumentNullException">The only failure is a missing or destroyed View.</exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViews<T>(this Span<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroyViewInternal(view, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewAndGameObject(IView)"/> on each View and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The View type.</typeparam>
        /// <param name="views">The Views to destroy.</param>
        /// <exception cref="ArgumentNullException">The only failure is a missing or destroyed View.</exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViewsAndGameObjects<T>(this Span<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroyViewInternal(view, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViews{T}(Span{T})"/>
        public static void DestroyViews<T>(this ReadOnlySpan<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroyViewInternal(view, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewsAndGameObjects{T}(Span{T})"/>
        public static void DestroyViewsAndGameObjects<T>(this ReadOnlySpan<T> views)
            where T : IView
        {
            var failures = new CleanupExceptions();

            foreach (var view in views)
                DestroyViewInternal(view, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViews{T}(T[])"/>
        public static void DestroyViews<T>(this List<T> views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            DestroyViewsInternal(views, destroyGameObject: false);
        }

        /// <inheritdoc cref="DestroyViewsAndGameObjects{T}(T[])"/>
        public static void DestroyViewsAndGameObjects<T>(this List<T> views)
            where T : IView
        {
            if (views is null)
                throw new ArgumentNullException(nameof(views));

            DestroyViewsInternal(views, destroyGameObject: true);
        }

        /// <inheritdoc cref="DestroyViews{T}(T[])"/>
        public static void DestroyViews<T>(this IEnumerable<T> views)
            where T : IView
        {
            switch (views)
            {
                case null: throw new ArgumentNullException(nameof(views));
                case T[] array: array.DestroyViews(); return;
                case List<T> list: list.DestroyViews(); return;
            }

            DestroyViewsInternal(views, destroyGameObject: false);
        }

        /// <inheritdoc cref="DestroyViewsAndGameObjects{T}(T[])"/>
        public static void DestroyViewsAndGameObjects<T>(this IEnumerable<T> views)
            where T : IView
        {
            switch (views)
            {
                case null: throw new ArgumentNullException(nameof(views));
                case T[] array: array.DestroyViewsAndGameObjects(); return;
                case List<T> list: list.DestroyViewsAndGameObjects(); return;
            }

            DestroyViewsInternal(views, destroyGameObject: true);
        }

        private static void DestroyViewsInternal<T>(List<T> views, bool destroyGameObject)
            where T : IView
        {
            var failures = new CleanupExceptions();

            try
            {
                foreach (var view in views)
                    DestroyViewInternal(view, destroyGameObject, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        private static void DestroyViewsInternal<T>(IEnumerable<T> views, bool destroyGameObject)
            where T : IView
        {
            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = views.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        DestroyViewInternal(enumerator.Current!, destroyGameObject, ref failures);
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

        private static void DestroyViewInternal(IView view, bool destroyGameObject, ref CleanupExceptions failures)
        {
            try
            {
                if (destroyGameObject) 
                    view.DestroyViewAndGameObject();
                else view.DestroyView();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }

        private static IViewModel? DestroyViewInternal(IView view, bool destroyGameObject)
        {
            if (view is not Object target)
                return view.DisposeViewInternal();

            if (destroyGameObject && target is Component component)
                target = component.gameObject;

            IViewModel? viewModel = null;
            var failures = new CleanupExceptions();

            try
            {
                viewModel = view.DisposeViewInternal();
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
