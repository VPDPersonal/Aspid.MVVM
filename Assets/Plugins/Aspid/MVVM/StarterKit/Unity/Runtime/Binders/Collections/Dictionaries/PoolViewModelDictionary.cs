#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class PoolViewModelDictionary<TKey, TViewModel> : PoolViewModelDictionary<TKey, TViewModel, MonoView>
        where TViewModel : IViewModel
    {
        public PoolViewModelDictionary(
            MonoView prefab,
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(prefab, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelDictionary(
            MonoView prefab,
            Transform? container, 
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : base(prefab, container, initialCount, maxCount, mode) { }
    }
    
    [Serializable]
    public class PoolViewModelDictionary<TKey, TViewModel, TView> : DynamicViewModelDictionary<TKey, TViewModel, TView>
        where TView : MonoView 
        where TViewModel : IViewModel
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<TView>? _pool;

        private ObjectPool<TView> Pool => _pool ??= new ObjectPool<TView>(
            () => CreateView(Prefab, Container),
            view => view.gameObject.SetActive(true),
            view => view.gameObject.SetActive(false),
            view => view.DestroyView(),
            false,
            _initialCount,
            _maxCount);
        
        public PoolViewModelDictionary(TView prefab, 
            int initialCount = 0, 
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(prefab, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelDictionary(
            TView prefab,
            Transform? container, 
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : base(prefab, container, mode)
        {
            _maxCount = maxCount;
            _initialCount = initialCount;
        }

        protected sealed override TView GetNewView() => Pool.Get();

        protected sealed override void ReleaseView(TView view)
        {
            view.Deinitialize();
            Pool.Release(view);
        }
        
        protected virtual TView CreateView(TView prefab, Transform? container) =>
            Object.Instantiate(prefab, container);

        public override void Dispose()
        {
            base.Dispose();
            Pool.Dispose();
        }
    }
}