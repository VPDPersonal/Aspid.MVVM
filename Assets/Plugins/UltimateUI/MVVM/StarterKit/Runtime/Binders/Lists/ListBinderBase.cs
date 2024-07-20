using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Collections;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Lists
{
    public abstract partial class ListBinderBase<T> : MonoBinder, IBinder<IReadOnlyObservableList<T>>
    {
        protected IReadOnlyObservableList<T> List { get; private set; }
        
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> list)
        {
            if (List != null)
            {
                Unsubscribe();
                DisposeList();
            }
            
            List = list;
            
            Subscribe();
            Initialize();
        }
        
        protected virtual void Initialize() { }
        
        private void Subscribe()
        {
            List.CollectionChanged += OnCollectionChanged;
        }

        private void Unsubscribe()
        {
            List.CollectionChanged -= OnCollectionChanged;
        }

        protected virtual void DisposeList() { }

        protected abstract void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e);
    }
}