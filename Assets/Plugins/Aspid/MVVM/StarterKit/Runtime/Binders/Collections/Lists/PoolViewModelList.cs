#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class PoolViewModelList : PoolViewModelList<MonoView>
    {
        public PoolViewModelList(
            MonoView prefab, 
            int initialCount = 0, 
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(prefab, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelList(
            MonoView prefab, 
            Transform? container,
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : base(prefab, container, initialCount, maxCount, mode) { }
    }
    
    [Serializable]
    public class PoolViewModelList<T> : DynamicViewModelList<T> 
        where T : MonoView
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<T>? _pool;

        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            () => CreateView(Prefab, Container),
            view => view.gameObject.SetActive(true),
            view => view.gameObject.SetActive(false),
            view => view.DestroyView(),
            false,
            _initialCount,
            _maxCount);
        
        public PoolViewModelList(
            T prefab,
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(prefab, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelList(
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

        protected sealed override T GetNewView() => Pool.Get();

        protected sealed override void ReleaseView(T view)
        {
            view.Deinitialize();
            Pool.Release(view);
        }
        
        protected virtual T CreateView(T prefab, Transform? container) => 
            Object.Instantiate(prefab, container);

        public override void Dispose()
        {
            base.Dispose();
            Pool.Dispose();
        }
    }
}