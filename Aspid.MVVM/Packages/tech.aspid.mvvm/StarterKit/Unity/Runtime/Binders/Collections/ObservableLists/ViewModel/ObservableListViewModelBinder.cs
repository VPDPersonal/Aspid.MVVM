using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Comparer = Aspid.MVVM.StarterKit.ICollectionComparer<Aspid.MVVM.IViewModel>;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactory<Aspid.MVVM.MonoView>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ObservableListViewModelBinder{T, TViewFactory}"/> that uses <see cref="MonoView"/> as the view type
    /// and the default <see cref="IViewFactory{T}"/> as the factory.
    /// </summary>
    /// <include file="XmlExampleDoc-ObservableList-ViewModel-1.1.0.xml" path="doc//member[@name='ObservableListViewModelBinder']/*" />
    [Serializable]
    public class ObservableListViewModelBinder : ObservableListViewModelBinder<MonoView, ViewFactory>
    {
        /// <inheritdoc/>
        public ObservableListViewModelBinder(ViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableListViewModelBinder{T, TViewFactory}"/> that uses <see cref="IViewFactory{T}"/> as the factory type.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="MonoBehaviour"/> view created for each item in the list.</typeparam>
    /// <include file="XmlExampleDoc-ObservableList-ViewModel-1.1.0.xml" path="doc//member[@name='ObservableListViewModelBinder{1}']/*" />
    [Serializable]
    public class ObservableListViewModelBinder<T> : ObservableListViewModelBinder<T, IViewFactory<T>>
        where T : MonoBehaviour, IView
    {
        /// <inheritdoc/>
        public ObservableListViewModelBinder(IViewFactory<T> viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableListBinder{T}"/> that instantiates and releases <typeparamref name="T"/> view objects
    /// for each <see cref="IViewModel"/> in a bound observable list,
    /// with optional filtering and sorting support.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="MonoBehaviour"/> view created for each item in the list.</typeparam>
    /// <typeparam name="TViewFactory">The factory type used to create and release view instances.</typeparam>
    /// <include file="XmlExampleDoc-ObservableList-ViewModel-1.1.0.xml" path="doc//member[@name='ObservableListViewModelBinder{2}']/*" />
    [Serializable]
    public class ObservableListViewModelBinder<T, TViewFactory> : ObservableListBinder<IViewModel>
        where T : MonoBehaviour, IView
        where TViewFactory : IViewFactory<T>
    {
        [Tooltip("Creates and releases a view for each list item.")]
        [SerializeReference] private TViewFactory _viewFactory;

        [Tooltip("Optional filter for which items are shown. Leave empty to show all.")]
        [SerializeReference] private Filter _filter;

        [Tooltip("Optional comparer for sort order. Leave empty to keep the collection's own order.")]
        [SerializeReference] private Comparer _comparer;

        private List<T> _views;
        private FilteredList<IViewModel> _filteredList;

        private List<T> Views => _views ??= new List<T>();

        /// <param name="viewFactory">The factory used to create and release view instances for each list item.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public ObservableListViewModelBinder(TViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        /// <summary>
        /// Called when the binding is released. Disposes the filtered view of the list before the base class
        /// detaches from it.
        /// </summary>
        /// <remarks>
        /// The order matters: the filtered list subscribes to the source, so disposing it after the base class
        /// has dropped the source would leave the subscription behind. When overriding, always call the base
        /// implementation.
        /// </remarks>
        protected override void OnUnbound()
        {
            DisposeFilteredList();
            base.OnUnbound();
        }

        protected sealed override IReadOnlyFilteredList<IViewModel> GetFilterList(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();

            var comparer = _comparer?.Get();
            var filter = _filter?.Get();

            if (comparer is not null || filter is not null)
                _filteredList = new FilteredList<IViewModel>(list, comparer, filter);

            return _filteredList;
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }

        protected sealed override void OnAdded(IViewModel newItem, int newStartingIndex) =>
            ObservableListViewModelBinderHelper.OnAdded(Views, _viewFactory, newItem, newStartingIndex);

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            if (newItems is null) return;

            var index = 0;

            foreach (var item in newItems)
                OnAdded(item, newStartingIndex: newStartingIndex + index++);
        }

        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex) =>
            ObservableListViewModelBinderHelper.OnRemoved(Views, _viewFactory, oldStartingIndex);

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;

            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex) =>
            ObservableListViewModelBinderHelper.OnReplace(Views, newItem, newStartingIndex);

        protected sealed override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex) =>
            ObservableListViewModelBinderHelper.OnMove(Views, oldStartingIndex, newStartingIndex);

        protected sealed override void OnReset() =>
            ObservableListViewModelBinderHelper.OnReset(Views, _viewFactory);
    }
}
