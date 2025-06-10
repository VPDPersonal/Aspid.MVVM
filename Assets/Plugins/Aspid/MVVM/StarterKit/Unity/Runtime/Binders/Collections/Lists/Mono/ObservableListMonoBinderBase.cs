using System;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract partial class ObservableListMonoBinderBase<T> : MonoBinder, IBinder<IReadOnlyObservableList<T>>
    {
        private IReadOnlyObservableList<T> _list;

        protected virtual void OnDestroy()
        {
            if (_list == null) return;
            
            OnReset();
            Unsubscribe();
        }

        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> list)
        {
            if (_list != null)
            {
                OnReset();
                Unsubscribe();
            }

            _list = list;
            OnAdded(_list, 0);
            
            Subscribe();
        }

        private void Subscribe() => 
            _list.CollectionChanged += OnCollectionChanged;

        private void Unsubscribe() =>
            _list.CollectionChanged -= OnCollectionChanged;

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e)
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

        protected abstract void OnAdded(T newItem, int newStartingIndex);

        protected abstract void OnAdded(IReadOnlyList<T> newItems, int newStartingIndex);

        protected abstract void OnRemoved(T oldItem, int oldStartingIndex);

        protected abstract void OnRemoved(IReadOnlyList<T> oldItems, int oldStartingIndex);

        protected abstract void OnReplace(T oldItem, T newItem, int newStartingIndex);
        
        protected abstract void OnMove(T oldItem, T newItem, int oldStartingIndex, int newStartingIndex);

        protected abstract void OnReset();
    }
}