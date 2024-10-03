using UnityEngine;
using UnityEngine.Pool;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Mono.Views.Extensions;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Collections.Lists.Lists
{
    public class PoolMonoViewListList : DynamicMonoViewListList
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<MonoView> _pool;

        private ObjectPool<MonoView> Pool => _pool ??= new ObjectPool<MonoView>(
            () => CreateView(Prefab, Container),
            view => view.gameObject.SetActive(true),
            view => view.gameObject.SetActive(false),
            view => view.DestroyView(),
            false,
            _initialCount,
            _maxCount);
        
        public PoolMonoViewListList() { }

        public PoolMonoViewListList(MonoView prefab, Transform container, int initialCount = 0, int maxCount = int.MaxValue)
            : base(prefab, container)
        {
            _maxCount = maxCount;
            _initialCount = initialCount;
        }

        protected sealed override MonoView GetNewView() => Pool.Get();

        protected sealed override void ReleaseView(MonoView view)
        {
            view.Deinitialize();
            Pool.Release(view);
        }
        
        protected virtual MonoView CreateView(MonoView prefab, Transform container) => 
            Instantiate(prefab, container);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Pool.Dispose();
        }
    }
}