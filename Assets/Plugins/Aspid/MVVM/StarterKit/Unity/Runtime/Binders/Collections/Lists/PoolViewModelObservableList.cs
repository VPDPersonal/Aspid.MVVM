#nullable enable
using System;
using UnityEngine;
using UnityEngine.Pool;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactory<UnityEngine.Transform, Aspid.MVVM.Unity.MonoView>;
#else
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactoryMonoView;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class PoolViewModelObservableList : PoolViewModelObservableList<MonoView, ViewFactory>
    {
        public PoolViewModelObservableList(ViewFactory viewFactory, BindMode mode) 
            : this(viewFactory, null, mode) { }
        
        public PoolViewModelObservableList(ViewFactory viewFactory, Transform? container, BindMode mode = BindMode.OneWay) 
            : base(viewFactory, container) { }
    }
    
    [Serializable]
    public class PoolViewModelObservableList<T> : PoolViewModelObservableList<T, IViewFactory<Transform, T>>
        where T : MonoBehaviour, IView
    {
        public PoolViewModelObservableList(
            IViewFactory<Transform, T> viewFactory, 
            int initialCount = 0, 
            int maxCount = int.MaxValue, 
            BindMode mode = BindMode.OneWay) 
            : base(viewFactory, initialCount, maxCount, mode) { }

        public PoolViewModelObservableList(
            IViewFactory<Transform, T> viewFactory, 
            Transform? container, 
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay) 
            : base(viewFactory, container, initialCount, maxCount, mode) { }
    }
    
    [Serializable]
    public class PoolViewModelObservableList<T, TViewFactory> : DynamicViewModelObservableList<T, TViewFactory> 
        where T : MonoBehaviour, IView
        where TViewFactory : IViewFactory<Transform, T>
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(1)] private int _maxCount = int.MaxValue;

        private ObjectPool<T>? _pool;
        private IViewModel? _lastViewModel;

        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            CreateView,
            OnPoolGet,
            OnPoolRelease,
            OnPoolDestroy,
            maxSize: _maxCount,
            collectionCheck: false,
            defaultCapacity: _initialCount);
        
        public PoolViewModelObservableList(
            TViewFactory viewFactory,
            int initialCount = 0,
            int maxCount = int.MaxValue,
            BindMode mode = BindMode.OneWay)
            : this(viewFactory, null, initialCount, maxCount, mode) { }
        
        public PoolViewModelObservableList(
            TViewFactory viewFactory,
            Transform? container, 
            int initialCount = 0, 
            int maxCount = int.MaxValue, 
            BindMode mode = BindMode.OneWay)
            : base(viewFactory, container, mode)
        {
            _maxCount = maxCount;
            _initialCount = initialCount;
        }
        
        protected sealed override void OnUnbound()
        {
            base.OnUnbound();
            Pool.Clear();
        }

        protected sealed override T GetNewView(IViewModel? viewModel)
        {
            _lastViewModel = viewModel;
            return Pool.Get();
        }

        protected sealed override void ReleaseView(T view) =>
            Pool.Release(view);

        private T CreateView()
        {
            var view = base.GetNewView(_lastViewModel);
            _lastViewModel = null;

            return view;
        }

        private static void OnPoolGet(T view)
        {
            view.gameObject.SetActive(true);
            view.transform.SetAsLastSibling();
        }

        private static void OnPoolRelease(T view)
        {
            view.Deinitialize();
            view.gameObject.SetActive(false);
        }

        private void OnPoolDestroy(T view) =>
            base.ReleaseView(view);
    }
}