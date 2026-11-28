using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Cleans up the ViewModel and destroys its Unity object, if any, preserving the owning GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skipped if <see langword="null"/> or destroyed.</param>
        /// <returns>The surviving GameObject for a component, or <see langword="null"/> otherwise.</returns>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static GameObject? DestroyViewModelSafely(this IViewModel? viewModel) => viewModel.IsMissing()
            ? null
            : DestroyViewModelInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Cleans up the ViewModel and destroys its GameObject for components or the object itself for other Unity objects.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="viewModel">The ViewModel to destroy. Skipped if <see langword="null"/> or destroyed.</param>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static void DestroyViewModelAndGameObjectSafely(this IViewModel? viewModel)
        {
            if (viewModel.IsMissing()) return;
            DestroyViewModelInternal(viewModel, destroyGameObject: true);
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModelSafely(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DestroyViewModelsSafely<T>(this T[]? viewModels)
            where T : IViewModel?
        {
            if (viewModels is null) return;

            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroySafelyInternal(viewModel, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModelAndGameObjectSafely(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels. Skips a <see langword="null"/> collection and <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DestroyViewModelsAndGameObjectsSafely<T>(this T[]? viewModels)
            where T : IViewModel?
        {
            if (viewModels is null) return;

            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroySafelyInternal(viewModel, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModelSafely(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DestroyViewModelsSafely<T>(this Span<T> viewModels)
            where T : IViewModel?
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroySafelyInternal(viewModel, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModelAndGameObjectSafely(IViewModel)"/> on each ViewModel and rethrows any failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels. Skips <see langword="null"/> or destroyed entries.</param>
        /// <exception cref="AggregateException">Multiple failures occur while processing the ViewModels.</exception>
        public static void DestroyViewModelsAndGameObjectsSafely<T>(this Span<T> viewModels)
            where T : IViewModel?
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroySafelyInternal(viewModel, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModelsSafely{T}(Span{T})"/>
        public static void DestroyViewModelsSafely<T>(this ReadOnlySpan<T> viewModels)
            where T : IViewModel?
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroySafelyInternal(viewModel, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModelsAndGameObjectsSafely{T}(Span{T})"/>
        public static void DestroyViewModelsAndGameObjectsSafely<T>(this ReadOnlySpan<T> viewModels)
            where T : IViewModel?
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroySafelyInternal(viewModel, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModelsSafely{T}(T[])"/>
        public static void DestroyViewModelsSafely<T>(this List<T>? viewModels)
            where T : IViewModel?
        {
            if (viewModels is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var viewModel in viewModels)
                    DestroySafelyInternal(viewModel, destroyGameObject: false, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModelsAndGameObjectsSafely{T}(T[])"/>
        public static void DestroyViewModelsAndGameObjectsSafely<T>(this List<T>? viewModels)
            where T : IViewModel?
        {
            if (viewModels is null) return;

            var failures = new CleanupExceptions();

            try
            {
                foreach (var viewModel in viewModels)
                    DestroySafelyInternal(viewModel, destroyGameObject: true, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModelsSafely{T}(T[])"/>
        public static void DestroyViewModelsSafely<T>(this IEnumerable<T>? viewModels)
            where T : IViewModel?
        {
            switch (viewModels)
            {
                case null: return;
                case T[] array: array.DestroyViewModelsSafely(); return;
                case List<T> list: list.DestroyViewModelsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = viewModels.GetEnumerator();

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

        /// <inheritdoc cref="DestroyViewModelsAndGameObjectsSafely{T}(T[])"/>
        public static void DestroyViewModelsAndGameObjectsSafely<T>(this IEnumerable<T>? viewModels)
            where T : IViewModel?
        {
            switch (viewModels)
            {
                case null: return;
                case T[] array: array.DestroyViewModelsAndGameObjectsSafely(); return;
                case List<T> list: list.DestroyViewModelsAndGameObjectsSafely(); return;
            }

            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = viewModels.GetEnumerator();

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

        private static void DestroySafelyInternal(
            IViewModel? viewModel,
            bool destroyGameObject,
            ref CleanupExceptions failures)
        {
            if (viewModel.IsMissing()) return;

            try
            {
                DestroyViewModelInternal(viewModel, destroyGameObject);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
