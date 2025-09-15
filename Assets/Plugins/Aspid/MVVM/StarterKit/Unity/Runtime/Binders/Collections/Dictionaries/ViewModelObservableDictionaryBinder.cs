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
    [Serializable]
    public class ViewModelObservableDictionaryBinder<TKey, TViewModel> : ViewModelObservableDictionaryBinder<TKey, TViewModel, MonoView, ViewFactory>
        where TViewModel : IViewModel
    {
        public ViewModelObservableDictionaryBinder(ViewFactory viewFactory, BindMode mode = BindMode.OneWay) 
            : base(viewFactory, mode) { }
    }
    
    [Serializable]
    public class ViewModelObservableDictionaryBinder<TKey, TViewModel, TView, TViewFactory> : ObservableDictionaryBinder<TKey, TViewModel>
        where TViewModel : IViewModel
        where TView : MonoBehaviour, IView
        where TViewFactory : IViewFactoryWithKey<TView>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private TViewFactory _viewFactory;
        
        private Dictionary<TKey, TView> _views;

        private Dictionary<TKey, TView> Views => _views ??= new Dictionary<TKey, TView>();
        
        public ViewModelObservableDictionaryBinder(TViewFactory viewFactory, BindMode mode = BindMode.OneWay)
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