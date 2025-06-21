using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Pool;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Observable Lists/Pool Observable List - ViewModel")]
    [AddComponentContextMenu(typeof(Component), "Add General Binder/Collection/Pool Observable List - ViewModel")]
    public class PoolViewModelMonoObservableList : PoolViewModelMonoObservableList<MonoView> { }
    
    public abstract class PoolViewModelMonoObservableList<T> : DynamicViewModelMonoObservableList<T>
        where T : MonoBehaviour, IView
    {
        [SerializeField] [Min(0)] private int _initialCount;
        [SerializeField] [Min(0)] private int _maxCount = int.MaxValue;

        private ObjectPool<T> _pool;

        private ObjectPool<T> Pool => _pool ??= new ObjectPool<T>(
            () => CreateView(Prefab, Container),
            actionOnGet: view =>
            {
                view.gameObject.SetActive(true);
                view.transform.SetAsLastSibling();
            },
            actionOnRelease: view => view.gameObject.SetActive(false),
            actionOnDestroy: view => view.DestroyView(),
            collectionCheck: false,
            defaultCapacity: _initialCount,
            maxSize: _maxCount);

        protected sealed override T GetNewView() => Pool.Get();

        protected override void OnUnbound()
        {
            base.OnUnbound();
            Pool.Clear();
        }

        protected sealed override void ReleaseView(T view)
        {
            view.Deinitialize();
            Pool.Release(view);
        }
        
        protected virtual T CreateView(T prefab, Transform container) => 
            Instantiate(prefab, container);
    }
}