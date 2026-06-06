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
    /// <include file="XmlExampleDoc-Collection-ViewModel-1.1.0.xml" path="doc//member[@name='CollectionViewModelBinder']/*" />
    [Serializable]
    public class CollectionViewModelBinder : CollectionViewModelBinder<MonoView>
    {
        /// <inheritdoc/>
        public CollectionViewModelBinder(MonoView[] views, BindMode mode = BindMode.OneWay)
            : base(views, mode) { }
    }

    /// <summary>
    /// <see cref="CollectionBinderBase{T}"/> that distributes bound <see cref="IViewModel"/> values
    /// across a fixed array of pre-instantiated <typeparamref name="T"/> view objects,
    /// activating and initializing each view in order and deactivating any excess views.
    /// </summary>
    /// <typeparam name="T">The type of pre-instantiated <see cref="MonoBehaviour"/> view objects in the collection.</typeparam>
    /// <include file="XmlExampleDoc-Collection-ViewModel-1.1.0.xml" path="doc//member[@name='ViewModelCollectionBinder{1}']/*" />
    [Serializable]
    public class CollectionViewModelBinder<T> : CollectionBinderBase<IViewModel>
        where T : MonoBehaviour, IView
    {
        [Tooltip("The pre-instantiated view objects assigned to bound ViewModel items in order.")]
        [SerializeField] private T[] _views;

        /// <summary>
        /// Initializes a new instance of <see cref="CollectionViewModelBinder{T}"/>.
        /// </summary>
        /// <param name="views">The pre-instantiated view objects to assign bound ViewModel items to.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public CollectionViewModelBinder(T[] views, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            _views = views;
            mode.ThrowExceptionIfTwo();
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
        /// Resets all views and re-applies the current <see cref="CollectionBinderBase{T}.Collection"/>
        /// positionally, so that each view reflects the item at its corresponding index.
        /// </summary>
        private void RebuildFromCollection()
        {
            OnReset();
            if (Collection?.Count > 0) OnAdded(Collection!);
        }

        protected override void OnReplace(IViewModel? oldItem, IViewModel? newItem, int newStartingIndex)
        {
            _views[newStartingIndex].Deinitialize();
            
            if (newItem is not null)
                _views[newStartingIndex].Initialize(newItem);
        }

        protected override void OnMove(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex) => RebuildFromCollection();

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
