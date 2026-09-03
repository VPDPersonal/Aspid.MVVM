#nullable enable
using System;
using UnityEngine;
using UnityEngine.Pool;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="PrefabViewFactory{T}"/> that keeps released views in an <see cref="ObjectPool{T}"/> and reuses them.
    /// </summary>
    /// <remarks>
    /// A released view is deinitialized and deactivated. A reused view is activated, repositioned and initialized again.
    /// </remarks>
    /// <typeparam name="T">The type of the view component on the prefab.</typeparam>
    [Serializable]
    public class PrefabViewPool<T> : PrefabViewFactory<T>
        where T : MonoBehaviour, IView
    {
        [Tooltip("Views instantiated up front.")]
        [SerializeField] [Min(0)] private int _initialCount;

        [Tooltip("Maximum inactive views kept; extra ones are destroyed.")]
        [SerializeField] [Min(1)] private int _maxCount = int.MaxValue;

        private ObjectPool<T>? _pool;
        private IViewModel? _pendingViewModel;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected PrefabViewPool() { }

        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="overrideSibling">Whether to place new views at <paramref name="siblingIndex"/> instead of last.</param>
        /// <param name="siblingIndex">The sibling index used when <paramref name="overrideSibling"/> is set.</param>
        public PrefabViewPool(
            T prefab,
            bool overrideSibling = false,
            int siblingIndex = 0)
            : this(prefab, null, overrideSibling, siblingIndex) { }

        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="settings">The pool size limits.</param>
        /// <param name="overrideSibling">Whether to place new views at <paramref name="siblingIndex"/> instead of last.</param>
        /// <param name="siblingIndex">The sibling index used when <paramref name="overrideSibling"/> is set.</param>
        public PrefabViewPool(
            T prefab,
            PoolSettings settings,
            bool overrideSibling = false,
            int siblingIndex = 0)
            : this(prefab, null, settings, overrideSibling, siblingIndex) { }

        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="container">The parent of created views, or <see langword="null"/> for the scene root.</param>
        /// <param name="overrideSibling">Whether to place new views at <paramref name="siblingIndex"/> instead of last.</param>
        /// <param name="siblingIndex">The sibling index used when <paramref name="overrideSibling"/> is set.</param>
        public PrefabViewPool(
            T prefab,
            Transform? container,
            bool overrideSibling = false,
            int siblingIndex = 0)
            : this(prefab, container, new PoolSettings(0), overrideSibling, siblingIndex) { }

        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="container">The parent of created views, or <see langword="null"/> for the scene root.</param>
        /// <param name="settings">The pool size limits.</param>
        /// <param name="overrideSibling">Whether to place new views at <paramref name="siblingIndex"/> instead of last.</param>
        /// <param name="siblingIndex">The sibling index used when <paramref name="overrideSibling"/> is set.</param>
        public PrefabViewPool(
            T prefab,
            Transform? container,
            PoolSettings settings,
            bool overrideSibling = false,
            int siblingIndex = 0)
            : base(prefab, container, overrideSibling, siblingIndex)
        {
            _maxCount = settings.MaxCount;
            _initialCount = settings.InitialCount;
        }

        private ObjectPool<T> Pool =>
            _pool ??= CreatePool();

        /// <summary>
        /// Takes a view from the pool, instantiating one if none is free, and initializes it with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <returns>The activated view.</returns>
        public override T Create(IViewModel? viewModel)
        {
            var pool = Pool;
            _pendingViewModel = viewModel;

            return pool.Get();
        }

        /// <summary>
        /// Deinitializes and deactivates the view, then returns it to the pool.
        /// </summary>
        /// <param name="view">The view to release.</param>
        public override void Release(T view) =>
            Pool.Release(view);

        /// <summary>
        /// Does nothing: the pool applies <see cref="PrefabViewFactory{T}.OnCreate"/> when a view is taken, not when it is instantiated.
        /// </summary>
        /// <param name="viewModel">Ignored.</param>
        /// <param name="view">Ignored.</param>
        protected sealed override void OnCreate(IViewModel? viewModel, T view) { }

        private ObjectPool<T> CreatePool()
        {
            var pool = new ObjectPool<T>(
                InstantiateView,
                OnGet,
                OnRelease,
                DestroyView,
                maxSize: _maxCount,
                collectionCheck: false,
                defaultCapacity: Math.Max(_initialCount, 1));

            for (var i = 0; i < _initialCount; i++)
            {
                var view = InstantiateView();
                view.gameObject.SetActive(false);
                pool.Release(view);
            }

            return pool;
        }

        private T InstantiateView() =>
            base.Create(null);

        private void OnGet(T view)
        {
            view.gameObject.SetActive(true);
            base.OnCreate(_pendingViewModel, view);
            _pendingViewModel = null;
        }

        private static void OnRelease(T view)
        {
            view.Deinitialize();
            view.gameObject.SetActive(false);
        }

        private void DestroyView(T view) =>
            base.Release(view);
    }
}
