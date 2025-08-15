#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class ViewModelCollectionBinder : ViewModelCollectionBinder<MonoView>
    {
        public ViewModelCollectionBinder(MonoView[] views, BindMode mode = BindMode.OneWay) 
            : base(views, mode) { }
    }
    
    [Serializable]
    public class ViewModelCollectionBinder<T> : CollectionBinderBase<IViewModel>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T[] _views;
        
        public ViewModelCollectionBinder(T[] views, BindMode mode = BindMode.OneWay) 
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
                _views[index].gameObject.SetActive(true);
                _views[index].Initialize(value);

                index++;
            }

            for (var i = index; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
        }

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