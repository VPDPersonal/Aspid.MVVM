using Aspid.Collections.Observable;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Lists
{
    public abstract partial class ListBinderBase<T> : Aspid.UI.MVVM.Mono.MonoBinder, IBinder<IReadOnlyObservableList<T>>
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