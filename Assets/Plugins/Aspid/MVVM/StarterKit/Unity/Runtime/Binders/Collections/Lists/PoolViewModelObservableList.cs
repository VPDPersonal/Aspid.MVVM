#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class PoolViewModelObservableList : PoolViewModelObservableList<MonoView>
    {
        public PoolViewModelObservableList(
            MonoView prefab, 
            int initialCount = 0, 
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(prefab, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelObservableList(
            MonoView prefab, 
            Transform? container,
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : base(prefab, container, initialCount, maxCount, mode) { }
    }
    
    [Serializable]
    public class PoolViewModelObservableList<T> : DynamicViewModelObservableList<T> 
        where T : MonoView
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<T>? _pool;

        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            () => CreateView(Prefab, Container),
            actionOnGet: view =>
            {
                view.gameObject.SetActive(true);
                view.transform.SetAsLastSibling();
            },
            view => view.gameObject.SetActive(false),
            view => view.DestroyView(),
            false,
            _initialCount,
            _maxCount);
        
        public PoolViewModelObservableList(
            T prefab,
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(prefab, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelObservableList(
            T prefab,
            Transform? container, 
            int initialCount = 0, 
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : base(prefab, container, mode)
        {
            _maxCount = maxCount;
            _initialCount = initialCount;
        }
        
        protected override void OnUnbound()
        {
            base.OnUnbound();
            Pool.Clear();
        }

        protected sealed override T GetNewView() => Pool.Get();

        protected sealed override void ReleaseView(T view)
        {
            view.Deinitialize();
            Pool.Release(view);
        }
        
        protected virtual T CreateView(T prefab, Transform? container) => 
            Object.Instantiate(prefab, container);
    }
}