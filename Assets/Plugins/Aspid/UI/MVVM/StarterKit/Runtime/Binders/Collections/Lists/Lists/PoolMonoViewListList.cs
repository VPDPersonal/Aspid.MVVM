using System;
using UnityEngine;
using UnityEngine.Pool;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Mono.Views.Extensions;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.StarterKit.Binders.Collections.Lists.Lists
{
    [Serializable]
    public class PoolMonoViewListList : PoolMonoViewListList<MonoView>
    {
        public PoolMonoViewListList() { }

        public PoolMonoViewListList(MonoView prefab, Transform container, int initialCount = 0, int maxCount = int.MaxValue)
            : base(prefab, container, initialCount, maxCount) { }
    }
    
    [Serializable]
    public class PoolMonoViewListList<T> : DynamicMonoViewListList<T> 
        where T : MonoView
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<T> _pool;

        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            () => CreateView(Prefab, Container),
            view => view.gameObject.SetActive(true),
            view => view.gameObject.SetActive(false),
            view => view.DestroyView(),
            false,
            _initialCount,
            _maxCount);
        
        public PoolMonoViewListList() { }

        public PoolMonoViewListList(T prefab, Transform container, int initialCount = 0, int maxCount = int.MaxValue)
            : base(prefab, container)
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
        
        protected virtual T CreateView(T prefab, Transform container) => 
            Object.Instantiate(prefab, container);

        public override void Dispose()
        {
            base.Dispose();
            Pool.Dispose();
        }
    }
}