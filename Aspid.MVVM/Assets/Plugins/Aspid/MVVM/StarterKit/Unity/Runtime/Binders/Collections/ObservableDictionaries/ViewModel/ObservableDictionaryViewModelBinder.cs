using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactory<Aspid.MVVM.MonoView>;
#else
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactoryMonoView;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ObservableDictionaryViewModelBinder{TKey, TViewModel, TView, TViewFactory}"/> that uses
    /// <see cref="MonoView"/> as the view type and the default <see cref="IViewFactory{T}"/> as the factory.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
    /// <typeparam name="TViewModel">The type of <see cref="IViewModel"/> stored as the dictionary value.</typeparam>
    /// <include file="XmlExampleDoc-ObservableDictionary-ViewModel-1.1.0.xml" path="doc//member[@name='ObservableDictionaryViewModelBinder{2}']/*" />
    [Serializable]
    public class ObservableDictionaryViewModelBinder<TKey, TViewModel> : ObservableDictionaryViewModelBinder<TKey, TViewModel, MonoView, ViewFactory>
        where TViewModel : IViewModel
    {
        /// <inheritdoc/>
        public ObservableDictionaryViewModelBinder(ViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableDictionaryBinder{TKey, TViewModel}"/> that instantiates and releases <typeparamref name="TView"/> view objects
    /// for each key-value pair in a bound observable dictionary, using <typeparamref name="TViewFactory"/> to create and release views.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
    /// <typeparam name="TViewModel">The type of <see cref="IViewModel"/> stored as the dictionary value.</typeparam>
    /// <typeparam name="TView">The type of <see cref="MonoBehaviour"/> view created for each dictionary entry.</typeparam>
    /// <typeparam name="TViewFactory">The factory type used to create and release view instances keyed by <typeparamref name="TKey"/>.</typeparam>
    /// <include file="XmlExampleDoc-ObservableDictionary-ViewModel-1.1.0.xml" path="doc//member[@name='ObservableDictionaryViewModelBinder{4}']/*" />
    [Serializable]
    public class ObservableDictionaryViewModelBinder<TKey, TViewModel, TView, TViewFactory> : ObservableDictionaryBinder<TKey, TViewModel>
        where TViewModel : IViewModel
        where TView : MonoBehaviour, IView
        where TViewFactory : IViewFactoryWithKey<TView>
    {
        [SerializeReferenceDropdown]
        [Tooltip("The factory used to create and release view instances keyed by the dictionary key.")]
        [SerializeReference] private TViewFactory _viewFactory;

        private Dictionary<TKey, TView> _views;

        private Dictionary<TKey, TView> Views => _views ??= new Dictionary<TKey, TView>();

        /// <summary>
        /// Initializes a new instance of <see cref="ObservableDictionaryViewModelBinder{TKey, TViewModel, TView, TViewFactory}"/>.
        /// </summary>
        /// <param name="viewFactory">The factory used to create and release view instances for each dictionary entry.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ObservableDictionaryViewModelBinder(TViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        protected sealed override void OnAdded(KeyValuePair<TKey, TViewModel> newItem)
        {
            var view = _viewFactory.Create(newItem.Value, newItem.Key);
            Views.Add(newItem.Key, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<KeyValuePair<TKey, TViewModel>> newItems)
        {
            foreach (var item in newItems)
                OnAdded(item);
        }

        protected sealed override void OnRemoved(KeyValuePair<TKey, TViewModel> oldItem)
        {
            _viewFactory.Release(Views[oldItem.Key]);
            Views.Remove(oldItem.Key);
        }

        protected sealed override void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TViewModel>> oldItems)
        {
            foreach (var item in oldItems)
                OnRemoved(item);
        }

        protected sealed override void OnReplace(KeyValuePair<TKey, TViewModel> oldItem, KeyValuePair<TKey, TViewModel> newItem)
        {
            Views[oldItem.Key].Deinitialize();

            if (newItem.Value is not null)
                Views[oldItem.Key].Initialize(newItem.Value);
        }

        protected sealed override void OnReset()
        {
            foreach (var view in Views.Values)
                _viewFactory.Release(view);

            Views.Clear();
        }
    }
}
