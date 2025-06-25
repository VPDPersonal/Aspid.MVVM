using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class PrefabViewFactory : PrefabViewFactory<MonoView>, IViewFactoryMonoView
    {
        [Obsolete("For Unity Inspector", true)]
        public PrefabViewFactory() { }
        
        public PrefabViewFactory(MonoView prefab, bool addNewElementOnTop = false) :
            this(prefab, null, addNewElementOnTop) { }
        
        public PrefabViewFactory(MonoView prefab, Transform container, bool addNewElementOnTop = false)
            : base(prefab, container, addNewElementOnTop) { }
    }
    
    [Serializable]
    public class PrefabViewFactory<T> : IViewFactory<T>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private bool _addNewElementOnTop;
        
        [Obsolete("For Unity Inspector", true)]
        public PrefabViewFactory() { }
        
        public PrefabViewFactory(T prefab, bool addNewElementOnTop = false) :
            this(prefab, null, addNewElementOnTop) { }
        
        public PrefabViewFactory(T prefab, Transform container, bool addNewElementOnTop = false)
        {
            _prefab = prefab;
            _container = container;
            _addNewElementOnTop = addNewElementOnTop;
        }

        public virtual T Create(IViewModel viewModel)
        {
            var view = Object.Instantiate(_prefab, _container);
            SetSibling(view);
            
            if (viewModel is not null)
                view.Initialize(viewModel);

            return view;
        }

        public virtual void Release(T view) =>
            view.DestroyView();

        protected void SetSibling(T view)
        {
            if (_addNewElementOnTop) view.transform.SetAsFirstSibling();
            else view.transform.SetAsLastSibling();
        }
    }
}