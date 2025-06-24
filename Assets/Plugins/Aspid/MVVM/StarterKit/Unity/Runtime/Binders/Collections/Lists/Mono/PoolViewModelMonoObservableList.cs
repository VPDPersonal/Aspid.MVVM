using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Pool;
#if UNITY_2023_1_OR_NEWER
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactory<UnityEngine.Transform, Aspid.MVVM.Unity.MonoView>;
#else
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactoryMonoView;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Observable Lists/Pool Observable List - ViewModel")]
    [AddComponentContextMenu(typeof(Component), "Add General Binder/Collection/Pool Observable List - ViewModel")]
    public class PoolViewModelMonoObservableList : PoolViewModelMonoObservableList<MonoView, ViewFactory> { }
    
    public class PoolViewModelMonoObservableList<T> : PoolViewModelMonoObservableList<T, IViewFactory<Transform, T>>
        where T : MonoBehaviour, IView { }
    
    public abstract class PoolViewModelMonoObservableList<T, TViewFactory> : DynamicViewModelMonoObservableList<T, TViewFactory> 
        where T : MonoBehaviour, IView
        where TViewFactory : IViewFactory<Transform, T>
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(1)] private int _maxCount = int.MaxValue;

        private ObjectPool<T> _pool;
        private IViewModel _lastViewModel;

        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            CreateView,
            OnPoolGet,
            OnPoolRelease,
            OnPoolDestroy,
            maxSize: _maxCount,
            collectionCheck: false,
            defaultCapacity: _initialCount);
        
        protected sealed override void OnUnbound()
        {
            base.OnUnbound();
            Pool.Clear();
        }

        protected sealed override T GetNewView(IViewModel viewModel)
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