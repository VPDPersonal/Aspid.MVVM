#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="CollectionViewModelBinder{T}"/> that uses <see cref="MonoView"/> as the view type.
    /// </summary>
    [Serializable]
    public class CollectionViewModelBinder : CollectionViewModelBinder<MonoView>
    {
        /// <inheritdoc/>
        public CollectionViewModelBinder(MonoView[] views, BindMode mode = BindMode.OneWay)
            : base(views, mode) { }
    }

    /// <summary>
    /// <see cref="CollectionBinder{T}"/> that distributes bound <see cref="IViewModel"/> values
    /// across a fixed array of pre-instantiated <typeparamref name="T"/> view objects,
    /// activating and initializing each view in order and deactivating any excess views.
    /// </summary>
    /// <typeparam name="T">The type of pre-instantiated <see cref="MonoBehaviour"/> view objects in the collection.</typeparam>
    [Serializable]
    public class CollectionViewModelBinder<T> : CollectionBinder<IViewModel>
        where T : MonoBehaviour, IView
    {
        [Tooltip("Pre-instantiated views, assigned in order. Extra items beyond this many are hidden.")]
        [SerializeField] private T[] _views;

        /// <param name="views">The pre-instantiated views to assign items to, in order; extra items beyond this many are hidden.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="views"/> is <see langword="null"/>.</exception>
        public CollectionViewModelBinder(T[] views, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _views = views ?? throw new ArgumentNullException(nameof(views));
        }

        protected override void OnAdded(IReadOnlyCollection<IViewModel> values)
        {
            var index = 0;

            foreach (var value in values)
            {
                if (index >= _views.Length) break;

                _views[index].gameObject.SetActive(true);
                _views[index].Initialize(value);

                index++;
            }

            for (var i = index; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
        }

        protected override void OnAdded(IViewModel? newItem) => RebuildFromCollection();

        protected override void OnAdded(IReadOnlyList<IViewModel?> newItems) => RebuildFromCollection();

        protected override void OnRemoved(IViewModel? oldItem) => RebuildFromCollection();

        protected override void OnRemoved(IReadOnlyList<IViewModel?> oldItems) => RebuildFromCollection();

        /// <summary>
        /// Resets all views and re-applies the current <see cref="CollectionBinder{T}.Collection"/>
        /// positionally, so that each view reflects the item at its corresponding index.
        /// </summary>
        private void RebuildFromCollection()
        {
            OnReset();
            if (Collection?.Count > 0) OnAdded(Collection!);
        }

        protected override void OnReplaced(IViewModel? oldItem, IViewModel? newItem, int index)
        {
            if (index >= _views.Length) return;

            _views[index].Deinitialize();
            
            if (newItem is not null)
                _views[index].Initialize(newItem);
        }

        protected override void OnMoved(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex) => RebuildFromCollection();

        protected override void OnReset()
        {
            foreach (var view in _views)
            {
                view.Deinitialize();
                view.gameObject.SetActive(false);
            }
        }
    }
}
