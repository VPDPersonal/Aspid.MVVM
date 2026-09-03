#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CollectionViewModelBinder{TView}"/> over <see cref="MonoView"/>.
    /// </summary>
    [Serializable]
    public class CollectionViewModelBinder : CollectionViewModelBinder<MonoView>
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected CollectionViewModelBinder() { }

        /// <inheritdoc/>
        public CollectionViewModelBinder(MonoView[] views, BindMode mode = BindMode.OneWay)
            : base(views, mode) { }
    }

    /// <summary>
    /// <see cref="CollectionBinder{T}"/> that shows bound ViewModels in a fixed set of pre-placed views, in order.
    /// Views beyond the item count are deactivated; items beyond the view count are not shown. Every change rebuilds the whole set.
    /// </summary>
    /// <typeparam name="TView">The type of the pre-placed views.</typeparam>
    [Serializable]
    public class CollectionViewModelBinder<TView> : CollectionBinder<IViewModel>
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Views the items are shown in, in order. Extra items are not shown.")]
        [SerializeField] private TView[] _views = Array.Empty<TView>();

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected CollectionViewModelBinder() { }

        /// <param name="views">The views the items are shown in, in order. Extra items are not shown.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="views"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public CollectionViewModelBinder(TView[] views, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _views = views ?? throw new ArgumentNullException(nameof(views));
        }

        /// <inheritdoc/>
        protected override void OnAdded(IReadOnlyCollection<IViewModel>? values)
        {
            var index = 0;

            if (values is not null)
            {
                foreach (var value in values)
                {
                    if (index >= _views.Length) break;

                    _views[index].gameObject.SetActive(true);
                    _views[index].Initialize(value);

                    index++;
                }
            }

            for (var i = index; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
        }

        /// <inheritdoc/>
        protected override void OnAdded(IViewModel? newItem) =>
            Rebuild();

        /// <inheritdoc/>
        protected override void OnAdded(IReadOnlyList<IViewModel?>? newItems) =>
            Rebuild();

        /// <inheritdoc/>
        protected override void OnRemoved(IViewModel? oldItem) =>
            Rebuild();

        /// <inheritdoc/>
        protected override void OnRemoved(IReadOnlyList<IViewModel?>? oldItems) =>
            Rebuild();

        /// <inheritdoc/>
        protected override void OnReplaced(IViewModel? oldItem, IViewModel? newItem, int index)
        {
            if (index >= _views.Length) return;

            _views[index].Deinitialize();

            if (newItem is not null)
                _views[index].Initialize(newItem);
        }

        /// <inheritdoc/>
        protected override void OnMoved(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex) =>
            Rebuild();

        /// <inheritdoc/>
        protected override void OnReset()
        {
            foreach (var view in _views)
            {
                view.Deinitialize();
                view.gameObject.SetActive(false);
            }
        }

        private void Rebuild()
        {
            OnReset();
            if (Collection?.Count > 0) OnAdded(Collection);
        }
    }
}
