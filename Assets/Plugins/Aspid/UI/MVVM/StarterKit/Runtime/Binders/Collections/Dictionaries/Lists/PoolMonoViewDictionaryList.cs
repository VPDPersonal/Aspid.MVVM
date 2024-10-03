using System;
using UnityEngine;
using UnityEngine.Pool;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Views.Extensions;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.StarterKit.Binders.Collections.Dictionaries.Lists
{
    [Serializable]
    public class PoolMonoViewDictionaryList<TKey, TViewModel> : PoolMonoViewDictionaryList<TKey, TViewModel, MonoView>
        where TViewModel : IViewModel
    {
        public PoolMonoViewDictionaryList() { }

        public PoolMonoViewDictionaryList(MonoView prefab, Transform container, int initialCount = 0, int maxCount = int.MaxValue)
            : base(prefab, container, initialCount, maxCount) { }
    }
    
    [Serializable]
    public class PoolMonoViewDictionaryList<TKey, TViewModel, TView> : DynamicMonoViewDictionaryList<TKey, TViewModel, TView>
        where TView : MonoView 
        where TViewModel : IViewModel
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<TView> _pool;

        private ObjectPool<TView> Pool => _pool ??= new ObjectPool<TView>(
            () => CreateView(Prefab, Container),
            view => view.gameObject.SetActive(true),
            view => view.gameObject.SetActive(false),
            view => view.DestroyView(),
            false,
            _initialCount,
            _maxCount);
        
        public PoolMonoViewDictionaryList() { }

        public PoolMonoViewDictionaryList(TView prefab, Transform container, int initialCount = 0, int maxCount = int.MaxValue)
            : base(prefab, container)
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
        
        protected virtual TView CreateView(TView prefab, Transform container) => 
            Object.Instantiate(prefab, container);

        public override void Dispose()
        {
            base.Dispose();
            Pool.Dispose();
        }
    }
}