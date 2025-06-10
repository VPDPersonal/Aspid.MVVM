using System;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit
{
    public abstract class CollectionBinderBase<T> : Binder, IBinder<IReadOnlyCollection<T>>, IDisposable
    {
        protected IReadOnlyCollection<T>? Collection { get; private set; }
        
        protected CollectionBinderBase(BindMode mode) 
            : base(mode) { }
        
        public void SetValue(IReadOnlyCollection<T>? collection)
        {
            if (Collection is not null)
                OnReset();
            
            Collection = collection;
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);
        }

        protected abstract void OnAdded(IReadOnlyCollection<T> values);
        
        protected abstract void OnReset();

        public virtual void Dispose() =>
            OnReset();
    }
}