using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

namespace Aspid.MVVM.StarterKit
{
    public abstract class ObservableListBinderBase<T> : Binder, IBinder<IReadOnlyObservableList<T>>
    {
        protected IReadOnlyObservableList<T?>? List { get; private set; }

        protected ObservableListBinderBase(BindMode mode)
            : base(mode) { }
        
        public void SetValue(IReadOnlyObservableList<T?>? list)
        {
            DeinitializeList();
            
            List = list;
            if (List is null) return;
            if (List.Count > 0) OnAdded(List, 0);
            
            InitializeList();
        }

        protected override void OnUnbound() =>
            DeinitializeList();

        private void InitializeList() =>
            List!.CollectionChanged += OnCollectionChanged;

        private void DeinitializeList() 
        {
            if (List is null) return;
            
            OnReset();
            List.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T?> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem, e.NewStartingIndex);
                        else OnAdded(e.NewItems, e.NewStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem, e.OldStartingIndex);
                        else OnRemoved(e.OldItems, e.OldStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem, e.OldStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        OnMove(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void OnAdded(T? newItem, int newStartingIndex);

        protected abstract void OnAdded(IReadOnlyList<T?>? newItems, int newStartingIndex);

        protected abstract void OnRemoved(T? oldItem, int oldStartingIndex);

        protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems, int oldStartingIndex);

        protected abstract void OnReplace(T? oldItem, T? newItem, int newStartingIndex);
        
        protected abstract void OnMove(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex);

        protected abstract void OnReset();
    }
}