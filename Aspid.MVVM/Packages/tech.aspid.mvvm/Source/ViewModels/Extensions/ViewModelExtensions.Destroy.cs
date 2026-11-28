using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public static partial class ViewModelExtensions
    {
        /// <summary>
        /// Cleans up the ViewModel and destroys its Unity object, if any, preserving the owning GameObject.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="viewModel">The ViewModel to destroy.</param>
        /// <returns>The surviving GameObject for a component, or <see langword="null"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> is <see langword="null"/> or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static GameObject? DestroyViewModel(this IViewModel viewModel) => viewModel.IsMissing()
            ? throw new ArgumentNullException(nameof(viewModel))
            : DestroyViewModelInternal(viewModel, destroyGameObject: false);

        /// <summary>
        /// Cleans up the ViewModel and destroys its GameObject for components or the object itself for other Unity objects.
        /// </summary>
        /// <remarks>
        /// Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
        /// It is immediate in Edit Mode and deferred in Play Mode.
        /// </remarks>
        /// <param name="viewModel">The ViewModel to destroy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModel"/> is <see langword="null"/> or destroyed.
        /// </exception>
        /// <exception cref="AggregateException">Cleanup and destruction both fail.</exception>
        public static void DestroyViewModelAndGameObject(this IViewModel viewModel)
        {
            if (viewModel.IsMissing())
                throw new ArgumentNullException(nameof(viewModel));

            DestroyViewModelInternal(viewModel, destroyGameObject: true);
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModel(IViewModel)"/> on each ViewModel and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels to destroy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModels"/> is <see langword="null"/>, or the only failure is a missing or destroyed ViewModel.
        /// </exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViewModels<T>(this T[] viewModels)
            where T : IViewModel
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroyViewModelInternal(viewModel, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModelAndGameObject(IViewModel)"/> on each ViewModel and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels to destroy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="viewModels"/> is <see langword="null"/>, or the only failure is a missing or destroyed ViewModel.
        /// </exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViewModelsAndGameObjects<T>(this T[] viewModels)
            where T : IViewModel
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroyViewModelInternal(viewModel, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModel(IViewModel)"/> on each ViewModel and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels to destroy.</param>
        /// <exception cref="ArgumentNullException">The only failure is a missing or destroyed ViewModel.</exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViewModels<T>(this Span<T> viewModels)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroyViewModelInternal(viewModel, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Calls <see cref="DestroyViewModelAndGameObject(IViewModel)"/> on each ViewModel and rethrows failures after cleanup.
        /// </summary>
        /// <remarks>Call on the main thread for Unity objects.</remarks>
        /// <typeparam name="T">The ViewModel type.</typeparam>
        /// <param name="viewModels">The ViewModels to destroy.</param>
        /// <exception cref="ArgumentNullException">The only failure is a missing or destroyed ViewModel.</exception>
        /// <exception cref="AggregateException">Multiple failures occur during cleanup, destruction, or enumeration.</exception>
        public static void DestroyViewModelsAndGameObjects<T>(this Span<T> viewModels)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroyViewModelInternal(viewModel, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModels{T}(Span{T})"/>
        public static void DestroyViewModels<T>(this ReadOnlySpan<T> viewModels)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroyViewModelInternal(viewModel, destroyGameObject: false, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModelsAndGameObjects{T}(Span{T})"/>
        public static void DestroyViewModelsAndGameObjects<T>(this ReadOnlySpan<T> viewModels)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            foreach (var viewModel in viewModels)
                DestroyViewModelInternal(viewModel, destroyGameObject: true, ref failures);

            failures.ThrowIfAny();
        }

        /// <inheritdoc cref="DestroyViewModels{T}(T[])"/>
        public static void DestroyViewModels<T>(this List<T> viewModels)
            where T : IViewModel
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            DestroyViewModelsInternal(viewModels, destroyGameObject: false);
        }

        /// <inheritdoc cref="DestroyViewModelsAndGameObjects{T}(T[])"/>
        public static void DestroyViewModelsAndGameObjects<T>(this List<T> viewModels)
            where T : IViewModel
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            DestroyViewModelsInternal(viewModels, destroyGameObject: true);
        }

        /// <inheritdoc cref="DestroyViewModels{T}(T[])"/>
        public static void DestroyViewModels<T>(this IEnumerable<T> viewModels)
            where T : IViewModel
        {
            switch (viewModels)
            {
                case null: throw new ArgumentNullException(nameof(viewModels));
                case T[] array: array.DestroyViewModels(); return;
                case List<T> list: list.DestroyViewModels(); return;
            }

            DestroyViewModelsInternal(viewModels, destroyGameObject: false);
        }

        /// <inheritdoc cref="DestroyViewModelsAndGameObjects{T}(T[])"/>
        public static void DestroyViewModelsAndGameObjects<T>(this IEnumerable<T> viewModels)
            where T : IViewModel
        {
            switch (viewModels)
            {
                case null: throw new ArgumentNullException(nameof(viewModels));
                case T[] array: array.DestroyViewModelsAndGameObjects(); return;
                case List<T> list: list.DestroyViewModelsAndGameObjects(); return;
            }

            DestroyViewModelsInternal(viewModels, destroyGameObject: true);
        }

        private static void DestroyViewModelsInternal<T>(List<T> viewModels, bool destroyGameObject)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            try
            {
                foreach (var viewModel in viewModels)
                    DestroyViewModelInternal(viewModel, destroyGameObject, ref failures);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        private static void DestroyViewModelsInternal<T>(IEnumerable<T> viewModels, bool destroyGameObject)
            where T : IViewModel
        {
            var failures = new CleanupExceptions();

            try
            {
                using var enumerator = viewModels.GetEnumerator();

                try
                {
                    while (enumerator.MoveNext())
                        DestroyViewModelInternal(enumerator.Current!, destroyGameObject, ref failures);
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

        private static GameObject? DestroyViewModelInternal(IViewModel viewModel, bool destroyGameObject)
        {
            if (viewModel is not Object target)
            {
                viewModel.DisposeViewModel();
                return null;
            }

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

        private static void DestroyViewModelInternal(
            IViewModel viewModel,
            bool destroyGameObject,
            ref CleanupExceptions failures)
        {
            try
            {
                if (destroyGameObject)
                    viewModel.DestroyViewModelAndGameObject();
                else viewModel.DestroyViewModel();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
        }
    }
}
