using UnityEngine;
using UnityEngine.Pool;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.Mono.Views.Extensions;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Collections/Lists/Pool List - ViewModel")]
    public class PoolViewModelMonoList : DynamicViewModelMonoList
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<MonoView> _pool;

        private ObjectPool<MonoView> Pool => _pool ??= new ObjectPool<MonoView>(
            () => CreateView(Prefab, Container),
            actionOnGet: view =>
            {
                view.gameObject.SetActive(true);
                view.transform.SetSiblingIndex(Pool.CountActive + Pool.CountInactive);
            },
            actionOnRelease: view => view.gameObject.SetActive(false),
            actionOnDestroy: view => view.DestroyView(),
            collectionCheck: false,
            defaultCapacity: _initialCount,
            maxSize: _maxCount);
        
        public PoolViewModelMonoList() { }

        public PoolViewModelMonoList(MonoView prefab, Transform container, int initialCount = 0, int maxCount = int.MaxValue)
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