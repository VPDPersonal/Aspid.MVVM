using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract partial class CollectionMonoBinder<T> : MonoBinder, IBinder<IReadOnlyCollection<T>>
    {
        protected IReadOnlyCollection<T> Collection { get; private set; }
        
        [BinderLog]
        public void SetValue(IReadOnlyCollection<T> collection)
        {
            if (Collection is not null)
                OnReset();
            
            Collection = collection;
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);
        }

        protected abstract void OnAdded(IReadOnlyCollection<T> values);
        
        protected abstract void OnReset();
    }
}