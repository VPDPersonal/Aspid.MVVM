using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Pool;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class PrefabViewPool : PrefabViewPool<MonoView>, IViewFactoryMonoView
    {
        [Obsolete("For Unity Inspector", true)]
        public PrefabViewPool() { }
        
        public PrefabViewPool(MonoView prefab, bool addNewElementOnTop = false) :
            this(prefab, null, addNewElementOnTop) { }
        
        public PrefabViewPool(MonoView prefab, PoolSettings settings, bool addNewElementOnTop = false) :
            this(prefab, null, settings, addNewElementOnTop) { }
        
        public PrefabViewPool(MonoView prefab, Transform container, bool addNewElementOnTop = false)
            : this(prefab, container, new PoolSettings(0), addNewElementOnTop) { }
        
        public PrefabViewPool(MonoView prefab, Transform container, PoolSettings settings, bool addNewElementOnTop = false)
            : base(prefab, container, settings, addNewElementOnTop) { }
    }
    
    [Serializable]
    public class PrefabViewPool<T> : PrefabViewFactory<T>
        where T : MonoBehaviour, IView
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(1)] private int _maxCount = int.MaxValue;
        
        private ObjectPool<T> _pool;
        private IViewModel _lastViewModel;
        
        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            OnCreate,
            OnGet,
            OnRelease,
            OnDestroy,
            maxSize: _maxCount,
            collectionCheck: false,
            defaultCapacity: _initialCount);
        
        [Obsolete("For Unity Inspector", true)]
        public PrefabViewPool() { }
        
        public PrefabViewPool(T prefab, bool addNewElementOnTop = false) :
            this(prefab, null, addNewElementOnTop) { }
        
        public PrefabViewPool(T prefab, PoolSettings settings, bool addNewElementOnTop = false) :
            this(prefab, null, settings, addNewElementOnTop) { }
        
        public PrefabViewPool(T prefab, Transform container, bool addNewElementOnTop = false)
            : this(prefab, container, new PoolSettings(0), addNewElementOnTop) { }

        public PrefabViewPool(T prefab, Transform container, PoolSettings settings, bool addNewElementOnTop = false)
            : base(prefab, container, addNewElementOnTop)
        {
            _maxCount = settings.MaxCount;
            _initialCount = settings.InitialCount;
        }

        public override T Create(IViewModel viewModel)
        {
            _lastViewModel = viewModel;
            return Pool.Get();
        }

        public override void Release(T view) =>
            Pool.Release(view);
        
        private T OnCreate()
        {
            var view = base.Create(_lastViewModel);
            _lastViewModel = null;

            return view;
        }

        private void OnGet(T view)
        {
            view.gameObject.SetActive(true);
            
            if (_lastViewModel is not null)
                view.Initialize(_lastViewModel);
            
            SetSibling(view);
        }
        
        private static void OnRelease(T view)
        {
            view.Deinitialize();
            view.gameObject.SetActive(false);
        }
        
        private void OnDestroy(T view) =>
            base.Release(view);
    }
}