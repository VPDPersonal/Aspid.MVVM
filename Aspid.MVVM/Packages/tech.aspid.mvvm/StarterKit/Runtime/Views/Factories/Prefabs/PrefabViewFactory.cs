#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IViewFactory{TView}"/> that instantiates a prefab per view and destroys it on release.
    /// </summary>
    /// <typeparam name="T">The type of the view component on the prefab.</typeparam>
    [Serializable]
    public class PrefabViewFactory<T> : IViewFactory<T>
        where T : MonoBehaviour, IView
    {
        [Tooltip("Prefab instantiated for each view.")]
        [SerializeField] private T? _prefab;

        [Tooltip("Parent of created views. Empty places them at the scene root.")]
        [SerializeField] private Transform? _container;

        [Tooltip("Place new views at Sibling Index instead of last.")]
        [SerializeField] private bool _overrideSibling;

        [Tooltip("Sibling index of new views when Override Sibling is on.")]
        [SerializeField] [Min(0)] private int _siblingIndex;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected PrefabViewFactory() { }

        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="overrideSibling">Whether to place new views at <paramref name="siblingIndex"/> instead of last.</param>
        /// <param name="siblingIndex">The sibling index used when <paramref name="overrideSibling"/> is set.</param>
        public PrefabViewFactory(
            T prefab,
            bool overrideSibling = false,
            int siblingIndex = 0)
            : this(prefab, null, overrideSibling, siblingIndex) { }

        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="container">The parent of created views, or <see langword="null"/> for the scene root.</param>
        /// <param name="overrideSibling">Whether to place new views at <paramref name="siblingIndex"/> instead of last.</param>
        /// <param name="siblingIndex">The sibling index used when <paramref name="overrideSibling"/> is set.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="prefab"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="siblingIndex"/> is negative.</exception>
        public PrefabViewFactory(
            T prefab,
            Transform? container,
            bool overrideSibling = false,
            int siblingIndex = 0)
        {
            if (siblingIndex < 0) throw new ArgumentOutOfRangeException(nameof(siblingIndex));

            _container = container;
            _siblingIndex = siblingIndex;
            _overrideSibling = overrideSibling;
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        }

        /// <summary>
        /// Instantiates the prefab and runs <see cref="OnCreate"/> on it.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to leave it uninitialized.</param>
        /// <returns>The created view.</returns>
        public virtual T Create(IViewModel? viewModel)
        {
            var view = Object.Instantiate(_prefab, _container);
            OnCreate(viewModel, view);

            return view;
        }

        /// <summary>
        /// Destroys the view together with its GameObject.
        /// </summary>
        /// <param name="view">The view to release.</param>
        public virtual void Release(T view) =>
            view.DestroyViewAndGameObject();

        /// <summary>
        /// Places the view among its siblings and initializes it with <paramref name="viewModel"/> if one is given.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with, or <see langword="null"/> to skip initialization.</param>
        /// <param name="view">The freshly instantiated view.</param>
        protected virtual void OnCreate(IViewModel? viewModel, T view)
        {
            SetSibling(view);

            if (viewModel is not null)
                view.Initialize(viewModel);
        }

        /// <summary>
        /// Moves the view to the configured sibling position.
        /// </summary>
        /// <param name="view">The view to move.</param>
        protected void SetSibling(T view)
        {
            if (_overrideSibling) view.transform.SetSiblingIndex(_siblingIndex);
            else view.transform.SetAsLastSibling();
        }
    }
}
