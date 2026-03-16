using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
#if UNITY_2023_1_OR_NEWER
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Comparer = Aspid.MVVM.StarterKit.ICollectionComparer<Aspid.MVVM.IViewModel>;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactory<Aspid.MVVM.MonoView>;
#else
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactoryMonoView;
using Filter = Aspid.MVVM.StarterKit.IViewModelCollectionFilter;
using Comparer = Aspid.MVVM.StarterKit.IViewModelCollectionComparer;
#endif

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
        [SerializeReferenceDropdown]
        [Tooltip("The factory used to create and release view instances for each item in the list.")]
        [SerializeReference] private TViewFactory _viewFactory;

        [SerializeReferenceDropdown]
        [Tooltip("Optional filter applied to the bound list. Pass null to show all items.")]
        [SerializeReference] private Filter _filter;

        [SerializeReferenceDropdown]
        [Tooltip("Optional comparer used to sort the bound list. Pass null to use the source order.")]
        [SerializeReference] private Comparer _comparer;

        private List<T> _views;
        private FilteredList<IViewModel> _filteredList;

        private List<T> Views => _views ??= new List<T>();

        /// <summary>
        /// Initializes a new instance of <see cref="ObservableListViewModelBinder{T, TViewFactory}"/>.
        /// </summary>
        /// <param name="viewFactory">The factory used to create and release view instances for each list item.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ObservableListViewModelBinder(TViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

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

        protected sealed override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var view = _viewFactory.Create(newItem);
            Views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            if (newItems is null) return;

            var index = 0;

            foreach (var item in newItems)
                OnAdded(item, newStartingIndex: newStartingIndex + index++);
        }

        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            _viewFactory.Release(Views[oldStartingIndex]);
            Views.RemoveAt(oldStartingIndex);
        }

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;

            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            Views[newStartingIndex].Deinitialize();

            if (newItem is not null)
                Views[newStartingIndex].Initialize(newItem);
        }

        protected sealed override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
        {
            var oldSiblingIndex = Views[oldStartingIndex].transform.GetSiblingIndex();
            var newSiblingIndex = Views[newStartingIndex].transform.GetSiblingIndex();

            Views[oldSiblingIndex].transform.SetSiblingIndex(newSiblingIndex);
            Views[newSiblingIndex].transform.SetSiblingIndex(oldSiblingIndex);
        }

        protected sealed override void OnReset()
        {
            foreach (var view in Views)
                _viewFactory.Release(view);

            Views.Clear();
        }
    }
}
